import {View, Text, Image} from "@tarojs/components";
import styles from "./index.module.scss"
import {useEffect, useMemo, useState} from "react";
import {AtButton, AtMessage} from "taro-ui";
import Taro from "@tarojs/taro";
import Available from "../available/available";
import {isImage} from "../../utils"
import {
  RepairOrderGetUnTakeOrderPagedList,
  RepairOrderTake,
  RepairOrderRefuse
} from "../../api";


export const Pending = () => {

  const [list, setList] = useState([]);

  useEffect(() => {
    getList()
  },[])

  function getList () {
    RepairOrderGetUnTakeOrderPagedList().then(res => {
      setList(() => res.data)
    })
  }
  // 同意
  const handleSuccess = (item) => () => {
    RepairOrderTake(item.id).then(res => {
      console.log(res)
      if (res.code === 200) {
        getList()
        Taro.atMessage({
          type: "success",
          message: res.message
        })
      }else {
        Taro.atMessage({
          type: "warning",
          message: res.Message
        })
      }
    })
  }
  // 拒绝
  const handleDanger = (item) => () => {
    RepairOrderRefuse(item.id).then(res => {
      if (res.code === 200) {
        getList()
        Taro.atMessage({
          type: "success",
          message: res.message
        })
      }else {
        Taro.atMessage({
          type: "warning",
          message: res.Message
        })
      }

    })
  }


  const lists = useMemo(() => {
    if (list.length) {
      return  list.map((item) => (
        <View className={styles.card} key={item.id}>
          <View className={styles.cardBox}>
            <Text>区域:</Text>
            <Text>{item.areaName}</Text>
          </View>
          <View className={styles.cardBox}>
            <Text>手机号:</Text>
            <Text>{item.phoneNumber}</Text>
          </View>
          <View className={styles.cardBox}>
            <Text>订单:</Text>
            <Text>{item.orderTypeDescription}</Text>
          </View>
          <View className={styles.cardBox}>
            <Text>学号:</Text>
            <Text>{item.studentNumber}</Text>
          </View>
          <View className={styles.cardBox}>
            <Text>描述:</Text>
            <Text>{item.description}</Text>
          </View>
          
          <View className={styles.cardBox}>
            <Text></Text>
            <View className={styles.cardBoxBtn}>
              <AtButton
                size={"small"}
                className={styles.btnSuccess}
                onClick={handleSuccess(item)}
              >同意</AtButton>
              <AtButton
                size={"small"}
                className={styles.btnDanger}
                onClick={handleDanger(item)}
              >拒绝</AtButton>
            </View>
          </View>
        </View>
      ))
    }
    return (
      <Available></Available>
    )
  }, [list.length])

  return (
    <View className={styles.tabs}>
      <AtMessage></AtMessage>
      <View className={styles.box}>
        {lists}
      </View>
    </View>
  )
}
