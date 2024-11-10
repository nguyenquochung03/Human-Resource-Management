import * as yup from 'yup';

export const computeSchema = yup.object().shape({
    thoiGianTinhLuong: yup.date().required("Vui lòng chọn thời gian tính lương"),
});
