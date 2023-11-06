import img from "../../assets/image/available.png"
import {View, Image} from '@tarojs/components'
import styled from "./index.module.scss"
const Available = () => {
  return (
    <View className={styled.box}>
      <View>
        <Image src={img}></Image>
        <View className={styled.text}>暂无数据</View>
      </View>
    </View>
  )
}

export default Available
