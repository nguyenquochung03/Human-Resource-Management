import * as yup from 'yup';

const contractSchema = yup.object().shape({
    maHopDong: yup.string().required('Vui lòng nhập mã hợp đồng'),
    loaiHopDong: yup.string()
        .notOneOf(["default"], "Vui lòng chọn loại hợp đồng")
        .required('Vui lòng nhập loại hợp đồng'),
    ngayBatDau: yup.date().required('Vui lòng nhập ngày bắt đầu hợp đồng'),
    ngayKetThuc: yup.date().required('Vui lòng nhập ngày kết thúc hợp đồng'),
    thoiHanHopDong: yup.string().required('Vui lòng nhập thời hạn hợp đồng'),
    noiDungHopDong: yup.string().required('Vui lòng nhập nội dung hợp đồng'),
    trangThaiHopDong: yup.string()
        .notOneOf(["default"], "Vui lòng chọn trạng thái hợp đồng")
        .required('Vui lòng chọn trạng thái hợp đồng'),
    ngayKy: yup.date().required('Vui lòng nhập ngày ký hợp đồng'),
    ghiChu: yup.string().required('Vui lòng nhập ghi chú'),
    nhanVienId: yup.string()
        .notOneOf(["default"], "Vui lòng chọn nhân viên")
        .required("Vui lòng chọn nhân viên"),
});

export default contractSchema;