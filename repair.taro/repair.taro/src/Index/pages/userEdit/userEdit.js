import {View} from "@tarojs/components";
import Taro, {getCurrentInstance} from "@tarojs/taro";
import {useEffect, useState} from "react";
import {AtForm, AtInput, AtButton, AtMessage} from "taro-ui";
import {AppUserUpdate} from "../../../api";

const list = ['普通用户', '管理员', '维修人员']

const UserEdit = () =>{
  const edit = JSON.parse(getCurrentInstance().router.params.edit)
  const [data, setData] = useState({
    email: "",
    id: "",
    name: "",
    roleName: "",
    userName: "",
  });

  useEffect(() =>{
    setData(edit)
  }, [])

  const submit = () => {
    console.log(data)
    const obj = JSON.parse(JSON.stringify(data))
    delete data.roleName
    AppUserUpdate(data).then(res =>{
      console.log(res)
      if (res.code === 200) {
        Taro.atMessage({
          type: "success",
          message: "用户修改成功"
        })
        setTimeout(() => {
          Taro.navigateTo({
            url: "/Index/pages/user/user"
          })
        }, 500)
      } else {
        Taro.atMessage({
          type: "warning",
          message: res.message
        })
      }
    })
  }

  const onChange = (v) =>{
    console.log(Number(v.detail.value))
    setData(() => ({
      ...data,
      roleName: list[ Number(v.detail.value)]
    }))
  }

  return (
    <View>
      <AtMessage></AtMessage>
      <AtForm>
        <AtInput
          name={"用户名"}
          title='用户名'
          type='text'
          value={data.userName}
          onChange={(v) => setData(() => ({
            ...data,
            userName: v
          }))}
        ></AtInput>
       {/* <AtInput
          name={"角色"}
          title='角色'
          type='text'
          value={data.roleName}
          onChange={(v) => setData(() => ({
            ...data,
            roleName: v
          }))}
        ></AtInput>*/}
      {/*  <Picker
          mode='selector'
          range={list}
          onChange={onChange}
        >
          <AtList>
            <AtListItem
              title='国家地区'
              extraText={data.roleName}
            />
          </AtList>
        </Picker>*/}
        <AtInput
          name={"姓名"}
          title='姓名'
          type='text'
          value={data.name}
          onChange={(v) => setData(() => ({
            ...data,
            name: v
          }))}
        ></AtInput>
        <AtInput
          name={"邮箱"}
          value={data.email}
          title='邮箱'
          type='text'
          onChange={(v) => setData(() => ({
            ...data,
            email: v
          }))}
        ></AtInput>
        <AtButton
          circle
          type={"primary"}
          onClick={submit}
        >保存</AtButton>
      </AtForm>
    </View>
  )
}

export default UserEdit
