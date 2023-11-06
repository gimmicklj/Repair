import {Image, View} from "@tarojs/components";
import {useEffect, useMemo, useState} from "react";
import FInput from "../FInput";
import up from "../../assets/image/up.png"
import down from "../../assets/image/down.png"
import styled from "./index.module.scss"


export function FSelect(props) {
  const [mask, setMask] = useState(false)
  const [value, setValue] = useState("")

  const FinputBind = {
    name: props.name,
  }
  useEffect(() => {
    // console.log(props.value)
    console.log(String(props.value))
    if (String(props.value).length === 0) {
      setValue("")
      props.onChange("")
    }else{
      if (props.data.length) {
        const v = [...JSON.parse(JSON.stringify(props.data))].filter(item => item.value === props.value)
        if (v.length) {
          setValue(v[0].name)
        }
      }
    }
  }, [props.value, props.data])

  const onOptionChange = (item) => {
    setValue( () => item.name)
    props.onChange(item)
    setMask(false)
  }

  const options = useMemo(() => {
    if (mask) {
      return (
        <View className={styled.bodys}>
          {
            props.data.map((item, i) => (
              <View key={i} className={styled.option} onClick={() => onOptionChange(item)}>{item.name}</View>
            ))
          }
        </View>
      )
    }else{
      return (
        <></>
      )
    }
  },[mask])

  const icon = useMemo(() => {
    if (mask) {
      return <Image
        src={up}
        alt=""
        style={{
          width: "20px",
          height: "20px"
        }}
      />
    }else {
      return <Image
        src={down}
        alt=""
        style={{width: "20px", height: "20px"}}
      />
    }
  }, [mask])

  const onClick = (e) => {
    e.stopPropagation()
    setMask(() => true)
  }
	return (
    <View>
      <FInput {...FinputBind} value={value} onClick={onClick} icon={icon}></FInput>
      <View className={mask && styled.mask || styled.maskFalse} onClick={() => setMask(false)}></View>
      {options}
    </View>
	)
}
