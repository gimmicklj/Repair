import {Text, View, Image} from "@tarojs/components";
import {AtButton, AtMessage} from "taro-ui";
import {useEffect, useMemo, useState} from "react";
import Available from "../available/available";
import styles from "./index.module.scss";
import Taro from "@tarojs/taro";
import {isImage} from "../../utils"
import {RepairOrderFinish, RepairOrderGetPendingOrderPagedList} from "../../api";
export const Processing = () => {

  const [list, setList] = useState([]);

  useEffect(() => {
    getList()
  }, [])

  function getList () {
    RepairOrderGetPendingOrderPagedList().then(res => {
      console.log(res)
      setList(() => res.data)
    })
  }

  const handleFinish = (item) => () => {
    RepairOrderFinish(item.id).then(res => {
      if (res.code ) {
        Taro.atMessage({
          type: "success",
          message: res.message
        })
        getList()
      }else {
        Taro.atMessage({
          type: "warning",
          message: res.message
        })
      }
    })
  }

  const lists = useMemo(() => {
    if (list.length) {
      return list.map(item => (
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
            <View className={styles.cardBoxBtn1}>

              <AtButton
                size={"small"}
                className={styles.btnSuccess}
                onClick={handleFinish(item)}
              >完成</AtButton>
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
    <View className={styled.tabs}>
      <AtMessage></AtMessage>
      <View className={styled.box}>
        {lists}
      </View>
    </View>
  )
}
