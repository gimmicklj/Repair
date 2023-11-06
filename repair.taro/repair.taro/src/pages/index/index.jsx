import { View, Text, Image } from '@tarojs/components'
import "taro-ui/dist/style/components/button.scss" // 按需引入
import styled from "./index.module.scss"
import Taro from '@tarojs/taro'
import {useEffect, useState} from 'react'
import Back from "../../assets/image/back.jpg"
import Order from "../../assets/image/order.svg"
import My from "../../assets/image/my.png"
import {AppUserGetEmployeeEvaluationCounts} from "../../api";
const styleImg = {
  width: "20vw",
  height: "60px"
}

function Index () {
  const [list, setList] = useState([]);
  useEffect(() => {
    const token = Taro.getStorageSync("token")
    if (!token) {
      Taro.redirectTo({
        url: "/pages/Login/Login"
      })
    }
    AppUserGetEmployeeEvaluationCounts().then(res => {
      console.log(res.data)
      setList(res.data)
    })


  }, [])

  return (
    <View className={styled.home}>
      <View className={styled.box}>
        <Image
          className={styled.img}
          src={Back}
        />
        <View className={styled.boxBtns}>
          <View
            style={{
              textAlign: "center"
            }}
            onClick={() => {
              Taro.navigateTo({
                url: "/Index/pages/user/user"
              })
            }}
          >
            <Image style={styleImg} src={My}></Image>
            <View>
              用户管理
            </View>
          </View>
          <View
            style={{
              textAlign: "center"
            }}
            onClick={() => {
              Taro.navigateTo({
                url: "/Index/pages/order/order"
              })
            }}
          >
            <Image style={styleImg} src={Order}></Image>
            <View>
              订单管理
            </View>
          </View>
        </View>
        <View className={styled.echarts}>
          <View style={{
            margin: "20px 0",
            fontSize: "30px",
            fontWeight: 800
          }}>
            <Text style={{
              borderBottom: "1px solid aqua"
            }}>
              统计分析
            </Text>
          </View>
          {
            list.map((item,i) => (
              <View className={styled.card} key={i}>
                <View className={styled.cardBox}>
                  <Text>用户名</Text>
                  <Text>{item.name}</Text>
                </View>
                <View className={styled.cardBox}>
                  <Text>好评:</Text>
                  <Text>{item.goodCount}</Text>
                </View>
                <View className={styled.cardBox}>
                  <Text>一般:</Text>
                  <Text>{item.averageCount}</Text>
                </View>
                <View className={styled.cardBox}>
                  <Text>差评:</Text>
                  <Text>{item.poorCount}</Text>
                </View>

              </View>
            ))
          }

        </View>
      </View>
    </View>
  )
}

export default Index
