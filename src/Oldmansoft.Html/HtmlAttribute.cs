namespace Oldmansoft.Html
{
    /// <summary>
    /// Html 属性
    /// </summary>
    public enum HtmlAttribute
    {
        /// <summary>
        /// 规定单元格中内容的缩写版本。
        /// </summary>
        Abbr,

        /// <summary>
        /// 规定通过文件上传来提交的文件的类型。
        /// </summary>
        Accept,

        /// <summary>
        /// 规定服务器可处理的表单数据字符集。
        /// </summary>
        Accept_Charset,

        /// <summary>
        /// 规定激活元素的快捷键。
        /// </summary>
        AccessKey,

        /// <summary>
        /// 规定当提交表单时向何处发送表单数据。
        /// </summary>
        Action,

        /// <summary>
        /// 规定图像输入的对齐方式。
        /// 规定与 col 元素相关的内容的水平对齐方式。
        /// 定义在列组合中内容的水平对齐方式。
        /// 定义围绕该对象的文本对齐方式。
        /// 规定单元格内容的水平对齐方式。
        /// 定义 tfoot 元素中内容的对齐方式。
        /// 定义 thead 元素中内容的对齐方式。
        /// 定义表格行的内容对齐方式。
        /// </summary>
        Align,

        /// <summary>
        /// 定义图像输入的替代文本。
        /// 定义此区域的替换文本。
        /// </summary>
        Alt,

        /// <summary>
        /// 由空格分隔的指向档案文件的 URL 列表。这些档案文件包含了与对象相关的资源。
        /// </summary>
        Archive,

        /// <summary>
        /// 规定异步执行脚本（仅适用于外部脚本）。
        /// </summary>
        Async,

        /// <summary>
        /// 规定是否使用输入字段的自动完成功能。
        /// 规定是否启用表单的自动完成功能。
        /// </summary>
        AutoComplete,

        /// <summary>
        /// 规定输入字段在页面加载时是否获得焦点。（不适用于 type="hidden"）
        /// 使 keygen 字段在页面加载时获得焦点。
        /// 规定在页面加载后文本区域自动获得焦点。
        /// </summary>
        AutoFocus,

        /// <summary>
        /// 如果出现该属性，则音频在就绪后马上播放。
        /// 如果出现该属性，则视频在就绪后马上播放。
        /// </summary>
        AutoPlay,

        /// <summary>
        /// 对单元进行分类。
        /// </summary>
        Axis,

        /// <summary>
        /// 定义对象周围的边框。
        /// 规定表格边框的宽度。
        /// </summary>
        Border,

        /// <summary>
        /// 规定单元边沿与其内容之间的空白。
        /// </summary>
        CellPadding,

        /// <summary>
        /// 规定单元格之间的空白。
        /// </summary>
        CellSpacing,

        /// <summary>
        /// 如果使用，则将 keygen 的值设置为在提交时询问。
        /// </summary>
        Challenge,

        /// <summary>
        /// 规定根据哪个字符来对齐与 col 元素相关的内容。
        /// 规定根据哪个字符来进行文本对齐。
        /// </summary>
        Char,

        /// <summary>
        /// 规定第一个对齐字符的偏移量。
        /// 规定第一个对齐字符的偏移量。
        /// </summary>
        CharOff,

        /// <summary>
        /// 规定在外部脚本文件中使用的字符编码。
        /// </summary>
        Charset,

        /// <summary>
        /// 规定此 input 元素首次加载时应当被选中。
        /// 规定在页面加载后选中命令/菜单项目。
        /// </summary>
        Checked,

        /// <summary>
        /// 规定引用的来源。
        /// 指向另外一个文档的 URL，此文档可解释文本被删除的原因。
        /// </summary>
        Cite,

        /// <summary>
        /// 规定元素的一个或多个类名（引用样式表中的类）。
        /// </summary>
        Class,

        /// <summary>
        /// 定义嵌入 Windows Registry 中或某个 URL 中的类的 ID 值，此属性可用来指定浏览器中包含的对象的位置，通常是一个 Java 类。
        /// </summary>
        ClassId,

        /// <summary>
        /// 定义在何处可找到对象所需的代码，提供一个基准 URL。
        /// </summary>
        CodeBase,

        /// <summary>
        /// 通过 classid 属性所引用的代码的 MIME 类型。
        /// </summary>
        CodeType,

        /// <summary>
        /// 定义框架集中列的数目和尺寸。
        /// 规定文本区内的可见宽度。
        /// </summary>
        Cols,

        /// <summary>
        /// 规定单元格可横跨的列数。
        /// </summary>
        Colspan,

        /// <summary>
        /// 定义与 http-equiv 或 name 属性相关的元信息
        /// </summary>
        Content,

        /// <summary>
        /// 规定元素内容是否可编辑。
        /// </summary>
        ContentEditable,

        /// <summary>
        /// 如果出现该属性，则向用户显示控件，比如播放按钮。
        /// 如果出现该属性，则向用户显示控件，比如播放按钮。
        /// </summary>
        Controls,

        /// <summary>
        /// 定义可点击区域（对鼠标敏感的区域）的坐标。
        /// </summary>
        Coords,

        /// <summary>
        /// 同源资源
        /// </summary>
        CrossOrigin,

        /// <summary>
        /// 定义引用对象数据的 URL。如果有需要对象处理的数据文件,要用 data 属性来指定这些数据文件。
        /// </summary>
        Data,

        /// <summary>
        /// 定义文本被删除的日期和时间。
        /// 规定日期 / 时间。否则，由元素的内容给定日期 / 时间。
        /// </summary>
        DateTime,

        /// <summary>
        /// 可定义此对象仅可被声明，但不能被创建或例示，直到此对象得到应用为止。
        /// </summary>
        Declare,

        /// <summary>
        /// 把命令/菜单项设置为默认命令。
        /// 规定该轨道是默认的，假如没有选择任何轨道。
        /// </summary>
        Default,

        /// <summary>
        /// 规定是否对脚本执行进行延迟，直到页面加载为止。
        /// </summary>
        Defer,

        /// <summary>
        /// 规定元素中内容的文本方向。
        /// </summary>
        Dir,

        /// <summary>
        /// 当 input 元素加载时禁用此元素。
        /// 定义 command 是否可用。
        /// 规定应该禁用 fieldset。
        /// 禁用 keytag 字段。
        /// 规定命令/菜单项应该被禁用。
        /// 规定禁用该选项组。
        /// 规定此选项应在首次加载时被禁用。
        /// 规定禁用该下拉列表。
        /// 规定禁用该文本区。
        /// </summary>
        Disabled,

        /// <summary>
        /// 规定被下载的超链接目标。
        /// </summary>
        Download,

        /// <summary>
        /// 规定元素是否可拖动。
        /// </summary>
        Draggable,

        /// <summary>
        /// 规定在拖动被拖动数据时是否进行复制、移动或链接。
        /// </summary>
        DropZone,

        /// <summary>
        /// 规定在发送表单数据之前如何对其进行编码。
        /// </summary>
        Enctype,

        /// <summary>
        /// 规定 label 绑定到哪个表单元素。
        /// 定义输出域相关的一个或多个元素。
        /// </summary>
        For,

        /// <summary>
        /// 规定输入字段所属的一个或多个表单。
        /// 规定 fieldset 所属的一个或多个表单。
        /// 定义该 keygen 字段所属的一个或多个表单。
        /// 规定 label 字段所属的一个或多个表单。
        /// 规定 meter 元素所属的一个或多个表单。
        /// 规定对象所属的一个或多个表单。
        /// 规定文本区域所属的一个或多个表单。
        /// 规定文本区域所属的一个或多个表单。
        /// </summary>
        Form,

        /// <summary>
        /// 覆盖表单的 action 属性。
        /// （适用于 type = "submit" 和 type = "image"）
        /// </summary>
        FormAction,

        /// <summary>
        /// 覆盖表单的 enctype 属性。
        /// （适用于 type="submit" 和 type="image"）
        /// </summary>
        FormEnctype,

        /// <summary>
        /// 覆盖表单的 method 属性。
        /// （适用于 type="submit" 和 type="image"）
        /// </summary>
        FormMethod,

        /// <summary>
        /// 覆盖表单的 novalidate 属性。
        /// 如果使用该属性，则提交表单时不进行验证。
        /// </summary>
        FormNoValidate,

        /// <summary>
        /// 覆盖表单的 target 属性。
        /// （适用于 type="submit" 和 type="image"）
        /// </summary>
        FormTarget,

        /// <summary>
        /// 规定外侧边框的哪个部分是可见的。
        /// </summary>
        Frame,

        /// <summary>
        /// 规定是否显示框架周围的边框。
        /// </summary>
        FrameBorder,

        /// <summary>
        /// 规定与单元格相关的表头。
        /// </summary>
        Headers,

        /// <summary>
        /// 定义 input 字段的高度。（适用于 type="image"）
        /// 设置 canvas 的高度。
        /// 设置嵌入内容的高度。
        /// 规定 iframe 的高度。
        /// 定义图像的高度。
        /// 定义对象的高度。
        /// 设置视频播放器的高度。
        /// </summary>
        Height,

        /// <summary>
        /// 规定元素仍未或不再相关。
        /// </summary>
        Hidden,

        /// <summary>
        /// 规定被视作高的值的范围。
        /// </summary>
        High,

        /// <summary>
        /// 规定链接指向的页面的 URL。
        /// 定义此区域的目标 URL。
        /// </summary>
        Href,

        /// <summary>
        /// 规定被链接文档的语言。
        /// </summary>
        HrefLang,

        /// <summary>
        /// 定义对象周围水平方向的空白。
        /// </summary>
        HSpace,

        /// <summary>
        /// 把 content 属性关联到 HTTP 头部。
        /// </summary>
        Http_Equiv,

        /// <summary>
        /// 定义作为 command 来显示的图像的 url。
        /// 规定命令/菜单项的图标。
        /// </summary>
        Icon,

        /// <summary>
        /// 规定元素的唯一 id。
        /// 为 map 标签定义唯一的名称。
        /// </summary>
        Id,

        /// <summary>
        /// 指定脚本完整性的算法和值
        /// </summary>
        Integrity,

        /// <summary>
        /// 将图像定义为服务器端图像映射。
        /// </summary>
        IsMap,

        /// <summary>
        /// 定义 keytype。rsa 生成 RSA 密钥。
        /// </summary>
        KeyType,

        /// <summary>
        /// 表示轨道属于什么文本类型。
        /// </summary>
        Kind,

        /// <summary>
        /// 为 command 定义可见的 label。
        /// 规定菜单的可见标签。
        /// 规定命令/菜单项的名称，以向用户显示。
        /// 为选项组规定描述。
        /// 定义当使用 optgroup 时所使用的标注。
        /// 轨道的标签或标题。
        /// </summary>
        Label,

        /// <summary>
        /// 规定元素内容的语言。
        /// </summary>
        Lang,

        /// <summary>
        /// 引用包含输入字段的预定义选项的 datalist 。
        /// </summary>
        List,

        /// <summary>
        /// 规定一个包含有关框架内容的长描述的页面。
        /// 规定一个页面，该页面包含了有关 iframe 的较长描述。
        /// 指向包含长的图像描述文档的 URL。
        /// </summary>
        LongDesc,

        /// <summary>
        /// 如果出现该属性，则每当音频结束时重新开始播放。
        /// 如果出现该属性，则当媒介文件完成播放后再次开始播放。
        /// </summary>
        Loop,

        /// <summary>
        /// 规定被视作低的值的范围。
        /// </summary>
        Low,

        /// <summary>
        /// 定义一个 URL，在这个 URL 上描述了文档的缓存信息。
        /// </summary>
        Manifest,

        /// <summary>
        /// 定义框架的上方和下方的边距。
        /// 定义 iframe 的顶部和底部的边距。
        /// </summary>
        MarginHeight,

        /// <summary>
        /// 定义框架的左侧和右侧的边距。
        /// 定义 iframe 的左侧和右侧的边距。
        /// </summary>
        MarginWidth,

        /// <summary>
        /// 规定输入字段的最大值。
        /// 请与 "min" 属性配合使用，来创建合法值的范围。
        /// 规定范围的最大值。
        /// 规定任务一共需要多少工作。
        /// </summary>
        Max,

        /// <summary>
        /// 规定输入字段中的字符的最大长度。
        /// 规定文本区域的最大字符数。
        /// </summary>
        MaxLength,

        /// <summary>
        /// 规定被链接文档是为何种媒介/设备优化的。
        /// 规定媒体资源的类型。
        /// 为样式表规定不同的媒介类型。
        /// </summary>
        Media,

        /// <summary>
        /// 规定用于发送 form-data 的 HTTP 方法。
        /// </summary>
        Method,

        /// <summary>
        /// 规定输入字段的最小值。
        /// 请与 "max" 属性配合使用，来创建合法值的范围。
        /// 规定范围的最小值。
        /// </summary>
        Min,

        /// <summary>
        /// 如果使用该属性，则允许一个以上的值。
        /// 使用以下 input 类型：email 和 file。
        /// 规定可选择多个选项。
        /// </summary>
        Multiple,

        /// <summary>
        /// 规定视频输出应该被静音。
        /// 规定视频的音频输出应该被静音。
        /// </summary>
        Muted,

        /// <summary>
        /// 定义 input 元素的名称。
        /// 规定 fieldset 的名称。
        /// 规定表单的名称。
        /// 规定框架的名称。
        /// 规定 iframe 的名称。
        /// 定义 keygen 元素的唯一名称。name 属性用于在提交表单时搜集字段的值。
        /// 为 image-map 规定的名称。
        /// 把 content 属性关联到一个名称。
        /// 为对象定义唯一的名称（以便在脚本中使用）。
        /// 定义参数的名称（用在脚本中）。
        /// 规定下拉列表的名称。
        /// 规定文本区的名称。
        /// </summary>
        Name,

        /// <summary>
        /// 从图像映射排除某个区域。
        /// </summary>
        NoHref,

        /// <summary>
        /// 规定无法调整框架的大小。
        /// </summary>
        NoResize,

        /// <summary>
        /// 如果使用该属性，则提交表单时不进行验证。
        /// </summary>
        NoValidate,

        /// <summary>
        /// 定义 details 是否可见。
        /// 规定 dialog 元素是活动的，用户可与之交互。
        /// </summary>
        Open,

        /// <summary>
        /// 规定度量的优化值。
        /// </summary>
        Optimum,

        /// <summary>
        /// 规定输入字段的值的模式或格式。
        /// </summary>
        Pattern,

        /// <summary>
        /// 规定帮助用户填写输入字段的提示。
        /// 规定描述文本区域预期值的简短提示。
        /// </summary>
        PlaceHolder,

        /// <summary>
        /// 规定视频下载时显示的图像，或者在用户点击播放按钮前显示的图像。
        /// </summary>
        Poster,

        /// <summary>
        /// 如果出现该属性，则音频在页面加载时进行加载，并预备播放。如果使用 "autoplay"，则忽略该属性。
        /// 如果出现该属性，则视频在页面加载时进行加载，并预备播放。如果使用 "autoplay"，则忽略该属性。
        /// </summary>
        PreLoad,

        /// <summary>
        /// 一个由空格分隔的 URL 列表，这些 URL 包含着有关页面的元数据信息。
        /// </summary>
        Profile,

        /// <summary>
        /// 指示 time 元素中的日期 / 时间是文档（或 article 元素）的发布日期。
        /// </summary>
        PubDate,

        /// <summary>
        /// 定义 command 所属的组名。仅在类型为 radio 时使用。
        /// 规定命令组的名称，命令组会在命令/菜单项本身被切换时进行切换。
        /// </summary>
        RadioGroup,

        /// <summary>
        /// 规定输入字段为只读。
        /// 规定文本区为只读。
        /// </summary>
        ReadOnly,

        /// <summary>
        /// 规定当前文档与被链接文档之间的关系。
        /// </summary>
        Rel,

        /// <summary>
        /// 指示输入字段的值是必需的。
        /// 规定文本区域是必填的。
        /// </summary>
        Required,

        /// <summary>
        /// 规定列表顺序为降序。(9,8,7...)
        /// </summary>
        Reversed,

        /// <summary>
        /// 定义框架集中行的数目和尺寸。
        /// 规定文本区内的可见行数。
        /// </summary>
        Rows,

        /// <summary>
        /// 规定单元格可横跨的行数。
        /// </summary>
        Rowspan,

        /// <summary>
        /// 规定内侧边框的哪个部分是可见的。
        /// </summary>
        Rules,

        /// <summary>
        /// 启用一系列对 iframe 中内容的额外限制。
        /// </summary>
        Sandbox,

        /// <summary>
        /// 定义用于翻译 content 属性值的格式。
        /// </summary>
        Scheme,

        /// <summary>
        /// 定义将表头数据与单元数据相关联的方法。
        /// </summary>
        Scope,

        /// <summary>
        /// 规定是否在框架中显示滚动条。
        /// 规定是否在 iframe 中显示滚动条。
        /// </summary>
        Scrolling,

        /// <summary>
        /// 规定 iframe 看上去像是包含文档的一部分。
        /// </summary>
        Seamless,

        /// <summary>
        /// 规定选项（在首次显示在列表中时）表现为选中状态。
        /// </summary>
        Selected,

        /// <summary>
        /// 定义区域的形状。
        /// </summary>
        Shape,

        /// <summary>
        /// 定义输入字段的宽度。
        /// 规定下拉列表中可见选项的数目。
        /// </summary>
        Size,

        /// <summary>
        /// 规定被链接资源的尺寸。仅适用于 rel="icon"。
        /// </summary>
        Sizes,

        /// <summary>
        /// 规定 col 元素应该横跨的列数。
        /// </summary>
        Span,

        /// <summary>
        /// 规定是否对元素进行拼写和语法检查。
        /// </summary>
        SpellCheck,

        /// <summary>
        /// 定义以提交按钮形式显示的图像的 URL。
        /// 要播放的音频的 URL。
        /// 嵌入内容的 URL。
        /// 规定在框架中显示的文档的 URL。
        /// 规定在 iframe 中显示的文档的 URL。
        /// 规定显示图像的 URL。
        /// 规定外部脚本文件的 URL。
        /// 规定媒体文件的 URL。
        /// 轨道的 URL。
        /// 要播放的视频的 URL。
        /// </summary>
        Src,

        /// <summary>
        /// 轨道的语言，若 kind 属性值是 "subtitles"，则该属性必需的。
        /// </summary>
        SrcLang,

        /// <summary>
        /// 规定在 iframe 中显示的页面的 HTML 内容。
        /// </summary>
        SrcDoc,

        /// <summary>
        /// 定义当对象正在加载时所显示的文本。
        /// </summary>
        Standby,

        /// <summary>
        /// 规定有序列表的起始值。
        /// </summary>
        Start,

        /// <summary>
        /// 规定输入字的的合法数字间隔。
        /// </summary>
        Step,

        /// <summary>
        /// 规定元素的行内 CSS 样式。
        /// </summary>
        Style,

        /// <summary>
        /// 规定表格的摘要。
        /// </summary>
        Summary,

        /// <summary>
        /// 规定元素的 tab 键次序。
        /// </summary>
        TabIndex,

        /// <summary>
        /// 规定在何处打开链接文档。
        /// 规定在何处打开 action URL。
        /// </summary>
        Target,

        /// <summary>
        /// 规定有关元素的额外信息。
        /// </summary>
        Title,

        /// <summary>
        /// 规定 input 元素的类型。
        /// 规定被链接文档的的 MIME 类型。
        /// 定义嵌入内容的类型。
        /// 规定要显示哪种菜单类型。
        /// 规定命令/菜单项的类型。默认是 "command"。
        /// 定义被规定在 data 属性中指定的文件中出现的数据的 MIME 类型。
        /// 规定在列表中使用的标记类型。
        /// 规定参数的 MIME 类型（internet media type）。
        /// 指示脚本的 MIME 类型。
        /// 规定媒体资源的 MIME 类型。
        /// 规定样式表的 MIME 类型。
        /// </summary>
        Type,

        /// <summary>
        /// 将图像定义为客户器端图像映射。
        /// 规定与对象一同使用的客户端图像映射的 URL。
        /// </summary>
        UseMap,

        /// <summary>
        /// 定义与 col 元素相关的内容的垂直对齐方式。
        /// 规定单元格内容的垂直排列方式。
        /// 规定 tfoot 元素中内容的垂直对齐方式。
        /// 规定 thead 元素中内容的垂直对齐方式。
        /// 规定表格行中内容的垂直对齐方式。
        /// </summary>
        VAlign,

        /// <summary>
        /// 规定 input 元素的值。
        /// 规定度量的当前值。
        /// 定义送往服务器的选项值。
        /// 规定参数的值。
        /// 规定已经完成多少任务。
        /// </summary>
        Value,

        /// <summary>
        /// 规定值的 MIME 类型。
        /// </summary>
        ValueType,

        /// <summary>
        /// 定义对象的垂直方向的空白。
        /// </summary>
        VSpace,

        /// <summary>
        /// 定义 input 字段的宽度。（适用于 type="image"）
        /// 设置 canvas 的宽度。
        /// 规定 col 元素的宽度。
        /// 设置嵌入内容的宽度。
        /// 定义 iframe 的宽度。
        /// 设置图像的宽度。
        /// 定义对象的宽度。
        /// 规定表格的宽度。
        /// 设置视频播放器的宽度。
        /// </summary>
        Width,

        /// <summary>
        /// 规定当在表单中提交时，文本区域中的文本如何换行。
        /// </summary>
        Wrap,

        /// <summary>
        /// 定义 XML namespace 属性。
        /// </summary>
        Xmlns
    }
}