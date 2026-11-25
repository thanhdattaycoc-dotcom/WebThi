
using System;
using System.Text.RegularExpressions;

namespace WebsiteThiTracNghiem2
{
    public partial class DangKy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            lblThongBao.Text = "";
            lblThongBao.CssClass = "server-message";

            // 1. Lay du lieu tu giao dien
            string hoTen = txtHoTen.Text.Trim();
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;
            string nhapLaiMatKhau = txtNhapLaiMatKhau.Text;
            string email = txtEmail.Text.Trim();
            string lop = txtLop.Text.Trim();
            string queQuan = ddlQueQuan.SelectedValue;
            string soThich = txtSoThich.Text.Trim();
            string tienAn = txtTienAn.Text.Trim();
            string maSinhVien = txtMaSinhVien.Text.Trim();


            // 2. Kiem tra thong tin bat buoc
            if (string.IsNullOrWhiteSpace(hoTen) ||
                string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(matKhau) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(queQuan) ||
                string.IsNullOrWhiteSpace(soThich))
            {
                HienThongBao("Vui lòng nhập đầy đủ thông tin bắt buộc.", false);
                return;
            }

            // 3. Kiem tra email
            string mauEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, mauEmail))
            {
                HienThongBao("Email không đúng định dạng.", false);
                return;
            }

            // 4. Kiem tra mat khau
            if (matKhau != nhapLaiMatKhau)
            {
                HienThongBao("Mật khẩu nhập lại không khớp.", false);
                return;
            }
            // kiem tra lop
            if (lop.Length < 5 || lop.Length > 10)
            {
                HienThongBao("Lớp phải từ 5 đến 10 ký tự.", false);
                return;
            }
            if (!Regex.IsMatch(lop, @"^\d{2}[a-zA-Z0-9]{3,8}$"))
            {
                HienThongBao("Lớp chỉ được hai chữ cái đầu tiên là sô.", false);
                return;
            }

            // 5. Kiem tra so thich
            if (!Regex.IsMatch(soThich, @"^[\p{L} ]{5,20}$"))
            {
                HienThongBao(
                    "Sở thích chỉ được chứa chữ và dấu cách.",
                    false
                );
                return;
            }

            // 6. Kiem tra dieu khoan
            if (!chkDongYDieuKhoan.Checked)
            {
                HienThongBao("Bạn cần đồng ý với điều khoản sử dụng.", false);
                return;
            }
            //
            XuLyTaiKhoan.ThongTinTaiKhoan taiKhoan =
             new XuLyTaiKhoan.ThongTinTaiKhoan
             {
              HoTen = hoTen,
              TenDangNhap = tenDangNhap,
              Email = email,
              Lop = lop,
              QueQuan = queQuan,
              SoThich = soThich,
              TienAn = tienAn,
              MaSinhVien = maSinhVien,
              DongYDieuKhoan = true
              
             };

            // 8. Goi lop XuLyTaiKhoan de dang ky
            try
            {
                string thongBao;

                bool thanhCong = XuLyTaiKhoan.DangKy(
                    taiKhoan,
                    matKhau,
                    out thongBao
                );

                if (thanhCong)
                {
                    HienThongBao(thongBao, true);

                    // Chuyen den trang dang nhap sau khi hien thong bao
                    Response.AddHeader(
                        "Refresh",
                        "2;url=DangNhap.aspx?dangky=thanhcong"
                    );
                }
                else
                {
                    HienThongBao(thongBao, false);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);

                HienThongBao(
                    "Có lỗi xảy ra khi đăng ký. Vui lòng thử lại.",
                    false
                );
            }
        }

        private void HienThongBao(string noiDung, bool thanhCong)
        {
            lblThongBao.Text = Server.HtmlEncode(noiDung);

            lblThongBao.CssClass = thanhCong
                ? "server-message success"
                : "server-message error";
        }
    }
}
