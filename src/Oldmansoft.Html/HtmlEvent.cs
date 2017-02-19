using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html
{
    /// <summary>
    /// 事件
    /// </summary>
    public enum HtmlEvent
    {
        /// <summary>
        /// 文档打印之后运行的脚本。
        /// </summary>
        AfterPrint,
        /// <summary>
        /// 文档打印之前运行的脚本。
        /// </summary>
        BeforePrint,
        /// <summary>
        /// 文档卸载之前运行的脚本。
        /// </summary>
        BeforeUnload,
        /// <summary>
        /// 在错误发生时运行的脚本。
        /// </summary>
        Error,
        /// <summary>
        /// 当文档已改变时运行的脚本。
        /// </summary>
        HashChange,
        /// <summary>
        /// 页面结束加载之后触发。
        /// </summary>
        Load,
        /// <summary>
        /// 在消息被触发时运行的脚本。
        /// </summary>
        Message,
        /// <summary>
        /// 当文档离线时运行的脚本。
        /// </summary>
        Offline,
        /// <summary>
        /// 当文档上线时运行的脚本。
        /// </summary>
        Online,
        /// <summary>
        /// 当窗口隐藏时运行的脚本。
        /// </summary>
        PageHide,
        /// <summary>
        /// 当窗口成为可见时运行的脚本。
        /// </summary>
        PageShow,
        /// <summary>
        /// 当窗口历史记录改变时运行的脚本。
        /// </summary>
        PopState,
        /// <summary>
        /// 当文档执行撤销（redo）时运行的脚本。
        /// </summary>
        Redo,
        /// <summary>
        /// 当浏览器窗口被调整大小时触发。
        /// </summary>
        Resize,
        /// <summary>
        /// 在 Web Storage 区域更新后运行的脚本。
        /// </summary>
        Storage,
        /// <summary>
        /// 在文档执行 undo 时运行的脚本。
        /// </summary>
        Undo,
        /// <summary>
        /// 一旦页面已下载时触发（或者浏览器窗口已被关闭）。
        /// </summary>
        Unload,

        /// <summary>
        /// 元素失去焦点时运行的脚本。
        /// </summary>
        Blur,
        /// <summary>
        /// 在元素值被改变时运行的脚本。
        /// </summary>
        Change,
        /// <summary>
        /// 当上下文菜单被触发时运行的脚本。
        /// </summary>
        ContextMenu,
        /// <summary>
        /// 当元素获得焦点时运行的脚本。
        /// </summary>
        Focus,
        /// <summary>
        /// 在表单改变时运行的脚本。
        /// </summary>
        FormChange,
        /// <summary>
        /// 当表单获得用户输入时运行的脚本。
        /// </summary>
        FormInput,
        /// <summary>
        /// 当元素获得用户输入时运行的脚本。
        /// </summary>
        Input,
        /// <summary>
        /// 当元素无效时运行的脚本。
        /// </summary>
        Invalid,
        /// <summary>
        /// 当表单中的重置按钮被点击时触发。HTML5 中不支持。
        /// </summary>
        Reset,
        /// <summary>
        /// 在元素中文本被选中后触发。
        /// </summary>
        Select,
        /// <summary>
        /// 在提交表单时触发。
        /// </summary>
        Submit,

        /// <summary>
        /// 在用户按下按键时触发。
        /// </summary>
        KeyDown,
        /// <summary>
        /// 在用户敲击按钮时触发。
        /// </summary>
        KeyPress,
        /// <summary>
        /// 当用户释放按键时触发。
        /// </summary>
        KeyUp,

        /// <summary>
        /// 元素上发生鼠标点击时触发。
        /// </summary>
        Click,
        /// <summary>
        /// 元素上发生鼠标双击时触发。
        /// </summary>
        DblClick,
        /// <summary>
        /// 元素被拖动时运行的脚本。
        /// </summary>
        Drag,
        /// <summary>
        /// 在拖动操作末端运行的脚本。
        /// </summary>
        DragEnd,
        /// <summary>
        /// 当元素元素已被拖动到有效拖放区域时运行的脚本。
        /// </summary>
        DragEnter,
        /// <summary>
        /// 当元素离开有效拖放目标时运行的脚本。
        /// </summary>
        DragLeave,
        /// <summary>
        /// 当元素在有效拖放目标上正在被拖动时运行的脚本。
        /// </summary>
        DragOver,
        /// <summary>
        /// 在拖动操作开端运行的脚本。
        /// </summary>
        DragStart,
        /// <summary>
        /// 当被拖元素正在被拖放时运行的脚本。
        /// </summary>
        Drop,
        /// <summary>
        /// 当元素上按下鼠标按钮时触发。
        /// </summary>
        MouseDown,
        /// <summary>
        /// 当鼠标指针移动到元素上时触发。
        /// </summary>
        MouseMove,
        /// <summary>
        /// 当鼠标指针移出元素时触发。
        /// </summary>
        MouseOut,
        /// <summary>
        /// 当鼠标指针移动到元素上时触发。
        /// </summary>
        MouseOver,
        /// <summary>
        /// 当在元素上释放鼠标按钮时触发。
        /// </summary>
        MouseUp,
        /// <summary>
        /// 当鼠标滚轮正在被滚动时运行的脚本。
        /// </summary>
        MouseWheel,
        /// <summary>
        /// 当元素滚动条被滚动时运行的脚本。
        /// </summary>
        Scroll,

        /// <summary>
        /// 在退出时运行的脚本。
        /// </summary>
        Abort,
        /// <summary>
        /// 当文件就绪可以开始播放时运行的脚本（缓冲已足够开始时）。
        /// </summary>
        CanPlay,
        /// <summary>
        /// 当媒介能够无需因缓冲而停止即可播放至结尾时运行的脚本。
        /// </summary>
        CanPlayThrough,
        /// <summary>
        /// 当媒介长度改变时运行的脚本。
        /// </summary>
        DurationChange,
        /// <summary>
        /// 当发生故障并且文件突然不可用时运行的脚本（比如连接意外断开时）。
        /// </summary>
        Emptied,
        /// <summary>
        /// 当媒介已到达结尾时运行的脚本（可发送类似“感谢观看”之类的消息）。
        /// </summary>
        Ended,
        /// <summary>
        /// 当媒介数据已加载时运行的脚本。
        /// </summary>
        LoadedData,
        /// <summary>
        /// 当元数据（比如分辨率和时长）被加载时运行的脚本。
        /// </summary>
        LoadedMetaData,
        /// <summary>
        /// 在文件开始加载且未实际加载任何数据前运行的脚本。
        /// </summary>
        LoadStart,
        /// <summary>
        /// 当媒介被用户或程序暂停时运行的脚本。
        /// </summary>
        Pause,
        /// <summary>
        /// 当媒介已就绪可以开始播放时运行的脚本。
        /// </summary>
        Play,
        /// <summary>
        /// 当媒介已开始播放时运行的脚本。
        /// </summary>
        Playing,
        /// <summary>
        /// 当浏览器正在获取媒介数据时运行的脚本。
        /// </summary>
        Progress,
        /// <summary>
        /// 每当回放速率改变时运行的脚本（比如当用户切换到慢动作或快进模式）。
        /// </summary>
        RateChange,
        /// <summary>
        /// 每当就绪状态改变时运行的脚本（就绪状态监测媒介数据的状态）。
        /// </summary>
        ReadyStateChange,
        /// <summary>
        /// 当 seeking 属性设置为 false（指示定位已结束）时运行的脚本。
        /// </summary>
        Seeked,
        /// <summary>
        /// 当 seeking 属性设置为 true（指示定位是活动的）时运行的脚本。
        /// </summary>
        Seeking,
        /// <summary>
        /// 在浏览器不论何种原因未能取回媒介数据时运行的脚本。
        /// </summary>
        Stalled,
        /// <summary>
        /// 在媒介数据完全加载之前不论何种原因终止取回媒介数据时运行的脚本。
        /// </summary>
        Suspend,
        /// <summary>
        /// 当播放位置改变时（比如当用户快进到媒介中一个不同的位置时）运行的脚本。
        /// </summary>
        TimeUpDate,
        /// <summary>
        /// 每当音量改变时（包括将音量设置为静音）时运行的脚本。
        /// </summary>
        VolumeChange,
        /// <summary>
        /// 当媒介已停止播放但打算继续播放时（比如当媒介暂停已缓冲更多数据）运行脚本
        /// </summary>
        Waiting,

        /// <summary>
        /// 触摸开始的时候触发
        /// </summary>
        TouchStart,
        /// <summary>
        /// 手指在屏幕上滑动的时候触发
        /// </summary>
        TouchMove,
        /// <summary>
        /// 触摸结束的时候触发
        /// </summary>
        TouchEnd
    }
}
