import {Text, View, Image} from "@tarojs/components";
import {useEffect, useReducer, useState} from "react";
import {isImage} from "../../../utils";
import {
  AtButton,
  AtTabs,
  AtTabsPane,
  AtMessage,
  AtModal,
  AtModalHeader,
  AtModalContent,
  AtModalAction,
  AtInput
} from "taro-ui";
import order from "./index.module.scss"
import {
  RepairOrderGetTobeReviewedOrderPagedList,
  RepairOrderGetUnTakeByAdminOrderPagedList,
  RepairOrderGetAllUnFinishedOrderPagedList,
  RepairOrderGetAllFinishedOrderPagedList,
  RepairOrderApprove,
  AuditLogAdd,
} from "../../../api";
import Taro from "@tarojs/taro";

const list = [
  {
    title: "待审核"
  },
  {
    title: "待派单"
  },
  {
    title: "未完成"
  },
  {
    title: "已完成"
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

const Modal = (props) => {

  const [value, setValue] = useState("");

  useEffect(() => {
    setValue("")
  }, [props.open])

  const submit = () => {
    console.log(value)
    console.log(props.repairOrderId)
    AuditLogAdd({
      repairOrderId: props.repairOrderId,
      suggestion: value
    }).then(res => {
      if (res.code === 200) {
        Taro.atMessage({
          type: "success",
          message: "添加成功"
        })
        props.onOpen(false)
      }else{
        Taro.atMessage({
          type: "warning",
          message: res.message
        })
      }
    })
  }

  return (
    <AtModal
      isOpened={props.open}
      closeOnClickOverlay={false}
    >
      <AtModalHeader>审核意见添加</AtModalHeader>
      <AtModalContent>
        <AtInput
          name={"value"}
          value={value}
          title={"意见"}
          onChange={v => setValue(v)}
          placeholder={"请输入意见"}
        />
      </AtModalContent>
      <AtModalAction>
        <AtButton
          size={"small"}
          type={"secondary"}
          onClick={() => props.onOpen(false)}
        >取消</AtButton>
        <AtButton
          size={"small"}
          type={"primary"}
          onClick={submit}
        >确定</AtButton>
      </AtModalAction>
    </AtModal>
  )
}

const Btns1 = (props) => {
  const [open, setOpen] = useState(false);
  const handleFinish = () => {
    console.log(props)
    RepairOrderApprove(props.id).then(res => {
      if (res.code === 200) {
        Taro.atMessage({
          type: "success",
          message: "审核成功"
        })
        props.getList()
      }else{
        Taro.atMessage({
          type: "warning",
          message: res.message
        })
      }
    })
  }

  const handleOpen = () => {
    setOpen(true)
    // console.log(props)
  }

  function opens (bool) {
    setOpen(bool)
    props.getList()
  }

  return (
    <View className={order.cardBox}>
      <Text></Text>
      <View className={order.cardBoxBtn1}>
        <AtButton
          size={"small"}
          className={order.btnSuccess}
          onClick={handleFinish}
        >通过
        </AtButton>
        <AtButton
          size={"small"}
          className={order.btnSuccess}
          onClick={handleOpen}
        >
          审核意见
        </AtButton>
        <AtButton
          size={"small"}
          className={order.btnSuccess}
          onClick={() => Taro.navigateTo({
            url: "/Index/pages/AuditLog/AuditLog?id="+props.id
          })}
        >
          审核日志
        </AtButton>
      </View>
      <Modal open={open} repairOrderId={props.id} onOpen={opens}></Modal>
    </View>
  )
}

const Btns2 = (props) => {

  const handleFinish = () => {
    // console.log(props.id)
    Taro.navigateTo({
      url: "/Index/pages/Dispatch/Dispatch?id=" + props.id
    })
  }

  return (
    <View className={order.cardBox}>
      <Text></Text>
      <View className={order.cardBoxBtn1}>
        <AtButton
          size={"small"}
          className={order.btnSuccess}
          onClick={handleFinish}
        >
          {props.children}
        </AtButton>
      </View>
    </View>
  )
}
const Btns3 = (props) => {

  const handleFinish = () => {
    console.log(props)
  }

  return (
    <View className={order.cardBox}>
      <Text></Text>
      <View className={order.cardBoxBtn1}>
        <AtButton
          size={"small"}
          className={order.btnSuccess}
          onClick={handleFinish}
        >
          {props.children}
        </AtButton>
      </View>
    </View>
  )
}
const Btns4 = (props) => {

  const handleFinish = () => {
    console.log(props)
  }

  return (
    <View className={order.cardBox}>
      <Text></Text>
      <View className={order.cardBoxBtn1}>
        <AtButton
          size={"small"}
          className={order.btnSuccess}
          onClick={handleFinish}
        >
          {props.children}
        </AtButton>
      </View>
    </View>
  )
}

const Order = () => {
  const [state, dispatch] = useReducer(initReducer, initState)

  const [list1, setList1] = useState([]);
  const [list2, setList2] = useState([]);
  const [list3, setList3] = useState([]);
  const [list4, setList4] = useState([]);
  useEffect(() => {
    getList()
  }, [state.count])

  function getList () {
    switch (state.count) {
      case 0:
        RepairOrderGetTobeReviewedOrderPagedList().then(res => {
          setList1(res.data)
        })
        break
      case 1:
        RepairOrderGetUnTakeByAdminOrderPagedList().then(res => {
          setList2(res.data)
        })
        break
      case 2:
        RepairOrderGetAllUnFinishedOrderPagedList().then(res => {
          setList3(res.data)
        })
        break
      case 3:
        RepairOrderGetAllFinishedOrderPagedList().then(res => {
          setList4(res.data)
        })
        break
    }
  }

  const handleClick = v => {
    dispatch({
      type: "count",
      payload: v
    })
  }

  return (
    <View className={"home"}>
      <AtMessage></AtMessage>
      <AtTabs
        current={state.count}
        tabList={list}
        onClick={handleClick}
      >
        <AtTabsPane current={state.count} index={0}>
          <View className={order.tabs}>
            <View className={order.box}>
              {
                list1.length && list1.map(item => (
                  <View key={item.id} className={order.card}>
                    <View className={order.cardBox}>
                      <Text>订单号:</Text>
                      <Text>{item.specificNumber}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>区域:</Text>
                      <Text>{item.areaName}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>订单:</Text>
                      <Text>{item.orderTypeDescription}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>描述:</Text>
                      <Text>{item.description}</Text>
                    </View>
                    {
                      item.latestSuggestion && (
                        <View className={order.cardBox}>
                          <Text>建议:</Text>
                          <Text>{item.latestSuggestion}</Text>
                        </View>
                      )
                    }
                    <View className={order.cardBox}>
                      <Text>时间:</Text>
                      <Text>{item.repairTime}</Text>
                    </View>
                    {
                      isImage(item.imageUrls) && (
                        <View className={order.cardBox}>
                          <Text>图片:</Text>
                          <View>
                            <Image src={item.imageUrls} style={{
                              width: "60px",
                              height: "60px"
                            }}></Image>
                          </View>
                        </View>
                      )
                    }
                    <Btns1 {...item} getList={getList}/>
                  </View>
                ))
              }
            </View>
          </View>
        </AtTabsPane>
        <AtTabsPane current={state.count} index={1}>
          <View className={order.tabs}>
            <View className={order.box}>
              {
                list2.length && list2.map(item => (
                  <View key={item.id} className={order.card}>
                    <View className={order.cardBox}>
                      <Text>订单号:</Text>
                      <Text>{item.specificNumber}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>区域:</Text>
                      <Text>{item.areaName}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>订单:</Text>
                      <Text>{item.orderTypeDescription}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>描述:</Text>
                      <Text>{item.description}</Text>
                    </View>
                    {
                      item.latestSuggestion && (
                        <View className={order.cardBox}>
                          <Text>建议:</Text>
                          <Text>{item.latestSuggestion}</Text>
                        </View>
                      )
                    }
                    <View className={order.cardBox}>
                      <Text>时间:</Text>
                      <Text>{item.repairTime}</Text>
                    </View>
                    {
                      isImage(item.imageUrls) && (
                        <View className={order.cardBox}>
                          <Text>图片:</Text>
                          <View>
                            <Image src={item.imageUrls} style={{
                              width: "60px",
                              height: "60px"
                            }}></Image>
                          </View>
                        </View>
                      )
                    }
                    <Btns2 {...item} getList={getList}>派单</Btns2>
                  </View>
                ))
              }
            </View>
          </View>
        </AtTabsPane>
        <AtTabsPane current={state.count} index={2}>
          <View className={order.tabs}>
            <View className={order.box}>
              {
                list3.length && list3.map(item => (
                  <View key={item.id} className={order.card}>
                    <View className={order.cardBox}>
                      <Text>订单号:</Text>
                      <Text>{item.specificNumber}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>区域:</Text>
                      <Text>{item.areaName}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>订单:</Text>
                      <Text>{item.orderTypeDescription}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>描述:</Text>
                      <Text>{item.description}</Text>
                    </View>
                    {
                      item.latestSuggestion && (
                        <View className={order.cardBox}>
                          <Text>建议:</Text>
                          <Text>{item.latestSuggestion}</Text>
                        </View>
                      )
                    }
                    <View className={order.cardBox}>
                      <Text>时间:</Text>
                      <Text>{item.repairTime}</Text>
                    </View>
                    {
                      isImage(item.imageUrls) && (
                        <View className={order.cardBox}>
                          <Text>图片:</Text>
                          <View>
                            <Image src={item.imageUrls} style={{
                              width: "60px",
                              height: "60px"
                            }}></Image>
                          </View>
                        </View>
                      )
                    }
                    {/*<Btns3 {...item}>派单</Btns2>*/}
                  </View>
                ))
              }
            </View>
          </View>
        </AtTabsPane>
        <AtTabsPane current={state.count} index={3}>
          <View className={order.tabs}>
            <View className={order.box}>
              {
                list4.length && list4.map(item => (
                  <View key={item.id} className={order.card}>
                    <View className={order.cardBox}>
                      <Text>订单号:</Text>
                      <Text>{item.specificNumber}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>区域:</Text>
                      <Text>{item.areaName}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>订单:</Text>
                      <Text>{item.orderTypeDescription}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>描述:</Text>
                      <Text>{item.description}</Text>
                    </View>
                    {
                      item.latestSuggestion && (
                        <View className={order.cardBox}>
                          <Text>建议:</Text>
                          <Text>{item.latestSuggestion}</Text>
                        </View>
                      )
                    }
                    <View className={order.cardBox}>
                      <Text>时间:</Text>
                      <Text>{item.repairTime}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>员工:</Text>
                      <Text>{item.repairWorkerName}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>评论分数:</Text>
                      <Text>{item.rating}</Text>
                    </View>
                    <View className={order.cardBox}>
                      <Text>评论:</Text>
                      <Text>{item.commentText}</Text>
                    </View>
                    {
                      isImage(item.imageUrls) && (
                        <View className={order.cardBox}>
                          <Text>图片:</Text>
                          <View>
                            <Image src={item.imageUrls} style={{
                              width: "60px",
                              height: "60px"
                            }}></Image>
                          </View>
                        </View>
                      )
                    }

                    {/*<Btns4 {...item}>派单</Btns2>*/}
                  </View>
                ))
              }
            </View>
          </View>
        </AtTabsPane>
      </AtTabs>
    </View>
  )
}
export default Order
