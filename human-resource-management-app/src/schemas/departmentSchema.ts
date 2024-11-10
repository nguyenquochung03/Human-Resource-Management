import * as yup from 'yup'

export const departmentSchema = yup.object().shape({
    tenPhong: yup.string().required("Vui lòng nhập tên phòng ban"),
    nguoiQuanLy: yup.string().required("Vui lòng nhập tên trưởng phòng"),
    email: yup.string().required("Vui lòng nhập email phòng ban")
        .email("Địa chỉ email không hợp lệ"),
    diaDiem: yup.string().required("Vui lòng nhập địa điểm của phòng ban"),
    moTa: yup.string().required("Vui lòng nhập mô tả về phòng ban"),
})