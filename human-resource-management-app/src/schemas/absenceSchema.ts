import * as yup from 'yup';

const absenceSchema = yup.object().shape({
    loaiNghiVang: yup.string()
        .notOneOf(["default"], "Vui lòng chọn loại nghỉ vắng")
        .required('Vui lòng chọn loại nghỉ vắng'),
    ngayBatDau: yup.date().required('Vui lòng nhập ngày bắt đầu'),
    ngayKetThuc: yup.date().required('Vui lòng nhập ngày kết thúc'),
    lyDo: yup.string().required('Vui lòng nhập lý do'),
    ghiChu: yup.string().required('Vui lòng nhập ghi chú'),
    nhanVienId: yup.string()
        .notOneOf(["default"], "Vui lòng chọn nhân viên")
        .required("Vui lòng chọn nhân viên"),
});

export default absenceSchema;
