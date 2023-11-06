import {View,Text} from "@tarojs/components";
import {useEffect, useState} from "react";
import {getUserInfo} from "../../api";
import styled from "./index.module.scss"

const Info = () => {
  const [data, setData] = useState({});

  useEffect(() => {
    getUserInfo().then(res => {
      console.log(res)
      setData(() => res.data)
    })
  }, [])

  return (
    <View className={styled.home}>
      <View className={styled.box}>
        <View className={styled.view}>
          <Text className={styled.label}>用户名:</Text>
          <Text className={styled.value}>{data.userName}</Text>
        </View>
        <View className={styled.view}>
          <Text className={styled.label}>姓名:</Text>
          <Text className={styled.value}>{data.name}</Text>
        </View>
        <View className={styled.view}>
          <Text className={styled.label}>邮箱:</Text>
          <Text className={styled.value}>{data.email}</Text>
        </View>
        <View className={styled.view}>
          <Text className={styled.label}>角色:</Text>
          <Text className={styled.value}>{data.roleName}</Text>
        </View>
      </View>
    </View>
  )
}


export default Info
