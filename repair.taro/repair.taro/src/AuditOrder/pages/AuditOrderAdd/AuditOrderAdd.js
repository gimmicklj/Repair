import {View, Text, Image} from "@tarojs/components";
import Taro,{getCurrentInstance} from "@tarojs/taro";
import { AtTextarea, AtButton, AtMessage } from 'taro-ui'
import {useEffect, useState} from "react";
import styled from "./index.module.scss"
import rating from "../../../assets/image/rating.png"
import ratingActive from "../../../assets/image/ratingActivce.png"
import {CommentAdd} from "../../../api";



const AuditOrderAdd = () => {
  const params = getCurrentInstance().router.params
  const [value, setValue] = useState("");
  const [list, setList] = useState([]);
  const [rate, setRate] = useState(0);

  useEffect(() => {
    console.log(params)
    const arr = []
    for (let i = 0; i < 5; i++) {
      arr.push(rating)
    }
    setList(arr)
  }, [])

  const active = num => () => {
    const arr = []
    for (let i = 0; i < (num+1); i++) {
      arr.push(ratingActive)
    }
    for (let i = 0; i < 5 - (num+1); i++) {
      arr.push(rating)
    }
    setRate(num + 1)
    setList(arr)
  }

  const onSubmit = () => {


    CommentAdd({
      "rating": rate,
      "commentText": value,
      "repairOrderId": params.id
    }).then(res => {
      if (res.code === 200) {
        Taro.atMessage({
          type: "success",
          message: "评论成功"
        })
        setTimeout(() => {
          Taro.navigateBack({
            delta: 1
          })
        },1000)
      }else {
        Taro.atMessage({
          type: "warning",
          message: res.message
        })
      }
    })

  }

  return (
    <View className={"home"}>
      <AtMessage></AtMessage>
      <View className={styled.box}>
        <View className={styled.rate}>
          <Text>评分:</Text>
          <View>
            {
              list.map((item, i) => (
                <Image
                  className={styled.img}
                  src={item}
                  key={i}
                  onClick={active(i)}
                />
              ))
            }
          </View>
        </View>
        <View>
          <Text>评论:</Text>
          <AtTextarea
            value={value}
            onChange={(v) => setValue(v)}
            maxLength={200}
          ></AtTextarea>
        </View>
        <View className={styled.btn}>
          <AtButton
            type='secondary'
            circle
            onClick={onSubmit}
          >提交</AtButton>
        </View>
      </View>
    </View>
  )
}

export default AuditOrderAdd
