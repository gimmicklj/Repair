import Taro from "@tarojs/taro";

export const auth = () => {
  const info = Taro.getStorageSync("info")

  return JSON.parse(info)
}
export function isImage(str) {
  return /\.(jpeg|jpg|png|gif|bmp)$/i.test(str);
}
