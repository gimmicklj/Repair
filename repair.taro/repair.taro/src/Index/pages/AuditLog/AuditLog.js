import {View, Text} from "@tarojs/components";
import {useEffect, useState} from "react";
import {AuditLogGetAuditLogPagedList} from "../../../api";
import {getCurrentInstance} from "@tarojs/taro";
import styled from "./index.module.scss"

function time (data) {
  const date = new Date(data)
  return `
  ${
    date.getFullYear()
  }-${
    String((date.getMonth()+1)).padStart(2, "0")
  }-${
    String(date.getDate()).padStart(2, "0")
  } ${
    String(date.getHours()).padStart(2, "0")
  }:${
    String(date.getMinutes()).padStart(2, "0")
  }:${
    String(date.getSeconds()).padStart(2, "0")
  }`
}

const AuditLog = () => {
  const [list, setList] = useState([]);
  const params = getCurrentInstance().router.params
  console.log(params)
  useEffect(() =>{
    AuditLogGetAuditLogPagedList(params.id).then(res => {
      console.log(res)
      setList(res.data)
    })
  }, [])


  return (
    <View className={"home"}>
      <View className={styled.box}>
        {
          list.map(item => (
            <View className={styled.card} key={item.id}>
              <View className={styled.cardBox}>
                <Text>审核时间</Text>
                {/*<Text>{item.createTime}</Text>*/}
                <Text>{time(item.createTime)}</Text>
              </View>
              <View className={styled.cardBox}>
                <Text>审核人</Text>
                <Text>{item.creatorName}</Text>
              </View>
              <View className={styled.cardBox}>
                <Text>审核意见</Text>
                <Text>{item.suggestion}</Text>
              </View>
            </View>
          ))
        }
      </View>
    </View>
  )
}

export default AuditLog
