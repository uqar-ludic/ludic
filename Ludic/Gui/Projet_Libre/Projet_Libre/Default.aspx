<%@ Page Title="Default" Language="C#" MasterPageFile="~/SandBox.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Projet_Libre.Default" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="Content/Default.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftContent" runat="server">
    <div>
        <ul class="nav nav-list"> 
            <li><a href="#"><i class="glyphicon glyphicon-home"></i> Listes des exercices</a></li>
            <li><a href="Edition.aspx"><i class="glyphicon glyphicon-edit"></i> Editer une solution</a></li>
            <li><a href="#"><i class="glyphicon glyphicon-comment"></i> Commenter une solution</a></li>
            <li><a href="#"><i class="glyphicon glyphicon-star-empty"></i> Gallerie des trophés</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
</asp:Content>
