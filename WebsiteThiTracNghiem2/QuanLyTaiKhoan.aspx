
<%@ Page Title="Quản lý tài khoản" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuanLyTaiKhoan.aspx.cs" Inherits="WebsiteThiTracNghiem2.QuanLyTaiKhoan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Quản lý tài khoản
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .account-container {
            max-width: 1150px;
            margin: 30px auto;
            padding: 25px;
            background: white;
            border-radius: 12px;
            box-shadow: 0 3px 15px rgba(0,0,0,.08);
        }

        .account-container h2 { color: #244a9b; }

        .search-box {
            display: flex;
            gap: 10px;
            margin: 20px 0;
            flex-wrap: wrap;
        }

        .search-box input {
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 6px;
            min-width: 250px;
        }

        .btn {
            padding: 8px 13px;
            border: none;
            border-radius: 6px;
            color: white;
            cursor: pointer;
            margin: 3px;
        }

        .search { background: #0d6efd; }
        .reset { background: #6c757d; }
        .save { background: #198754; }
        .delete { background: #dc3545; }

        .account-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .account-table th, .account-table td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: center;
        }

        .account-table th {
            background: #244a9b;
            color: white;
        }

        .account-table select {
            padding: 7px;
            border-radius: 5px;
        }

        .message {
            display: block;
            margin: 15px 0;
            font-weight: bold;
        }

        .table-wrap { overflow-x: auto; }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="account-container">
        <h2>QUẢN LÝ TÀI KHOẢN</h2>

        <asp:Label ID="lblThongBao" runat="server"
            CssClass="message"></asp:Label>

        <div class="search-box">
            <asp:TextBox ID="txtTimKiem" runat="server"
                placeholder="Nhập họ tên hoặc tên đăng nhập..." />

            <asp:Button ID="btnTimKiem" runat="server"
                Text="Tìm kiếm" CssClass="btn search"
                OnClick="btnTimKiem_Click" />

            <asp:Button ID="btnTatCa" runat="server"
                Text="Hiển thị tất cả" CssClass="btn reset"
                OnClick="btnTatCa_Click" />
        </div>

        <div class="table-wrap">
            <asp:GridView ID="gvTaiKhoan" runat="server"
                AutoGenerateColumns="False"
                CssClass="account-table"
                DataKeyNames="TenDangNhap"
                OnRowCommand="gvTaiKhoan_RowCommand"
                EmptyDataText="Không tìm thấy tài khoản.">

                <Columns>
                    <asp:BoundField DataField="HoTen"
                        HeaderText="Họ tên" />

                    <asp:BoundField DataField="TenDangNhap"
                        HeaderText="Tên đăng nhập" />

                    <asp:BoundField DataField="Email"
                        HeaderText="Email" />

                    <asp:BoundField DataField="Lop"
                        HeaderText="Lớp" />
                    <asp:BoundField DataField="TienAn"
                        HeaderText="Tiền ăn" />
                     <asp:BoundField DataField="MaSinhVien"
                        HeaderText="Mã Sinh Viên" />


                    <asp:BoundField DataField="Quyen"
                        HeaderText="Quyền hiện tại" />

                    <asp:TemplateField HeaderText="Đổi quyền">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlQuyen" runat="server">
                                
                                <asp:ListItem Value="User">User</asp:ListItem>
                                <asp:ListItem Value="Admin">Admin</asp:ListItem>
                            </asp:DropDownList>

                            <asp:Button ID="btnLuuQuyen" runat="server"
                                Text="Lưu" CssClass="btn save"
                                CommandName="DoiQuyen"
                                CommandArgument='<%# Eval("TenDangNhap") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Xóa">
                        <ItemTemplate>
                            <asp:Button ID="btnXoa" runat="server"
                                Text="Xóa" CssClass="btn delete"
                                CommandName="XoaTaiKhoan"
                                CommandArgument='<%# Eval("TenDangNhap") %>'
                                OnClientClick="return confirm('Bạn có chắc muốn xóa tài khoản này?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
