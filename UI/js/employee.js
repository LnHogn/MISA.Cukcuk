$(document).ready(function(){
    //call api
    $.ajax({
        type: "GET",
        url: "https://cukcuk.manhnv.net/api/v1/Employees",
        success: function (response) {
            for (const employee of response) {
                //;ay thong tin
                let EmployeeCode = employee.EmployeeCode;
                let FullName = employee.FullName;
                let gender = employee['GenderName'];
                let dob = employee['DateOfBirth'];
                let email = employee.Email;
                let diaChi = employee.Address;
                //dinh dang du lieu
                if(dob){
                    dob = new Date(dob);
                    let day = dob.getDate();
                    day = day<10?`0${day}`:day;
                    let month = dob.getMonth()+1;
                    month = month<10?`0${month}`:month;
                    let year = dob.getFullYear();

                    dob = `${day}/${month}/${year}`;
                }
                //hien thi du lieu
                var el = `<tr>
                                <td class="text-align-left">001</td>
                                <td class="text-align-left">${EmployeeCode}</td>
                                <td class="text-align-left">${FullName}</td>
                                <td class="text-align-center">${gender}</td>
                                <td class="text-align-center">${dob}</td>
                                <td class="text-align-left">${email}</td>
                                <td class="text-align-left no-border-right">${diaChi}</td>
                                <td>
                                    <div class="actions">
                                        <button  id="btn-sua"  class="btn-edit"></button>
                                        <button id="btn-xoa" class="btn-delete"></button>                               
                                    </div>
                                </td>
                            </tr>`;
            $("table#table-emp tbody").append(el);                
            }            
        }
    });


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
  