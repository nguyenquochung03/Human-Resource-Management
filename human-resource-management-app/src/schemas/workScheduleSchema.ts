import * as yup from 'yup';

const workScheduleSchema = yup.object().shape({
    ngayLam: yup.date().required('Vui lòng nhập ngày làm'),
    gioBatDau: yup.string().required('Vui lòng nhập giờ bắt đầu'),
    gioKetThuc: yup.string().required('Vui lòng nhập giờ kết thúc'),
    loaiLich: yup.string()
        .notOneOf(["default"], "Vui lòng chọn loại lịch trình")
        .required("Vui lòng chọn loại lịch trình"),
    moTaCongViec: yup.string().required('Vui lòng nhập mô tả công việc'),
    ghiChu: yup.string().required('Vui lòng nhập ghi chú')
});

export default workScheduleSchema;