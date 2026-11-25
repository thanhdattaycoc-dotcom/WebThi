
function kiemTraTaiKhoan() {
    var hoTen = document.getElementById(
        '<%= txtHoTen.ClientID %>'
    ).value.trim();

    var email = document.getElementById(
        '<%= txtEmail.ClientID %>'
    ).value.trim();

    var lop = document.getElementById(
        '<%= txtLop.ClientID %>'
    ).value.trim();

    var queQuan = document.getElementById(
        '<%= txtQueQuan.ClientID %>'
    ).value.trim();

    var soThich = document.getElementById(
        '<%= txtSoThich.ClientID %>'
    ).value.trim();

    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (hoTen === "") {
        alert("Vui lòng nhập họ và tên!");
        return false;
    }

    if (email === "" || !emailRegex.test(email)) {
        alert("Vui lòng nhập email hợp lệ!");
        return false;
    }

    if (lop === "") {
        alert("Vui lòng nhập lớp!");
        return false;
    }

    if (queQuan === "") {
        alert("Vui lòng nhập quê quán!");
        return false;
    }

    var danhSach = [hoTen, email, lop, queQuan, soThich];

    for (var i = 0; i < danhSach.length; i++) {
        if (danhSach[i].indexOf("|") !== -1) {
            alert("Thông tin không được chứa ký tự |!");
            return false;
        }
    }

    return true;
}
