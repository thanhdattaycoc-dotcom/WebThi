
<%@ Page Title="Thông tin tài khoản" Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="TaiKhoan.aspx.cs"
    Inherits="WebsiteThiTracNghiem2.TaiKhoan" %>

<asp:Content ID="ContentTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Thông tin tài khoản
</asp:Content>

<asp:Content ID="ContentHead" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ResolveUrl("~/CSS/TaiKhoan.css") %>" rel="stylesheet" />
    <script src="<%= ResolveUrl("~/JS/TaiKhoan.js") %>"></script>
</asp:Content>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">

    <div class="tai-khoan-container">

        <div class="tai-khoan-header">
            <h2>Thông tin tài khoản</h2>
            <p>Quản lý và cập nhật thông tin cá nhân của bạn.</p>
        </div>

        <div class="tai-khoan-card">

            <div class="tai-khoan-avatar">
                <span>👤</span>
                <h3>Hồ sơ cá nhân</h3>
                <asp:Label ID="lblTenDangNhap" runat="server"
                    CssClass="ten-dang-nhap"></asp:Label>
            </div>

            <div class="tai-khoan-form">

                <div class="form-group">
                    <label>Họ và tên</label>
                    <asp:TextBox ID="txtHoTen" runat="server"
                        CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Tên đăng nhập</label>
                    <asp:TextBox ID="txtTenDangNhap" runat="server"
                        CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail" runat="server"
                        CssClass="form-control" TextMode="Email"
                        MaxLength="100"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Lớp</label>
                    <asp:TextBox ID="txtLop" runat="server"
                        CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Quê quán</label>
                    <asp:TextBox ID="txtQueQuan" runat="server"
                        CssClass="form-control" MaxLength="100"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label>Sở thích</label>
                    <asp:TextBox ID="txtSoThich" runat="server"
                        CssClass="form-control" TextMode="MultiLine"
                        Rows="3" MaxLength="300"></asp:TextBox>
                </div>

                <div class="thong-bao">
                    <asp:Label ID="lblThongBao" runat="server"></asp:Label>
                </div>

                <div class="form-buttons">
                    <asp:Button ID="btnCapNhat" runat="server"
                        Text="Lưu thay đổi"
                        CssClass="btn-cap-nhat"
                        OnClientClick="return kiemTraTaiKhoan();"
                        OnClick="btnCapNhat_Click" />

                    <asp:HyperLink ID="lnkTrangChu" runat="server"
                        NavigateUrl="~/TrangChu.aspx"
                        CssClass="btn-quay-lai">
                        Quay lại
                    </asp:HyperLink>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
