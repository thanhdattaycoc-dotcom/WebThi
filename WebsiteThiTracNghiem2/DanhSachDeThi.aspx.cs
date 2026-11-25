
using System;
using System.Collections.Generic;
using System.IO;

namespace WebsiteThiTracNghiem2
{
    public partial class DanhSachDeThi : System.Web.UI.Page
    {
        public class ThongTinDeThi
        {
            public string MaDe { get; set; }
            public string TenDe { get; set; }
            public string MonHoc { get; set; }
            public string ThoiGian { get; set; }
            public string SoCau { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HienThiDanhSachDeThi();
            }
        }

        private void HienThiDanhSachDeThi()
        {
            string duongDan = Server.MapPath("~/App-Data/DeThi.txt");

            List<ThongTinDeThi> danhSach = new List<ThongTinDeThi>();

            if (!File.Exists(duongDan))
            {
                lblThongBao.Text = "Chưa tìm thấy file dữ liệu đề thi.";
                lblSoDeThi.Text = "0 đề thi";
                return;
            }

            try
            {
                string[] cacDong = File.ReadAllLines(duongDan);

                foreach (string dong in cacDong)
                {
                    if (string.IsNullOrWhiteSpace(dong))
                        continue;

                    string[] cot = dong.Split('|');

                    if (cot.Length != 5)
                        continue;

                    if (cot[0].Trim() == "MaDe")
                        continue;

                    ThongTinDeThi deThi = new ThongTinDeThi
                    {
                        MaDe = cot[0].Trim(),
                        TenDe = cot[1].Trim(),
                        MonHoc = cot[2].Trim(),
                        ThoiGian = cot[3].Trim(),
                        SoCau = cot[4].Trim()
                    };

                    danhSach.Add(deThi);
                }

                rptDeThi.DataSource = danhSach;
                rptDeThi.DataBind();

                lblSoDeThi.Text = danhSach.Count + " đề thi";

                if (danhSach.Count == 0)
                {
                    lblThongBao.Text = "Hiện chưa có đề thi nào.";
                }
                else
                {
                    lblThongBao.Text = "";
                }
            }
            catch (Exception)
            {
                lblThongBao.Text = "Có lỗi khi đọc dữ liệu đề thi.";
            }
        }
    }
}
