
using System;
using System.Collections.Generic;
using System.IO;

namespace WebsiteThiTracNghiem2
{
    public partial class KetQuaThi : System.Web.UI.Page
    {
        public class ThongTinKetQua
        {
            public string MaKetQua { get; set; }
            public string TenDangNhap { get; set; }
            public string MaDe { get; set; }
            public string TenDe { get; set; }
            public string SoCauDung { get; set; }
            public string TongSoCau { get; set; }
            public string Diem { get; set; }
            public string NgayThi { get; set; }
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
                HienThiKetQua();
            }
        }

        private void HienThiKetQua()
        {
            string duongDan = Server.MapPath(
                "~/App-Data/KetQuaThi.txt"
            );

            List<ThongTinKetQua> danhSach =
                new List<ThongTinKetQua>();

            if (!File.Exists(duongDan))
            {
                lblSoKetQua.Text = "0 kết quả";
                lblThongBao.Text = "Bạn chưa có kết quả thi nào.";
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

                    if (cot.Length != 8)
                        continue;

                    if (cot[0].Trim() == "MaKetQua")
                        continue;

                    // Chỉ hiển thị kết quả của tài khoản đang đăng nhập
                    if (!string.Equals(
                        cot[1].Trim(),
                        tenDangNhap,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    danhSach.Add(new ThongTinKetQua
                    {
                        MaKetQua = cot[0].Trim(),
                        TenDangNhap = cot[1].Trim(),
                        MaDe = cot[2].Trim(),
                        TenDe = cot[3].Trim(),
                        SoCauDung = cot[4].Trim(),
                        TongSoCau = cot[5].Trim(),
                        Diem = cot[6].Trim(),
                        NgayThi = cot[7].Trim()
                    });
                }

                // Hiển thị kết quả mới nhất lên đầu
                danhSach.Reverse();

                rptKetQua.DataSource = danhSach;
                rptKetQua.DataBind();

                lblSoKetQua.Text =
                    danhSach.Count + " kết quả";

                if (danhSach.Count == 0)
                {
                    lblThongBao.Text =
                        "Bạn chưa có kết quả thi nào.";
                }
                else
                {
                    lblThongBao.Text = "";
                }
            }
            catch (Exception)
            {
                lblThongBao.Text =
                    "Có lỗi khi đọc dữ liệu kết quả thi.";
            }
        }
    }
}
