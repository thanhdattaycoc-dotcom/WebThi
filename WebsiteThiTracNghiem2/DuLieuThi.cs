
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebsiteThiTracNghiem2
{
    public static class DuLieuThi
    {
        private static readonly object KhoaFile = new object();

        // ==========================================
        // 1. CAC LOP DU LIEU
        // ==========================================

        public class DeThi
        {
            public string MaDe { get; set; }
            public string TenDe { get; set; }
            public string MonHoc { get; set; }
            public int ThoiGian { get; set; }
            public int SoCau { get; set; }
        }

        public class CauHoi
        {
            public string MaCauHoi { get; set; }
            public string MaDe { get; set; }
            public string NoiDung { get; set; }
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string DapAn { get; set; }
        }

        public class KetQuaThi
        {
            public string MaKetQua { get; set; }
            public string TenDangNhap { get; set; }
            public string MaDe { get; set; }
            public string TenDe { get; set; }
            public DateTime NgayThi { get; set; }
            public int TongCau { get; set; }
            public int SoCauDung { get; set; }
            public int SoCauSai { get; set; }
            public decimal Diem { get; set; }
        }

        public class TaiKhoan
        {
            public string HoTen { get; set; }
            public string TenDangNhap { get; set; }
            public string MatKhau { get; set; }
            public string Email { get; set; }
            public string Lop { get; set; }
            public string QueQuan { get; set; }
            public string SoThich { get; set; }
            public string DongYDieuKhoan { get; set; }
            public string Quyen { get; set; }
        }

        // ==========================================
        // 2. DUONG DAN FILE
        // ==========================================

        private static string LayDuongDan(string tenFile)
        {
            HttpContext context = HttpContext.Current;

            if (context == null)
                throw new InvalidOperationException(
                    "Khong tim thay HttpContext cua ung dung.");

            string thuMuc = context.Server.MapPath("~/App-Data/");

            if (!Directory.Exists(thuMuc))
                Directory.CreateDirectory(thuMuc);

            string duongDan = Path.Combine(thuMuc, tenFile);

            if (!File.Exists(duongDan))
            {
                using (File.Create(duongDan))
                {
                }
            }

            return duongDan;
        }

        private static string[] DocDong(string tenFile)
        {
            string path = LayDuongDan(tenFile);

            if (!File.Exists(path))
                return new string[0];

            return File.ReadAllLines(path, Encoding.UTF8);
        }

        private static void GhiTatCa(string tenFile, List<string> danhSach)
        {
            string path = LayDuongDan(tenFile);

            File.WriteAllLines(
                path,
                danhSach.ToArray(),
                new UTF8Encoding(false)
            );
        }

        private static bool HopLe(string giaTri)
        {
            return !string.IsNullOrWhiteSpace(giaTri)
                && !giaTri.Contains("|")
                && !giaTri.Contains("\r")
                && !giaTri.Contains("\n");
        }

        private static string GiaTri(string[] cot, int viTri)
        {
            if (cot == null || viTri >= cot.Length)
                return "";

            return cot[viTri].Trim();
        }

        // ==========================================
        // 3. DOC DANH SACH DE THI
        // DeThi.txt: 5 truong
        // MaDe|TenDe|MonHoc|ThoiGian|SoCau
        // ==========================================

        public static List<DeThi> LayDanhSachDeThi()
        {
            List<DeThi> danhSach = new List<DeThi>();

            lock (KhoaFile)
            {
                string[] dong = DocDong("DeThi.txt");

                foreach (string line in dong)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] cot = line.Split('|');

                    int thoiGian;
                    int soCau;

                    if (cot.Length != 5)
                        continue;

                    if (!int.TryParse(cot[3], out thoiGian))
                        continue;

                    if (!int.TryParse(cot[4], out soCau))
                        continue;

                    danhSach.Add(new DeThi
                    {
                        MaDe = cot[0].Trim(),
                        TenDe = cot[1].Trim(),
                        MonHoc = cot[2].Trim(),
                        ThoiGian = thoiGian,
                        SoCau = soCau
                    });
                }
            }

            return danhSach;
        }

        public static DeThi LayDeThi(string maDe)
        {
            return LayDanhSachDeThi().FirstOrDefault(
                x => string.Equals(
                    x.MaDe, maDe,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }

        public static bool ThemDeThi(DeThi deThi, out string thongBao)
        {
            thongBao = "";

            if (deThi == null ||
                !HopLe(deThi.MaDe) ||
                !HopLe(deThi.TenDe) ||
                !HopLe(deThi.MonHoc) ||
                deThi.ThoiGian <= 0 ||
                deThi.SoCau <= 0)
            {
                thongBao = "Thong tin de thi khong hop le.";
                return false;
            }

            lock (KhoaFile)
            {
                List<DeThi> ds = LayDanhSachDeThi();

                if (ds.Any(x => x.MaDe.Equals(
                    deThi.MaDe, StringComparison.OrdinalIgnoreCase)))
                {
                    thongBao = "Ma de thi da ton tai.";
                    return false;
                }

                string line = string.Join("|", new string[]
                {
                    deThi.MaDe.Trim(),
                    deThi.TenDe.Trim(),
                    deThi.MonHoc.Trim(),
                    deThi.ThoiGian.ToString(),
                    deThi.SoCau.ToString()
                });

                File.AppendAllText(
                    LayDuongDan("DeThi.txt"),
                    line + Environment.NewLine,
                    new UTF8Encoding(false)
                );
            }

            thongBao = "Them de thi thanh cong.";
            return true;
        }

        public static bool CapNhatDeThi(DeThi deThi, out string thongBao)
        {
            thongBao = "";

            if (deThi == null ||
                !HopLe(deThi.MaDe) ||
                !HopLe(deThi.TenDe) ||
                !HopLe(deThi.MonHoc) ||
                deThi.ThoiGian <= 0 ||
                deThi.SoCau <= 0)
            {
                thongBao = "Du lieu de thi khong hop le.";
                return false;
            }

            lock (KhoaFile)
            {
                List<string> ds = DocDong("DeThi.txt").ToList();
                bool timThay = false;

                for (int i = 0; i < ds.Count; i++)
                {
                    string[] cot = ds[i].Split('|');

                    if (cot.Length == 5 &&
                        cot[0].Trim().Equals(
                            deThi.MaDe,
                            StringComparison.OrdinalIgnoreCase))
                    {
                        ds[i] = string.Join("|", new string[]
                        {
                            deThi.MaDe.Trim(),
                            deThi.TenDe.Trim(),
                            deThi.MonHoc.Trim(),
                            deThi.ThoiGian.ToString(),
                            deThi.SoCau.ToString()
                        });

                        timThay = true;
                        break;
                    }
                }

                if (!timThay)
                {
                    thongBao = "Khong tim thay de thi.";
                    return false;
                }

                GhiTatCa("DeThi.txt", ds);
            }

            thongBao = "Cap nhat de thi thanh cong.";
            return true;
        }

        public static bool XoaDeThi(string maDe, out string thongBao)
        {
            thongBao = "";

            if (!HopLe(maDe))
            {
                thongBao = "Ma de khong hop le.";
                return false;
            }

            lock (KhoaFile)
            {
                if (LayCauHoiTheoMaDe(maDe).Count > 0)
                {
                    thongBao = "Hay xoa cac cau hoi cua de truoc.";
                    return false;
                }

                List<string> ds = DocDong("DeThi.txt").ToList();

                int soLuongCu = ds.Count;

                ds = ds.Where(line =>
                {
                    string[] cot = line.Split('|');

                    return cot.Length != 5 ||
                        !cot[0].Trim().Equals(
                            maDe,
                            StringComparison.OrdinalIgnoreCase);
                }).ToList();

                if (ds.Count == soLuongCu)
                {
                    thongBao = "Khong tim thay de thi.";
                    return false;
                }

                GhiTatCa("DeThi.txt", ds);
            }

            thongBao = "Xoa de thi thanh cong.";
            return true;
        }

        // ==========================================
        // 4. DOC DANH SACH CAU HOI
        // CauHoi.txt: 8 truong
        // MaCauHoi|MaDe|NoiDung|A|B|C|D|DapAn
        // ==========================================

        public static List<CauHoi> LayDanhSachCauHoi()
        {
            List<CauHoi> danhSach = new List<CauHoi>();

            lock (KhoaFile)
            {
                string[] dong = DocDong("CauHoi.txt");

                foreach (string line in dong)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] cot = line.Split('|');

                    if (cot.Length != 8)
                        continue;

                    string dapAn = cot[7].Trim().ToUpperInvariant();

                    if (!"ABCD".Contains(dapAn) || dapAn.Length != 1)
                        continue;

                    danhSach.Add(new CauHoi
                    {
                        MaCauHoi = cot[0].Trim(),
                        MaDe = cot[1].Trim(),
                        NoiDung = cot[2].Trim(),
                        A = cot[3].Trim(),
                        B = cot[4].Trim(),
                        C = cot[5].Trim(),
                        D = cot[6].Trim(),
                        DapAn = dapAn
                    });
                }
            }

            return danhSach;
        }

        public static List<CauHoi> LayCauHoiTheoMaDe(string maDe)
        {
            return LayDanhSachCauHoi().Where(
                x => x.MaDe.Equals(
                    maDe,
                    StringComparison.OrdinalIgnoreCase
                )
            ).ToList();
        }

        public static CauHoi LayCauHoi(string maCauHoi)
        {
            return LayDanhSachCauHoi().FirstOrDefault(
                x => x.MaCauHoi.Equals(
                    maCauHoi,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }

        private static bool KiemTraCauHoi(CauHoi c, out string thongBao)
        {
            thongBao = "";

            if (c == null ||
                !HopLe(c.MaCauHoi) ||
                !HopLe(c.MaDe) ||
                !HopLe(c.NoiDung) ||
                !HopLe(c.A) ||
                !HopLe(c.B) ||
                !HopLe(c.C) ||
                !HopLe(c.D))
            {
                thongBao = "Vui long nhap day du thong tin cau hoi.";
                return false;
            }

            string dapAn = (c.DapAn ?? "").Trim().ToUpperInvariant();

            if (!new string[] { "A", "B", "C", "D" }.Contains(dapAn))
            {
                thongBao = "Dap an dung phai la A, B, C hoac D.";
                return false;
            }

            if (LayDeThi(c.MaDe) == null)
            {
                thongBao = "De thi khong ton tai.";
                return false;
            }

            return true;
        }

        public static bool ThemCauHoi(CauHoi c, out string thongBao)
        {
            if (!KiemTraCauHoi(c, out thongBao))
                return false;

            lock (KhoaFile)
            {
                if (LayCauHoi(c.MaCauHoi) != null)
                {
                    thongBao = "Ma cau hoi da ton tai.";
                    return false;
                }

                string line = string.Join("|", new string[]
                {
                    c.MaCauHoi.Trim(),
                    c.MaDe.Trim(),
                    c.NoiDung.Trim(),
                    c.A.Trim(),
                    c.B.Trim(),
                    c.C.Trim(),
                    c.D.Trim(),
                    c.DapAn.Trim().ToUpperInvariant()
                });

                File.AppendAllText(
                    LayDuongDan("CauHoi.txt"),
                    line + Environment.NewLine,
                    new UTF8Encoding(false)
                );
            }

            thongBao = "Them cau hoi thanh cong.";
            return true;
        }

        public static bool CapNhatCauHoi(CauHoi c, out string thongBao)
        {
            if (!KiemTraCauHoi(c, out thongBao))
                return false;

            lock (KhoaFile)
            {
                List<string> ds = DocDong("CauHoi.txt").ToList();
                bool timThay = false;

                for (int i = 0; i < ds.Count; i++)
                {
                    string[] cot = ds[i].Split('|');

                    if (cot.Length == 8 &&
                        cot[0].Trim().Equals(
                            c.MaCauHoi,
                            StringComparison.OrdinalIgnoreCase))
                    {
                        ds[i] = string.Join("|", new string[]
                        {
                            c.MaCauHoi.Trim(),
                            c.MaDe.Trim(),
                            c.NoiDung.Trim(),
                            c.A.Trim(),
                            c.B.Trim(),
                            c.C.Trim(),
                            c.D.Trim(),
                            c.DapAn.Trim().ToUpperInvariant()
                        });

                        timThay = true;
                        break;
                    }
                }

                if (!timThay)
                {
                    thongBao = "Khong tim thay cau hoi.";
                    return false;
                }

                GhiTatCa("CauHoi.txt", ds);
            }

            thongBao = "Cap nhat cau hoi thanh cong.";
            return true;
        }

        public static bool XoaCauHoi(string maCauHoi, out string thongBao)
        {
            thongBao = "";

            if (!HopLe(maCauHoi))
            {
                thongBao = "Ma cau hoi khong hop le.";
                return false;
            }

            lock (KhoaFile)
            {
                List<string> ds = DocDong("CauHoi.txt").ToList();

                int soLuongCu = ds.Count;

                ds = ds.Where(line =>
                {
                    string[] cot = line.Split('|');

                    return cot.Length != 8 ||
                        !cot[0].Trim().Equals(
                            maCauHoi,
                            StringComparison.OrdinalIgnoreCase);
                }).ToList();

                if (ds.Count == soLuongCu)
                {
                    thongBao = "Khong tim thay cau hoi.";
                    return false;
                }

                GhiTatCa("CauHoi.txt", ds);
            }

            thongBao = "Xoa cau hoi thanh cong.";
            return true;
        }

        // ==========================================
        // 5. LUU VA DOC KET QUA THI
        // KetQuaThi.txt: 9 truong
        // ==========================================

        public static bool LuuKetQua(
            KetQuaThi kq,
            out string thongBao)
        {
            thongBao = "";

            if (kq == null ||
                !HopLe(kq.TenDangNhap) ||
                !HopLe(kq.MaDe) ||
                !HopLe(kq.TenDe) ||
                kq.TongCau <= 0 ||
                kq.SoCauDung < 0 ||
                kq.SoCauSai < 0 ||
                kq.SoCauDung + kq.SoCauSai != kq.TongCau)
            {
                thongBao = "Thong tin ket qua khong hop le.";
                return false;
            }

            if (kq.NgayThi == DateTime.MinValue)
                kq.NgayThi = DateTime.Now;

            if (string.IsNullOrWhiteSpace(kq.MaKetQua))
                kq.MaKetQua = Guid.NewGuid().ToString("N");

            if (kq.Diem < 0 || kq.Diem > 10)
            {
                thongBao = "Diem thi phai tu 0 den 10.";
                return false;
            }

            lock (KhoaFile)
            {
                string line = string.Join("|", new string[]
                {
                    kq.MaKetQua,
                    kq.TenDangNhap.Trim(),
                    kq.MaDe.Trim(),
                    kq.TenDe.Trim(),
                    kq.NgayThi.ToString(
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture),
                    kq.TongCau.ToString(),
                    kq.SoCauDung.ToString(),
                    kq.SoCauSai.ToString(),
                    kq.Diem.ToString(
                        "0.##",
                        CultureInfo.InvariantCulture)
                });

                File.AppendAllText(
                    LayDuongDan("KetQuaThi.txt"),
                    line + Environment.NewLine,
                    new UTF8Encoding(false)
                );
            }

            thongBao = "Luu ket qua thanh cong.";
            return true;
        }

        public static List<KetQuaThi> LayTatCaKetQua()
        {
            List<KetQuaThi> ds = new List<KetQuaThi>();

            lock (KhoaFile)
            {
                foreach (string line in DocDong("KetQuaThi.txt"))
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] c = line.Split('|');

                    if (c.Length != 9)
                        continue;

                    DateTime ngay;
                    int tong, dung, sai;
                    decimal diem;

                    if (!DateTime.TryParseExact(
                        c[4].Trim(),
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out ngay))
                        continue;

                    if (!int.TryParse(c[5], out tong) ||
                        !int.TryParse(c[6], out dung) ||
                        !int.TryParse(c[7], out sai) ||
                        !decimal.TryParse(
                            c[8],
                            NumberStyles.Number,
                            CultureInfo.InvariantCulture,
                            out diem))
                        continue;

                    ds.Add(new KetQuaThi
                    {
                        MaKetQua = c[0].Trim(),
                        TenDangNhap = c[1].Trim(),
                        MaDe = c[2].Trim(),
                        TenDe = c[3].Trim(),
                        NgayThi = ngay,
                        TongCau = tong,
                        SoCauDung = dung,
                        SoCauSai = sai,
                        Diem = diem
                    });
                }
            }

            return ds;
        }

        public static List<KetQuaThi> LayKetQuaTheoTaiKhoan(
            string tenDangNhap)
        {
            return LayTatCaKetQua().Where(
                x => x.TenDangNhap.Equals(
                    tenDangNhap,
                    StringComparison.OrdinalIgnoreCase
                )
            ).OrderByDescending(x => x.NgayThi).ToList();
        }

        // ==========================================
        // 6. QUAN LY TAI KHOAN
        // TaiKhoan.txt: 9 truong
        // ==========================================

        public static List<TaiKhoan> LayDanhSachTaiKhoan()
        {
            List<TaiKhoan> ds = new List<TaiKhoan>();

            lock (KhoaFile)
            {
                foreach (string line in DocDong("TaiKhoan.txt"))
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] c = line.Split('|');

                    if (c.Length != 9)
                        continue;

                    ds.Add(new TaiKhoan
                    {
                        HoTen = c[0].Trim(),
                        TenDangNhap = c[1].Trim(),
                        MatKhau = c[2],
                        Email = c[3].Trim(),
                        Lop = c[4].Trim(),
                        QueQuan = c[5].Trim(),
                        SoThich = c[6].Trim(),
                        DongYDieuKhoan = c[7].Trim(),
                        Quyen = c[8].Trim()
                    });
                }
            }

            return ds;
        }

        public static bool CapNhatQuyenTaiKhoan(
            string tenDangNhap,
            string quyen,
            out string thongBao)
        {
            thongBao = "";

            if (!HopLe(tenDangNhap) ||
                (quyen != "User" && quyen != "Admin"))
            {
                thongBao = "Thong tin quyen khong hop le.";
                return false;
            }

            lock (KhoaFile)
            {
                List<string> ds = DocDong("TaiKhoan.txt").ToList();
                bool timThay = false;

                for (int i = 0; i < ds.Count; i++)
                {
                    string[] c = ds[i].Split('|');

                    if (c.Length == 9 &&
                        c[1].Trim().Equals(
                            tenDangNhap,
                            StringComparison.OrdinalIgnoreCase))
                    {
                        // Khong cho ha quyen Admin cuoi cung.
                        if (c[8].Trim() == "Admin" && quyen == "User")
                        {
                            int soAdmin = ds.Count(x =>
                            {
                                string[] t = x.Split('|');
                                return t.Length == 9 &&
                                    t[8].Trim() == "Admin";
                            });

                            if (soAdmin <= 1)
                            {
                                thongBao = "Khong the ha quyen Admin cuoi cung.";
                                return false;
                            }
                        }

                        c[8] = quyen;
                        ds[i] = string.Join("|", c);
                        timThay = true;
                        break;
                    }
                }

                if (!timThay)
                {
                    thongBao = "Khong tim thay tai khoan.";
                    return false;
                }

                GhiTatCa("TaiKhoan.txt", ds);
            }

            thongBao = "Cap nhat quyen thanh cong.";
            return true;
        }

        public static bool XoaTaiKhoan(
            string tenDangNhap,
            out string thongBao)
        {
            thongBao = "";

            if (!HopLe(tenDangNhap))
            {
                thongBao = "Ten dang nhap khong hop le.";
                return false;
            }

            lock (KhoaFile)
            {
                List<string> ds = DocDong("TaiKhoan.txt").ToList();

                string taiKhoan = ds.FirstOrDefault(x =>
                {
                    string[] c = x.Split('|');

                    return c.Length == 9 &&
                        c[1].Trim().Equals(
                            tenDangNhap,
                            StringComparison.OrdinalIgnoreCase);
                });

                if (taiKhoan == null)
                {
                    thongBao = "Khong tim thay tai khoan.";
                    return false;
                }

                string[] thongTin = taiKhoan.Split('|');

                if (thongTin[8].Trim() == "Admin")
                {
                    int soAdmin = ds.Count(x =>
                    {
                        string[] c = x.Split('|');
                        return c.Length == 9 && c[8].Trim() == "Admin";
                    });

                    if (soAdmin <= 1)
                    {
                        thongBao = "Khong the xoa Admin cuoi cung.";
                        return false;
                    }
                }

                ds.Remove(taiKhoan);
                GhiTatCa("TaiKhoan.txt", ds);
            }

            thongBao = "Xoa tai khoan thanh cong.";
            return true;
        }
    }
}
