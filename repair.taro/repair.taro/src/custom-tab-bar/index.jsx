import {CoverView, CoverImage} from "@tarojs/components";
import styled from "./index.module.scss"
import Taro,{useLoad, useRouter} from "@tarojs/taro";
import tabbarList from "../utils/tabbarList";
import {useEffect, useState} from "react";



const CustomTabBar = () => {
  const [list, setList] = useState([])
  const page = Taro.getCurrentPages();
  const currentPage = page[0];
  const router = useRouter()
  useEffect(() => {
    setTimeout(() => {

      const info = Taro.getStorageSync("info")
      console.log(info,1)
      if (info) {
        setList( () => tabbarList[JSON.parse(info).roleName])
      }
    },0)
  }, [])
  useLoad(() => {
  })
  useEffect(() => {
  }, [Taro.getCurrentPages()])

  const handleSwitchTab = (item) => {
    const pages = Taro.getCurrentPages()[0].config
    return () => {
      Taro.switchTab({
        url: item.pagePath
      })
    }
  }



  return (
    <CoverView className={styled.bottom}>
      {
        list.map(item => {
          const pages = Taro.getCurrentPages()[0].config.navigationBarTitleText
          return (
            <CoverView
              key={item.text}
              data-path={item.pagePath}
              onClick={handleSwitchTab(item)}
              className={styled.bottomList}
            >
              <CoverImage src={ item.text === pages && item.selectedIconPath || item.iconPath} style={{
                width: "30px",
                height: "30px"
              }}></CoverImage>
              <CoverView>
                {item.text}
              </CoverView>
            </CoverView >
          )
        })
      }
    </CoverView>
  )
}
CustomTabBar.options = {
  addGlobalClass: true,
}

export default CustomTabBar
