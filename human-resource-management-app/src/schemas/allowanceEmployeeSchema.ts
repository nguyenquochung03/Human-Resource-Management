import * as yup from 'yup';

export const allowanceEmployeeSchema = yup.object().shape({
    phuCapId: yup.string()
        .notOneOf(["default"], "Vui lòng chọn phụ cấp")
        .required("Vui lòng chọn phụ cấp"),
    nhanVienIds: yup.array()
        .min(1, "Vui lòng chọn ít nhất một nhân viên")
        .required("Vui lòng chọn ít nhất một nhân viên"),
});