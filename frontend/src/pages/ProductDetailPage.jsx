import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchProductByNo } from '../services/api';
import PriceChart from '../components/PriceChart';

function ProductDetailPage() {
  const { productNo } = useParams();           // 获取 URL 中的商品编号参数
  const [product, setProduct] = useState(null);
  const [historyData, setHistoryData] = useState([]);

  useEffect(() => {
    // 根据商品编号获取商品详情
    fetchProductByNo(productNo).then(response => {
      const data = response.data;
      setProduct(data);
      // 假设后端返回的数据包含历史价格数组 historyPrices
      if (data.historyPrices) {
        setHistoryData(data.historyPrices);
      } else {
        // 如无历史数据，这里可以调用其它 API 获取，或暂不展示图表
        setHistoryData([]);
      }
    }).catch(error => {
      console.error('Failed to fetch product detail', error);
    });
  }, [productNo]);

  if (!product) {
    return <div>loading...</div>;
  }

  return (
    <div className="product-detail">
      <h2>{product.name}</h2>
      <p>current price: ${product.price}</p>
      <p>category: {product.category}</p>
      <img src={product.image_url} alt={product.name} style={{ maxWidth: '300px' }} />
      
      {/* 价格变化折线图 */}
      <h3>price changing trend</h3>
      <PriceChart data={historyData} />
    </div>
  );
}

export default ProductDetailPage;
