
<%@ Page Title="Đăng ký tài khoản" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="DangKy.aspx.cs"
    Inherits="WebsiteThiTracNghiem2.DangKy" %>

<asp:Content ID="ContentDangKy" ContentPlaceHolderID="MainContent" runat="server">

    <link href="css/DangKy.css" rel="stylesheet" />

    <div class="dangky-container">
        <div class="dangky-card">

            <div class="dangky-header">
                <h1>ĐĂNG KÝ TÀI KHOẢN</h1>
                <p>Tạo tài khoản để tham gia các bài thi trắc nghiệm</p>
            </div>

            <div class="dangky-form">

                <div class="form-group">
                    <label>Họ và tên *</label>
                    <asp:TextBox ID="txtHoTen" runat="server"
                        ClientIDMode="Static"
                        CssClass="form-control"
                        placeholder="Nhập họ và tên" />
                </div>

                <div class="form-group">
                    <label>Tên đăng nhập *</label>
                    <asp:TextBox ID="txtTenDangNhap" runat="server"
                        ClientIDMode="Static"
                        CssClass="form-control"
                        placeholder="Nhập tên đăng nhập" />
                </div>

                <div class="form-group">
                    <label>Mật khẩu *</label>
                    <div class="password-box">
                        <asp:TextBox ID="txtMatKhau" runat="server"
                            ClientIDMode="Static"
                            TextMode="Password"
                            CssClass="form-control"
                            placeholder="Nhập mật khẩu" />
                        <button type="button" class="toggle-password"
                            onclick="doiHienThiMatKhau('txtMatKhau')">
                            Hiện
                        </button>
                    </div>
                </div>

                <div class="form-group">
                    <label>Nhập lại mật khẩu *</label>
                    <div class="password-box">
                        <asp:TextBox ID="txtNhapLaiMatKhau" runat="server"
                            ClientIDMode="Static"
                            TextMode="Password"
                            CssClass="form-control"
                            placeholder="Nhập lại mật khẩu" />
                        <button type="button" class="toggle-password"
                            onclick="doiHienThiMatKhau('txtNhapLaiMatKhau')">
                            Hiện
                        </button>
                    </div>
                </div>

                <div class="form-group">
                    <label>Email *</label>
                    <asp:TextBox ID="txtEmail" runat="server"
                        ClientIDMode="Static"
                        TextMode="Email"
                        CssClass="form-control"
                        placeholder="example@gmail.com" />
                </div>

                <div class="form-group">
                    <label>Lớp</label>
                    <asp:TextBox ID="txtLop" runat="server"
                        ClientIDMode="Static"
                        CssClass="form-control"
                        placeholder="Nhập lớp của bạn" />
                </div>
                 <div class="form-group">
                    <label>Tiền ăn</label>
                     <asp:TextBox ID="txtTienAn" runat="server"
                      ClientIDMode="Static"
                       CssClass="form-control"
TextMode="Number"
                          placeholder="Nhập tiền của bạn (VND)." />
                         </div>
                 <div class="form-group">
    <label>Mã Sinh Viên</label>
     <asp:TextBox ID="txtMaSinhVien" runat="server"
      ClientIDMode="Static"
       CssClass="form-control"
      
          placeholder="Nhập mã sinh viên của bạn." />
         </div>


                <div class="form-group">
                    <label>Quê quán *</label>
                    <asp:DropDownList ID="ddlQueQuan" runat="server"
                        ClientIDMode="Static"
                        CssClass="form-control">
                        <asp:ListItem Text="-- Chọn quê quán --" Value="" />
                        <asp:ListItem Text="Hà Nội" Value="Hà Nội" />
                        <asp:ListItem Text="Hồ Chí Minh" Value="Hồ Chí Minh" />
                        <asp:ListItem Text="Đà Nẵng" Value="Đà Nẵng" />
                        <asp:ListItem Text="Hải Phòng" Value="Hải Phòng" />
                        <asp:ListItem Text="Cần Thơ" Value="Cần Thơ" />
                        <asp:ListItem Text="Khác" Value="Khác" />
                    </asp:DropDownList>
                </div>
   
                <div class="form-group">
                    <label>Sở thích *</label>
                    <asp:TextBox ID="txtSoThich" runat="server"
                        ClientIDMode="Static"
                        CssClass="form-control"
                        placeholder="Nhập sở thích (5 - 20 ký tự)" />
                    <small>Chỉ nhập chữ và dấu cách.</small>
                </div>

                <div class="form-check">
                    <asp:CheckBox ID="chkDongYDieuKhoan" runat="server"
                        ClientIDMode="Static" />
                    <label for="chkDongYDieuKhoan">
                        Tôi đồng ý với các điều khoản sử dụng website.
                    </label>
                </div>

                <div id="loiDangKyClient" class="client-error"></div>

                <asp:Label ID="lblThongBao" runat="server"
                    ClientIDMode="Static"
                    CssClass="server-message" />

                <asp:Button ID="btnDangKy" runat="server"
                    Text="ĐĂNG KÝ TÀI KHOẢN"
                    CssClass="btn-dangky"
                  

                    

                    OnClientClick="return kiemTraTenDangNhap() && kiemTraDangKy();" />




                <div class="dangky-footer">
                    Đã có tài khoản?
                    <a href="DangNhap.aspx">Đăng nhập ngay</a>
                </div>

            </div>
        </div>
    </div>

    <script src="JS/DangKy.js"></script>

</asp:Content>
