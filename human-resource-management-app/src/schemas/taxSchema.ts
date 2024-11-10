import * as yup from 'yup';

export const taxSchema = yup.object().shape({
    soNguoiPhuThuoc: yup.number().required("Vui lòng nhập số người phụ thuộc"),
    trangThaiHonNhan: yup.string()
        .notOneOf(["default"], "Vui lòng chọn trạng thái hôn nhân")
        .required("Vui lòng nhập trạng thái hôn nhân"),
    thoiGianTinh: yup.date().required("Vui lòng nhập thời gian tính thuế"),
    nhanVienId: yup.string()
        .notOneOf(["default"], "Vui lòng chọn nhân viên")
        .required("Vui lòng chọn nhân viên"),

});