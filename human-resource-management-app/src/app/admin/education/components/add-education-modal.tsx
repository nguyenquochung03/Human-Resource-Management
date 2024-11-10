import Employee from '@/models/employee';
import { chamCongSchema } from '@/schemas/chamCongSchema';
import employeeService from '@/services/employeeService';
import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import { Fragment, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import AddEducationModalProps from './add-education-modal-props';
import educationService from '@/services/educationService';
import { educationSchema } from '@/schemas/educationSchema';


export default function AddEducationModal({ isOpen, handleCancel, handleConfirm, onEducationCreated }: AddEducationModalProps) {

    const [employees, setEmployees] = useState<Employee[]>([]);

    const onSubmit = async (values: Education, actions: FormikHelpers<Education>) => {
        const response = await educationService.add(values);

        if (response?.statusCode === 201) {
            actions.resetForm();
            toast.success(response?.message);
            handleCancel();
            onEducationCreated();
        }  else toast.error(response?.message);
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

    const formik = useFormik<Education>({
        initialValues: {
            "maTrinhDo": "####",
            "tenTrinhDoHocVan": "",
            "chuyenNganh": "",
            "tenTruong": "",
            "namTotNghiep": new Date().getFullYear(),
            "bangCap": "",
            "nhanVienId": "default"
        },
        onSubmit,
        validationSchema: educationSchema
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
                                            Thêm trình độ học vấn
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
                                                    htmlFor="tenTrinhDoHocVan"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Tên trình độ
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.tenTrinhDoHocVan}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="tenTrinhDoHocVan"
                                                        name="tenTrinhDoHocVan"
                                                        placeholder='Vd: Phòng nhân sự'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.tenTrinhDoHocVan && formik.touched.tenTrinhDoHocVan && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.tenTrinhDoHocVan}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="chuyenNganh"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Chuyên ngành
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.chuyenNganh}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="chuyenNganh"
                                                        name="chuyenNganh"
                                                        placeholder='Vd: CNTT'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.chuyenNganh && formik.touched.chuyenNganh && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.chuyenNganh}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="tenTruong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Tên trường
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.tenTruong}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="tenTruong"
                                                        name="tenTruong"
                                                        min={formik.values.tenTruong}
                                                        placeholder='Vd: TDTU'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.tenTruong && formik.touched.tenTruong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.tenTruong}</p>}
                                                </div>

                                            </div>
                                            
                                            <div>
                                                <label
                                                    htmlFor="namTotNghiep"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Năm tốt nghiệp
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.namTotNghiep}
                                                        onChange={formik.handleChange}
                                                        type="number"
                                                        min={1950}
                                                        max={2500}
                                                        id="namTotNghiep"
                                                        name="namTotNghiep"
                                                        placeholder='Vd: Phòng ban quản lí ...'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.namTotNghiep && formik.touched.namTotNghiep && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.namTotNghiep}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="bangCap"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Bằng cấp
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.bangCap}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="bangCap"
                                                        name="bangCap"
                                                        placeholder='Vd: Cử nhân'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.bangCap && formik.touched.bangCap && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.bangCap}</p>}
                                                </div>

                                            </div>
                                           
                                           
                                        </div>
                                        <button
                                            type='submit'
                                            className="uppercase w-full mt-8 py-3 px-4 inline-flex justify-center items-center gap-2 rounded-md border border-transparent font-semibold bg-green-600 hover:bg-green-700 text-white transition-all text-sm"
                                            onClick={handleConfirm}
                                        >
                                            Thêm trình độ chuyên môn
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