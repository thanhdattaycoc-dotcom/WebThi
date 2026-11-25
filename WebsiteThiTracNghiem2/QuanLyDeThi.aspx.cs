
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebsiteThiTracNghiem2
{
    public partial class QuanLyDeThi : Page
    {
        private string DuongDanFile
        {
            get { return Server.MapPath("~/App-Data/DeThi.txt"); }
        }

        public class DeThi
        {
            public string MaDe { get; set; }
            public string TenDe { get; set; }
            public string MonHoc { get; set; }
            public int ThoiGian { get; set; }
            public int SoCau { get; set; }
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
                NapDanhSach();
                btnCapNhat.Visible = false;
            }
        }

        private List<DeThi> DocDanhSach()
        {
            List<DeThi> danhSach = new List<DeThi>();

            if (!File.Exists(DuongDanFile))
                return danhSach;

            foreach (string dong in File.ReadAllLines(DuongDanFile))
            {
                string[] p = dong.Split('|');

                if (p.Length != 5)
                    continue;

                int thoiGian, soCau;

                if (!int.TryParse(p[3], out thoiGian) ||
                    !int.TryParse(p[4], out soCau))
                    continue;

                danhSach.Add(new DeThi
                {
                    MaDe = p[0],
                    TenDe = p[1],
                    MonHoc = p[2],
                    ThoiGian = thoiGian,
                    SoCau = soCau
                });
            }

            return danhSach;
        }

        private void GhiDanhSach(List<DeThi> danhSach)
        {
            string thuMuc = Path.GetDirectoryName(DuongDanFile);

            if (!Directory.Exists(thuMuc))
                Directory.CreateDirectory(thuMuc);

            List<string> cacDong = danhSach.Select(d =>
                string.Join("|", new string[]
                {
                    d.MaDe, d.TenDe, d.MonHoc,
                    d.ThoiGian.ToString(), d.SoCau.ToString()
                })).ToList();

            File.WriteAllLines(DuongDanFile, cacDong);
        }

        private void NapDanhSach()
        {
            gvDeThi.DataSource = DocDanhSach();
            gvDeThi.DataBind();
        }

        private void XoaTrang()
        {
            txtMaDe.Text = "";
            txtTenDe.Text = "";
            txtMonHoc.Text = "";
            txtThoiGian.Text = "";
            txtSoCau.Text = "";

            txtMaDe.Enabled = true;
            btnThem.Visible = true;
            btnCapNhat.Visible = false;
        }

        private bool KiemTraDuLieu(out int thoiGian, out int soCau)
        {
            thoiGian = 0;
            soCau = 0;

            if (string.IsNullOrWhiteSpace(txtMaDe.Text) ||
                string.IsNullOrWhiteSpace(txtTenDe.Text) ||
                string.IsNullOrWhiteSpace(txtMonHoc.Text))
            {
                lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin.";
                return false;
            }

            if (new[] { txtMaDe.Text, txtTenDe.Text, txtMonHoc.Text }
                .Any(x => x.Contains("|") || x.Contains("\n") || x.Contains("\r")))
            {
                lblThongBao.Text = "Thông tin không được chứa ký tự | hoặc xuống dòng.";
                return false;
            }

            if (!int.TryParse(txtThoiGian.Text, out thoiGian) ||
                thoiGian <= 0)
            {
                lblThongBao.Text = "Thời gian phải là số nguyên dương.";
                return false;
            }

            if (!int.TryParse(txtSoCau.Text, out soCau) || soCau <= 0)
            {
                lblThongBao.Text = "Số câu phải là số nguyên dương.";
                return false;
            }

            return true;
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            int thoiGian, soCau;

            if (!KiemTraDuLieu(out thoiGian, out soCau))
                return;

            List<DeThi> ds = DocDanhSach();

            if (ds.Any(d => d.MaDe.Equals(txtMaDe.Text.Trim(),
                StringComparison.OrdinalIgnoreCase)))
            {
                lblThongBao.Text = "Mã đề thi đã tồn tại.";
                return;
            }

            ds.Add(new DeThi
            {
                MaDe = txtMaDe.Text.Trim(),
                TenDe = txtTenDe.Text.Trim(),
                MonHoc = txtMonHoc.Text.Trim(),
                ThoiGian = thoiGian,
                SoCau = soCau
            });

            GhiDanhSach(ds);
            lblThongBao.Text = "Thêm đề thi thành công.";
            XoaTrang();
            NapDanhSach();
        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            int thoiGian, soCau;

            if (!KiemTraDuLieu(out thoiGian, out soCau))
                return;

            List<DeThi> ds = DocDanhSach();
            DeThi deThi = ds.FirstOrDefault(d =>
                d.MaDe.Equals(txtMaDe.Text.Trim(),
                StringComparison.OrdinalIgnoreCase));

            if (deThi == null)
            {
                lblThongBao.Text = "Không tìm thấy đề thi.";
                return;
            }

            deThi.TenDe = txtTenDe.Text.Trim();
            deThi.MonHoc = txtMonHoc.Text.Trim();
            deThi.ThoiGian = thoiGian;
            deThi.SoCau = soCau;

            GhiDanhSach(ds);
            lblThongBao.Text = "Cập nhật đề thi thành công.";
            XoaTrang();
            NapDanhSach();
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
            lblThongBao.Text = "";
            NapDanhSach();
        }

        protected void gvDeThi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string maDe = Convert.ToString(e.CommandArgument);
            List<DeThi> ds = DocDanhSach();

            DeThi deThi = ds.FirstOrDefault(d => d.MaDe == maDe);

            if (deThi == null)
                return;

            if (e.CommandName == "SuaDe")
            {
                txtMaDe.Text = deThi.MaDe;
                txtTenDe.Text = deThi.TenDe;
                txtMonHoc.Text = deThi.MonHoc;
                txtThoiGian.Text = deThi.ThoiGian.ToString();
                txtSoCau.Text = deThi.SoCau.ToString();

                txtMaDe.Enabled = false;
                btnThem.Visible = false;
                btnCapNhat.Visible = true;
                lblThongBao.Text = "Đang chỉnh sửa đề thi: " + maDe;
            }
            else if (e.CommandName == "XoaDe")
            {
                string fileCauHoi = Server.MapPath("~/App-Data/CauHoi.txt");

                if (File.Exists(fileCauHoi))
                {
                    bool coCauHoi = File.ReadAllLines(fileCauHoi)
                        .Any(dong =>
                        {
                            string[] p = dong.Split('|');
                            return p.Length == 8 && p[1] == maDe;
                        });

                    if (coCauHoi)
                    {
                        lblThongBao.Text =
                            "Đề thi đang có câu hỏi. Hãy xử lý câu hỏi trước khi xóa đề.";
                        return;
                    }
                }

                ds.RemoveAll(d => d.MaDe == maDe);
                GhiDanhSach(ds);

                lblThongBao.Text = "Xóa đề thi thành công.";
                XoaTrang();
                NapDanhSach();
            }
        }
    }
}
