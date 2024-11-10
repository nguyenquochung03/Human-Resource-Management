import * as yup from 'yup'

export const positionSchema = yup.object().shape({
    tenChucVu: yup.string().required("Vui lòng nhập tên chức vụ"),
    mucLuong: yup.number().required("Vui lòng nhập mức lương")
        .min(0, "Mức lương không được nhỏ hơn 0"),
    moTaCongViec: yup.string().required("Vui lòng nhập mô tả chức vụ"),
    phongBanId: yup.string().required("Vui lòng chọn phòng ban"),
})