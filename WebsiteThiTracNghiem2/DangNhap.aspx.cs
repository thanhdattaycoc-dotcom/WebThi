
using System;
using System.IO;
using System.Web;

namespace WebsiteThiTracNghiem2
{
    public partial class DangNhap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Neu da dang nhap thi khong can dang nhap lai
            if (!IsPostBack &&
                !string.IsNullOrWhiteSpace(
                    Session["TenDangNhap"] as string))
            {
                Response.Redirect("~/TrangChu.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }

            // Hien thi thong bao khi dang ky thanh cong
            if (!IsPostBack &&
                Request.QueryString["dangky"] == "thanhcong")
            {
                lblThongBao.Text =
                    "Đăng ký thành công! Vui lòng đăng nhập.";

                lblThongBao.CssClass =
                    "login-message success";
            }
        }

        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            lblThongBao.Text = "";
            lblThongBao.CssClass = "login-message";

            // 1. Lay thong tin tu giao dien
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            // 2. Kiem tra du lieu rong
            if (string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(matKhau))
            {
                HienThongBao(
                    "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.",
                    false
                );
                return;
            }

            try
            {
                // 3. Lay duong dan file TaiKhoan.txt
                string duongDan = Server.MapPath(
                    "~/App-Data/TaiKhoan.txt"
                );

                // 4. Kiem tra file co ton tai khong
                if (!File.Exists(duongDan))
                {
                    HienThongBao(
                        "Chưa có dữ liệu tài khoản. Vui lòng đăng ký trước.",
                        false
                    );
                    return;
                }

                // 5. Doc danh sach tai khoan
                string[] danhSach = File.ReadAllLines(duongDan);

                bool timThay = false;

                foreach (string dong in danhSach)
                {
                    if (string.IsNullOrWhiteSpace(dong))
                        continue;

                    // Tach du lieu thanh 9 cot
                    string[] cot = dong.Split('|');

                    if (cot.Length <= 8)
                        continue;

                    string tenCu = cot[1];
                    string matKhauCu = cot[2];
                    string quyen = cot[10];

                    // 6. Kiem tra ten dang nhap va mat khau
                    if (string.Equals(
                        tenDangNhap,
                        tenCu,
                        StringComparison.OrdinalIgnoreCase)
                        && matKhau == matKhauCu)
                    {
                        timThay = true;

                        // 7. Luu thong tin vao Session
                        Session["TenDangNhap"] = tenCu;
                        Session["VaiTro"] = quyen;

                        // 8. Chuyen ve trang chu
                        Response.Redirect(
                            "~/TrangChu.aspx",
                            false
                        );

                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }
                }

                // 9. Neu khong tim thay tai khoan
                if (!timThay)
                {
                    HienThongBao(
                        "Tên đăng nhập hoặc mật khẩu không chính xác.",
                        false
                    );
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);

                HienThongBao(
                    "Có lỗi xảy ra khi đăng nhập. Vui lòng thử lại.",
                    false
                );
            }
        }

        private void HienThongBao(string noiDung, bool thanhCong)
        {
            lblThongBao.Text = Server.HtmlEncode(noiDung);

            lblThongBao.CssClass = thanhCong
                ? "login-message success"
                : "login-message error";
        }
    }
}
