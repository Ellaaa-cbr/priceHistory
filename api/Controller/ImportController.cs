using System.Text.Json;
using api.Data;
using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [Route("api/import")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ImportController(AppDbContext db) => _db = db;

        [HttpPost("coles")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ImportColesJsonl([FromForm] ColesImportForm form, CancellationToken ct)
        {
            if (form?.file == null || form.file.Length == 0)
                return BadRequest("请选择一个 .jsonl 文件");

            using var stream = form.file.OpenReadStream();
            using var reader = new StreamReader(stream);

            // 先把已存在的键取出来做去重（按 Name+ImageUrl）
            var existedKeys = await _db.Products
                .Select(p => new { p.Name, p.ImageUrl })
                .ToListAsync(ct);
            var seen = new HashSet<string>(
                existedKeys.Select(e => $"{e.Name}|{e.ImageUrl}"),
                StringComparer.OrdinalIgnoreCase
            );

            var buffer = new List<Product>(capacity: 512);
            int total = 0, inserted = 0, skipped = 0;

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                total++;
                ColesProductRaw? raw;
                try
                {
                    raw = JsonSerializer.Deserialize<ColesProductRaw>(line);
                }
                catch
                {
                    skipped++; // 非法行
                    continue;
                }

                if (raw == null || string.IsNullOrWhiteSpace(raw.name) || (raw.price_value ?? 0m) <= 0)
                {
                    skipped++;
                    continue;
                }

                var key = $"{raw.name}|{raw.image_url}";
                if (!seen.Add(key))
                {
                    skipped++; // 重复
                    continue;
                }

                var product = new Product
                {
                    // 注意：ProductNo 数据库会自动生成，无需赋值（见你的迁移配置）
                    Name = raw.name.Trim(),
                    price = Math.Round(raw.price_value!.Value, 2),
                    date = DateTime.UtcNow,                         // 文件里没有时间，用当前时间
                    category = LastCategory(raw.category_path) ?? "Unknown",
                    weight = 0,                                     // 文件没有重量字段，先置 0
                    Comment = ComposeComment(raw.price_text),
                    ImageUrl = SafeUrl(raw.image_url)
                };

                buffer.Add(product);

                if (buffer.Count >= 500)
                {
                    await _db.Products.AddRangeAsync(buffer, ct);
                    await _db.SaveChangesAsync(ct);
                    inserted += buffer.Count;
                    buffer.Clear();
                }
            }

            if (buffer.Count > 0)
            {
                await _db.Products.AddRangeAsync(buffer, ct);
                await _db.SaveChangesAsync(ct);
                inserted += buffer.Count;
            }

            return Ok(new { total, inserted, skipped });
        }

        private static string? SafeUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return url;
            // 数据库中 ImageUrl 最大 2048，超过就截断/或置空
            return url.Length <= 2048 ? url : url[..2048];
        }

        private static string? LastCategory(string? path)
        {
            if (string.IsNullOrWhiteSpace(path)) return null;
            var parts = path.Split('>');
            return parts.LastOrDefault()?.Trim();
        }

        private static string ComposeComment(string? priceText)
        {
            if (string.IsNullOrWhiteSpace(priceText)) return  "";
            return $"{priceText}";
        }
    }
}
