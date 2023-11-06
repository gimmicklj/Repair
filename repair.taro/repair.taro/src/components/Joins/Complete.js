import {Text, View, Image} from "@tarojs/components";
import {AtButton, AtMessage} from "taro-ui";
import styles from "./index.module.scss";
import {useEffect, useState} from "react";
import {isImage} from "../../utils"
import {
  RepairOrderGetRatedByUserOrderPagedList,
  CommentGet
} from "../../api";
import Available from "../available/available";
import ratingActive from "../../assets/image/ratingActivce.png"
import ratings from "../../assets/image/rating.png"


export const Complete = () => {

  const [list, setList] = useState([]);

  useEffect(() => {
    getList()
  }, [])

  function getList () {
    RepairOrderGetRatedByUserOrderPagedList().then(res => {
      setList(() => res.data)
    })
  }

  const handleView = (row) => () => {
    CommentGet(row.id).then(res => {
      console.log(res.data)
      if (list.length) {
        const arr = list.map(item => {
          if (item.id === row.id) {
            if (res.data) {
              const rating = []
              if (res.data.rating) {
                for (let i = 0; i < res.data.rating; i++) {
                  rating.push(ratingActive)
                }
                if (rating.length < 5) {
                  for (let i = 0; i < 5 - res.data.rating; i++) {
                    rating.push(ratings)
                  }
                }
                console.log(rating)
              }
              return {
                ...item,
                ...res.data,
                rating: rating,
                isRating: true,
                btn: true
              }
            }
            return {
              ...item,
              isRating: false,
              btn: true
            }
          }
          return item
        })
        setList(x => arr)
      }
    })
  }

  const lists = () => {
    if (list.length){
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
          
          {
            (item.isRating) && (
              <>
                <View className={styles.cardBox}>
                  <Text>评价:</Text>
                  <Text>{item.commentText}</Text>
                </View>
                <View className={styles.cardBox}>
                  <Text>评分:</Text>
                  <View>
                    {
                      item.rating.length && item.rating.map((value, index)=> (
                        <Image className={styles.ratingImage} key={index} src={value}></Image>
                      ))
                    }
                  </View>
                </View>
              </>
            ) || (
              <View className={styles.cardBox}>
                <Text>评价:</Text>
                <Text>用户还没有评价</Text>
              </View>
            )
          }
          {
            item.btn || (
              <View className={styles.cardBox}>
                <Text></Text>
                <View className={styles.cardBoxBtn1}>
                  <AtButton
                    size={"small"}
                    className={styles.btnSuccess}
                    onClick={handleView(item)}
                  >查看评价</AtButton>
                </View>
              </View>
            )
          }
        </View>
      ))
    }
    return <Available/>
  }

  return (
    <>
      <AtMessage></AtMessage>
      <View className={styles.box}>
        {lists()}
      </View>
    </>
  )
}
