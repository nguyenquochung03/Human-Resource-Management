import Employee from '@/models/employee';
import absenceSchema from '@/schemas/absenceSchema';
import absenceService from '@/services/absenceService';
import employeeService from '@/services/employeeService';
import formatDateForInput from '@/utils/formatDateInput';
import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import { Fragment, useEffect, useState } from 'react';
import toast from 'react-hot-toast';


export default function EditAbsenceModal({ isOpen, handleCancel, handleConfirm, selectedAbsence, onAbsenceUpdated }: EditAbsenceModalProps) {

    const [employees, setEmployees] = useState<Employee[]>([]);

    const onSubmit = async (values: Absence, actions: FormikHelpers<Absence>) => {
        console.log(values)
        const response = await absenceService.edit(values.maNghiVang, values);

        if (response?.statusCode === 204) {
            actions.resetForm();
            toast.success(response?.message);
            handleCancel();
            onAbsenceUpdated();
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

    const formik = useFormik<Absence>({
        initialValues: {
            "maNghiVang": "#####",
            "loaiNghiVang": "default",
            "ngayBatDau": new Date(),
            "ngayKetThuc": new Date(),
            "soNgayNghi": 0,
            "lyDo": "",
            "trangThai": "",
            "ghiChu": "",
            "nhanVienId": "default",
            ...selectedAbsence
        },
        onSubmit,
        validationSchema: absenceSchema
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
                                            Chỉnh sửa thông tin nghỉ vắng
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
                                                    htmlFor="loaiNghiVang"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Loại nghỉ vắng
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formatDateForInput(formik.values.loaiNghiVang.toString())}
                                                        onChange={formik.handleChange}
                                                        id="loaiNghiVang"
                                                        name="loaiNghiVang"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    >
                                                        <option value="default">--------Chọn loại nghỉ vắng--------</option>
                                                        <option value="Được trả lương">Được trả lương</option>
                                                        <option value="Không được trả lương">Không được trả lương</option>
                                                    </select>
                                                    {formik.errors.loaiNghiVang && formik.touched.loaiNghiVang && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.loaiNghiVang as string}</p>}
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

                                            {/* <div>
                                                <label
                                                    htmlFor="soNgayNghi"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Số ngày nghỉ
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.soNgayNghi}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="soNgayNghi"
                                                        name="soNgayNghi"
                                                        placeholder='Vd: Phòng ban quản lí ...'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.soNgayNghi && formik.touched.soNgayNghi && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.soNgayNghi}</p>}
                                                </div>

                                            </div> */}
                                            <div>
                                                <label
                                                    htmlFor="lyDo"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                   Lí do
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.lyDo}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="lyDo"
                                                        name="lyDo"
                                                        placeholder='Lí do'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.lyDo && formik.touched.lyDo && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.lyDo}</p>}
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