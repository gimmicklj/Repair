import Taro from "@tarojs/taro"

// const baseUrl = "https://localhost:3000/"
export const baseUrl = "https://localhost:7192/"
const http = (data) => new Promise((resolve) => {
  const token = Taro.getStorageSync("token")
  let header = {}
  if (token) {
    header.Authorization = `Bearer ${token}`
  }
  // console.log(baseUrl + data.url)
  Taro.request({
    header: {
      ...header
    },
    url: baseUrl + data.url,
    method: data.method,
    data: data.data,
    success(res) {
      // console.log(res)

      if (res.statusCode === 401) {
        Taro.navigateTo({
          url: "/pages/Login/Login"
        })
      }
      resolve(res.data)
    }
  })
})


export const upload = (data) => new Promise(resolve => {
  const token = Taro.getStorageSync("token")
  let header = {}
  if (token) {
    header.Token = token
  }
  Taro.uploadFile({
    url: baseUrl + "Resources/BatchUpload",
    name: 'file',
    filePath: data,
    success: (res) => {
      resolve(JSON.parse(res.data))
    }
  })
})

export default http
