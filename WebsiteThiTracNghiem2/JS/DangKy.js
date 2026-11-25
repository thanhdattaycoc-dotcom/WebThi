
    // ==========================================
    // 1. KIEM TRA DU LIEU DANG KY
    // ==========================================

    function kiemTraDangKy() {
        const hoTen = document.getElementById("txtHoTen").value.trim();
        const tenDangNhap = document.getElementById("txtTenDangNhap").value.trim();
        const matKhau = document.getElementById("txtMatKhau").value;
        const nhapLaiMatKhau = document.getElementById("txtNhapLaiMatKhau").value;
        const email = document.getElementById("txtEmail").value.trim();
        const queQuan = document.getElementById("ddlQueQuan").value;
        const soThich = document.getElementById("txtSoThich").value.trim();
        const tienAn = document.getElementById("txtTienAn").value.trim();
        const maSinhVien = document.getElementById("txtMaSinhVien").value.trim();

        const dongY = document.getElementById("chkDongYDieuKhoan").checked;

        const thongBao = document.getElementById("loiDangKyClient");

        thongBao.textContent = "";

        // Kiem tra thong tin bat buoc
        if (!hoTen || !tenDangNhap || !matKhau ||
            !email || !queQuan || !soThich) {
            return hienLoi("Vui lòng nhập đầy đủ thông tin bắt buộc.");
        }

        // Kiem tra email
        const mauEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (!mauEmail.test(email)) {
            return hienLoi("Email không đúng định dạng.");
        }

        // Kiem tra mat khau
        if (matKhau !== nhapLaiMatKhau) {
            return hienLoi("Mật khẩu nhập lại không khớp.");
        }

        // Lay du lieu lop
        const lop = document.getElementById("txtLop").value.trim();

        // Kiem tra lop
        if (lop.length < 5 || lop.length > 10) {
            return hienLoi("Lớp phải từ 5 đến 10 ký tự.");
        }

        const mauLop = /^\d{2}[a-zA-Z0-9]{3,8}$/;

        if (!mauLop.test(lop)) {
            return hienLoi("Hai ký tự đầu phải là số, các ký tự còn lại là chữ hoặc số.");
        }

        // Kiem tra so thich
        if (soThich.length < 5 || soThich.length > 20) {
            return hienLoi("Sở thích phải từ 5 đến 20 ký tự.");
        }

        const mauSoThich = /^[\p{L} ]+$/u;

        if (!mauSoThich.test(soThich.normalize("NFC"))) {
            return hienLoi("Sở thích chỉ được chứa chữ và dấu cách.");
        }
        // Tiền ăn
        console.log("Hàm đã chạy! Giá trị tienAn là:", tienAn);
        const soTienAn = Number(tienAn);
        if (isNaN(soTienAn) || soTienAn < 10000 || soTienAn > 200000) {
            return hienLoi("Tiền ăn phải từ 10.000 đến 200.000 (VND).");
        }
        // Mã Sinh Viên
        if (maSinhVien.length < 5 || maSinhVien.length > 20) {
            return hienLoi("Mã Sinh Viên phải từ 5 đến 20 ký tự.");
        }
        if (!/^[a-zA-Z0-9]+$/.test(maSinhVien)) {
            return hienLoi("Mã Sinh Viên chỉ được chứa chữ và số.");

            // Kiem tra dieu khoan
            if (!dongY) {
                return hienLoi("Bạn cần đồng ý với điều khoản.");
            }

            return true;
        }

        // ==========================================
        // 2. HIEN THI LOI
        // ==========================================

        function hienLoi(noiDung) {
            const thongBao = document.getElementById("loiDangKyClient");

            thongBao.textContent = noiDung;

            return false;
        }

        // ==========================================
        // 3. AN / HIEN MAT KHAU
        // ==========================================

        function doiHienThiMatKhau(id) {
            const oMatKhau = document.getElementById(id);

            if (oMatKhau.type === "password") {
                oMatKhau.type = "text";
            } else {
                oMatKhau.type = "password";
            }
        }
    }



    /* Mới làm*/


    function kiemTraTenDangNhap() {

        var tenDangNhap = document.getElementById("txtTenDangNhap").value.trim();

        // Đúng 10 ký tự và có chứa ký tự @
        var regex = /^(?=.*@).{10}$/;

        if (!regex.test(tenDangNhap)) {
            alert("Tên đăng nhập phải đúng 10 ký tự và chứa ký tự @!");
            return false;
        }

        return true;
    }


    //^ Bắt đầu chuỗi
    //    (?=.* @)	Chuỗi phải chứa ký tự @
    //.Đại diện cho một ký tự bất kỳ
    //{ 10 }	Đúng 10 ký tự
    //$	Kết thúc chuỗi