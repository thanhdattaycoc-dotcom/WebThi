
<%@ Page Title="Đăng nhập" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="DangNhap.aspx.cs"
    Inherits="WebsiteThiTracNghiem2.DangNhap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Đăng nhập
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ResolveUrl("~/css/DangNhap.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="login-wrapper">
        <div class="login-card">

            <div class="login-icon">Q</div>

            <h2>ĐĂNG NHẬP</h2>
            <p class="login-description">
                Chào mừng bạn đến với Quiz Online
            </p>

            <div class="login-group">
                <label for="txtTenDangNhap">Tên đăng nhập</label>

                <asp:TextBox ID="txtTenDangNhap" runat="server"
                    ClientIDMode="Static"
                    CssClass="login-input"
                    placeholder="Nhập tên đăng nhập"
                    MaxLength="50">
                </asp:TextBox>
            </div>

            <div class="login-group">
                <label for="txtMatKhau">Mật khẩu</label>

                <asp:TextBox ID="txtMatKhau" runat="server"
                    ClientIDMode="Static"
                    CssClass="login-input"
                    TextMode="Password"
                    placeholder="Nhập mật khẩu"
                    MaxLength="100">
                </asp:TextBox>
            </div>

            <asp:Label ID="lblThongBao" runat="server"
                CssClass="login-message">
            </asp:Label>

            <asp:Button ID="btnDangNhap" runat="server"
                Text="ĐĂNG NHẬP"
                CssClass="login-button"
                OnClick="btnDangNhap_Click"
                OnClientClick="return kiemTraDangNhap();" />

            <div class="login-register">
                Chưa có tài khoản?
                <a href="DangKy.aspx">Đăng ký ngay</a>
            </div>

            <div class="login-home">
                <a href="TrangChu.aspx">← Quay lại trang chủ</a>
            </div>

        </div>
    </div>

    <script src="<%= ResolveUrl("~/JS/DangNhap.js") %>"></script>

</asp:Content>
