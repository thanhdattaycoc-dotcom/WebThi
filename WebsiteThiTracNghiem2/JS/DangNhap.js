
function kiemTraDangNhap() {
    var tenDangNhap = document.getElementById("txtTenDangNhap");
    var matKhau = document.getElementById("txtMatKhau");

    if (tenDangNhap.value.trim() === "") {
        alert("Vui lòng nhập tên đăng nhập!");
        tenDangNhap.focus();
        return false;
    }

    if (matKhau.value.trim() === "") {
        alert("Vui lòng nhập mật khẩu!");
        matKhau.focus();
        return false;
    }

    return true;
}
