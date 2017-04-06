using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.Html.WebMan.Util
{
    abstract class ChainOfResponsibilityHandler<TInput, TResult>
    {
        private ChainOfResponsibilityHandler<TInput, TResult> Next { get; set; }

        /// <summary>
        /// 设置下一个处理者
        /// </summary>
        /// <param name="handler">下一个处理者</param>
        /// <returns>返回下一个处理者</returns>
        public ChainOfResponsibilityHandler<TInput, TResult> SetNext(ChainOfResponsibilityHandler<TInput, TResult> handler)
        {
            if (handler == this) throw new ArgumentException("不能传递自己作为参数");
            Next = handler;
            return Next;
        }

        internal ChainOfResponsibilityHandler<TInput, TResult> GetNext()
        {
            return Next;
        }

        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="input"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected abstract bool Request(TInput input, ref TResult result);

        internal bool Handle(TInput input, ref TResult result)
        {
            return Request(input, ref result);
        }
    }
}
