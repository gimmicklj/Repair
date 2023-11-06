import {View, Text, Image} from "@tarojs/components";
import {useEffect, useState} from "react";
import {RepairOrderGetTobeAuditOrderPagedList} from "../../api";
import styled from "./index.module.scss"
import {AtButton} from "taro-ui";
import Taro from "@tarojs/taro";
import {isImage} from "../../utils"

const Btns = (props) => {
  const {status} = props

  const handleFinish = () => {
    Taro.navigateTo({
      url: "/AuditOrder/pages/AuditOrderEdit/AuditOrderEdit?id="+props.id
    })
  }


  switch (status) {
    case "审核失败":
      return (
        <View className={styled.cardBox}>
          <Text></Text>
          <View className={styled.cardBoxBtn1}>
            <AtButton
              size={"small"}
              className={styled.btnSuccess}
              onClick={handleFinish}
            >
              重新审核
            </AtButton>
          </View>
        </View>
      )
    default:
      return ""
  }
}
export const AuditOrder = (props) => {
  const [list, setList] = useState([]);
  useEffect(() => {
    if (props.current === 0) {
      getList()
    }
  }, [props.current])

  function getList () {
    RepairOrderGetTobeAuditOrderPagedList().then(res => {
      setList(() => res.data)
    })
  }

  return (
    <View className={styled.tabs}>
      <View className={styled.box}>
        {
          list.length && list.map(item => (
            <View key={item.id} className={styled.card}>
              {/*specificNumber*/}
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
              <View className={styled.cardBox}>
                <Text>描述:</Text>
                <Text>{item.description}</Text>
              </View>
              {/*建议有值显示无值不显示*/}
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
              <Btns status={item.statusDescription} {...item} />
            </View>
          )) || ""
        }
      </View>
    </View>
  )
}
