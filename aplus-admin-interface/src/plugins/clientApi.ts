import axios from 'axios'

export const clientApi = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    timeout: 500000
  })
  clientApi.defaults.headers.post['Content-Type'] = 'application/json'