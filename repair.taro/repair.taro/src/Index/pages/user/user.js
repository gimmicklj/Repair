import {View, Text} from "@tarojs/components";
import {AppUserDelete, AppUserGetPagedListAppUser} from "../../../api";
import {useEffect, useState} from "react";
import styled from "./index.module.scss"
import {AtButton, AtMessage} from "taro-ui";
import Taro from "@tarojs/taro";



const User = () => {
  const [list, setList] = useState([]);

  useEffect(() => {
    getList()
  }, [])


  function getList () {
    AppUserGetPagedListAppUser().then(res => {
      if (res.data && res.data.length) {
        setList(res.data)
      }
    })
  }

  const onDelete = (item) => {
    AppUserDelete({id: item.id}).then(res =>{
      if (res.code === 200) {
        Taro.atMessage({
          type: "success",
          message: "删除成功"
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

  return (
    <View className={"home"}>
      <AtMessage></AtMessage>
      <View className={styled.card}>
        {
          list.map(item =>(
            <View className={styled.cardBox}>
              <View className={styled.view}>
                <Text>用户名:</Text>
                <Text>{item.userName}</Text>
              </View>
              <View className={styled.view}>
                <Text>角色:</Text>
                <Text>{item.roleName}</Text>
              </View>
              <View className={styled.view}>
                <Text>姓名:</Text>
                <Text>{item.name}</Text>
              </View>
              <View className={styled.view}>
                <Text>邮箱:</Text>
                <Text>{item.email}</Text>
              </View>
              <View className={styled.view}>
                <Text></Text>
                <View className={styled.btn}>
                  <AtButton
                    className={styled.btnSuccess}
                    size={"small"}
                    onClick={() => {
                      Taro.navigateTo({
                        url: "/Index/pages/userEdit/userEdit?edit="+ JSON.stringify(item),
                      })
                    }}
                  >编辑</AtButton>
                  <AtButton
                    className={styled.btnDanger}
                    size={"small"}
                    onClick={() => onDelete(item)}
                  >删除</AtButton>
                </View>
              </View>
            </View>
          ))
        }
      </View>
    </View>
  )
}

export default User
