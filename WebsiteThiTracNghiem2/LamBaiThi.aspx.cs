
using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;
namespace WebsiteThiTracNghiem2
{
    public partial class LamBaiThi : System.Web.UI.Page
    {
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

        private List<CauHoi> danhSachCauHoi = new List<CauHoi>();

        private string MaDe
        {
            get { return Request.QueryString["MaDe"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra đăng nhập
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("~/DangNhap.aspx");
                return;
            }

            NapDuLieu();

            if (!IsPostBack)
            {
                pnlKetQua.Visible = false;
                pnlBaiThi.Visible = true;
            }
        }

        private void NapDuLieu()
        {
            string maDe = MaDe;

            if (string.IsNullOrWhiteSpace(maDe))
            {
                pnlBaiThi.Visible = false;
                lblThongBao.Text = "Không tìm thấy mã đề thi.";
                return;
            }

            // Đọc thông tin đề thi
            string fileDeThi = Server.MapPath("~/App-Data/DeThi.txt");

            if (!File.Exists(fileDeThi))
            {
                pnlBaiThi.Visible = false;
                lblThongBao.Text = "Không tìm thấy dữ liệu đề thi.";
                return;
            }

            bool timThayDe = false;

            foreach (string dong in File.ReadAllLines(fileDeThi))
            {
                if (string.IsNullOrWhiteSpace(dong))
                    continue;

                string[] cot = dong.Trim('\uFEFF').Split('|');

                if (cot.Length != 5 || cot[0].Trim() == "MaDe")
                    continue;

                if (cot[0].Trim() == maDe)
                {
                    lblTenDe.Text = cot[1].Trim();
                    lblMonHoc.Text = cot[2].Trim();
                    lblThoiGian.Text = cot[3].Trim();
                    timThayDe = true;
                    break;
                }
            }

            if (!timThayDe)
            {
                pnlBaiThi.Visible = false;
                lblThongBao.Text = "Đề thi không tồn tại.";
                return;
            }

            // Đọc câu hỏi
            string fileCauHoi = Server.MapPath("~/App-Data/CauHoi.txt");

            if (!File.Exists(fileCauHoi))
            {
                pnlBaiThi.Visible = false;
                lblThongBao.Text = "Chưa tìm thấy file CauHoi.txt.";
                return;
            }

            foreach (string dong in File.ReadAllLines(fileCauHoi))
            {
                if (string.IsNullOrWhiteSpace(dong))
                    continue;

                string[] cot = dong.Trim('\uFEFF').Split('|');

                if (cot.Length != 8 || cot[0].Trim() == "MaCauHoi")
                    continue;

                if (cot[1].Trim() == maDe)
                {
                    CauHoi cauHoi = new CauHoi
                    {
                        MaCauHoi = cot[0].Trim(),
                        MaDe = cot[1].Trim(),
                        NoiDung = cot[2].Trim(),
                        DapAnA = cot[3].Trim(),
                        DapAnB = cot[4].Trim(),
                        DapAnC = cot[5].Trim(),
                        DapAnD = cot[6].Trim(),
                        DapAnDung = cot[7].Trim().ToUpper()
                    };

                    danhSachCauHoi.Add(cauHoi);
                }
            }

            if (danhSachCauHoi.Count == 0)
            {
                pnlBaiThi.Visible = false;
                lblThongBao.Text = "Đề thi này hiện chưa có câu hỏi.";
                return;
            }

            lblSoCau.Text = danhSachCauHoi.Count.ToString();

            // Hiển thị câu hỏi
            rptCauHoi.DataSource = danhSachCauHoi;
            rptCauHoi.DataBind();
        }
        private static readonly object KhoaKetQua = new object();

        protected void btnNopBai_Click(object sender, EventArgs e)
        {
            try
            {
                int soCauDung = 0;
                int tongSoCau = danhSachCauHoi.Count;

                if (tongSoCau == 0)
                {
                    lblThongBao.Text = "Không có câu hỏi để chấm điểm.";
                    return;
                }

                string tenDangNhap = Session["TenDangNhap"] as string;

                if (string.IsNullOrWhiteSpace(tenDangNhap))
                {
                    lblThongBao.Text = "Phiên đăng nhập đã hết hạn.";
                    return;
                }

                foreach (CauHoi cauHoi in danhSachCauHoi)
                {
                    string dapAn = Request.Form["cau_" + cauHoi.MaCauHoi];

                    if (!string.IsNullOrWhiteSpace(dapAn) &&
                        dapAn.Trim().Equals(
                            cauHoi.DapAnDung.Trim(),
                            StringComparison.OrdinalIgnoreCase))
                    {
                        soCauDung++;
                    }
                }

                double diem = (double)soCauDung / tongSoCau * 10;

                // Đường dẫn lưu kết quả
                string duongDan = Server.MapPath("~/App-Data/KetQuaThi.txt");

                string thuMuc = Path.GetDirectoryName(duongDan);

                if (!Directory.Exists(thuMuc))
                {
                    Directory.CreateDirectory(thuMuc);
                }

                // Tạo dòng kết quả
                string maKetQua = Guid.NewGuid().ToString("N");

                string dongMoi = string.Join("|", new string[]
                {
            maKetQua,
            tenDangNhap,
            MaDe,
            lblTenDe.Text,
            soCauDung.ToString(),
            tongSoCau.ToString(),
            diem.ToString("0.00", CultureInfo.InvariantCulture),
            DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                });

                // Ghi dữ liệu
                lock (KhoaKetQua)
                {
                    if (!File.Exists(duongDan) ||
                        new FileInfo(duongDan).Length == 0)
                    {
                        File.WriteAllText(
                            duongDan,
                            "MaKetQua|TenDangNhap|MaDe|TenDe|SoCauDung|TongSoCau|Diem|NgayThi"
                            + Environment.NewLine,
                            Encoding.UTF8);
                    }

                    File.AppendAllText(
                        duongDan,
                        dongMoi + Environment.NewLine,
                        Encoding.UTF8);
                }

                // Hiển thị kết quả
                lblSoCauDung.Text = soCauDung.ToString();
                lblTongSoCau.Text = tongSoCau.ToString();
                lblDiem.Text = diem.ToString("0.00") + " điểm";

                pnlBaiThi.Visible = false;
                pnlKetQua.Visible = true;

                lblThongBao.Text = "Nộp bài và lưu kết quả thành công.";

            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Lỗi lưu kết quả: " + Server.HtmlEncode(ex.Message);
            }
        }
    }
}