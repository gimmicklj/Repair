import {View} from "@tarojs/components";
import Taro, {
  getCurrentInstance
} from "@tarojs/taro";
import {AtButton, AtMessage} from "taro-ui";
import {useEffect, useReducer} from "react";
import FInput from "../../../components/FInput";
import {FSelect} from "../../../components/FSelect/FSelect";
import {AreaGetAreaSelect, RepairOrderEdit, RepairOrderUpdate} from "../../../api";
import Fupload from "../../../components/Fupload";


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
    case "setForm":
      return {
        ...state,
        form: action.payload
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

const AuditOrderEdit = () => {
  const [state, dispatch] = useReducer(initReducer, initState)
  const params = getCurrentInstance().router.params
  useEffect(() => {
    getEdit()
    AreaGetAreaSelect().then(res => {
      dispatch({type: "area", payload: res.data.map(item => ({name: item.areaName, value: item.id}))})
    })
  },[])

  const areaChange = (item ) => {
    dispatch({
      type: "form",
      payload: item.value,
      key: "areaId"
    })
  }

  function getEdit () {
    RepairOrderEdit({id: params.id}).then(res => {
      dispatch({
        type: "setForm",
        payload: res.data
      })
    })
  }

  const onSubmit = () => {
    console.log(state.form)
    RepairOrderUpdate(state.form).then(res => {
      if (res.code === 200) {
        Taro.atMessage({
          type: "success",
          message: "修改成功"
        })
        Taro.navigateBack({
          delta: 1
        })
      }else{
        Taro.atMessage({
          type: "success",
          message: res.message
        })
      }
    })
  }

  return (
    <View>
      <AtMessage></AtMessage>
      <View>
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
          onClick={onSubmit}
        >提交</AtButton>
      </View>
    </View>
  )
}

export default AuditOrderEdit
