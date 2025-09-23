import axios from 'axios'

const BASE_URL = 'http://localhost:5098/api'

const api = axios.create({
  baseURL: BASE_URL,
  timeout: 10000
})

api.interceptors.response.use(
  (res) => res,
  (err) => {
    console.error('API fail:', err?.response?.status, err?.response?.data || err.message)
    return Promise.reject(err)
  }
)

export const fetchAllProducts = () => api.get('/product')
export const fetchProductByNo = (productNo) => api.get(`/product/${productNo}`)

export default api