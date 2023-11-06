import {View} from "@tarojs/components";
import {getCurrentInstance, atMessage, navigateTo} from "@tarojs/taro";
import {useEffect, useState} from "react";
import {AppUserGetRepairWorkerSelect, RepairOrderDispatch} from "../../../api";
import {AtMessage, AtRadio, AtButton} from "taro-ui";
import {FSelect} from "../../../components/FSelect/FSelect";

const Dispatch = () => {
  const param = getCurrentInstance().router.params
  const [list, setList] = useState([]);
  const [data, setData] = useState({
    orderType: 0,
    repairWorkerId: '',
    orderId: param.id
  });
  useEffect(() => {
    AppUserGetRepairWorkerSelect().then(res => {
      if (res.data.length){
        const data = res.data.map(item =>{
          return  {
            value: item.id,
            name: item.name
          }
        })
        setList(data)
        setData({
          repairWorkerId: data[0].value,
          orderId: param.id,
          orderType: 0,
        })
      }
    })
  }, [])

  const onSelectChange = (v) => {
    console.log(v)
    setData(() => ({
      ...data,
      repairWorkerId: v.value
    }))
  }
  const onChange = (v) => {
    setData(() => ({
      ...data,
      orderType: v
    }))
  }

  const click = () => {
    console.log(data);
    RepairOrderDispatch(data).then(res => {
      console.log(res)
      if (res.code === 200) {
        atMessage({
          type: "success",
          message: "派发成功"
        })
        setTimeout(() => {
          navigateTo({
            url: "/Index/pages/order/order"
          })
        }, 500)
      }else{
        atMessage({
          type: "warning",
          message: res.message
        })
      }
    })
  }

  return (
    <View>
      <AtMessage />
      <FSelect
        name={"员工"}
        data={list}
        onChange={onSelectChange}
        value={data.repairWorkerId}
      />
      <AtRadio
        options={[
          { label: "普通", value: 0 },
          { label: '加急', value: 1 },
        ]}
        value={data.orderType}
        onClick={onChange}
      ></AtRadio>

      <View style={{
        margin: "40px 0"
      }}>
        <AtButton
          type={"primary"}
          circle
          onClick={click}
        >
          派发
        </AtButton>
      </View>
    </View>
  )
}

export default Dispatch
