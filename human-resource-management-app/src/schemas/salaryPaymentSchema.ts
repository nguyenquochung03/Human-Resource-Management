import * as yup from 'yup';

export const salaryPaymentSchema = yup.object().shape({
    soTienDaTra: yup.number().required("Vui lòng nhập số tiền đã trả"),
    ngayTraLuong: yup.date().required("Vui lòng nhập ngày trả lương"),
    ghiChu: yup.string().required("Vui lòng nhập ghi chú"),
    nhanVienId: yup.string()
        .notOneOf(["default"], "Vui lòng chọn nhân viên")
        .required("Vui lòng chọn nhân viên"),
    dotTraLuongId: yup.string()
        .notOneOf(["default"], "Vui lòng chọn đợt trả lương")
        .required("Vui lòng chọn đợt trả lương"),
});
