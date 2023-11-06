import {View} from "@tarojs/components";
import styled from "./index.module.scss"
import {useReducer} from "react";
import {AtTabs, AtTabsPane} from "taro-ui";
import {Pending} from "../../components/Joins/pending";
import {Processing} from "../../components/Joins/Processing";
const initState = {
  count: 0,
}

const reducer = (state, action) => {
  switch (action.type) {
    case "count":
      return {
        ...state,
        count: action.payload
      }
  }
  return state
}

const Joins = () => {
  const [state, dispatch] = useReducer(reducer,initState)

  const tabList = [{ title: '待处理' }, { title: '处理中' }]

  const handleTabsClick = (count) => {
    dispatch({type: "count", payload: count})
  }



  return (
    <View className={styled.home}>
      <AtTabs tabList={tabList} current={state.count} onClick={handleTabsClick}>
        <AtTabsPane current={state.count} index={0}>
          {/*待处理组件*/}
          <Pending></Pending>
        </AtTabsPane>
        <AtTabsPane current={state.count} index={1}>
          {/*处理中组件*/}
          <Processing></Processing>
        </AtTabsPane>
      </AtTabs>
    </View>
  )
}

export default Joins
