import { View, Text } from '@tarojs/components'
import {useReducer} from "react";
import { AtButton, AtMessage } from 'taro-ui'
import FInput from "../../components/FInput";
import {FSelect} from "../../components/FSelect/FSelect";
import Roles from "../../utils/Roles";
import './register.scss'
import {authRegister} from "../../api";
import Taro from "@tarojs/taro";



const initState = {
  userName: "",
  password: "",
  name: "",
  email: "",
  roleId: undefined
}

const initReducer = (state, action) => {
  switch (action.type) {
    case "userName":
      return {
        ...state,
        userName: action.payload
      }
    case "password":
      return {
        ...state,
        password: action.payload
      }
    case "name":
      return {
        ...state,
        name: action.payload
      }
    case "email":
      return {
        ...state,
        email: action.payload
      }
    case "roleId":
      return {
        ...state,
        roleId: action.payload
      }
  }
  return state
}

const Register = () => {
  const [state, dispatch] = useReducer(initReducer, initState)

  const roles = Roles

  const onSelectChange = (item) => {
    dispatch({type: "roleId", payload: item.value})
  }

  const onSubmit = () => {
    let flag = false
    for (const key in state) {
      if (!state[key]) {
        flag = true
      }else {
        if (key === "roleId") {
          if (state[key] !== undefined){
            flag = false
          }
        }
      }
    }
    // console.log(state)
    // console.log(flag)
    // if (!flag) {
      authRegister(state).then(res => {
        if (res.code === 200) {
          Taro.atMessage({
            type: "success",
            message: "注册成功"
          })
          Taro.navigateTo({
            url: "/pages/Login/Login"
          })
        }
        console.log(res)
      })
    // }
  }

  return (
    <View>
      <AtMessage />
      <FInput
        name={"用户名"}
        value={state.userName}
        onChange={(e) => dispatch({type: "userName", payload: e})}
      />
      <FInput
        name={"密码"}
        value={state.password}
        onChange={(e) => dispatch({type: "password", payload: e})}
      />
      <FInput
        name={"姓名"}
        value={state.name}
        onChange={(e) => dispatch({type: "name", payload: e})}
      />
      <FInput
        name={"邮箱"}
        value={state.email}
        onChange={(e) => dispatch({type: "email", payload: e})}
      />
      <FSelect
        name={"角色"}
        data={roles}
        onChange={onSelectChange}
      />
      <View
        className={"btn"}
      >
        <AtButton
          type='secondary'
          circle
          onClick={onSubmit}
        >注册</AtButton>
      </View>
    </View>
  )
}


export default Register
