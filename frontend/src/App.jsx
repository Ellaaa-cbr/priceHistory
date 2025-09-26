import React, { useState } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import ProductListPage from './pages/ProductListPage';
import ProductDetailPage from './pages/ProductDetailPage';

function App() {
  const [count, setCount] = useState(0)

  return (
    <BrowserRouter>
      <Routes>
        {/* 列表页路由 */}
        <Route path="/" element={<ProductListPage />} />
        {/* 详情页路由，利用 URL 参数传递商品编号 */}
        <Route path="/product/:productNo" element={<ProductDetailPage />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
