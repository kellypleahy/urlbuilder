<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="MvcTestApp.Controllers" %>
<%@ Import Namespace="UrlBuilder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ViewPage1</title>
</head>
<body>
    <div>
    <%= Url.For<ViewPage1Controller>(c => c.Action1(Model.GetHashCode())) %>
    </div>
</body>
</html>
