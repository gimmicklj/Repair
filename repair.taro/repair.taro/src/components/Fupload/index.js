import {View, Label, Text, Image} from "@tarojs/components";
import uploadImg from "../../assets/image/gdxt-ylgdsctp.png"
import {upload} from "../../utils/http";
import Taro from "@tarojs/taro";
import {useEffect, useMemo, useState} from "react";

const labelStyle = {
  display: "flex",
  // height: "40px",
  alignItems: "center",
  borderBottom: "1px solid #ccc",
}

function isImage(str) {
  return /\.(jpeg|jpg|png|gif|bmp)$/i.test(str);
}
const Fupload = (props) => {
  const [img, setImg] = useState(uploadImg);

  useEffect(() => {
    if (props.value.length === 0) {
      setImg(uploadImg)
    }else {
      if (isImage(props.value)) {
        setImg(props.value)
      }else {
        setImg(uploadImg)
      }
    }
  },[props.value])

  const handleUpload = (file) => {
    Taro.chooseImage({
      count: 1,
      sizeType: ['original', 'compressed'],
      sourceType: ['album', 'camera'],
      success(file){
        console.log(file)
        const tempFiles = file.tempFilePaths[0]
        upload(tempFiles).then(res => {
          setImg(res.data[0].url)
          props.onChange(res.data[0].url)
        })
      }
    })
  }



  return (
    <View
      style={{
        margin: "10px 0"
      }}
    >
      <Label style={{
        ...labelStyle,
        padding: "0px 10px"
      }}>
        <Text>上传图片：</Text>
        <Image
          onClick={handleUpload}
          src={img}
          style={{
            height: "80px",
            width: "80px"
          }}
        ></Image>
      </Label>
    </View>
  )
}
export default Fupload
