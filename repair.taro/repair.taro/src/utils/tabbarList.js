import home from "../assets/image/home.png";
import homeSelected from "../assets/image/homeSelected.png";
import my from "../assets/image/my.png";
import mySelected from "../assets/image/mySelected.png";

const tabbarList = {
  "维修人员": [
    {
      pagePath: "/pages/Joins/Joins",
      text: "接单",
      iconPath: home,
      selectedIconPath: homeSelected
    },
    {
      pagePath: "/pages/my/my",
      text: "我的",
      iconPath: my,
      selectedIconPath: mySelected
    }
  ],
  "管理员": [
    {
      pagePath: "/pages/index/index",
      text: "订单管理",
      iconPath: home,
      selectedIconPath: homeSelected
    },
    {
      pagePath: "/pages/my/my",
      text: "我的",
      iconPath: my,
      selectedIconPath: mySelected
    }
  ],
  "普通用户": [
    {
      pagePath: "/pages/apply/apply",
      text: "申请",
      iconPath: home,
      selectedIconPath: homeSelected
    },
    {
      pagePath: "/pages/my/my",
      text: "我的",
      iconPath: my,
      selectedIconPath: mySelected
    }
  ]
}

export default tabbarList
