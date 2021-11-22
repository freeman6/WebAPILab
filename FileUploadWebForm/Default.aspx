<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FileUploadWebForm._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Download Lab</h2>
            <asp:Button id="btnDownLoad" class="btn btn-default" Text="後端下載" runat="server" OnClick="btnDownLoad_Click" />
            <a type="button" class="btn btn-default" href="https://localhost:44319/Files/lab.xls" download="lab.xls" target="_blank">以 Tag 型式下載</a>
        </div>
        <div class="col-md-6">
            <h2>Upload Lab</h2>
            <asp:FileUpload runat="server" ID="FileUpload" AllowMultiple="true" />
            <asp:button runat="server" ID="btnSubmit" Text="上傳檔案" OnClick="btnSubmit_Click" />
        </div>
    </div>

</asp:Content>
