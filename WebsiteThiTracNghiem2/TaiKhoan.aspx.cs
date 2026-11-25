
using System;

namespace WebsiteThiTracNghiem2
{
    public partial class TaiKhoan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiem tra dang nhap
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("~/DangNhap.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                HienThiThongTin();
            }
        }

        // Hien thi thong tin tai khoan
        private void HienThiThongTin()
        {
            string tenDangNhap = Session["TenDangNhap"].ToString();

            XuLyTaiKhoan.ThongTinTaiKhoan taiKhoan =
                XuLyTaiKhoan.LayThongTin(tenDangNhap);

            if (taiKhoan == null)
            {
                lblThongBao.Text = "Không tìm thấy thông tin tài khoản.";
                btnCapNhat.Enabled = false;
                return;
            }

            lblTenDangNhap.Text = Server.HtmlEncode(taiKhoan.TenDangNhap);

            txtTenDangNhap.Text = taiKhoan.TenDangNhap;
            txtHoTen.Text = taiKhoan.HoTen;
            txtEmail.Text = taiKhoan.Email;
            txtLop.Text = taiKhoan.Lop;
            txtQueQuan.Text = taiKhoan.QueQuan;
            txtSoThich.Text = taiKhoan.SoThich;
        }

        // Xu ly nut cap nhat
        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            string tenDangNhap = Session["TenDangNhap"].ToString();

            XuLyTaiKhoan.ThongTinTaiKhoan taiKhoan =
                new XuLyTaiKhoan.ThongTinTaiKhoan();

            taiKhoan.TenDangNhap = tenDangNhap;
            taiKhoan.HoTen = txtHoTen.Text.Trim();
            taiKhoan.Email = txtEmail.Text.Trim();
            taiKhoan.Lop = txtLop.Text.Trim();
            taiKhoan.QueQuan = txtQueQuan.Text.Trim();
            taiKhoan.SoThich = txtSoThich.Text.Trim();

            string thongBao;

            bool ketQua = XuLyTaiKhoan.CapNhatThongTin(
                taiKhoan,
                out thongBao
            );

            lblThongBao.Text = Server.HtmlEncode(thongBao);

            if (ketQua)
            {
                lblThongBao.CssClass = "thong-bao-thanh-cong";
                HienThiThongTin();
            }
            else
            {
                lblThongBao.CssClass = "thong-bao-loi";
            }
        }
    }
}
