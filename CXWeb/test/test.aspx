



<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<link rel="stylesheet" href="http://www.jq22.com/jquery/font-awesome.4.6.0.css"/>
<link href="resources/css/foundation-datepicker.css" rel="stylesheet" type="text/css"/>
<title>设备请修单明细</title>
</head>

<body>
<div style="position: fixed; left: 0px; top: 0px; width:100%; height: 30px;background:#22324C;z-index:3;">
<font style="font-size: 15px; font-weight: bold; color:#C8DAF2;position: absolute; top: 12px;left:5px;">设备请修单明细</font>
</div>
<div style="width: 100%; margin: auto; height: 60px; position: fixed; left: 0px; top: 30px;z-index:3;background: #22324C; 
color:#C8DAF2;font-size:13px;">
<form id="select" style="width:600px;position:relative; left:5px;letter-spacing:1px;">
起始日期:<input id="time1" name="time1" type="text" value="2018/03/10" />&nbsp;&nbsp;&nbsp;
终止日期:<input id="time2" name="time2" type="text" value="2018/04/10" /><br />
<script src="resources/js/jquery-1.11.3.min.js"></script>
      <script src="/CXHOME/resources/js/foundation-datepicker.js"></script>
      <script src="/CXHOME/resources/js/foundation-datepicker.zh-CN.js"></script>
      <script type="text/javascript">   
$('#time1').fdatepicker({
	format: 'yyyy/mm/dd',
});
$('#time2').fdatepicker({
	format: 'yyyy/mm/dd',
});

</script>
<button type="button" style="position: absolute; left: 20px; top: 30px;font-size:13px;width:50px;height:20px;" onclick="showlist()">查询</button>
<button type="button" style="position: absolute; left: 100px; top: 30px;font-size:13px;width:90px;height:20px;" onclick="getExcel('t1')">导出Excel</button>
</form>
<script type="text/javascript">
function showlist(){
	var beginDate=$("#time1").val();  
  	 var endDate=$("#time2").val();
  	if(beginDate == "")  
  	 {  
  	  alert("起始日期不能为空！");
  	  return false;
  	 }else if (endDate<beginDate){
  		alert("起始日期不能大于终止日期！");
  		return false;
  	 }
	$('#loading').text('正在加载数据，请稍等...').show();
	$.ajax({
		beforeSend: function () {
			
		},
			complete: function () {
				$('#loading').hide();
			},
		type:"POST",
		url:"/CXHOME/Equipment_divisionlist",
		dataType:"html",
		data:$('#select').serialize(),
		success:function(data){
$('#loading').hide();
			$("#Equipment_division").html(data);
		},
		error:function(XmlHttpRequest, textStatus, errorThrown){
$('#loading').hide();
			alert("error:"+XmlHttpRequest.status);
		}
	});
}

function getExcel(tableid)  
{  

    var curTbl = document.getElementById(tableid);  
    try{  
        oXL = new ActiveXObject("Excel.Application"); //创建AX对象excel  
    }catch(e){  
        alert("无法启动Excel!\n\n如果您确信您的电脑中已经安装了Excel，"+"那么请调整IE的安全级别。\n\n具体操作：\n\n"+"工具 → Internet选项 → 安全 → 自定义级别 → 对没有标记为安全的ActiveX进行初始化和脚本运行 → 启用");  
        return false;  
    }     
    var oWB = oXL.Workbooks.Add();  
    var oSheet = oWB.ActiveSheet;  
    var Lenr = curTbl.rows.length;  
    for (i = 0; i < Lenr; i++)  
    {        var Lenc = curTbl.rows(i).cells.length;  
        for (j = 0; j < Lenc; j++)  
        {  
            oSheet.Cells(i + 1, j + 1).value = curTbl.rows(i).cells(j).innerText;  

        }  

    }  
    oXL.Visible = true;  
}

</script>
<div id="loading" style="width: 220px; height: 46px; position:fixed; left: 500px; top: 0px;font-size:15px; color: #FFF;z-index:3;float:right;"></div>
</div>

<div id="Equipment_division" style="position: absolute; width: auto; height: auto; left: 0px; top: 90px;"></div>

</body>
</html>