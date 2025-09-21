import axios from 'axios'

const BASE_URL = 'https://localhost:5001/api' || '/api'

const api = axios.create({
  baseURL: BASE_URL,
  timeout: 10000
})

api.interceptors.response.use(
  (res) => res,
  (err) => {
    console.error('API 请求失败:', err?.response?.status, err?.response?.data || err.message)
    return Promise.reject(err)
  }
)

export const fetchAllProducts = () => api.get('/product')
export const fetchProductByNo = (productNo) => api.get(`/product/${productNo}`)

export default api