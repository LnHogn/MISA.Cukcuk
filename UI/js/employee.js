$(document).ready(function () {
    //load du lieu
    loadData();
    //them
    $("#btnAdd").click(function () {
        //hien thi form them
        $("#dialog-them").show();
        //focus o ma nhan vien
        $("#txtMaNV").focus();
    })
    //an form
    $("#btnClose").click(function () {
        $("#dialog-them").hide();
    })
    $("#btnHuy").click(function () {
        $("#dialog-them").hide();
    })
    //hien thi thong tin
    $(".m-table tbody tr").dblclick(function () {
        $("#dialog-them").show();
    })
    //validate
    $("#btnSave").click(function () {
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
        let email = $("#txtEmail").val();
        let tkNH = $("#txtTaiKhoanNH").val();
        let tenNK = $("#txtTenNH").val();
        let chiNhanh = $("#txtChiNhanh").val();
        let gioiTinh = $("input[name='gioi-tinh']:checked").val();
        let GenderName = $("input[name='gioi-tinh']:checked").next('label').text();
        //build object
        let employee = {
            "EmployeeId": "13414164-3eaf-38d9-6f99-04b3a3d51ef2",
            "EmployeeCode": maNV,
            "FirstName": "Đinh Việt",
            "LastName": "Hằng",
            "FullName": hoTen,
            "Gender": gioiTinh,
            "DateOfBirth": ngaySinh,
            "PhoneNumber": diDong,
            "Email": email,
            "Address": diaChi,
            "IdentityNumber": soCMT,
            "IdentityDate": ngayCap,
            "IdentityPlace": noiCap,
            "JoinDate": "2019-09-01T00:00:00",
            "MartialStatus": 4,
            "EducationalBackground": 4,
            "QualificationId": "3541ff76-58f0-6d1a-e836-63d5d5eff719",
            "DepartmentId": "3f8e6896-4c7d-15f5-a018-75d8bd200d7c",
            "PositionId": "589edf01-198a-4ff5-958e-fb52fd75a1d4",
            "NationalityId": "b5cf83af-f756-11ec-9b48-00163e06abee",
            "WorkStatus": null,
            "PersonalTaxCode": "4970676329",
            "Salary": 46845361.0,
            "PositionCode": null,
            "PositionName": null,
            "DepartmentCode": null,
            "DepartmentName": null,
            "QualificationName": null,
            "NationalityName": null,
            "GenderName": GenderName,
            "EducationalBackgroundName": "Cao đẳng",
            "MartialStatusName": "Góa",
            "CreatedDate": "1970-01-01T00:00:09",
            "CreatedBy": "Edra Brill",
            "ModifiedDate": "1992-05-11T06:55:38",
            "ModifiedBy": "Omar Hwang"
        }
        //loading
        $('.m-loading').show();
        //call api post
        $.ajax({
            type: "POST",
            url: "https://cukcuk.manhnv.net/api/v1/Employees",
            data: JSON.stringify(employee),
            dataType: "json",
            contentType: "application/json",
            success: function (response) {
                //an loading
                $('.m-loading').hide();
                //an form
                $("#dialog-them").hide();
                //thong bao thanh cong
                $('#suc-toast .m-toast-text').text('Thêm thành công');
                showToast('#suc-toast');
                //load lai du lieu
                loadData();
            },
            error: function(response){
                var response = response.responseJSON;
                var errorMessage = response.errors.EmployeeCode ? response.errors.EmployeeCode[0] : 'Có lỗi xảy ra!';
                alert(errorMessage);
                //an loading
                $('.m-loading').hide();
            }
            
        });
    })
    //load du lieu
    function loadData() {
        //clear du lieu
        $("table#table-emp tbody").empty();
        //loading
        $('.m-loading').show();
        //call api
        $.ajax({
            type: "GET",
            url: "https://cukcuk.manhnv.net/api/v1/Employees",
            success: function (response) {
                let index = 0;
                let sum = 0;
                for (const employee of response) {
                    //lay thong tin
                    index++;
                    sum++;
                    let EmployeeCode = employee.EmployeeCode;
                    let FullName = employee.FullName;
                    let gender = employee['GenderName'];
                    let dob = employee['DateOfBirth'];
                    let email = employee.Email;
                    let diaChi = employee.Address;
                    //dinh dang du lieu
                    if (dob) {
                        dob = new Date(dob);
                        let day = dob.getDate();
                        day = day < 10 ? `0${day}` : day;
                        let month = dob.getMonth() + 1;
                        month = month < 10 ? `0${month}` : month;
                        let year = dob.getFullYear();

                        dob = `${day}/${month}/${year}`;
                    }
                    //hien thi du lieu
                    var el = `<tr>
                                <td class="text-align-left">${index = index < 10 ? `0${index}` : index}</td>
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
                    $(".m-loading").hide();
                    $("#total").text(`${sum}`);
                }
            }
        });
    }
    //validate input
    function validateInput(input) {
        let value = $(input).val();
        if (value == null || value === "") {
            $(input).addClass("m-input-err");
            $(input).attr("title", "Thông tin không được phép để trống!");

        } else {
            $(input).removeClass("m-input-err");
            $(input).removeAttr("title");
        }
    }

    //validate du lieu khi nhap vao o bat buoc
    $("input[required]").blur(function () {
        var point = this;
        validateInput(point)
    });

    //validate ngay thang
    validateDateInput('#dtDateOfBirth');
    validateDateInput('#dtNgayCap');
    function validateDateInput(selector) {
        $(selector).on('change blur', function () {
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
        $(selector).on('change blur', function () {
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
    function showToast(toastId) {
        var toast = $(toastId);
        toast.css('display', 'flex');
        toast.fadeIn();

        setTimeout(function() {
            toast.css('display', 'none');
            toast.fadeOut();
        }, 1500);
    }
    
});
