
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WebsiteThiTracNghiem2
{
    public partial class LichSuThi : System.Web.UI.Page
    {
        public class ThongTinLichSu
        {
            public string MaKetQua { get; set; }
            public string TenDangNhap { get; set; }
            public string MaDe { get; set; }
            public string TenDe { get; set; }
            public int SoCauDung { get; set; }
            public int TongSoCau { get; set; }
            public decimal Diem { get; set; }
            public string NgayThi { get; set; }
            public DateTime ThoiGianThi { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TenDangNhap"] == null)
            {
                Response.Redirect("~/DangNhap.aspx");
                return;
            }

            if (!IsPostBack)
            {
                HienThiLichSu();
            }
        }

        private void HienThiLichSu()
        {
            string duongDan = Server.MapPath(
                "~/App-Data/KetQuaThi.txt"
            );

            List<ThongTinLichSu> danhSach =
                new List<ThongTinLichSu>();

            if (!File.Exists(duongDan))
            {
                HienThiThongKe(danhSach);
                lblThongBao.Text = "Bạn chưa có lịch sử thi.";
                return;
            }

            try
            {
                string tenDangNhap =
                    Session["TenDangNhap"].ToString();

                string[] cacDong = File.ReadAllLines(duongDan);

                foreach (string dong in cacDong)
                {
                    if (string.IsNullOrWhiteSpace(dong))
                        continue;

                    string[] cot = dong.Trim('\uFEFF').Split('|');

                    // File kết quả có 8 trường
                    if (cot.Length != 8)
                        continue;

                    if (cot[0].Trim() == "MaKetQua")
                        continue;

                    // Chỉ lấy lịch sử của người đang đăng nhập
                    if (!string.Equals(
                        cot[1].Trim(),
                        tenDangNhap,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    int soCauDung;
                    int tongSoCau;
                    decimal diem;
                    DateTime ngayThi;

                    if (!int.TryParse(cot[4].Trim(), out soCauDung))
                        continue;

                    if (!int.TryParse(cot[5].Trim(), out tongSoCau))
                        continue;

                    if (!decimal.TryParse(
                        cot[6].Trim(),
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out diem))
                    {
                        continue;
                    }

                    if (!DateTime.TryParseExact(
                        cot[7].Trim(),
                        "dd/MM/yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out ngayThi))
                    {
                        continue;
                    }

                    danhSach.Add(new ThongTinLichSu
                    {
                        MaKetQua = cot[0].Trim(),
                        TenDangNhap = cot[1].Trim(),
                        MaDe = cot[2].Trim(),
                        TenDe = cot[3].Trim(),
                        SoCauDung = soCauDung,
                        TongSoCau = tongSoCau,
                        Diem = diem,
                        NgayThi = cot[7].Trim(),
                        ThoiGianThi = ngayThi
                    });
                }

                // Sắp xếp lần thi mới nhất lên đầu
                danhSach = danhSach
                    .OrderByDescending(x => x.ThoiGianThi)
                    .ToList();

                rptLichSu.DataSource = danhSach;
                rptLichSu.DataBind();

                HienThiThongKe(danhSach);

                if (danhSach.Count == 0)
                {
                    lblThongBao.Text =
                        "Bạn chưa có bài thi nào được lưu.";
                }
                else
                {
                    lblThongBao.Text = "";
                }
            }
            catch (Exception)
            {
                lblThongBao.Text =
                    "Có lỗi khi đọc lịch sử thi. Vui lòng kiểm tra file dữ liệu.";
            }
        }

        private void HienThiThongKe(List<ThongTinLichSu> danhSach)
        {
            lblTongLanThi.Text = danhSach.Count.ToString();

            lblSoKetQua.Text =
                danhSach.Count + " lần thi";

            if (danhSach.Count == 0)
            {
                lblDiemTrungBinh.Text = "0.00";
                lblDiemCaoNhat.Text = "0.00";
                return;
            }

            decimal diemTrungBinh =
                danhSach.Average(x => x.Diem);

            decimal diemCaoNhat =
                danhSach.Max(x => x.Diem);

            lblDiemTrungBinh.Text =
                diemTrungBinh.ToString("0.00");

            lblDiemCaoNhat.Text =
                diemCaoNhat.ToString("0.00");
        }
    }
}
