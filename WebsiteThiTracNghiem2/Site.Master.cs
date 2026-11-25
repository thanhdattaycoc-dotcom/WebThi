

using System;
using System.IO;

namespace WebsiteThiTracNghiem
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HienThiMenu();
        }

        // Đọc quyền tài khoản trực tiếp từ TaiKhoan.txt
        private string LayVaiTroTuFile(string tenDangNhap)
        {
            try
            {
                string duongDan = Server.MapPath(
                    "~/App-Data/TaiKhoan.txt"
                );

                if (!File.Exists(duongDan))
                {
                    return "";
                }

                string[] danhSachDong = File.ReadAllLines(duongDan);

                foreach (string dong in danhSachDong)
                {
                    if (string.IsNullOrWhiteSpace(dong))
                        continue;

                    string[] parts = dong.Split('|');

                    // TaiKhoan.txt phải có đủ 9 trường
                    if (parts.Length != 9)
                        continue;

                    string tenTaiKhoan = parts[1].Trim();

                    if (string.Equals(
                        tenTaiKhoan,
                        tenDangNhap,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        return parts[8].Trim();
                    }
                }
            }
            catch
            {
                return "";
            }

            return "";
        }

        // Hiển thị menu theo quyền tài khoản
        private void HienThiMenu()
        {
            string tenDangNhap =
                Session["TenDangNhap"] as string;

            if (tenDangNhap != null)
                tenDangNhap = tenDangNhap.Trim();

            bool daDangNhap =
                !string.IsNullOrWhiteSpace(tenDangNhap);

            string vaiTro = "";

            if (daDangNhap)
            {
                // Đọc quyền thật từ file TXT
                vaiTro = LayVaiTroTuFile(tenDangNhap);

                // Cập nhật lại Session
                Session["VaiTro"] = vaiTro;
            }

            bool laAdmin =
                daDangNhap &&
                string.Equals(
                    vaiTro,
                    "Admin",
                    StringComparison.OrdinalIgnoreCase
                );

            // Hiển thị nút đăng nhập, đăng ký
            phKhach.Visible = !daDangNhap;

            // Hiển thị thông tin tài khoản
            phNguoiDung.Visible = daDangNhap;

            // Menu dành cho người dùng
            phMenuUser.Visible = daDangNhap;

            // Chỉ Admin mới thấy menu quản trị
            phMenuAdmin.Visible = laAdmin;

            // Hiển thị tên tài khoản
            if (daDangNhap)
            {
                lblTenNguoiDung.Text =
                    Server.HtmlEncode(tenDangNhap);
            }
        }

        // Xử lý đăng xuất
        protected void btnDangXuat_Click(
            object sender,
            EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect(
                "~/TrangChu.aspx",
                false
            );

            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
