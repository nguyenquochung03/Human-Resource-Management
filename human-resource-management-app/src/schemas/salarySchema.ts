import * as yup from 'yup';

export const salarySchema = yup.object().shape({
    
    thoiGian: yup.date().required("Vui lòng nhập năm tính lương"),
    nhanVienId: yup.string()
        .notOneOf(["default"], "Vui lòng chọn nhân viên")
        .required("Vui lòng chọn nhân viên"),
});
