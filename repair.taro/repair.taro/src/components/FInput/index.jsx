import { View, Label, Input, Text } from '@tarojs/components'
import {useEffect, useState} from "react";


const labelStyle = {
  display: "flex",
  height: "40px",
  alignItems: "center",
  borderBottom: "1px solid #ccc",
}

const FInput = (props) => {
  const [value, setValue] = useState("")

  useEffect(() => {
    setValue(props.value)
    // console.log(props.value)
  }, [props.value])
  function onChange ({detail}) {
    setValue(() => detail.value)
    props.onChange(detail.value)
    return detail.value
  }

  const onClick = (e) => {
    if (props.onClick) {
      props.onClick(e)
    }
  }

  const TextName = props.name && (
    <Text style={{
      width: props.width||"90px",
      textAlign: "right"
    }}
    >{props.name}：</Text>
  ) || ""

  const disabled = props.disabled && (value) || (
    <Input
      value={value}
      onInput={onChange}
      type={props.type || "text"}
      onClick={onClick}
      onFocus={onClick}
    />
  )

  return (
    <View style={{
      margin: "10px 0"
    }}
    >
      <Label
        style={{
          ...labelStyle,
          padding: props.name && 0 || "0px 10px"
        }}
      >
        {TextName}
        {disabled}
        {props.icon || ""}
      </Label>
    </View>
  )
}

export default FInput
