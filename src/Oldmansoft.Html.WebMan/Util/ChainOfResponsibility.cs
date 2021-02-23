using System;

namespace Oldmansoft.Html.WebMan.Util
{
    /// <summary>
    /// 职责链模式
    /// </summary>
    /// <typeparam name="TInput">输入处理参数</typeparam>
    /// <typeparam name="TResult">返回结果</typeparam>
    abstract class ChainOfResponsibility<TInput, TResult>
    {
        private ChainOfResponsibilityHandler<TInput, TResult> Handler { get; set; }

        /// <summary>
        /// 初始化责任链
        /// </summary>
        /// <returns></returns>
        protected abstract ChainOfResponsibilityHandler<TInput, TResult> InitChain();

        private ChainOfResponsibilityHandler<TInput, TResult> GetHandler()
        {
            if (Handler != null) return Handler;

            Handler = InitChain();
            if (Handler == null) throw new ArgumentNullException("InitChain 方法返回空");
            return Handler;
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TResult Handle(TInput input)
        {
            var handler = GetHandler();
            while (handler != null)
            {
                TResult result = default(TResult);
                if (handler.Handle(input, ref result)) return result;
                handler = handler.GetNext();
            }
            return default(TResult);
        }
    }
}
