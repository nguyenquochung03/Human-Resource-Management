import * as yup from 'yup';

export const bonusSchema = yup.object().shape({
    maKhoanThuong: yup.string().required("Vui lòng nhập mã khoản thưởng"),
    ngayKhenThuong: yup.date().required("Vui lòng nhập ngày khen thưởng"),
    soTienThuong: yup.number().required("Vui lòng nhập số tiền thưởng")
        .min(0, "Số tiền thưởng không được nhỏ hơn 0"),
    lyDoKhenThuong: yup.string().required("Vui lòng nhập lý do khen thưởng"),
});