
document.addEventListener("DOMContentLoaded", function () {

    var oTimKiem = document.getElementById("txtTimKiemDeThi");
    var danhSach = document.getElementById("danhSachDeThi");
    var khongTimThay = document.getElementById("khongTimThay");
    var lblSoDeThi = document.getElementById("lblSoDeThi");

    if (!oTimKiem || !danhSach) {
        return;
    }

    var cacTheDeThi = danhSach.querySelectorAll(".exam-card");

    function TimKiemDeThi() {

        var tuKhoa = oTimKiem.value.trim().toLowerCase();
        var soLuongHienThi = 0;

        cacTheDeThi.forEach(function (the) {

            var noiDung = the.textContent.toLowerCase();

            if (noiDung.includes(tuKhoa)) {
                the.style.display = "";
                soLuongHienThi++;
            } else {
                the.style.display = "none";
            }

        });

        if (khongTimThay) {
            khongTimThay.style.display =
                soLuongHienThi === 0 ? "block" : "none";
        }

        if (lblSoDeThi) {
            lblSoDeThi.textContent = soLuongHienThi + " đề thi";
        }
    }

    oTimKiem.addEventListener("input", TimKiemDeThi);

});
