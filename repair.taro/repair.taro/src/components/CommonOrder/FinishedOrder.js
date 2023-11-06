import {Text, View, Image} from "@tarojs/components";
import {useEffect, useState} from "react";
import {RepairOrderGetTobeFinishedOrderPagedList} from "../../api";
import styled from "./index.module.scss"
import {isImage} from "../../utils"
export const FinishedOrder = (props) => {
  const [list, setList] = useState([]);
  useEffect(() => {
    if (props.current === 1) {
      getList()
    }
  }, [props.current])

  function getList () {
    RepairOrderGetTobeFinishedOrderPagedList().then(res => {
      setList(() => res.data)
    })
  }

  return (
    <View className={styled.tabs}>
      <View className={styled.box}>
        {
          list.length && list.map(item => (
            <View key={item.id} className={styled.card}>
              <View className={styled.cardBox}>
                <Text>订单号:</Text>
                <Text>{item.specificNumber}</Text>
              </View>
              <View className={styled.cardBox}>
                <Text>区域:</Text>
                <Text>{item.areaName}</Text>
              </View>
              <View className={styled.cardBox}>
                <Text>订单:</Text>
                <Text>{item.orderTypeDescription}</Text>
              </View>
              {/* <View className={styled.cardBox}>
                <Text>事件:</Text>
                <Text>{item.description}</Text>
              </View> */}
              <View className={styled.cardBox}>
                <Text>描述:</Text>
                <Text>{item.description}</Text>
              </View>
              {
                item.latestSuggestion && (
                  <View className={styled.cardBox}>
                    <Text>建议:</Text>
                    <Text>{item.latestSuggestion}</Text>
                  </View>
                )
              }
              <View className={styled.cardBox}>
                <Text>时间:</Text>
                <Text>{item.repairTime}</Text>
              </View>
              <View className={styled.cardBox}>
                <Text>状态:</Text>
                <Text>{item.statusDescription}</Text>
              </View>
              {
                isImage(item.imageUrls) && (
                  <View className={styled.cardBox}>
                     <Text>图片:</Text>
                     <Image src={item.imageUrls} style={{
                       width: "40px",
                       height: "40px"
                     }}></Image>
                  </View>
                )
              }
            </View>
          )) || ""
        }
      </View>
    </View>
  )
}
