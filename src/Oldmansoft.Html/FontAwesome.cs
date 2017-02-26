using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 字体图标
    /// </summary>
    public enum FontAwesome
    {
        /// <summary>
        /// 校正
        /// </summary>
        Adjust,
        /// <summary>
        /// 锚
        /// </summary>
        Anchor,
        /// <summary>
        /// 档案
        /// </summary>
        Archive,
        /// <summary>
        /// 箭
        /// </summary>
        Arrows_V,
        /// <summary>
        /// 星号
        /// </summary>
        Asterisk,
        /// <summary>
        /// 禁止
        /// </summary>
        Ban,
        /// <summary>
        /// 图表空心
        /// </summary>
        Bar_Chart_O,
        /// <summary>
        /// 条形码
        /// </summary>
        Barcode,
        /// <summary>
        /// 条
        /// </summary>
        Bars,
        /// <summary>
        /// 啤酒
        /// </summary>
        Beer,
        /// <summary>
        /// 铃
        /// </summary>
        Bell,
        /// <summary>
        /// 铃空心
        /// </summary>
        Bell_O,
        /// <summary>
        /// 闪电
        /// </summary>
        Bolt,
        /// <summary>
        /// 书本
        /// </summary>
        Book,
        /// <summary>
        /// 书签
        /// </summary>
        Bookmark,
        /// <summary>
        /// 书签空心
        /// </summary>
        Bookmark_O,
        /// <summary>
        /// 工具箱
        /// </summary>
        Briefcase,
        /// <summary>
        /// 虫
        /// </summary>
        Bug,
        /// <summary>
        /// 建筑物空心
        /// </summary>
        Building_O,
        /// <summary>
        /// 扩音器
        /// </summary>
        Bullhorn,
        /// <summary>
        /// 靶眼
        /// </summary>
        Bullseye,
        /// <summary>
        /// 日历
        /// </summary>
        Calendar,
        /// <summary>
        /// 日历空心
        /// </summary>
        Calendar_O,
        /// <summary>
        /// 相机
        /// </summary>
        Camera,
        /// <summary>
        /// 复古相机
        /// </summary>
        Camera_Retro,
        /// <summary>
        /// 认证
        /// </summary>
        Certificate,
        /// <summary>
        /// 圆
        /// </summary>
        Circle,
        /// <summary>
        /// 圆空心
        /// </summary>
        Circle_O,
        /// <summary>
        /// 时钟
        /// </summary>
        Clock_O,
        /// <summary>
        /// 云
        /// </summary>
        Cloud,
        /// <summary>
        /// 云下载
        /// </summary>
        Cloud_Download,
        /// <summary>
        /// 云上传
        /// </summary>
        Cloud_Upload,
        /// <summary>
        /// 代码
        /// </summary>
        Code,
        /// <summary>
        /// 代码分叉
        /// </summary>
        Code_Fork,
        /// <summary>
        /// 咖啡
        /// </summary>
        Coffee,
        /// <summary>
        /// 轮齿
        /// </summary>
        Cog,
        /// <summary>
        /// 轮齿组
        /// </summary>
        Cogs,
        /// <summary>
        /// 注释
        /// </summary>
        Comment,
        /// <summary>
        /// 注释空心
        /// </summary>
        Comment_O,
        /// <summary>
        /// 注释组
        /// </summary>
        Comments,
        /// <summary>
        /// 注释组空间
        /// </summary>
        Comments_O,
        /// <summary>
        /// 指南针
        /// </summary>
        Compass,
        /// <summary>
        /// 信用卡
        /// </summary>
        Credit_Card,
        /// <summary>
        /// 剪裁
        /// </summary>
        Crop,
        /// <summary>
        /// 十字线
        /// </summary>
        Crosshairs,
        /// <summary>
        /// 餐具
        /// </summary>
        Cutlery,
        /// <summary>
        /// 仪表盘
        /// </summary>
        Dashboard,
        /// <summary>
        /// 桌面
        /// </summary>
        Desktop,
        /// <summary>
        /// 下载
        /// </summary>
        Download,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit,
        /// <summary>
        /// 省略号
        /// </summary>
        Ellipsis_H,
        /// <summary>
        /// 省略号
        /// </summary>
        Ellipsis_V,
        /// <summary>
        /// 邮件
        /// </summary>
        Envelope,
        /// <summary>
        /// 邮件
        /// </summary>
        Envelope_O,
        /// <summary>
        /// 橡皮擦
        /// </summary>
        Eraser,
        /// <summary>
        /// 交换
        /// </summary>
        Exchange,
        /// <summary>
        /// 感叹号
        /// </summary>
        Exclamation,
        /// <summary>
        /// 圆形感叹号
        /// </summary>
        Exclamation_Circle,
        /// <summary>
        /// 三角感叹号
        /// </summary>
        Exclamation_Triangle,
        /// <summary>
        /// 外部
        /// </summary>
        External_Link,
        /// <summary>
        /// 外部
        /// </summary>
        External_Link_Square,
        /// <summary>
        /// 眼睛
        /// </summary>
        Eye,
        /// <summary>
        /// 斜切眼睛
        /// </summary>
        Eye_Slash,
        /// <summary>
        /// 女人
        /// </summary>
        Female,
        /// <summary>
        /// 战斗机
        /// </summary>
        Fighter_Jet,
        /// <summary>
        /// 胶片
        /// </summary>
        Film,
        /// <summary>
        /// 过滤器
        /// </summary>
        Filter,
        /// <summary>
        /// 火焰
        /// </summary>
        Fire,
        /// <summary>
        /// 灭火器
        /// </summary>
        Fire_Extinguisher,
        /// <summary>
        /// 旗
        /// </summary>
        Flag,
        /// <summary>
        /// 方格旗
        /// </summary>
        Flag_Checkered,
        /// <summary>
        /// 旗
        /// </summary>
        Flag_O,
        /// <summary>
        /// 闪电
        /// </summary>
        Flash,
        /// <summary>
        /// 烧瓶
        /// </summary>
        Flask,
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder,
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder_O,
        /// <summary>
        /// 文件夹打开
        /// </summary>
        Folder_Open,
        /// <summary>
        /// 文件夹打开
        /// </summary>
        Folder_Open_O,
        /// <summary>
        /// 皱眉
        /// </summary>
        Frown_O,
        /// <summary>
        /// 游戏手柄
        /// </summary>
        Gamepad,
        /// <summary>
        /// 小槌
        /// </summary>
        Gavel,
        /// <summary>
        /// 齿轮
        /// </summary>
        Gear,
        /// <summary>
        /// 齿轮组
        /// </summary>
        Gears,
        /// <summary>
        /// 礼物
        /// </summary>
        Gift,
        /// <summary>
        /// 酒杯
        /// </summary>
        Glass,
        /// <summary>
        /// 地球
        /// </summary>
        Globe,
        /// <summary>
        /// 群组
        /// </summary>
        Group,
        /// <summary>
        /// 硬盘
        /// </summary>
        Hdd_O,
        /// <summary>
        /// 头戴式耳机
        /// </summary>
        Headphones,
        /// <summary>
        /// 心
        /// </summary>
        Heart,
        /// <summary>
        /// 心
        /// </summary>
        Heart_O,
        /// <summary>
        /// 家
        /// </summary>
        Home,
        /// <summary>
        /// 收件箱
        /// </summary>
        Inbox,
        /// <summary>
        /// 信息
        /// </summary>
        Info,
        /// <summary>
        /// 圆形信息
        /// </summary>
        Info_Circle,
        /// <summary>
        /// 钥匙
        /// </summary>
        Key,
        /// <summary>
        /// 键盘
        /// </summary>
        Keyboard_O,
        /// <summary>
        /// 便携式电脑
        /// </summary>
        Laptop,
        /// <summary>
        /// 叶子
        /// </summary>
        Leaf,
        /// <summary>
        /// 法定权利
        /// </summary>
        Legal,
        /// <summary>
        /// 柠檬
        /// </summary>
        Lemon_O,
        /// <summary>
        /// 等级下降
        /// </summary>
        Level_Down,
        /// <summary>
        /// 等级上升
        /// </summary>
        Level_Up,
        /// <summary>
        /// 灯泡
        /// </summary>
        Lightbulb_O,
        /// <summary>
        /// 列表
        /// </summary>
        List,
        /// <summary>
        /// 指向箭头
        /// </summary>
        Location_Arrow,
        /// <summary>
        /// 锁
        /// </summary>
        Lock,
        /// <summary>
        /// 魔术棒
        /// </summary>
        Magic,
        /// <summary>
        /// 磁铁
        /// </summary>
        Magnet,
        /// <summary>
        /// 邮件转寄
        /// </summary>
        Mail_Forward,
        /// <summary>
        /// 邮件回复
        /// </summary>
        Mail_Reply,
        /// <summary>
        /// 邮件全回复
        /// </summary>
        Mail_Reply_All,
        /// <summary>
        /// 男人
        /// </summary>
        Male,
        /// <summary>
        /// 地图定位
        /// </summary>
        Map_Marker,
        /// <summary>
        /// 咩
        /// </summary>
        Meh_O,
        /// <summary>
        /// 话筒
        /// </summary>
        Microphone,
        /// <summary>
        /// 斜切话筒
        /// </summary>
        Microphone_Slash,
        /// <summary>
        /// 负号
        /// </summary>
        Minus,
        /// <summary>
        /// 圆形负号
        /// </summary>
        Minus_Circle,
        /// <summary>
        /// 方形负号
        /// </summary>
        Minus_Square,
        /// <summary>
        /// 方形负号
        /// </summary>
        Minus_Square_O,
        /// <summary>
        /// 手机
        /// </summary>
        Mobile,
        /// <summary>
        /// 钱币
        /// </summary>
        Money,
        /// <summary>
        /// 月亮
        /// </summary>
        Moon_O,
        /// <summary>
        /// 音乐
        /// </summary>
        Music,
        /// <summary>
        /// 铅笔
        /// </summary>
        Pencil,
        /// <summary>
        /// 铅笔
        /// </summary>
        Pencil_Square,
        /// <summary>
        /// 铅笔
        /// </summary>
        Pencil_Square_O,
        /// <summary>
        /// 电话
        /// </summary>
        Phone,
        /// <summary>
        /// 方形电话
        /// </summary>
        Phone_Square,
        /// <summary>
        /// 图片
        /// </summary>
        Picture_O,
        /// <summary>
        /// 飞机
        /// </summary>
        Plane,
        /// <summary>
        /// 加号
        /// </summary>
        Plus,
        /// <summary>
        /// 圆形加号
        /// </summary>
        Plus_Circle,
        /// <summary>
        /// 方形加号
        /// </summary>
        Plus_Square,
        /// <summary>
        /// 方形加号
        /// </summary>
        Plus_Square_O,
        /// <summary>
        /// 关机
        /// </summary>
        Power_Off,
        /// <summary>
        /// 打印
        /// </summary>
        Print,
        /// <summary>
        /// 拼图块
        /// </summary>
        Puzzle_Piece,
        /// <summary>
        /// 二维码
        /// </summary>
        Qrcode,
        /// <summary>
        /// 问题
        /// </summary>
        Question,
        /// <summary>
        /// 圆形问题
        /// </summary>
        Question_Circle,
        /// <summary>
        /// 左双引号
        /// </summary>
        Quote_Left,
        /// <summary>
        /// 右双引号
        /// </summary>
        Quote_Right,
        /// <summary>
        /// 随机
        /// </summary>
        Random,
        /// <summary>
        /// 刷新
        /// </summary>
        Refresh,
        /// <summary>
        /// 回复
        /// </summary>
        Reply,
        /// <summary>
        /// 回复
        /// </summary>
        Reply_All,
        /// <summary>
        /// 转发
        /// </summary>
        Retweet,
        /// <summary>
        /// 路
        /// </summary>
        Road,
        /// <summary>
        /// 火箭
        /// </summary>
        Rocket,
        /// <summary>
        /// RSS
        /// </summary>
        Rss,
        /// <summary>
        /// 方形 RSS
        /// </summary>
        Rss_Square,
        /// <summary>
        /// 搜索
        /// </summary>
        Search,
        /// <summary>
        /// 搜索缩小
        /// </summary>
        Search_Minus,
        /// <summary>
        /// 搜索放大
        /// </summary>
        Search_Plus,
        /// <summary>
        /// 分享
        /// </summary>
        Share,
        /// <summary>
        /// 方形分享
        /// </summary>
        Share_Square,
        /// <summary>
        /// 方形分享
        /// </summary>
        Share_Square_O,
        /// <summary>
        /// 盾牌
        /// </summary>
        Shield,
        /// <summary>
        /// 购物车
        /// </summary>
        Shopping_Cart,
        /// <summary>
        /// 签入
        /// </summary>
        Sign_In,
        /// <summary>
        /// 签出
        /// </summary>
        Sign_Out,
        /// <summary>
        /// 讯号
        /// </summary>
        Signal,
        /// <summary>
        /// 网站地图
        /// </summary>
        Sitemap,
        /// <summary>
        /// 笑脸
        /// </summary>
        Smile_O,
        /// <summary>
        /// 排序
        /// </summary>
        Sort,
        /// <summary>
        /// 字符顺序
        /// </summary>
        Sort_Alpha_Asc,
        /// <summary>
        /// 字符倒序
        /// </summary>
        Sort_Alpha_Desc,
        /// <summary>
        /// 数量顺序
        /// </summary>
        Sort_Amount_Asc,
        /// <summary>
        /// 数量倒序
        /// </summary>
        Sort_Amount_Desc,
        /// <summary>
        /// 顺序
        /// </summary>
        Sort_Asc,
        /// <summary>
        /// 倒序
        /// </summary>
        Sort_Desc,
        /// <summary>
        /// 倒序
        /// </summary>
        Sort_Down,
        /// <summary>
        /// 数字顺序
        /// </summary>
        Sort_Numeric_Asc,
        /// <summary>
        /// 数字倒序
        /// </summary>
        Sort_Numeric_Desc,
        /// <summary>
        /// 顺序
        /// </summary>
        Sort_Up,
        /// <summary>
        /// 旋转亮片
        /// </summary>
        Spinner,
        /// <summary>
        /// 方形
        /// </summary>
        Square,
        /// <summary>
        /// 方形
        /// </summary>
        Square_O,
        /// <summary>
        /// 星星
        /// </summary>
        Star,
        /// <summary>
        /// 半星
        /// </summary>
        Star_Half,
        /// <summary>
        /// 空半星
        /// </summary>
        Star_Half_Empty,
        /// <summary>
        /// 星星
        /// </summary>
        Star_O,
        /// <summary>
        /// 下标
        /// </summary>
        Subscript,
        /// <summary>
        /// 手提箱
        /// </summary>
        Suitcase,
        /// <summary>
        /// 太阳
        /// </summary>
        Sun_O,
        /// <summary>
        /// 上标
        /// </summary>
        Superscript,
        /// <summary>
        /// 平板电脑
        /// </summary>
        Tablet,
        /// <summary>
        /// 转速表
        /// </summary>
        Tachometer,
        /// <summary>
        /// 标签
        /// </summary>
        Tag,
        /// <summary>
        /// 标签组
        /// </summary>
        Tags,
        /// <summary>
        /// 任务
        /// </summary>
        Tasks,
        /// <summary>
        /// 终端
        /// </summary>
        Terminal,
        /// <summary>
        /// 图钉
        /// </summary>
        Thumb_Tack,
        /// <summary>
        /// 不赞成
        /// </summary>
        Thumbs_Down,
        /// <summary>
        /// 不赞成
        /// </summary>
        Thumbs_O_Down,
        /// <summary>
        /// 赞成
        /// </summary>
        Thumbs_O_Up,
        /// <summary>
        /// 赞成
        /// </summary>
        Thumbs_Up,
        /// <summary>
        /// 票据
        /// </summary>
        Ticket,
        /// <summary>
        /// X
        /// </summary>
        Times,
        /// <summary>
        /// 圆形 X
        /// </summary>
        Times_Circle,
        /// <summary>
        /// 圆形 X
        /// </summary>
        Times_Circle_O,
        /// <summary>
        /// 水滴
        /// </summary>
        Tint,
        /// <summary>
        /// 下
        /// </summary>
        Toggle_Down,
        /// <summary>
        /// 左
        /// </summary>
        Toggle_Left,
        /// <summary>
        /// 右
        /// </summary>
        Toggle_Right,
        /// <summary>
        /// 上
        /// </summary>
        Toggle_Up,
        /// <summary>
        /// 垃圾桶
        /// </summary>
        Trash_O,
        /// <summary>
        /// 奖杯
        /// </summary>
        Trophy,
        /// <summary>
        /// 货车
        /// </summary>
        Truck,
        /// <summary>
        /// 雨伞
        /// </summary>
        Umbrella,
        /// <summary>
        /// 解锁
        /// </summary>
        Unlock,
        /// <summary>
        /// 解锁
        /// </summary>
        Unlock_Alt,
        /// <summary>
        /// 排序前
        /// </summary>
        Unsorted,
        /// <summary>
        /// 上传
        /// </summary>
        Upload,
        /// <summary>
        /// 用户
        /// </summary>
        User,
        /// <summary>
        /// 用户组
        /// </summary>
        Users,
        /// <summary>
        /// 录像机
        /// </summary>
        Video_Camera,
        /// <summary>
        /// 声音小
        /// </summary>
        Volume_Down,
        /// <summary>
        /// 声音没
        /// </summary>
        Volume_Off,
        /// <summary>
        /// 声音大
        /// </summary>
        Volume_Up,
        /// <summary>
        /// 警告
        /// </summary>
        Warning,
        /// <summary>
        /// 轮椅
        /// </summary>
        Wheelchair,
        /// <summary>
        /// 板手
        /// </summary>
        Wrench
    }
}