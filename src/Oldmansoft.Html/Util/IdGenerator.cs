using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.Util
{
    /// <summary>
    /// 序号生成器
    /// </summary>
    public class IdGenerator
    {
        private int Start { get; set; }

        private int Store { get; set; }

        /// <summary>
        /// 上限
        /// </summary>
        private int Max { get; set; }

        /// <summary>
        /// 创建序号生成器
        /// </summary>
        public IdGenerator()
        {
            Start = 1;
            Store = Start - 1;
            Max = int.MaxValue;
        }

        /// <summary>
        /// 创建序号生成器
        /// </summary>
        /// <param name="start">初始值</param>
        public IdGenerator(int start)
        {
            Start = start;
            Store = Start - 1;
            Max = int.MaxValue;
        }

        /// <summary>
        /// 创建序号生成器
        /// </summary>
        /// <param name="start"></param>
        /// <param name="max"></param>
        public IdGenerator(int start, int max)
        {
            Start = start;
            Store = Start - 1;
            Max = max;
        }

        /// <summary>
        /// 获取下一个值
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            if (Store == Max)
            {
                Store = Start - 1;
            }
            else
            {
                Store++;
            }
            return Store;
        }
    }
}
