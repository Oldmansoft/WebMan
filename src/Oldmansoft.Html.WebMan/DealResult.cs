using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan
{
    /// <summary>
    /// 处理结果
    /// </summary>
    public class DealResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 关闭新窗
        /// </summary>
        public bool CloseOpen { get; set; }

        /// <summary>
        /// 父级页面加载新数据
        /// </summary>
        public bool NewData { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 重定向位置
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 创建错误内容
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DealResult CreateWrong(string message)
        {
            var result = new DealResult();
            result.Success = false;
            result.Message = message;
            return result;
        }

        /// <summary>
        /// 重定向
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DealResult Location(string path)
        {
            var result = new DealResult();
            result.CloseOpen = true;
            result.Path = path;
            return result;
        }

        /// <summary>
        /// 显示消息并重定向
        /// </summary>
        /// <param name="path"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DealResult Location(string path, string message)
        {
            var result = new DealResult();
            result.CloseOpen = true;
            result.Path = path;
            result.Message = message;
            return result;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <returns></returns>
        public static DealResult Refresh()
        {
            var result = new DealResult();
            result.CloseOpen = true;
            result.NewData = true;
            return result;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DealResult Refresh(string message)
        {
            var result = new DealResult();
            result.CloseOpen = true;
            result.NewData = true;
            result.Message = message;
            return result;
        }
    }
}
