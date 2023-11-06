// eslint-disable-next-line no-undef
export default defineAppConfig({
  pages: [
    'pages/index/index',
    'pages/my/my',
    'pages/Login/Login',
    'pages/register/register',
    'pages/apply/apply',
    'pages/Joins/Joins',
    'pages/info/info',
    'pages/order/order',
    'pages/viewReview/viewReview',
    "pages/commonOrder/commonOrder"
  ],
  subpackages: [
    {
      root: "AuditOrder",
      pages: [
        "pages/AuditOrderEdit/AuditOrderEdit",
        "pages/AuditOrderAdd/AuditOrderAdd",
        "pages/AuditOrderIview/AuditOrderIview"
      ]
    },
    {
      root: "Index",
      pages: [
        "pages/order/order",
        "pages/user/user",
        "pages/userEdit/userEdit",
        "pages/AuditLog/AuditLog",
        "pages/Dispatch/Dispatch"
      ]
    }
  ],
  config: {
    usingComponents: {
      "custom-tab-bar": "./custom-tab-bar/CustomTabBar"
    }
  },
  // lazyCodeLoading: "requiredComponents",
  tabBar: {
    custom: true,
    borderStyle: "white",
    color: '#ffffff',
    selectedColor: '#0ff',
    backgroundColor: '#000000',
    list: [
      {
        pagePath: 'pages/index/index',
        text: '首页',
      },
      {
        pagePath: 'pages/my/my',
        text: '我的',
      },
      {
        pagePath: 'pages/apply/apply',
        text: '申请',
      },
      {
        pagePath: 'pages/Joins/Joins',
        text: '接单',
      },
    ],
  },
  window: {
    backgroundTextStyle: 'light',
    navigationBarBackgroundColor: '#fff',
    navigationBarTitleText: 'WeChat',
    navigationBarTextStyle: 'black'
  }
})
