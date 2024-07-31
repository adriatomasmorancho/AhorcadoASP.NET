<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Ahorcado.aspx.cs" Inherits="AhorcadoASP.NET.Ahorcado" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div>
            <asp:Image ID="Image" runat="server" />
             <div>
            <asp:Label ID="lblPalabra" runat="server" Font-Size="24px"></asp:Label>
            </div>
            <asp:Panel ID="PanelLetras" runat="server">
            </asp:Panel>  
            <div>        
            <asp:Label ID="LabelLetrasUtilizadas" runat="server" Text="Letras Utilizadas: "></asp:Label>
            </div>
            <div>
            <asp:Label ID="LabelIntentosRestantes" runat="server"></asp:Label>
            </div>
        </div>
</asp:Content>
