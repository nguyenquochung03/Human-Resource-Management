import Department from '@/models/department';
import departmentService from '@/services/departmentService';
import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import React, { ChangeEventHandler, Fragment, useEffect, useState } from 'react'
import toast from 'react-hot-toast';
import AddSalaryPaymentModalProps from './add-salary-payment-modal-props';
import salaryPaymentService from '@/services/salaryPaymentService';
import { salaryPaymentSchema } from '@/schemas/salaryPaymentSchema';
import PayrollPeriod from '@/models/payrollPeriod';
import payrollPeriodService from '@/services/payrollPeriodService';
import Employee from '@/models/employee';
import employeeService from '@/services/employeeService';
import formatDateForInput from '@/utils/formatDateInput';


export default function AddSalaryPaymentModal({ isOpen, handleCancel, handleConfirm, onSalaryPaymentCreated }: AddSalaryPaymentModalProps) {

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
        console.log(values)
        const response = await salaryPaymentService.add(values);
        if (response?.statusCode === 201) {
            onSalaryPaymentCreated();
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
            "dotTraLuongId": "default"
        },
        onSubmit,
        validationSchema: salaryPaymentSchema
    });

    const handlePayrollPeriodChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        formik.handleChange(event);
        const { value } = event.target;
        if(formik.values.nhanVienId !== "default") {
            const response = await salaryPaymentService.findById(value, formik.values.nhanVienId);

            if(response.statusCode === 200) {
                formik.setFieldValue('soTienCanPhaiTra', response.data.soTienCanPhaiTra)
                formik.setFieldValue('soTienConPhaiTra', response.data.soTienConPhaiTra)
            }
        }
        
    }

    const handleEmployeeChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        formik.handleChange(event);
        const { value } = event.target;
        if(formik.values.dotTraLuongId !== "default") {
            const response = await salaryPaymentService.findById(formik.values.dotTraLuongId, value);
            console.log(response)
            if(response.statusCode === 200) {
                formik.setFieldValue('soTienCanPhaiTra', response.data.soTienCanPhaiTra - response.data.soTienDaTra)
                formik.setFieldValue('soTienConPhaiTra', response.data.soTienConPhaiTra)
            }
        }
        
    }

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
                                            Thêm thông tin trả lương
                                        </Dialog.Title>
                                    </div>
                                    <form onSubmit={formik.handleSubmit} className="mt-5">
                                        <div className="grid grid-cols-2 gap-x-4 gap-y-4">
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
                                                        onChange={handlePayrollPeriodChange}
                                                        id="dotTraLuongId"
                                                        name="dotTraLuongId"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
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
                                                    Chọn nhân viên
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formik.values.nhanVienId}
                                                        onChange={handleEmployeeChange}
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
                                                    htmlFor="soTienCanPhaiTra"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                   Số tiền đã trả
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.soTienCanPhaiTra}
                                                        onChange={formik.handleChange}
                                                        type="number"
                                                        readOnly
                                                        disabled
                                                        id="soTienCanPhaiTra"
                                                        name="soTienCanPhaiTra"
                                                        placeholder='Vd: 500000'
                                                        className="bg-slate-200 py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="soTienConPhaiTra"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                   Số tiền nợ
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.soTienConPhaiTra}
                                                        onChange={formik.handleChange}
                                                        readOnly
                                                        disabled
                                                        type="number"
                                                        id="soTienConPhaiTra"
                                                        name="soTienConPhaiTra"
                                                        placeholder='Vd: 500000'
                                                        className="bg-slate-200 py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="soTienDaTra"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                   Số tiền trả
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.soTienDaTra}
                                                        onChange={formik.handleChange}
                                                        type="number"
                                                        id="soTienDaTra"
                                                        name="soTienDaTra"
                                                        placeholder='Vd: 500000'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
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
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
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
                                                        placeholder='Vd: Trả lương cho nhân viên'
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
                                            THÊM MỚI
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