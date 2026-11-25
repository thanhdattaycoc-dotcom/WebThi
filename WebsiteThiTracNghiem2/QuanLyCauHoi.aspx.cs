
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebsiteThiTracNghiem2
{
    public partial class QuanLyCauHoi : Page
    {
        private string FileCauHoi
        {
            get { return Server.MapPath("~/App-Data/CauHoi.txt"); }
        }

        private string FileDeThi
        {
            get { return Server.MapPath("~/App-Data/DeThi.txt"); }
        }

        public class CauHoi
        {
            public string MaCauHoi { get; set; }
            public string MaDe { get; set; }
            public string NoiDung { get; set; }
            public string DapAnA { get; set; }
            public string DapAnB { get; set; }
            public string DapAnC { get; set; }
            public string DapAnD { get; set; }
            public string DapAnDung { get; set; }
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
                NapDanhSachDe();
                NapDanhSachCauHoi();
                btnCapNhat.Visible = false;
            }
        }

        private List<CauHoi> DocCauHoi()
        {
            List<CauHoi> ds = new List<CauHoi>();

            if (!File.Exists(FileCauHoi))
                return ds;

            foreach (string dong in File.ReadAllLines(FileCauHoi))
            {
                string[] p = dong.Split('|');

                if (p.Length != 8)
                    continue;

                ds.Add(new CauHoi
                {
                    MaCauHoi = p[0],
                    MaDe = p[1],
                    NoiDung = p[2],
                    DapAnA = p[3],
                    DapAnB = p[4],
                    DapAnC = p[5],
                    DapAnD = p[6],
                    DapAnDung = p[7]
                });
            }

            return ds;
        }

        private void GhiCauHoi(List<CauHoi> ds)
        {
            string thuMuc = Path.GetDirectoryName(FileCauHoi);

            if (!Directory.Exists(thuMuc))
                Directory.CreateDirectory(thuMuc);

            List<string> cacDong = ds.Select(c =>
                string.Join("|", new string[]
                {
                    c.MaCauHoi, c.MaDe, c.NoiDung,
                    c.DapAnA, c.DapAnB, c.DapAnC,
                    c.DapAnD, c.DapAnDung
                })).ToList();

            File.WriteAllLines(FileCauHoi, cacDong);
        }

        private void NapDanhSachDe()
        {
            ddlMaDe.Items.Clear();

            if (File.Exists(FileDeThi))
            {
                foreach (string dong in File.ReadAllLines(FileDeThi))
                {
                    string[] p = dong.Split('|');

                    if (p.Length == 5 && !string.IsNullOrWhiteSpace(p[0]))
                    {
                        ddlMaDe.Items.Add(new ListItem(
                            p[0] + " - " + p[1], p[0]));
                    }
                }
            }

            if (ddlMaDe.Items.Count == 0)
            {
                ddlMaDe.Items.Add(new ListItem("-- Chưa có đề thi --", ""));
            }
        }

        private void NapDanhSachCauHoi()
        {
            gvCauHoi.DataSource = DocCauHoi();
            gvCauHoi.DataBind();
        }

        private void XoaTrang()
        {
            txtMaCauHoi.Text = "";
            txtNoiDung.Text = "";
            txtA.Text = "";
            txtB.Text = "";
            txtC.Text = "";
            txtD.Text = "";
            ddlDapAnDung.SelectedIndex = 0;

            txtMaCauHoi.Enabled = true;
            btnThem.Visible = true;
            btnCapNhat.Visible = false;

            if (ddlMaDe.Items.Count > 0)
                ddlMaDe.SelectedIndex = 0;
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaCauHoi.Text) ||
                string.IsNullOrWhiteSpace(ddlMaDe.SelectedValue) ||
                string.IsNullOrWhiteSpace(txtNoiDung.Text) ||
                string.IsNullOrWhiteSpace(txtA.Text) ||
                string.IsNullOrWhiteSpace(txtB.Text) ||
                string.IsNullOrWhiteSpace(txtC.Text) ||
                string.IsNullOrWhiteSpace(txtD.Text) ||
                string.IsNullOrWhiteSpace(ddlDapAnDung.SelectedValue))
            {
                lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin câu hỏi.";
                return false;
            }

            string[] giaTri =
            {
                txtMaCauHoi.Text,
                txtNoiDung.Text,
                txtA.Text,
                txtB.Text,
                txtC.Text,
                txtD.Text
            };

            foreach (string giaTriNhap in giaTri)
            {
                if (giaTriNhap.Contains("|") ||
                    giaTriNhap.Contains("\r") ||
                    giaTriNhap.Contains("\n"))
                {
                    lblThongBao.Text =
                        "Thông tin không được chứa ký tự | hoặc xuống dòng.";
                    return false;
                }
            }

            return true;
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            List<CauHoi> ds = DocCauHoi();

            if (ds.Any(c => c.MaCauHoi.Equals(
                txtMaCauHoi.Text.Trim(),
                StringComparison.OrdinalIgnoreCase)))
            {
                lblThongBao.Text = "Mã câu hỏi đã tồn tại.";
                return;
            }

            ds.Add(new CauHoi
            {
                MaCauHoi = txtMaCauHoi.Text.Trim(),
                MaDe = ddlMaDe.SelectedValue,
                NoiDung = txtNoiDung.Text.Trim(),
                DapAnA = txtA.Text.Trim(),
                DapAnB = txtB.Text.Trim(),
                DapAnC = txtC.Text.Trim(),
                DapAnD = txtD.Text.Trim(),
                DapAnDung = ddlDapAnDung.SelectedValue
            });

            GhiCauHoi(ds);

            lblThongBao.Text = "Thêm câu hỏi thành công.";
            XoaTrang();
            NapDanhSachCauHoi();
        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
                return;

            List<CauHoi> ds = DocCauHoi();

            CauHoi c = ds.FirstOrDefault(x =>
                x.MaCauHoi == txtMaCauHoi.Text.Trim());

            if (c == null)
            {
                lblThongBao.Text = "Không tìm thấy câu hỏi.";
                return;
            }

            c.MaDe = ddlMaDe.SelectedValue;
            c.NoiDung = txtNoiDung.Text.Trim();
            c.DapAnA = txtA.Text.Trim();
            c.DapAnB = txtB.Text.Trim();
            c.DapAnC = txtC.Text.Trim();
            c.DapAnD = txtD.Text.Trim();
            c.DapAnDung = ddlDapAnDung.SelectedValue;

            GhiCauHoi(ds);

            lblThongBao.Text = "Cập nhật câu hỏi thành công.";
            XoaTrang();
            NapDanhSachCauHoi();
        }

        protected void btnLamMoi_Click(object sender, EventArgs e)
        {
            XoaTrang();
            lblThongBao.Text = "";
            NapDanhSachCauHoi();
        }

        protected void gvCauHoi_RowCommand(
            object sender, GridViewCommandEventArgs e)
        {
            string maCauHoi = Convert.ToString(e.CommandArgument);

            List<CauHoi> ds = DocCauHoi();

            CauHoi c = ds.FirstOrDefault(x => x.MaCauHoi == maCauHoi);

            if (c == null)
                return;

            if (e.CommandName == "SuaCauHoi")
            {
                txtMaCauHoi.Text = c.MaCauHoi;
                txtMaCauHoi.Enabled = false;

                if (ddlMaDe.Items.FindByValue(c.MaDe) != null)
                    ddlMaDe.SelectedValue = c.MaDe;

                txtNoiDung.Text = c.NoiDung;
                txtA.Text = c.DapAnA;
                txtB.Text = c.DapAnB;
                txtC.Text = c.DapAnC;
                txtD.Text = c.DapAnD;
                ddlDapAnDung.SelectedValue = c.DapAnDung;

                btnThem.Visible = false;
                btnCapNhat.Visible = true;

                lblThongBao.Text = "Đang chỉnh sửa câu hỏi " + maCauHoi;
            }
            else if (e.CommandName == "XoaCauHoi")
            {
                ds.RemoveAll(x => x.MaCauHoi == maCauHoi);
                GhiCauHoi(ds);

                lblThongBao.Text = "Xóa câu hỏi thành công.";
                XoaTrang();
                NapDanhSachCauHoi();
            }
        }
    }
}
