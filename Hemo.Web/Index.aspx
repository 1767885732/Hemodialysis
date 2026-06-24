<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Hemo.Web.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>电子病历调阅</title>
    <script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="Styles/zTreeStyle/zTreeStyle.css" type="text/css" />
    <link href="Styles/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery.ztree.core-3.5.js"></script>
    <script src="Scripts/pdfobject.js" type="text/javascript"></script>
    <script type="text/javascript">

        var zTree;

        var setting = {
            view: {
                dblClickExpand: false,
                showLine: true,
                selectedMulti: false
            },
            data: {
                simpleData: {
                    enable: true,
                    idKey: "id",
                    pIdKey: "pId",
                    rootPId: ""
                }
            },
            callback: {
                beforeClick: function (treeId, treeNode) {
                    var zTree = $.fn.zTree.getZTreeObj("tree");
                    if (treeNode.isParent) {
                        zTree.expandNode(treeNode);
                        return false;
                    } else {
                       //SetUrl(treeNode.file);
                       embedPDF(treeNode.file);
                        return true;
                    }
                }
            }
        };

        var zNodes =
         [
		<%=nodeData %>
        ];


    </script>
    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
        }
        #container
        {
            position: relative;
            margin-top: 15px;
            width: 100%;
        }
        #menu
        {
            position: absolute;
            top: 0;
            left: 0;
            width: 200px;
        }
        #content
        {
            margin-left: 215px;
        }
    </style>
</head>
<body style="width: 100%; height: 100%;">
    <div id="container">
        <div id="menu">
            <table>
                <tr>
                    <td>
                        <div style="height: 120px; font-size: large; text-align: center;">
                            <%
                                if (patientTable.Rows.Count > 0)
                                {
                                    var rowP = patientTable.Rows[0];
                            %>
                            <table>
                                <tr>
                                    <td colspan="2">
                                        基本信息
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        编号:
                                    </td>
                                    <td>
                                        <%=rowP["PATIENTID"]%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        姓 名:
                                    </td>
                                    <td>
                                        <%=rowP["PATIENTNAME"]%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        性 别:
                                    </td>
                                    <td>
                                        <%=rowP["PATIENTSEX"]%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        年 龄:
                                    </td>
                                    <td>
                                        <%=rowP["AGE"]%>
                                    </td>
                                </tr>
                            </table>
                            <%
                                    }       
                            %></div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        病历记录
                    </td>
                </tr>
                <tr>
                    <td>
                        <ul id="tree" class="ztree">
                        </ul>
                    </td>
                </tr>
            </table>
        </div>
        <div id="content">
        </div>
    </div>
    <script type="text/javascript">

        function embedPDF(url) {
            var myPDF = new PDFObject({

                url: url,
                pdfOpenParams: { scrollbars: '1', toolbar: '0', statusbar: '0', messages: '0', navpanes: '0' }

            }).embed('content');
            // Be sure your document contains an element with the ID 'ffff' 

        }
        function SetSize() {
            var he = $(document).height() - 20;
            $("#content").height(he);
            $("#menu").height(he);

        }
        //
        $(document).ready(function () {

            var t = $("#tree");
            t = $.fn.zTree.init(t, setting, zNodes);
            var zTree = $.fn.zTree.getZTreeObj("tree");
            zTree.selectNode(zTree.getNodeByParam("id", 101));
            var urlEmr = '<%=DefaultEmrFile%>';

            if (urlEmr != "") {
                embedPDF(urlEmr);
            }
            SetSize();
            $(window).resize(function () {
                SetSize();
            });
        });
    </script>
</body>
</html>
