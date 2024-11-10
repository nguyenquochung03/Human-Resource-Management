import * as yup from 'yup'

export const chamCongSchema = yup.object().shape({
    ngayChamCong: yup.date().required("Vui lòng nhập ngày chấm công"),
    soGioLamViec: yup.number().required("Vui lòng nhập số giờ làm việc"),
    soGioLamThem: yup.number().required("Vui lòng nhập số giờ làm thêm"),
    soGioNghiPhep: yup.number().required("Vui lòng nhập số giờ nghỉ phép"),
    soGioNghiKhongPhep: yup.number().required("Vui lòng nhập số giờ nghỉ không phép"),
    tongGioLam: yup.number().required("Vui lòng nhập tổng số giờ làm"),
    nhanVienId: yup.string()
        .notOneOf(["default"], "Vui lòng chọn nhân viên")
        .required("Vui lòng chọn nhân viên"),
})

