import { View } from '@tarojs/components'
import my from "./my.module.scss"
import {getUserInfo} from "../../api";
import {useEffect, useMemo, useState} from "react";
import Taro from "@tarojs/taro";


const route = {
  "维修人员": [
    {
      name: "我的订单",
      url: '/pages/order/order'
    },
    {
      name: "个人信息",
      url: '/pages/info/info'
    },
    {
      name: "退出登录",
    }
  ],
  "管理员": [
    {
      name: "个人信息",
      url: '/pages/info/info'
    },
    {
      name: "退出登录",
    }
  ],
  "普通用户": [
    {
      name: "我的订单",
      url: '/pages/commonOrder/commonOrder'
    },
    {
      name: "个人信息",
      url: '/pages/info/info'
    },
    {
      name: "退出登录",
    }
  ]
}

const My = () => {
  const [info, setInfo] = useState({
    name: "",
    email: " ",
    roleName: ""
  });

  useEffect(() => {
    getUserInfo().then(res => {
      setInfo(res.data)
    })
  }, [])

  const onClick = (item) =>{
    return () => {
      if (item.name === "退出登录") {
        Taro.clearStorageSync()
        Taro.navigateTo({
          url: "/pages/Login/Login"
        })
      }else {
        Taro.navigateTo({
          url: item.url
        })
      }
    }
  }

  const auth = useMemo(() => {
    if (route[info.roleName] && route[info.roleName].length) {
      return route[info.roleName].map(item => (
        <View
          key={item.name}
          className={my.card}
          onClick={onClick(item)}
        >{item.name}</View>
      ))
    }

  }, [info])

  return (
    <View>
      {auth}
      {/*.map(item => (
          <View
            key={item.name}
            className={my.card}
            onClick={onClick(item)}
          >{item.name}</View>
        ))*/}
    </View>
  )
}
export default My
