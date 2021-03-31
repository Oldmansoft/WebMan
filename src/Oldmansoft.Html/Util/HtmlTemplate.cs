using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Oldmansoft.Html.Util
{
    /// <summary>
    /// 模板
    /// </summary>
    public class HtmlTemplate
    {
        private readonly List<string> Content;

        private readonly List<string> Position;

        /// <summary>
        /// 创建模板
        /// </summary>
        /// <param name="input"></param>
        public HtmlTemplate(string input)
        {
            Content = new List<string>();
            Position = new List<string>();
            InitContent(input);
        }

        private void InitContent(string input)
        {
            var regex = new Regex("\\{(\\w+)\\}");
            var matches = regex.Matches(input);
            var lastIndex = 0;
            foreach (Match match in matches)
            {
                var block = input.Substring(lastIndex, match.Index - lastIndex);
                lastIndex = match.Index + match.Length;
                Content.Add(block);
                Position.Add(match.Groups[1].Value);
            }

            if (lastIndex < input.Length)
            {
                Content.Add(input.Substring(lastIndex));
            }
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        /// <param name="routeValues"></param>
        public void Format(IHtmlOutput outer, object routeValues)
        {
            if (routeValues == null) return;
            Format(outer, routeValues.ConvertToKeyValues());
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        /// <param name="args"></param>
        public void Format(IHtmlOutput outer, IDictionary<string, string> args)
        {
            if (outer == null) throw new ArgumentNullException("outer");
            if (args == null) throw new ArgumentNullException("args");

            for (var i = 0; i < Content.Count; i++)
            {
                outer.Append(Content[i]);
                if (Position.Count > i && args.ContainsKey(Position[i]))
                {
                    outer.Append(args[Position[i]]);
                }
            }
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="outer"></param>
        /// <param name="args"></param>
        public void Format(IHtmlOutput outer, IDictionary<string, Action> args)
        {
            if (outer == null) throw new ArgumentNullException("outer");
            if (args == null) throw new ArgumentNullException("args");

            for (var i = 0; i < Content.Count; i++)
            {
                outer.Append(Content[i]);
                if (Position.Count > i && args.ContainsKey(Position[i]))
                {
                    args[Position[i]]();
                }
            }
        }
    }
}
