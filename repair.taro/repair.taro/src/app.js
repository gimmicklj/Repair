import { Component } from 'react'
import './app.scss'
import "taro-ui/dist/style/index.scss"
import Taro from "@tarojs/taro"
import {AtMessage} from "taro-ui";
class App extends Component {

  componentDidMount () {
  }

  componentDidShow () {
    const info = Taro.getStorageSync("info")
    //   没有储存登录信息进入登录页面
    if (info) {
      console.log(info)
    }
    else {
      Taro.navigateTo({
        url: "/pages/Login/Login"
      })
    }
  }

  componentDidHide () {
  }

  render () {
    return (
      <>
        <AtMessage></AtMessage>
        {
          this.props.children
        }
      </>
    )
  }
}

export default App
