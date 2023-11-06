import {View} from "@tarojs/components";
import {AtButton, AtMessage} from "taro-ui";
import FInput from "../../components/FInput";
import Fupload from "../../components/Fupload";
import {FSelect} from "../../components/FSelect/FSelect";
import styled from "./index.module.scss"
import {useEffect, useReducer} from "react";
import {AreaGetAreaSelect,RepairOrderAdd} from "../../api";
import Taro from "@tarojs/taro";

const initState = {
  value: "",
  areaList: [],
  form: {
    areaId: "",
    repairTime: "",
    phoneNumber: "",
    studentNumber: "",
    description: "",
    imageUrls: "",
    specificNumber: ""
  }
}
const initReducer = (state, action) => {
  // const {key,payload, type} = action
  switch (action.type) {
    case "text":
      return {
        ...state,
        value: action.payload
      }
    case "area":
      return {
        ...state,
        areaList: action.payload
      }
    case "form" :
      return {
        ...state,
        form: {
          ...state.form,
          [action.key]: action.payload
        }
      }
    case "clearForm":
      const form = JSON.parse(JSON.stringify(state.form))

      for (const key in form) {
        form[key] = ""
      }
      console.log(form)
      return {
        ...state,
        form: form
      }
    default :
      return state
  }
  return state
}
const Apply = () => {
  const [state, dispatch] = useReducer(initReducer, initState)

  useEffect(() => {
    AreaGetAreaSelect().then(res => {
      dispatch({type: "area", payload: res.data.map(item => ({name: item.areaName, value: item.id}))})
    })
  }, [])


  const areaChange = (item ) => {
    dispatch({
      type: "form",
      payload: item.value,
      key: "areaId"
    })
  }

  function onSubmit (form) {

    const data = JSON.parse(JSON.stringify(form))
    data.repairTime = new Date()
    data.specificNumber = new String(Date.now())
    console.log(data)
    data.areaId = data.areaId.toString()
    dispatch({type: "clearForm"})
    RepairOrderAdd(data).then(res => {
      if (res.code === 200) {
        Taro.atMessage({
          type: "success",
          message: res.message
        })
        dispatch({type: "clearForm"})
      }
    })
  }

  return (
    <View>
      <AtMessage></AtMessage>
      <View className={styled.box}>
        <FInput
          name={"学号"}
          value={state.form.studentNumber}
          onChange={(v) => dispatch({ type: "form", payload: v, key: "studentNumber"})}
        />
        <FInput
          name={"手机号"}
          value={state.form.phoneNumber}
          onChange={(v) => dispatch({ type: "form", payload: v, key: "phoneNumber"})}
        ></FInput>
        <FSelect
          name={"区域"}
          data={state.areaList}
          onChange={areaChange}
          value={state.form.areaId}
        ></FSelect>
        <FInput
          name={"描述"}
          value={state.form.description}
          onChange={(v) => dispatch({ type: "form", payload: v, key: "description"})}
        ></FInput>
        <Fupload
          onChange={v => dispatch({ type: "form", payload: v, key: "imageUrls"})}
          value={state.form.imageUrls}
        ></Fupload>
        <AtButton
          type='secondary'
          circle
          formType='submit'
          onClick={() => onSubmit(state.form)}
        >提交</AtButton>
      </View>
    </View>
  )
}

export default Apply
