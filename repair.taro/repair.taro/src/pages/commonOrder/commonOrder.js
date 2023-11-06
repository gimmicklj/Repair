import {View} from "@tarojs/components";
import { AtTabs, AtTabsPane } from 'taro-ui'
import {useReducer} from "react";
import {AuditOrder} from "../../components/CommonOrder/AuditOrder";
import {FinishedOrder} from "../../components/CommonOrder/FinishedOrder";
import {NotRatingOrder} from "../../components/CommonOrder/NotRatingOrder";
import {RatedOrder} from "../../components/CommonOrder/RatedOrder";


const list = [
  {
    title: "待审核"
  },
  {
    title: "待处理"
  },
  {
    title: "待评价"
  },
  {
    title: "已评价"
  },
]


const initState = {
  count: 0
}
const initReducer = (state, action) => {
  const {type, payload} = action
  switch (type) {
    case "count":
      return {
        ...state,
        count: payload
      }
  }
  return state
}

const CommonOrder = () =>{
  const [state, dispatch] = useReducer(initReducer, initState)

  const handleClick = v => {
    dispatch({
      type: "count",
      payload: v
    })
  }

  return (
    <View className={"home"}>
      <AtTabs
        current={state.count}
        tabList={list}
        onClick={handleClick}
      >
        <AtTabsPane current={state.count} index={0}>
          <AuditOrder current={state.count}></AuditOrder>
        </AtTabsPane>
        <AtTabsPane current={state.count} index={1}>
          <FinishedOrder current={state.count}></FinishedOrder>
        </AtTabsPane>
        <AtTabsPane current={state.count} index={2}>
          <NotRatingOrder current={state.count}></NotRatingOrder>
        </AtTabsPane>
        <AtTabsPane current={state.count} index={3}>
          <RatedOrder current={state.count}></RatedOrder>
        </AtTabsPane>
      </AtTabs>
    </View>
  )
}

export default CommonOrder
