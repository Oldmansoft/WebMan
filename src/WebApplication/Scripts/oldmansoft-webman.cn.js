﻿$app.configText(function (text) {
    text.ok = "好的";
	text.yes = "是";
	text.no = "否";
	text.loading = "加载中";
});
$man.configText(function (text) {
    text.dataTable.info = "第 _PAGE_ 页 ( 总共 _PAGES_ 页 ) 共 _TOTAL_ 条数据";
    text.dataTable.infoEmpty = "空";
    text.dataTable.processing = "正在处理中";
    text.dataTable.paginate.first = "首页";
    text.dataTable.paginate.last = "尾页";
    text.dataTable.paginate.next = "后一页";
    text.dataTable.paginate.previous = "前一页";

    text.error = "错误";
    text.warning = "警告";
    text.denied = "权限不足";
});