
using System;
using System.IO;
using System.Web;

namespace WebsiteThiTracNghiem2
{
    public class XuLyTaiKhoan
    {
        // 1. Tao doi tuong khoa de tranh nhieu yeu cau ghi file cung luc
        private static readonly object KhoaFile = new object();

        public class ThongTinTaiKhoan
        {
            // Ho va ten nguoi dang ky
            public string HoTen { get; set; }

            // Ten dang nhap
            public string TenDangNhap { get; set; }

            // Email
            public string Email { get; set; }

            // Lop
            public string Lop { get; set; }

            // Que quan
            public string QueQuan { get; set; }

            // So thich
            public string SoThich { get; set; }
            // Tiền ăn
            public string TienAn { get; set; }

            // Mã sinh viên
            public string MaSinhVien { get; set; }


            // Trang thai dong y dieu khoan
            public bool DongYDieuKhoan { get; set; }
        }

        // 2. Doc thong tin tai khoan theo ten dang nhap
        public static ThongTinTaiKhoan LayThongTin(string tenDangNhap)
        {
            lock (KhoaFile)
            {
                if (!File.Exists(DuongDanFile))
                    return null;

                string[] danhSach = File.ReadAllLines(DuongDanFile);

                foreach (string dong in danhSach)
                {
                    if (string.IsNullOrWhiteSpace(dong))
                        continue;

                    string[] cot = dong.Split('|');

                    if (cot.Length <9) // có sửa theo kiểu mỗi lần thêm trường thi + 1 dùng hàm != ( vd != 10 )
                        continue;

                    if (string.Equals(
                        cot[1],
                        tenDangNhap,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        return new ThongTinTaiKhoan
                        {
                            HoTen = cot[0],
                            TenDangNhap = cot[1],
                            Email = cot[3],
                            Lop = cot[4],
                            QueQuan = cot[5],
                            SoThich = cot[6],
                            DongYDieuKhoan = cot.Length == 9
                                ? cot[7] == "True"
            :                     cot.Length >= 11 && cot[9] == "True",

                            TienAn = cot.Length >= 10 ? cot[7] : "",

                            MaSinhVien = cot.Length >= 11 ? cot[8] : "",

                        };
                    }
                }
            }

            return null;
        }

        // 2.1 Cap nhat thong tin ca nhan
        public static bool CapNhatThongTin(
            ThongTinTaiKhoan taiKhoan,
            out string thongBao)
        {
            thongBao = "";

            if (taiKhoan == null)
            {
                thongBao = "Thông tin tài khoản không hợp lệ.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(taiKhoan.TenDangNhap))
            {
                thongBao = "Không xác định được tài khoản.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(taiKhoan.HoTen) ||
                string.IsNullOrWhiteSpace(taiKhoan.Email) ||
                string.IsNullOrWhiteSpace(taiKhoan.Lop) ||
                string.IsNullOrWhiteSpace(taiKhoan.QueQuan))
            {
                thongBao = "Vui lòng nhập đầy đủ thông tin.";
                return false;
            }

            string[] giaTri = {
                taiKhoan.HoTen,
                taiKhoan.Email,
                taiKhoan.Lop,
                taiKhoan.QueQuan,
                taiKhoan.SoThich
            };

            foreach (string giaTriCot in giaTri)
            {
                if (giaTriCot != null &&
                    (giaTriCot.Contains("|") ||
                     giaTriCot.Contains("\r") ||
                     giaTriCot.Contains("\n")))
                {
                    thongBao = "Thông tin không được chứa ký tự |.";
                    return false;
                }
            }

            lock (KhoaFile)
            {
                if (!File.Exists(DuongDanFile))
                {
                    thongBao = "Không tìm thấy file tài khoản.";
                    return false;
                }

                string[] danhSach = File.ReadAllLines(DuongDanFile);
                bool timThay = false;

                for (int i = 0; i < danhSach.Length; i++)
                {
                    string[] cot = danhSach[i].Split('|');

                    if (cot.Length <=8) 
                        continue;

                    if (string.Equals(
                        cot[1],
                        taiKhoan.TenDangNhap,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        // Chi cap nhat thong tin ca nhan
                        // Giu nguyen mat khau, quyen va trang thai dieu khoan
                        cot[0] = taiKhoan.HoTen;
                        cot[3] = taiKhoan.Email;
                        cot[4] = taiKhoan.Lop;
                        cot[5] = taiKhoan.QueQuan;
                        cot[6] = taiKhoan.SoThich ?? "";

                        danhSach[i] = string.Join("|", cot);

                        timThay = true;
                        break;
                    }
                }

                if (!timThay)
                {
                    thongBao = "Không tìm thấy tài khoản.";
                    return false;
                }

                File.WriteAllLines(DuongDanFile, danhSach);

                thongBao = "Cập nhật thông tin thành công!";
                return true;
            }
        }




        // 3. Lay duong dan den file TaiKhoan.txt trong App_Data
        private static string DuongDanFile
        {
            get
            {
                return HttpContext.Current.Server.MapPath(
                    "~/App-Data/TaiKhoan.txt"
                );
            }
        }

        // 4. Ham dang ky tai khoan
        // taiKhoan: Chua thong tin nguoi dang ky
        // matKhau: Mat khau nguoi dung nhap
        // thongBao: Tra ve noi dung thong bao
        // Ket qua tra ve: true neu dang ky thanh cong, false neu that bai
        public static bool DangKy(
            ThongTinTaiKhoan taiKhoan,
            string matKhau,
            out string thongBao)
        {
            // Khoi tao thong bao rong
            thongBao = "";

            // Kiem tra doi tuong tai khoan co ton tai khong
            if (taiKhoan == null)
            {
                thongBao = "Thông tin tài khoản không hợp lệ.";
                return false;
            }

            // Kiem tra mat khau khong duoc de trong
            if (string.IsNullOrWhiteSpace(matKhau))
            {
                thongBao = "Mật khẩu không được để trống.";
                return false;
            }

            // Kiem tra nguoi dung da dong y dieu khoan chua
            if (!taiKhoan.DongYDieuKhoan)
            {
                thongBao = "Bạn cần đồng ý với điều khoản.";
                return false;
            }

            // 5. Khoa qua trinh doc va ghi file
            lock (KhoaFile)
            {
                // Lay duong dan thu muc App_Data
                string thuMuc = Path.GetDirectoryName(DuongDanFile);

                // Neu thu muc App_Data chua ton tai thi tao moi
                if (!Directory.Exists(thuMuc))
                {
                    Directory.CreateDirectory(thuMuc);
                }

                // Neu file TaiKhoan.txt chua ton tai thi tao file moi
                if (!File.Exists(DuongDanFile))
                {
                    File.Create(DuongDanFile).Close();
                }

                // Doc tat ca cac dong tai khoan da luu trong file
                string[] danhSach = File.ReadAllLines(DuongDanFile);

                // Duyet tung dong de kiem tra tai khoan trung lap
                foreach (string dong in danhSach)
                {
                    // Bo qua dong trong
                    if (string.IsNullOrWhiteSpace(dong))
                        continue;

                    // Tach cac truong thong tin bang ky tu |
                    string[] cot = dong.Split('|');

                    // Dam bao dong du lieu co du 9 truong
                    if (cot.Length >= 9)
                    {
                        // Cot thu 2 la ten dang nhap
                        string tenCu = cot[1];

                        // Cot thu 4 la email
                        string emailCu = cot[3];

                        // Kiem tra ten dang nhap da ton tai chua
                        if (string.Equals(
                            tenCu,
                            taiKhoan.TenDangNhap,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            thongBao = "Tên đăng nhập đã tồn tại.";
                            return false;
                        }

                        // Kiem tra email da duoc su dung chua
                        if (string.Equals(
                            emailCu,
                            taiKhoan.Email,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            thongBao = "Email đã được sử dụng.";
                            return false;
                        }
                    }
                }

                // Gan quyen User mac dinh cho tai khoan moi
                // Nguoi dang ky khong the tu cap quyen Admin
                string quyen = "User";

                // Ghep cac thong tin thanh mot dong du lieu
                // Moi truong duoc ngan cach bang ky tu |
                string dongMoi = string.Join("|", new string[]
                {
                    taiKhoan.HoTen,                 // Cot 1: Ho va ten
                    taiKhoan.TenDangNhap,           // Cot 2: Ten dang nhap
                    matKhau,                        // Cot 3: Mat khau
                    taiKhoan.Email,                 // Cot 4: Email
                    taiKhoan.Lop,                   // Cot 5: Lop
                    taiKhoan.QueQuan,               // Cot 6: Que quan
                    taiKhoan.SoThich,               // Cot 7: So thich
                    taiKhoan.TienAn,                // Cot 8: Tien an
                    taiKhoan.MaSinhVien,            // Cot 9: Ma sinh vien
                    taiKhoan.DongYDieuKhoan.ToString(), // Cot 10: Dong y dieu khoan
                    quyen                           // Cot 11: Quyen tai khoan
                });

                // Ghi them tai khoan moi vao cuoi file TaiKhoan.txt
                // Khong lam mat du lieu cua cac tai khoan cu
                File.AppendAllText(
                    DuongDanFile,
                    dongMoi + Environment.NewLine
                );

                // Thong bao dang ky thanh cong
                thongBao = "Đăng ký tài khoản thành công!";

                // Tra ve true de bao dang ky thanh cong
                return true;
            }
        }
    }
}
