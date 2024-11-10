import * as yup from 'yup'

const payrollPeriodSchema = yup.object().shape({
    tenDotTraLuong: yup.string()
        .notOneOf(["default"], "Vui lòng chọn tên đợt trả lương")
        .required("Vui lòng chọn tên đợt trả lương"),
    thoiGian: yup.date().required("Vui lòng nhập thời gian đợt trả lương"),
    nguoiTraLuong: yup.string().required("Vui lòng nhập người trả lương"),
    ghiChu: yup.string().required("Vui lòng nhập ghi chú"),
});

export default payrollPeriodSchema;