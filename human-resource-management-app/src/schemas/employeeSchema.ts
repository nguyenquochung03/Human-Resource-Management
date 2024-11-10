import * as yup from 'yup';

export const employeeSchema = yup.object().shape({
    maNhanVien: yup.string().required("Vui lòng nhập mã nhân viên"),
    hoTen: yup.string().required("Vui lòng nhập họ và tên"),
    ngaySinh: yup.date().required("Vui lòng nhập ngày sinh"),
    gioiTinh: yup.string()
        .notOneOf(["default"], "Vui lòng chọn giới tính")
        .required("Vui lòng chọn giới tính"),
    cccd: yup.string().required("Vui lòng nhập số CCCD").length(12, 'CCCD phải đủ 12 số'),
    tonGiao: yup.string().required("Vui lòng nhập tôn giáo"),
    diaChi: yup.string().required("Vui lòng nhập địa chỉ"),
    soDienThoai: yup.string().required("Vui lòng nhập số điện thoại").length(10, "SDT phải gồm đủ 10 số"),
    email: yup.string().required("Vui lòng nhập email").email("Địa chỉ email không hợp lệ"),
    ngayVaoLam: yup.date().required("Vui lòng nhập ngày vào làm"),
    mucLuong: yup.number().required("Vui lòng nhập mức lương").min(1, "Mức lương không được bằng 0"),
    soNguoiPhuThuoc: yup.number().required("Vui lòng nhập số người phụ thuộc"),
    trangThaiHonNhan: yup.string()
        .notOneOf(["default"], "Vui lòng chọn trạng thái hôn nhân")
        .required("Vui lòng nhập trạng thái hôn nhân"),
    trangThai: yup.string()
        .notOneOf(["default"], "Vui lòng chọn trạng thái")
        .required("Vui lòng chọn trạng thái"),
    phongBanId: yup.string()
        .notOneOf(["default"], "Vui lòng chọn phòng ban")
        .required("Vui lòng chọn phòng ban"),
    chucVuId: yup.string()
        .notOneOf(["default"], "Vui lòng chọn chức vụ")
        .required("Vui lòng chọn chức vụ")
});