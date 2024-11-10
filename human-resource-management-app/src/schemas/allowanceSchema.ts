import * as yup from 'yup';

export const allowanceSchema = yup.object().shape({
    tenPhuCap: yup.string().required("Vui lòng nhập tên phụ cấp"),
    soTienPhuCap: yup.number().required("Vui lòng nhập số tiền phụ cấp"),
    tanSuat: yup.string().required("Vui lòng nhập tần suất phụ cấp"),
    trangThai: yup.string()
        .notOneOf(["default"], "Vui lòng chọn trạng thái phụ cấp")
        .required("Vui lòng nhập trạng thái phụ cấp"),
});