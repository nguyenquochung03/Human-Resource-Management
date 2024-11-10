import * as yup from 'yup';

export const educationSchema = yup.object().shape({
    tenTrinhDoHocVan: yup.string().required("Vui lòng nhập tên trình độ học vấn"),
    chuyenNganh: yup.string().required("Vui lòng nhập chuyên ngành"),
    tenTruong: yup.string().required("Vui lòng nhập tên trường"),
    namTotNghiep: yup.number().required("Vui lòng nhập năm tốt nghiệp"),
    bangCap: yup.string().required("Vui lòng nhập bằng cấp"),
    nhanVienId: yup.string().required("Vui lòng nhập ID nhân viên"),
});