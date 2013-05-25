﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LojasCidadao.Models.Balcao>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Editar Balcão
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Editar Balcão</h2>

<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <fieldset>
        <legend>Editar Balcão</legend>

        <p>
            <label for="Estado">Estado (CheckBox selecionada para ativar balcão):</label>
            <%= Html.CheckBox("Estado", Model.isEstado()) %>
        </p>
        <p>
            <input type="submit" value="Guardar" />
        </p>
    </fieldset>
<% } %>

<div>
    <%: Html.ActionLink("Voltar atrás", "Balcoes", new { id = ViewData["Loja"] })%>
</div>

</asp:Content>
