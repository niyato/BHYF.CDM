using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;

namespace BHYF.CDM.Models
{
    public class Enums
    {
    }

    /// <summary>
    /// 审核状态
    /// </summary>
    public enum ApplyStaus
    {
        [Description("未审核")]
        WaitAuditing = 0,
        [Description("已拒绝")]
        Refuse = 1,
        [Description("待确认")]
        WaitConfirm = 2,
        [Description("已审核")]
        Auditing = 3,
        [Description("设计阶段")]
        Designing = 4
    }

    public enum ApplyReviewStatus
    {
        [Description("已拒绝")]
        Refuse = 1,
        [Description("待确认")]
        WaitConfirm = 2,
        [Description("已审核")]
        Auditing = 3,
    }

    public enum DesignStatus
    {
        [Description("已审核")]
        Auditing = 3,
        [Description("设计阶段")]
        Designing = 4
    }

    public enum ApplyLevel
    {
        P1,
        P2,
        P3
    }

    /// <summary>
    /// 申请来源
    /// </summary>
    public enum ApplySource
    {
        微信,
        ios,
        android,
        后台
    }

    public enum DeliveryApplyStatus
    {
        [Description("待审批")]
        WaitAudit = 0,
        [Description("待公司审批")]
        Audited = 1,
        [Description("待发货")]
        WaitSend = 2,
        [Description("待收货")]
        WaitReceived = 3,
        [Description("已签收")]
        Received = 4,
        [Description("拒绝")]
        Refuse = 5
    }

    /// <summary>
    /// 菜单枚举
    /// </summary>
    public enum DeliveryMenuType
    {
        [Description("市场-申请列表")]
        ApplyList = 1,
        [Description("新增申请")]
        CreateApply = 2,
        [Description("审批-申请列表")]
        ApplyListManage = 3,
        [Description("菜单分配")]
        MenuManage = 4,
        [Description("权限分配")]
        PowerManage = 5,
        [Description("发货商品管理")]
        ProductManage=6
    }

    /// <summary>
    /// 权限枚举
    /// </summary>
    public enum DeliveryPowerType
    {
        [Description("部门领导签字")]
        departmentsign = 1,
        [Description("公司领导签字")]
        companysign = 2,
        [Description("工厂发货")]
        factorydelivery = 3,
        [Description("财务查看详情权限")]
        deliveryapplydetail = 4,
        [Description("收货回执单")]
        reveivegoods=5
    }

}