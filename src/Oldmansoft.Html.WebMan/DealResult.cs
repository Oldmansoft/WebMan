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
        /// 重定向行为
        /// </summary>
        public LinkBehave Behave { get; set; }

        /// <summary>
        /// 创建错误内容
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static DealResult Wrong(string message)
        {
            var result = new DealResult
            {
                Success = false,
                Message = message
            };
            return result;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static DealResult WrongRefresh(string message)
        {
            var result = new DealResult
            {
                CloseOpen = true,
                NewData = true,
                Message = message,
                Success = false
            };
            return result;
        }

        /// <summary>
        /// 重定向
        /// </summary>
        /// <param name="location">位置</param>
        /// <returns></returns>
        public static DealResult Location(ILocation location)
        {
            var result = new DealResult
            {
                CloseOpen = true,
                Path = location.Path,
                Behave = location.Behave,
                Success = true
            };
            return result;
        }

        /// <summary>
        /// 显示消息并重定向
        /// </summary>
        /// <param name="location">位置</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static DealResult Location(ILocation location, string message)
        {
            var result = new DealResult
            {
                CloseOpen = true,
                Path = location.Path,
                Behave = location.Behave,
                Message = message,
                Success = true
            };
            return result;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <returns></returns>
        public static DealResult Refresh()
        {
            var result = new DealResult
            {
                CloseOpen = true,
                NewData = true,
                Success = true
            };
            return result;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static DealResult Refresh(string message)
        {
            var result = new DealResult
            {
                CloseOpen = true,
                NewData = true,
                Message = message,
                Success = true
            };
            return result;
        }
        
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static DealResult Show(string message)
        {
            var result = new DealResult
            {
                CloseOpen = true,
                Success = true,
                Message = message
            };
            return result;
        }
    }
}
