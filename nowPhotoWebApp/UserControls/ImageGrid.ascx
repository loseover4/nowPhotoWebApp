<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageGrid.ascx.cs" Inherits="nowPhotoWebApp.UserControls.ImageGrid" %>
<asp:ListView runat="server" ID="ImageGridListView" GroupPlaceholderID="groupPlaceHolder" 
    ItemPlaceholderID="itemPlaceHolder">
    <LayoutTemplate>
        <div id="groupPlaceHolder"></div>
    </LayoutTemplate>
    <GroupTemplate>
        <span>
            <div id="itemPlaceHolder"></div>
        </span>
    </GroupTemplate>
    <ItemTemplate>
        <asp:ImageButton runat="server" CommandArgument="<%# Container.DataItem %>" 
          ImageUrl="<%# Container.DataItem %>" Width="320" Height="240" />
    </ItemTemplate>
</asp:ListView>