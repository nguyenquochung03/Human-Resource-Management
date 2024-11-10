import Department from '@/models/department';
import Employee from '@/models/employee';
import Position from '@/models/position';
import { employeeSchema } from '@/schemas/employeeSchema';
import departmentService from '@/services/departmentService';
import employeeService from '@/services/employeeService';
import positionService from '@/services/positionService';
import formatDateForInput from '@/utils/formatDateInput';
import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import React, { Fragment, useEffect, useState } from 'react'
import toast from 'react-hot-toast';


export default function EditEmployeeModal({ isOpen, handleCancel, handleConfirm, selectedEmployee, onEmployeeUpdated }: Readonly<EditEmployeeModalProps>) {

    const [departments, setDepartments] = useState<Department[]>([]);
    const [positions, setPositions] = useState<Position[]>([]);

    const getDepartments = async () => {
        const response = await departmentService.findAll();
        if (response?.statusCode === 200) {
            setDepartments(response.data);
        }
    }

    const getPositions = async () => {
        const response = await positionService.findAll();
        if (response?.statusCode === 200) {
            setPositions(response.data);
        }
    }

    useEffect(() => {
        getDepartments();
        getPositions();
    }, [])


    const onSubmit = async (values: Employee, actions: FormikHelpers<Employee>) => {
        
        const response = await employeeService.edit(values.maNhanVien, values);
        if (response?.statusCode === 204) {
            onEmployeeUpdated();
            handleCancel();
            actions.resetForm();
            toast.success(response.message);
        } else toast.error(response.message);
    }

    const formik = useFormik<Employee>({
        initialValues: {
            "maNhanVien": "###",
            "hoTen": "",
            "ngaySinh": new Date(),
            "gioiTinh": "",
            "cccd": "",
            "tonGiao": "",
            "diaChi": "",
            "soDienThoai": "",
            "email": "",
            "ngayVaoLam": new Date(),
            "mucLuong": 0,
            "trangThai": "",
            "phongBanId": "",
            "chucVuId": "",
            "soNguoiPhuThuoc": '',
            "trangThaiHonNhan": '',
            ...selectedEmployee,
           
        },
        onSubmit,
        validationSchema: employeeSchema
    });

    return <Transition.Root show={isOpen} as={Fragment}>
        <Dialog as="div" className="relative z-10" onClose={handleCancel}>
            <Transition.Child
                as={Fragment}
                enter="ease-out duration-300"
                enterFrom="opacity-0"
                enterTo="opacity-100"
                leave="ease-in duration-200"
                leaveFrom="opacity-100"
                leaveTo="opacity-0"
            >
                <div className="fixed inset-0 bg-black bg-opacity-75 transition-opacity" />
            </Transition.Child>

            <div className="fixed inset-0 z-10 w-screen overflow-y-auto">
                <div className="flex min-h-full items-end justify-center p-4 text-center sm:items-center sm:p-0">
                    <Transition.Child
                        as={Fragment}
                        enter="ease-out duration-300"
                        enterFrom="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                        enterTo="opacity-100 translate-y-0 sm:scale-100"
                        leave="ease-in duration-200"
                        leaveFrom="opacity-100 translate-y-0 sm:scale-100"
                        leaveTo="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                    >
                        <Dialog.Panel className="relative transform overflow-hidden rounded-lg bg-white text-left shadow-xl transition-all sm:my-4 sm:w-full sm:max-w-4xl">
                            <div className="mt-2 rounded-xl shadow-lg">
                                <div className="p-4 sm:p-7">
                                    <div className="text-center">
                                        <Dialog.Title as="h1" className="block text-2xl font-bold text-gray-800">
                                            Chỉnh sửa thông tin nhân viên
                                        </Dialog.Title>
                                    </div>
                                    <form onSubmit={formik.handleSubmit} className="mt-5">
                                        <div className="grid grid-cols-3 gap-x-6 gap-y-4">
                                            <div>
                                                <label
                                                    htmlFor="maNhanVien"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Mã nhân viên
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.maNhanVien}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="maNhanVien"
                                                        name="maNhanVien"
                                                        readOnly
                                                       className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    />
                                                    {formik.errors.maNhanVien && formik.touched.maNhanVien && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.maNhanVien}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="phongBanId"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Phòng ban
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formik.values.phongBanId}
                                                        onChange={formik.handleChange}
                                                        id="phongBanId"
                                                        name="phongBanId"
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    >
                                                        <option value="default">------Chọn phòng ban------</option>
                                                        {departments.map(department =>
                                                            <option value={department.soPhong} key={department.soPhong}>{department.tenPhong}</option>
                                                        )}
                                                    </select>
                                                    {formik.errors.phongBanId && formik.touched.phongBanId && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.phongBanId}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="chucVuId"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Chức vụ
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formik.values.chucVuId}
                                                        onChange={formik.handleChange}
                                                        id="chucVuId"
                                                        name="chucVuId"
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    >
                                                        <option value="default">------Chọn chức vụ------</option>
                                                        {positions.map(pos =>
                                                            <option value={pos.maChucVu} key={pos.maChucVu}>{pos.tenChucVu}</option>
                                                        )}
                                                    </select>
                                                    {formik.errors.chucVuId && formik.touched.chucVuId && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.chucVuId}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="hoTen"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Họ và tên
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.hoTen}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="hoTen"
                                                        name="hoTen"
                                                        placeholder='Vd: Nguyễn Văn A'
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    />
                                                    {formik.errors.hoTen && formik.touched.hoTen && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.hoTen}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="ngaySinh"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Ngày sinh
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formatDateForInput(formik.values.ngaySinh.toString())}
                                                        onChange={formik.handleChange}
                                                        type="date"
                                                        id="ngaySinh"
                                                        name="ngaySinh"
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    />
                                                    {formik.errors.ngaySinh && formik.touched.ngaySinh && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.ngaySinh as string}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="gioiTinh"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Giới tính
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formik.values.gioiTinh}
                                                        onChange={formik.handleChange}
                                                        id="gioiTinh"
                                                        name="gioiTinh"
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    >
                                                        <option value="">------Chọn giới tính------</option>
                                                        <option value="Nam">Nam</option>
                                                        <option value="Nữ">Nữ</option>
                                                    </select>
                                                    {formik.errors.gioiTinh && formik.touched.gioiTinh && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.gioiTinh}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="cccd"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Căn cước công dân (12 số)
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.cccd}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="cccd"
                                                        name="cccd"
                                                        placeholder='Vd: 807807807956797808'
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    />
                                                    {formik.errors.cccd && formik.touched.cccd && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.cccd}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="tonGiao"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Tôn giáo
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.tonGiao}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="tonGiao"
                                                        name="tonGiao"
                                                        placeholder='Vd: Công giáo'
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    />
                                                    {formik.errors.tonGiao && formik.touched.tonGiao && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.tonGiao}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="diaChi"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Địa chỉ
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.diaChi}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="diaChi"
                                                        name="diaChi"
                                                        placeholder='Vd: Nguyễn Văn A'
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    />
                                                    {formik.errors.diaChi && formik.touched.diaChi && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.diaChi}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="soDienThoai"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Số điện thoại
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.soDienThoai}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="soDienThoai"
                                                        name="soDienThoai"
                                                        placeholder='Vd: 06786786786'
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    />
                                                    {formik.errors.soDienThoai && formik.touched.soDienThoai && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.soDienThoai}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="email"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Địa chỉ email
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.email}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="email"
                                                        name="email"
                                                        placeholder='Vd: abc@gmail.com'
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    />
                                                    {formik.errors.email && formik.touched.email && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.email}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="ngayVaoLam"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Ngày vào làm
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formatDateForInput(formik.values.ngayVaoLam.toString())}
                                                        onChange={formik.handleChange}
                                                        type="date"
                                                        id="ngayVaoLam"
                                                        name="ngayVaoLam"
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    />
                                                    {formik.errors.ngayVaoLam && formik.touched.ngayVaoLam && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.ngayVaoLam as string}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="mucLuong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Mức lương
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.mucLuong}
                                                        onChange={formik.handleChange}
                                                        type="number"
                                                        id="mucLuong"
                                                        name="mucLuong"
                                                        placeholder='Vd: Nguyễn Văn A'
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    />
                                                    {formik.errors.mucLuong && formik.touched.mucLuong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.mucLuong}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="trangThai"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Trạng thái
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formik.values.trangThai}
                                                        onChange={formik.handleChange}
                                                        id="trangThai"
                                                        name="trangThai"
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    >
                                                        <option value="default">------Chọn trạng thái------</option>
                                                        <option value="Nhân viên thực tập">Nhân viên thực tập</option>
                                                        <option value="Nhân viên chính thức">Nhân viên chính thức</option>
                                                    </select>
                                                    {formik.errors.trangThai && formik.touched.trangThai && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.trangThai}</p>}
                                                </div>

                                            </div>

                                        </div>
                                        <button
                                            type='submit'
                                            className="uppercase w-full mt-8 py-3 px-4 inline-flex justify-center items-center gap-2 rounded-md border border-transparent font-semibold bg-green-600 hover:bg-green-700 text-white transition-all text-sm"
                                            onClick={handleConfirm}
                                        >
                                            LƯU LẠI
                                        </button>
                                    </form>
                                </div>
                            </div>
                        </Dialog.Panel>
                    </Transition.Child>
                </div>
            </div>
        </Dialog>
    </Transition.Root>
}
