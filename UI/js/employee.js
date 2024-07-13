$(document).ready(function () {
    //load du lieu
    loadData();
    var flag = "edit"
    var idForUpdate = null;
    let highlightedRowId = null;

    //them
    $("#btnAdd").click(function () {
        flag = "add";
        //hien thi form them
        $("#dialog-them").find("input[type='text'], input[type='email'], input[type='date']").val("");
        $("#dialog-them").find("input[type='radio']").prop('checked', false);
        $("#dialog-them").show();
        //focus o ma nhan vien
        $("#txtMaNV").focus();
        $("#dialog-them input").removeClass("m-input-err");

    })
    //an form
    $("#btnClose").click(function () {
        $("#dialog-them").hide();
    })
    $("#btnHuy").click(function () {
        $("#dialog-them").hide();
    })


    //hien thi thong tin
    $(".m-table").on("dblclick", "tr", function () {
        flag = "edit";
        //lay doi tuong cua dong du lieu hien tai
        let employee = $(this).data("infor");
        console.log(employee);
        idForUpdate = employee.EmployeeId;
        $("#txtMaNV").val(employee.EmployeeCode);
        $("#txtHoTen").val(employee.FullName);
        $("#dtDateOfBirth").val(convertToDate(employee.DateOfBirth));
        $("#dtNgayCap").val(convertToDate(employee.IdentityDate));
        $("#txtSoCMT").val(employee.IdentityNumber);
        $("#txtNoiCap").val(employee.IdentityPlace);
        $("#txtDiaChi").val(employee.Address);
        $("#txtDTDD").val(employee.PhoneNumber);
        $("#txtEmail").val(employee.Email);
        $("input[name='gioi-tinh'][value='" + employee.Gender + "']").prop("checked", true);
        $("#dialog-them").show();
        $("#txtMaNV").focus();
        $("#dialog-them input").removeClass("m-input-err");
    })

    //xoa
    // Xử lý sự kiện khi nhấn vào nút xóa
    $(".m-table").on("click", "tr", function () {
        // Bỏ highlight dòng trước đó nếu có
        $(".m-table tr").removeClass("highlighted").find(".actions").removeClass("show");        
        // Highlight dòng hiện tại
        $(this).addClass("highlighted");
        $(this).find(".actions").addClass("show");

        // Lấy idForDelete từ dòng được highlight
        let employee = $(this).data("infor");
        idForDelete = employee.EmployeeId;
        
    });
    
    $(".m-table").on("click","#btn-xoa",function () {
        console.log(idForDelete);
        if (idForDelete) {
            // Hiển thị hộp thoại xác nhận
            if (confirm("Bạn có chắc chắn muốn xóa nhân viên này không?")) {
                // Thực hiện gọi API để xóa nhân viên
                $.ajax({
                    url: 'https://cukcuk.manhnv.net/api/v1/Employees/' + idForDelete,
                    type: 'DELETE',
                    success: function (response) {
                        // Nếu xóa thành công
                        alert("Nhân viên đã được xóa thành công!");
                        // Xóa dữ liệu khỏi bảng nếu cần
                        $(".m-table").find("tr[data-infor]").each(function () {
                            let employee = $(this).data("infor");
                            if (employee.EmployeeId === idForDelete) {
                                $(this).remove(); // Xóa hàng trong bảng
                                return false; // Thoát khỏi vòng lặp
                            }
                        });
                        // Đặt lại idForUpdate để tránh xóa sai đối tượng
                        idForUpdate = null;
                        // Xóa highlight
                        $(".m-table tr").removeClass("highlighted");
                        // Load lại dữ liệu
                        loadData();
                    },
                    error: function (xhr, status, error) {
                        // Xử lý lỗi nếu có
                        alert("Có lỗi xảy ra khi xóa nhân viên: " + error);
                    }
                });
            }
        } else {
            alert("Vui lòng chọn nhân viên để xóa!");
        }
    });
    


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
                for (const employee of response) {
                    //lay thong tin
                    index++;
                    let EmployeeCode = employee.EmployeeCode;
                    let FullName = employee.FullName;
                    let gender = employee.GenderName;
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
                    var el = $(`<tr>
                                <td class="text-align-left">${index = index < 10 ? `0${index}` : index}</td>
                                <td class="text-align-left">${EmployeeCode}</td>
                                <td class="text-align-left">${FullName}</td>
                                <td class="text-align-center">${gender}</td>
                                <td class="text-align-center">${dob}</td>
                                <td class="text-align-left">${email}</td>
                                <td class="text-align-left no-border-right">${diaChi}</td>   
                                <td>
                                    <div class="actions">
                                        <button id="btn-xoa" class="btn-delete"></button>                               
                                    </div>
                                </td>                
                            </tr>`);
                    el.data("infor", employee);
                    $("table#table-emp tbody").append(el);
                    $(".m-loading").hide();
                    $("#total").text(`${index}`);
                }
            },
            error: function (response) {
                var response = response.responseJSON;
                var errorMessage = response.errors.EmployeeCode ? response.errors.EmployeeCode[0] : 'Có lỗi xảy ra!';
                alert(errorMessage);
            }
        });
    }


    //them, sua nhan vien
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
        var GenderValue = $('input[name="gioi-tinh"]:checked').val();
        let GenderName = $("input[name='gioi-tinh']:checked").next('label').text();
        //build object
        let employee = {
            // "EmployeeId": maNV,
            "EmployeeCode": maNV,
            "FirstName": null,
            "LastName": null,
            "FullName": hoTen,
            "Gender": GenderValue,
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
        if (flag == "add") {
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
                error: function (response) {
                    var response = response.responseJSON;
                    var errorMessage = response.errors.EmployeeCode ? response.errors.EmployeeCode[0] : 'Có lỗi xảy ra!';
                    alert(errorMessage);
                    //an loading
                    $('.m-loading').hide();
                }

            });
        } else {
            //call api put
            $.ajax({
                type: "PUT",
                url: `https://cukcuk.manhnv.net/api/v1/Employees/${idForUpdate}`,
                data: JSON.stringify(employee),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    //an loading
                    $('.m-loading').hide();
                    //an form
                    $("#dialog-them").hide();
                    //thong bao thanh cong
                    $('#suc-toast .m-toast-text').text('Sửa thành công');
                    showToast('#suc-toast');
                    //load lai du lieu
                    loadData();
                },
                error: function (response) {
                    var response = response.responseJSON;
                    var errorMessage = response.errors.EmployeeCode ? response.errors.EmployeeCode[0] : 'Có lỗi xảy ra!';
                    alert(errorMessage);
                    //an loading
                    $('.m-loading').hide();
                }

            });
        }

    })

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
    //chuyen doi ngay thang de hien thi len input
    function convertToDate(dateString) {
        if (!dateString) return '';

        // Lấy phần ngày từ chuỗi ISO 8601
        let datePart = dateString.split('T')[0];

        // Đổi định dạng từ YYYY-MM-DD sang YYYY-MM-DD
        let [year, month, day] = datePart.split('-');

        // Trả về định dạng YYYY-MM-DD để hiển thị trên input type="date"
        return `${year}-${month}-${day}`;
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

        setTimeout(function () {
            toast.css('display', 'none');
            toast.fadeOut();
        }, 1500);
    }

    //an navbar
    $('#toggle-navbar').click(function () {
        $('#navbar').toggleClass('collapsed');
        $('.page-content').toggleClass('expanded');
    });

    // Cập nhật border-top khi trang tải lên, nếu nav bar đã bị thu gọn
    if ($('#navbar').hasClass('collapsed')) {
        $('#toggle-navbar').css('border-top', 'none');
    }
});
