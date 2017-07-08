<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="Default.aspx.cs" Inherits="Prueba2.Default" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <title></title>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <br />
    <br />
    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="control-label col-md-2">
                Torneos:
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlTorneo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlTorneo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
            
        </div>
        <br />
        <hr />
        <br />
        <div class="row">
            <asp:GridView ID="grdPartidos" runat="server" CssClass="table table-bordered bs-table" 
                allowpaging="true" OnPageIndexChanging="grdPartidos_PageIndexChanging"
                DataKeyNames="Id_Match" AutoGenerateSelectButton="true" OnSelectedIndexChanged="grdPartidos_SelectedIndexChanged" >
                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                <EmptyDataRowStyle forecolor="Red" CssClass="table table-bordered" />
                <emptydatatemplate>
                    ¡No hay Partidos para el filtro seleccionado!  
                </emptydatatemplate>
                
            </asp:GridView>
        </div>
        <br />
        <hr />
        <br />
        <div class="row">
            <asp:GridView ID="grdEventos" runat="server" CssClass="table table-bordered bs-table" allowpaging="true" 
                OnPageIndexChanging="grdEventos_PageIndexChanging">
                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                <EmptyDataRowStyle forecolor="Red" CssClass="table table-bordered" />
                <emptydatatemplate>
                    ¡No hay Partidos para el filtro seleccionado!  
                </emptydatatemplate>
            </asp:GridView>
        </div>
        
    </div>
</asp:Content>