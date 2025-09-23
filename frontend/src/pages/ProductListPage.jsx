import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { fetchAllProducts } from '../services/api';  // 已封装的 API 请求方法

function ProductListPage() {
  const [products, setProducts] = useState([]);
  const [searchQuery, setSearchQuery] = useState('');
  const [selectedCategory, setSelectedCategory] = useState('全部');

  useEffect(() => {
    // 获取所有商品数据
    fetchAllProducts().then(response => {
      setProducts(response.data);  // 保存获取的商品数组到状态
    }).catch(error => {
      console.error('Failed to fetch products', error);
    });
  }, []);

  // 根据搜索关键字和分类筛选商品列表
  const filteredProducts = products.filter(product => {
    const matchQuery = product.name.toLowerCase().includes(searchQuery.toLowerCase());
    const matchCategory = selectedCategory === '全部' || product.category === selectedCategory;
    return matchQuery && matchCategory;
  });

  // 提取所有分类选项（含“全部”）
  const categories = ['全部', ...new Set(products.map(p => p.category))];

  return (
    <div className="grid">
      {/* 标题和总数 */}
      <div className="stack">
        <h2 style={{ margin: 0 }}>商品列表</h2>
        <span className="pill">{filteredProducts.length} products in total</span>
      </div>
      {/* 搜索和分类筛选框 */}
      <div className="filters">
        <input 
          type="text" 
          placeholder="搜索商品..." 
          value={searchQuery}
          onChange={e => setSearchQuery(e.target.value)}
        />
        <select value={selectedCategory} onChange={e => setSelectedCategory(e.target.value)}>
          {categories.map(cat => (
            <option key={cat} value={cat}>{cat}</option>
          ))}
        </select>
      </div>

      {/* 商品列表 */}
      <div className="product-list">
        {filteredProducts.map(product => (
          <div key={product.productNo} className="product-item">
            {/* 点击商品项跳转到详情页 */}
            <Link to={`/product/${product.productNo}`}>
              <h3>{product.name}</h3>
              <p>价格: ${product.price}</p>
              <p>分类: {product.category}</p>
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
}

export default ProductListPage;
