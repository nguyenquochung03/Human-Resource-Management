import ChamCong from '@/models/chamCong';
import Employee from '@/models/employee';
import employeeService from '@/services/employeeService';
import formatDateForInput from '@/utils/formatDateInput';
import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import { Fragment, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import contractService from '@/services/contractService';
import Contract from '@/models/contract';
import contractSchema from '@/schemas/contractSchema';
import EditContractModalProps from './edit-contract-modal-props';


export default function EditContractModal({ isOpen, handleCancel, handleConfirm, selectedContract, onContractUpdated }: EditContractModalProps) {

    const [employees, setEmployees] = useState<Employee[]>([]);

    const onSubmit = async (values: Contract, actions: FormikHelpers<Contract>) => {
        const response = await contractService.edit(values.maHopDong, values);

        if (response?.statusCode === 204) {
            actions.resetForm();
            toast.success(response?.message);
            handleCancel();
            onContractUpdated();
        } else {
            toast.error(response?.message);
        }
    }

    const getEmployees = async () => {
        const response = await employeeService.findAll();

        if (response?.statusCode === 200) {
            setEmployees(response.data)
        }
    }

    useEffect(() => {
        getEmployees();
    }, []);

    const formik = useFormik<Contract>({
        initialValues: {
            "maHopDong": "####",
            "loaiHopDong": "",
            "ngayBatDau": new Date(),
            "ngayKetThuc": new Date(),
            "thoiHanHopDong": "",
            "noiDungHopDong": "",
            "trangThaiHopDong": "",
            "ngayKy": new Date(),
            "ghiChu": "",
            "nhanVienId": "default",
            ...selectedContract
        },
        onSubmit,
        validationSchema: contractSchema
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
                        <Dialog.Panel className="relative transform overflow-hidden rounded-lg bg-white text-left shadow-xl transition-all sm:my-4 sm:w-full sm:max-w-2xl">
                            <div className="mt-2 rounded-xl shadow-lg">
                                <div className="p-4 sm:p-7">
                                    <div className="text-center">
                                        <Dialog.Title as="h1" className="block text-2xl font-bold text-gray-800">
                                            Chỉnh sửa thông tin hợp đồng
                                        </Dialog.Title>
                                    </div>
                                    <form onSubmit={formik.handleSubmit} className="mt-5">
                                        <div className="grid grid-cols-2 gap-x-6 gap-y-4">
                                            <div>
                                                <label
                                                    htmlFor="nhanVienId"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Chọn nhân viên
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formik.values.nhanVienId}
                                                        onChange={formik.handleChange}
                                                        id="nhanVienId"
                                                        name="nhanVienId"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    >
                                                        <option value="default">------Chọn nhân viên------</option>
                                                        {employees.map(employee =>
                                                            <option value={employee.maNhanVien} key={employee.maNhanVien}>{employee.hoTen}</option>
                                                        )}
                                                    </select>
                                                    {formik.errors.nhanVienId && formik.touched.nhanVienId && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.nhanVienId}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="loaiHopDong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Loại hợp đồng
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formik.values.loaiHopDong.toString()}
                                                        onChange={formik.handleChange}
                                                        id="loaiHopDong"
                                                        name="loaiHopDong"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    >
                                                        <option value="default">-----Chọn loại hợp đồng-------</option>
                                                        <option value="Hợp đồng lao động chính thức">Hợp đồng lao động chính thức</option>
                                                        <option value="Hợp đồng thử việc">Hợp đồng thử việc</option>
                                                      
                                                    </select>
                                                    {formik.errors.loaiHopDong && formik.touched.loaiHopDong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.loaiHopDong}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="noiDungHopDong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Nội dung hợp đồng
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.noiDungHopDong.toString()}
                                                        onChange={formik.handleChange}
                                                        type="tex"
                                                        id="noiDungHopDong"
                                                        name="noiDungHopDong"
                                                        placeholder='Nội dung hợp đồng'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.noiDungHopDong && formik.touched.noiDungHopDong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.noiDungHopDong}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="ngayBatDau"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Ngày bắt đầu
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formatDateForInput(formik.values.ngayBatDau.toString())}
                                                        onChange={formik.handleChange}
                                                        type="date"
                                                        id="ngayBatDau"
                                                        name="ngayBatDau"
                                                        placeholder='Vd: 7h30'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.ngayBatDau && formik.touched.ngayBatDau && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.ngayBatDau as string}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="ngayKetThuc"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Ngày kết thúc
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formatDateForInput(formik.values.ngayKetThuc.toString())}
                                                        onChange={formik.handleChange}
                                                        type="date"
                                                        id="ngayKetThuc"
                                                        name="ngayKetThuc"
                                                        placeholder='Vd: 7h30'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.ngayKetThuc && formik.touched.ngayKetThuc && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.ngayKetThuc as string}</p>}
                                                </div>

                                            </div>

                                            <div>
                                                <label
                                                    htmlFor="trangThaiHopDong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Trạng thái hợp đồng
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formik.values.trangThaiHopDong.toString()}
                                                        onChange={formik.handleChange}
                                                        id="trangThaiHopDong"
                                                        name="trangThaiHopDong"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    >
                                                        <option value="default">-----Chọn trạng thái hợp đồng-------</option>
                                                        <option value="Đang hiệu lực">Đang hiệu lực</option>
                                                        <option value="Hết hạn">Hết hạn</option>
                                                        <option value="Gia hạn">Gia hạn</option>
                                                        <option value="Đình chỉ">Đình chỉ</option>
                                                        <option value="Chấm dứt">Chấm dứt</option>
                                                    </select>
                                                    {formik.errors.trangThaiHopDong && formik.touched.trangThaiHopDong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.trangThaiHopDong}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="ngayKy"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Ngày kí
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formatDateForInput(formik.values.ngayKy.toString())}
                                                        onChange={formik.handleChange}
                                                        type="date"
                                                        id="ngayKy"
                                                        name="ngayKy"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.ngayKy && formik.touched.ngayKy && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.ngayKy as string}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="thoiHanHopDong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Thời hạn hợp đồng
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.thoiHanHopDong}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="thoiHanHopDong"
                                                        name="thoiHanHopDong"
                                                        placeholder='Nhập thời hạn hợp đồng'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.thoiHanHopDong && formik.touched.thoiHanHopDong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.thoiHanHopDong}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="ghiChu"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Ghi chú
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.ghiChu}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="ghiChu"
                                                        name="ghiChu"
                                                        placeholder='Nhập ghi chú'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.ghiChu && formik.touched.ghiChu && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.ghiChu}</p>}
                                                </div>

                                            </div>

                                        </div>
                                        <button
                                            type='submit'
                                            className="uppercase w-full mt-8 py-3 px-4 inline-flex justify-center items-center gap-2 rounded-md border border-transparent font-semibold bg-green-600 hover:bg-green-700 text-white transition-all text-sm"
                                            onClick={handleConfirm}
                                        >
                                            Lưu lại
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