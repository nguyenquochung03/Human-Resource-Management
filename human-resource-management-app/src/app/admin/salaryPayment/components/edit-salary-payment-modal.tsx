import Department from '@/models/department';
import departmentService from '@/services/departmentService';
import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import React, { Fragment, useEffect, useState } from 'react'
import toast from 'react-hot-toast';
import AddSalaryPaymentModalProps from './add-salary-payment-modal-props';
import salaryPaymentService from '@/services/salaryPaymentService';
import { salaryPaymentSchema } from '@/schemas/salaryPaymentSchema';
import PayrollPeriod from '@/models/payrollPeriod';
import payrollPeriodService from '@/services/payrollPeriodService';
import Employee from '@/models/employee';
import employeeService from '@/services/employeeService';
import EditSalaryPaymentModalProps from './edit-salary-payment-modal-props';
import formatDateForInput from '@/utils/formatDateInput';


export default function EditSalaryPaymentModal({ isOpen, handleCancel, handleConfirm, selectedSalaryPayment, onSalaryPaymentUpdated }: EditSalaryPaymentModalProps) {

    const [payrollPeriods, setPayrollPeriods] = useState<PayrollPeriod[]>([]);
    const [employees, setEmployees] = useState<Employee[]>([]);

    const getDepartments = async () => {
        const response = await payrollPeriodService.findAll();
        if (response?.statusCode === 200) {
            setPayrollPeriods(response.data);
        }
    }

    const getEmployees = async () => {
        const response = await employeeService.findAll();
        if (response?.statusCode === 200) {
            setEmployees(response.data);
        }
    }

    useEffect(() => {
        getDepartments();
        getEmployees();
    }, [])


    const onSubmit = async (values: SalaryPayment, actions: FormikHelpers<SalaryPayment>) => {

        const response = await salaryPaymentService.edit(values.maTraLuong ,values);
        if (response?.statusCode === 204) {
            onSalaryPaymentUpdated();
            handleCancel();
            actions.resetForm();
            toast.success(response.message);
        } else toast.error(response.message);
    }

    const formik = useFormik<SalaryPayment>({
        initialValues: {
            "maTraLuong": "####",
            "trangThai": "",
            "soTienCanPhaiTra": 0,
            "soTienDaTra": 0,
            "soTienConPhaiTra": 0,
            "ngayTraLuong": new Date(),
            "ghiChu": "",
            "nhanVienId": "default",
            "dotTraLuongId": "default",
            ...selectedSalaryPayment
        },
        onSubmit,
        validationSchema: salaryPaymentSchema
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
                        <Dialog.Panel className="relative transform overflow-hidden rounded-lg bg-white text-left shadow-xl transition-all sm:my-4 sm:w-full sm:max-w-lg">
                            <div className="mt-2 rounded-xl shadow-lg">
                                <div className="p-4 sm:p-7">
                                    <div className="text-center">
                                        <Dialog.Title as="h1" className="block text-2xl font-bold text-gray-800">
                                            Chỉnh sửa thông tin trả lương
                                        </Dialog.Title>
                                    </div>
                                    <form onSubmit={formik.handleSubmit} className="mt-5">
                                        <div className="flex flex-col gap-y-4">
                                            <div>
                                                <label
                                                    htmlFor="dotTraLuongId"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Đợt trả lương
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formik.values.dotTraLuongId}
                                                        onChange={formik.handleChange}
                                                        id="phongBanId"
                                                        name="phongBanId"
                                                        className="py-3 px-4 block w-full border-2 border-slate-600 rounded-md text-sm"
                                                    >
                                                        <option value="default">------Chọn đợt trả lương------</option>
                                                        {payrollPeriods.map(payroll =>
                                                            <option value={payroll.maDotTraLuong} key={payroll.maDotTraLuong}>{payroll.tenDotTraLuong}</option>
                                                        )}
                                                    </select>
                                                    {formik.errors.dotTraLuongId && formik.touched.dotTraLuongId && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.dotTraLuongId}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="nhanVienId"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Đợt trả lương
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formik.values.nhanVienId}
                                                        onChange={formik.handleChange}
                                                        id="phongBanId"
                                                        name="phongBanId"
                                                        className="py-3 px-4 block w-full border-2 border-slate-600 rounded-md text-sm"
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
                                                    htmlFor="soTienDaTra"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                   Số tiền đã trả
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.soTienDaTra}
                                                        onChange={formik.handleChange}
                                                        type="number"
                                                        id="soTienDaTra"
                                                        name="soTienDaTra"
                                                        placeholder='Vd: 500000'
                                                        className="py-3 px-4 block w-full border-2 border-slate-600 rounded-md text-sm"
                                                    />
                                                    {formik.errors.soTienDaTra && formik.touched.soTienDaTra && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.soTienDaTra}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="ngayTraLuong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                   Ngày trả lương
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formatDateForInput(formik.values.ngayTraLuong.toString())}
                                                        onChange={formik.handleChange}
                                                        type="date"
                                                        id="ngayTraLuong"
                                                        name="ngayTraLuong"
                                                        placeholder='Vd: 500000'
                                                        className="py-3 px-4 block w-full border-2 border-slate-600 rounded-md text-sm"
                                                    />
                                                    {formik.errors.ngayTraLuong && formik.touched.ngayTraLuong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.ngayTraLuong as string}</p>}
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
                                                        placeholder='Vd: abc@gmail.com'
                                                        className="py-3 px-4 block w-full border-2 border-slate-600 rounded-md text-sm"
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