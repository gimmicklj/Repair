import http from "../utils/http";

//登录
export const authLogin = (data) => http({
  url: `Auth/Login`,
  method: "post",
  data
})

//注册
export const authRegister = data => http({
  url: "Auth/Register",
  data,
  method: "post"
})
// 区域接口查询
export const AreaGetAreaSelect = (data = {}) => http({
  url: "Area/GetAreaSelect",
  data,
  method: "get"
})
// 图片上传接口
export const RepairOrderAdd = data => http({
  url: "RepairOrder/Add",
  data,
  method: "post"
})

// 个人中心
export const getUserInfo = data => http({
  url: "AppUser/GetUserInfo",
  method: "get"
})
// 维修工待处理订单查询
export const RepairOrderGetUnTakeOrderPagedList = data => http({
  url: "RepairOrder/GetUnTakeOrderPagedList",
  method: "get"
})
// 未处理
export const RepairOrderGetPendingOrderPagedList = () => http({
  url: "RepairOrder/GetPendingOrderPagedList",
  method: "get"
})
// 完成
export const RepairOrderGetRatedByUserOrderPagedList = () => http({
  url: 'RepairOrder/GetRatedByUserOrderPagedList',
  method: "get"
})

// 维修工待处理同意
export const RepairOrderTake = data => http({
  url: "RepairOrder/Take?orderId=" + data ,
  method: "post"
})
// 拒绝
export const RepairOrderRefuse = data => http({
  url: "RepairOrder/Refuse?orderId=" + data,
  method: "post"
})
// 完成
export const RepairOrderFinish = data => http({
  url: "RepairOrder/Finish?orderId=" + data,
  method: "post"
})



//待审核
export const RepairOrderGetTobeAuditOrderPagedList = () =>http({
  url: "RepairOrder/GetTobeAuditOrderPagedList"
})
// 待处理
export const RepairOrderGetTobeFinishedOrderPagedList = () => http({
  url: "RepairOrder/GetTobeFinishedOrderPagedList"
})
// 待评价
export const RepairOrderGetNotRatingOrderPagedList = () => http({
  url: "RepairOrder/GetNotRatingOrderPagedList"
})
// 已评价
export const RepairOrderGetRatedOrderPagedList = () => http({
  url: "RepairOrder/GetRatedOrderPagedList"
})

// 编辑订单查看
export const RepairOrderEdit = (data) => http({
  url: "RepairOrder/Edit?id="+data.id,
  method: "post"
})
// 修改订单
export const RepairOrderUpdate = data => http({
  url: "RepairOrder/Update",
  data,
  method: "post"
})
// 查看评价
export const CommentGet = data => http({
  url: "Comment/Get?repairOrderId="+data,
  method: "post"
})

//添加评论
export const CommentAdd = data => http({
  url: "Comment/Add",
  data,
  method: "post"
})

// 用户查询
export const AppUserGetPagedListAppUser = data => http({
  url: "AppUser/GetPagedListAppUser",
  data,
})
// 用户添加
// 用户编辑
export const AppUserUpdate = data => http({
  url: "AppUser/Update",
  data,
  method: "post"
})
// 用户删除
export const AppUserDelete = data => http({
  url: "AppUser/Delete?id=" + data.id,
  method: "post"
})
  // 待审核
  // /RepairOrder/GetTobeReviewedOrderPagedList
export const RepairOrderGetTobeReviewedOrderPagedList = () => http({
  url: "RepairOrder/GetTobeReviewedOrderPagedList"
})
// 待派单
// /RepairOrder/GetUnTakeByAdminOrderPagedList
export const RepairOrderGetUnTakeByAdminOrderPagedList = () => http({
  url: "RepairOrder/GetUnTakeByAdminOrderPagedList"
})
// 未完成
// /RepairOrder/GetAllUnFinishedOrderPagedList
export const RepairOrderGetAllUnFinishedOrderPagedList = () => http({
  url: "RepairOrder/GetAllUnFinishedOrderPagedList"
})
// 完成
// /RepairOrder/GetAllFinishedOrderPagedList
export const RepairOrderGetAllFinishedOrderPagedList = () => http({
  url: "RepairOrder/GetAllFinishedOrderPagedList"
})

//审核通过
export const RepairOrderApprove = (id) => http({
  url: "RepairOrder/Approve?id=" + id,
  method: "post"
})
// 审核添加
export const AuditLogAdd = (data) => http({
  url: "AuditLog/Add",
  method: "post",
  data
})
// 审核日志
export const AuditLogGetAuditLogPagedList = OrderId => http({
  url: "AuditLog/GetAuditLogPagedList?OrderId=" + OrderId
})
// 维修工选择器
export const AppUserGetRepairWorkerSelect = () => http({
  url: "AppUser/GetRepairWorkerSelect"
})
// 派单
export const RepairOrderDispatch = data => http({
  url: "RepairOrder/Dispatch",
  data,
  method: "post"
})
  // /AppUser/GetEmployeeEvaluationCounts
export const AppUserGetEmployeeEvaluationCounts = () => http({
  url: "AppUser/GetEmployeeEvaluationCounts"
})
