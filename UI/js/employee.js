$(document).ready(function(){

    //them
    $("#btnAdd").click(function(){
        //hien thi form them
        $("#dialog-them").show();
        //focus o ma nhan vien
        $("#txtMaNV").focus();
    })
    //an form
    $("#btnClose").click(function(){
        $("#dialog-them").hide();
    })
    $("#btnHuy").click(function(){
        $("#dialog-them").hide();
    })
    //hien thi thong tin
    $(".m-table tbody tr").dblclick(function(){
        $("#dialog-them").show();
    })
    //validate
    $("#btnSave").click(function(){
        let maNV = $("#txtMaNV").val();
        let hoTen = $("#txtHoTen").val();
        let ngaySinh = $("#dtDateOfBirth").val();
        let viTri = $("#cbViTri").val();
        let soCMT = $("#txtSoCMT").val();
        let ngayCap = $("#dtNgayCap").val();
        let phongBan = $("#cbPhongBan").val();
        let noiCap = $("#txtNoiCap").val();
        let diaChi = $("#txtDiaChi").val();
        let diDong = $("#txtDTDD").val();
        let coDinh = $("#txtDTCD").val();
        let email = $("#txtEnail").val();
        let tkNH = $("#txtTaiKhoanNH").val();
        let tenNK = $("#txtTenNH").val();
        let chiNhanh = $("#txtChiNhanh").val();

    })

    function validateInput(input){
        let value = $(input).val();
        if(value == null || value === ""){
            $(input).addClass("m-input-err");
            $(input).attr("title","Thông tin không được phép để trống!");

        }else{
            $(input).removeClass("m-input-err");
            $(input).removeAttr("title");
        }
    }

    //validate du lieu khi nhap vao o bat buoc
    $("input[required]").blur(function(){
        var point = this;
        validateInput(point)
    });

    //validate ngay thang
    validateDateInput('#dtDateOfBirth');
    validateDateInput('#dtNgayCap');
    function validateDateInput(selector) {
        $(selector).on('change blur', function() {
            var inputDate = new Date($(this).val());
            var currentDate = new Date();

            // Remove the time part from currentDate for comparison
            currentDate.setHours(0, 0, 0, 0);

            if (inputDate > currentDate) {
                $(this).attr("title", "Ngày không được phép lớn hơn ngày hiện tại!").addClass('m-input-err');
            } else {
                $(this).removeAttr("title").removeClass('m-input-err');
            }
        });
    }

    //validate email
    validateEmailInput('#txtEmail');
    function validateEmailInput(selector) {
        $(selector).on('change blur', function() {
            var email = $(this).val();
            var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; 

            if (email.trim() === '') {
                $(this).attr("title", "Email không được để trống!").addClass('m-input-err');
            } else if (!emailPattern.test(email)) {
                $(this).attr("title", "Email không hợp lệ!").addClass('m-input-err');
            } else {
                $(this).removeAttr("title").removeClass('m-input-err');
            }
        });
    }
});
  