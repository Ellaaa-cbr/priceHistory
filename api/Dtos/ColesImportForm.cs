using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace api.Dtos
{
    public class ColesImportForm
    {
        public IFormFile? file { get; set; }

    }
}