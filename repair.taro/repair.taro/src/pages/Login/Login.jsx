import { useReducer } from 'react'
import { View} from '@tarojs/components'
import FInput from "../../components/FInput";
import {AtButton, AtMessage} from "taro-ui"
import Taro from '@tarojs/taro'
import './Login.scss'
import {authLogin} from "../../api";
import tabbarList from "../../utils/tabbarList";
const initState = {
  username: "",
  password: ""
}

function stateReducer (state, action) {
  switch (action.type) {
    case "name":
      return {
        ...state,
        username: action.value
      }
    case "pass":
      return {
        ...state,
        password: action.value
      }
  }

  return state
}


const Login = () => {

  const [state, dispatch] = useReducer(stateReducer, initState)
  // 登录
  const onSubmit = () => {
    console.log(state);
    authLogin(state).then(res => {
      if (res.code === 200) {
        Taro.atMessage({
          type: "success",
          message: "登录成功"
        })
        Taro.setStorageSync("token", res.data.token)
        Taro.setStorageSync("info", JSON.stringify(res.data.data))
        const data = res.data.data
        setTimeout(() =>{
          Taro.switchTab({
            url: tabbarList[data.roleName][0].pagePath
          })
        },20)
      }else {
        Taro.atMessage({
          type: "warning",
          message: res.message
        })
      }
    })
  }
  // 注册
  const onRegister = () => {
    Taro.navigateTo({
      url: "/pages/register/register"
    })
  }


  return (
    <View>
      <AtMessage></AtMessage>
      <FInput
        name="用户名"
        value={state.username}
        onChange={(v) => dispatch({type: "name", value: v})}
      />
      <FInput
        name="密码"
        value={state.password}
        onChange={(v) => dispatch({type: "pass", value: v})}
      />
      <View className={"btn"}>
        <AtButton
          type='secondary'
          circle
          formType='submit'
          onClick={onSubmit}
        >
          登录
        </AtButton>
      </View>
      <View className={"btn"}>
        <AtButton
          type='secondary'
          circle
          onClick={onRegister}
        >
          注册
        </AtButton>
      </View>
    </View>
  )
}

export default Login
