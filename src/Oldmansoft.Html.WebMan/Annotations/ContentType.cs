using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Annotations
{
    /// <summary>
    /// 类型内容
    /// </summary>
    [Flags]
    public enum ContentType
    {
        /// <summary>
        /// 空
        /// </summary>
        None = 0,

        /// <summary>
        /// 应用
        /// </summary>
        Application = 1,

        /// <summary>
        /// 音频
        /// </summary>
        Audio = 2,

        /// <summary>
        /// 绘图
        /// </summary>
        Drawing = 4,

        /// <summary>
        /// 字体
        /// </summary>
        Font = 8,

        /// <summary>
        /// 图片
        /// </summary>
        Image = 16,

        /// <summary>
        /// 消息
        /// </summary>
        Message = 32,

        /// <summary>
        /// 文本
        /// </summary>
        Text = 64,

        /// <summary>
        /// 视频
        /// </summary>
        Video = 128,

        /// <summary>
        /// x-world
        /// </summary>
        X_World = 256
    }
}
