using System.Linq;
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
            try
            {
                List<ThongTinDeThi> danhSach = LayDanhSachDeThi();

                rptDeThi.DataSource = danhSach;
                rptDeThi.DataBind();

                lblSoDeThi.Text = danhSach.Count + " đề thi";

                if (danhSach.Count == 0)
                    lblThongBao.Text = "Hiện chưa có đề thi nào.";
                else
                    lblThongBao.Text = "";
            }
            catch (Exception)
            {
                lblThongBao.Text = "Có lỗi khi đọc dữ liệu đề thi.";
            }
        }



        /* đâyy là hàm mới viết + sửa thêm hàm hienThiDanhSachDeThi() ở trên*/
        private List<ThongTinDeThi> LayDanhSachDeThi()
        {
            List<QuanLyDeThi.DeThi> ds;

            Application.Lock();

            try
            {
                ds = Application["DanhSachDeThi"]
                    as List<QuanLyDeThi.DeThi>;
            }
            finally
            {
                Application.UnLock();
            }

            // Neu Application chua co du lieu thi doc tu file
            if (ds == null)
            {
                ds = new List<QuanLyDeThi.DeThi>();

                string duongDan = Server.MapPath("~/App-Data/DeThi.txt");

                if (File.Exists(duongDan))
                {
                    foreach (string dong in File.ReadAllLines(duongDan))
                    {
                        string[] cot = dong.Split('|');

                        int thoiGian, soCau;

                        if (cot.Length != 5 ||
                            !int.TryParse(cot[3], out thoiGian) ||
                            !int.TryParse(cot[4], out soCau))
                            continue;

                        ds.Add(new QuanLyDeThi.DeThi
                        {
                            MaDe = cot[0].Trim(),
                            TenDe = cot[1].Trim(),
                            MonHoc = cot[2].Trim(),
                            ThoiGian = thoiGian,
                            SoCau = soCau
                        });
                    }
                }

                Application.Lock();

                try
                {
                    Application["DanhSachDeThi"] = ds;
                }
                finally
                {
                    Application.UnLock();
                }
            }

            // Chuyen sang danh sach ThongTinDeThi de hien thi
            return ds.Select(d => new ThongTinDeThi
            {
                MaDe = d.MaDe,
                TenDe = d.TenDe,
                MonHoc = d.MonHoc,
                ThoiGian = d.ThoiGian.ToString(),
                SoCau = d.SoCau.ToString()
            }).ToList();
        }
    }
}



//Thầy hỏi	Em trả lời
//CRUD ở đâu?	QuanLyDeThi.aspx.cs
//Thêm đề thi ở đâu?	btnThem_Click()
//Sửa đề thi ở đâu?	btnCapNhat_Click()
//Xóa đề thi ở đâu?	gvDeThi_RowCommand()
//Session ở đâu?	DangNhap.aspx.cs và TaiKhoan.aspx.cs
//Application ở đâu?	QuanLyDeThi.aspx.cs và DanhSachDeThi.aspx.cs sau khi bổ sung
//Dữ liệu lưu ở đâu?	File App-Data/DeThi.txt và Application trong bộ nhớ

//Điều quan trọng: Nếu thầy yêu cầu mở code chứng minh Application, bạn phải bổ sung và chạy thử phần Application trước. Không nên nói rằng code ban đầu đã có Application, vì thực tế ban đầu bạn chỉ sử dụng File.ReadAllLines() và File.WriteAllLines().