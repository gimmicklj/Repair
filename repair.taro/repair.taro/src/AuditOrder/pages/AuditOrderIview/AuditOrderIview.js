import {View, Text, Image} from "@tarojs/components";
import Taro, {getCurrentInstance} from "@tarojs/taro";
import {useEffect, useState} from "react";
import {CommentGet} from "../../../api";
import {AtButton} from "taro-ui";
import rating from "../../../assets/image/rating.png"
import ratingActive from "../../../assets/image/ratingActivce.png"
import styled from "./index.module.scss"
const AuditOrderIview = () => {
  const params = getCurrentInstance().router.params
  const [data, setData] = useState({
    commentText: "",
    rating: []
  });
  useEffect(() => {
    console.log(params)
    CommentGet(params.id).then(res => {
      console.log(res.data)
      const rate = res.data.rating
      const arr = []
      for (let i = 0; i < rate; i++) {
        arr.push(ratingActive)
      }
      for (let i = 0; i < (5 - rate); i++) {
        arr.push(rating)
      }
      console.log(arr)
      setData({
        commentText: res.data.commentText,
        rating: arr
      })
      // setData(res.data)
    })
  }, [])
  return (
    <View className={"home"}>
      <View className={styled.box}>
        <View className={styled.view}>
          <Text>评分</Text>
          <View>
            {
              data.rating.map((item, i) => (
                <Image className={styled.img} src={item} key={i}></Image>
              ))
            }
          </View>
        </View>
        <View className={styled.view}>
          <Text>评价: </Text>
          <Text>{data.commentText}</Text>
        </View>
        <AtButton
          type={"secondary"}
          circle
          onClick={() => Taro.navigateBack({
            delta: 1
          })}
        >确定</AtButton>
      </View>
    </View>
  )
}

export default AuditOrderIview
