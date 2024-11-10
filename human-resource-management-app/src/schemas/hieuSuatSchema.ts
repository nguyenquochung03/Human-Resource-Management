import * as yup from 'yup'

export const hieuSuatSchema = yup.object().shape({
    nguoiDanhGia: yup.string().required("Vui lòng nhập người đánh giá"),
    kyDanhGia: yup.string().required("Vui lòng nhập kì đánh giá"),
    mucTieuHieuSuat: yup.string().required("Vui lòng nhập mục tiêu hiệu suất"),
    danhGiaHieuSuat: yup.string().required("Vui lòng nhập đánh giá hiệu suất"),
    phanHoi: yup.string().required("Vui lòng nhập phản hồi"),
    keHoachPhatTrien: yup.string().required("Vui lòng nhập kế hoạch phát triển"),
    nhanVienId: yup.string()
        .notOneOf(["default"], "Vui lòng chọn nhân viên")
        .required("Vui lòng chọn nhân viên"),
})

