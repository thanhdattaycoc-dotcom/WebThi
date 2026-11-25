
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebsiteThiTracNghiem2
{
    public partial class QuanLyTaiKhoan : Page
    {
        private string DuongDanFile
        {
            get { return Server.MapPath("~/App-Data/TaiKhoan.txt"); }
        }

        public class TaiKhoan
        {
            public string HoTen { get; set; }
            public string TenDangNhap { get; set; }
            public string Email { get; set; }
            public string Lop { get; set; }
            public string TienAn { get; set; }
            public string MaSinhVien { get; set; }
            public string Quyen { get; set; }

            // Lưu nguyên 9 trường để không làm mất dữ liệu tài khoản.
            public string DongDuLieu { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("~/DangNhap.aspx");
                return;
            }

            if (Session["VaiTro"] == null ||
                Session["VaiTro"].ToString() != "Admin")
            {
                Response.Redirect("~/TrangChu.aspx");
                return;
            }

            if (!IsPostBack)
            {
                NapDanhSach("");
            }
        }

        private List<TaiKhoan> DocTaiKhoan()
        {
            List<TaiKhoan> ds = new List<TaiKhoan>();

            if (!File.Exists(DuongDanFile))
                return ds;

            foreach (string dong in File.ReadAllLines(DuongDanFile))
            {
                string[] p = dong.Split('|');
                if (p.Length < 9 || p.Length > 12) continue;

                ds.Add(new TaiKhoan
                {
                    HoTen = p[0],
                    TenDangNhap = p[1],
                    Email = p[3],
                    Lop = p[4],
                    Quyen = p[p.Length - 1],
                    DongDuLieu = dong,
                    TienAn = p.Length >= 8 ? p[7] : "", // sỡ dư liệu cũ có thể không có trường TienAn nếu ko thì ko hiện gì
                    MaSinhVien = p.Length >= 10 ? p[8] : "", // sỡ dư liệu cũ có thể không có trường MaSinhVien nếu ko thì ko hiện gì
                });
            }

            return ds;
        }

        private void GhiTaiKhoan(List<TaiKhoan> ds)
        {
            string thuMuc = Path.GetDirectoryName(DuongDanFile);

            if (!Directory.Exists(thuMuc))
                Directory.CreateDirectory(thuMuc);

            File.WriteAllLines(
                DuongDanFile,
                ds.Select(t => t.DongDuLieu).ToArray()
            );
        }

        private void NapDanhSach(string tuKhoa)
        {
            List<TaiKhoan> ds = DocTaiKhoan();

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                tuKhoa = tuKhoa.Trim();

                ds = ds.Where(t =>
                    t.HoTen.IndexOf(tuKhoa,
                        StringComparison.OrdinalIgnoreCase) >= 0 ||
                    t.TenDangNhap.IndexOf(tuKhoa,
                        StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
            }

            gvTaiKhoan.DataSource = ds;
            gvTaiKhoan.DataBind();
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            NapDanhSach(txtTimKiem.Text);
            lblThongBao.Text = "Đã thực hiện tìm kiếm.";
        }

        protected void btnTatCa_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            NapDanhSach("");
            lblThongBao.Text = "Đang hiển thị tất cả tài khoản.";
        }

        protected void gvTaiKhoan_RowCommand(
            object sender, GridViewCommandEventArgs e)
        {
            string tenDangNhap = Convert.ToString(e.CommandArgument);

            string taiKhoanDangDangNhap =
                Convert.ToString(Session["TenDangNhap"]);

            List<TaiKhoan> ds = DocTaiKhoan();

            TaiKhoan tk = ds.FirstOrDefault(t =>
                t.TenDangNhap == tenDangNhap);

            if (tk == null)
            {
                lblThongBao.Text = "Không tìm thấy tài khoản.";
                return;
            }

            if (e.CommandName == "DoiQuyen")
            {
                if (tenDangNhap == taiKhoanDangDangNhap)
                {
                    lblThongBao.Text =
                        "Không thể tự thay đổi quyền của tài khoản đang đăng nhập.";
                    return;
                }

                GridViewRow row =
                    (GridViewRow)((Control)e.CommandSource).NamingContainer;

                DropDownList ddl =
                    row.FindControl("ddlQuyen") as DropDownList;

                if (ddl == null)
                    return;

                string quyenMoi = ddl.SelectedValue;

                if (quyenMoi != "User" && quyenMoi != "Admin")
                {
                    lblThongBao.Text = "Quyền tài khoản không hợp lệ.";
                    return;
                }

                string[] p = tk.DongDuLieu.Split('|');

                if (p.Length <9)
                    
                return;
                p[p.Length - 1] = quyenMoi;
                tk.Quyen = quyenMoi;
                tk.DongDuLieu = string.Join("|", p);

                GhiTaiKhoan(ds);

                lblThongBao.Text = "Đã cập nhật quyền tài khoản.";
            }
            else if (e.CommandName == "XoaTaiKhoan")
            {
                if (tenDangNhap == taiKhoanDangDangNhap)
                {
                    lblThongBao.Text =
                        "Không thể xóa tài khoản đang đăng nhập.";
                    return;
                }

                // Không cho xóa Admin cuối cùng.
                int soAdmin = ds.Count(t => t.Quyen == "Admin");

                if (tk.Quyen == "Admin" && soAdmin <= 1)
                {
                    lblThongBao.Text =
                        "Không thể xóa tài khoản Admin cuối cùng.";
                    return;
                }

                ds.RemoveAll(t => t.TenDangNhap == tenDangNhap);

                GhiTaiKhoan(ds);

                lblThongBao.Text = "Đã xóa tài khoản thành công.";
            }

            NapDanhSach(txtTimKiem.Text);
        }
    }
}
