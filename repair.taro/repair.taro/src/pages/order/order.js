import {View} from "@tarojs/components";
import {Complete} from "../../components/Joins/Complete";
import styled from "./index.module.scss"

const Order = () => {
  return (
    <View className={styled.box}>
      <Complete></Complete>
    </View>
  )
}

export default Order
