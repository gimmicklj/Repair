import {View} from "@tarojs/components";
import styled from "./index.module.scss"
import {CommentGet} from "../../api";
import {getCurrentInstance} from "@tarojs/taro";
import {useEffect} from "react";
const ViewReview = () => {

  useEffect(() => {
    const instance = getCurrentInstance()
    const id = instance.router.params.id
    getList(id)
  }, [])

  function getList (id) {
    CommentGet(id).then(res => {
      console.log(res)
    })
  }

  return (
    <View className={styled.home}>查看评价</View>
  )
}
export default ViewReview
