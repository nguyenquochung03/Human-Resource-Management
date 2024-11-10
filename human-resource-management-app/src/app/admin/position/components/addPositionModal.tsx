import Department from '@/models/department';
import Position from '@/models/position';
import { positionSchema } from '@/schemas/positionSchema';
import departmentService from '@/services/departmentService';
import positionService from '@/services/positionService';
import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import React, { Fragment, useEffect, useState } from 'react'
import toast from 'react-hot-toast';


export default function AddPositionModal({ isOpen, handleCancel, handleConfirm, onPositionAdded } : Readonly<AddPositionModalProps>) {

    const [departments, setDepartments] = useState<Department[]>([]);

    const getDepartments = async () => {
        const response = await departmentService.findAll();
        if(response?.statusCode === 200) {
            setDepartments(response.data);
        }
    }

    useEffect(() => {
        getDepartments();
    }, [])


    const onSubmit = async (values: Position, actions: FormikHelpers<Position>) => {
        if(values.phongBanId == "default") {
            toast.error("Vui lòng chọn phòng ban")
        } else {

            const response = await positionService.add(values);
            if(response?.statusCode === 201) {
                onPositionAdded();
                handleCancel();
                actions.resetForm();
                toast.success(response.message);
            } else toast.error(response.message);
        }
    }

    const formik = useFormik<Position>({
        initialValues: {
            "maChucVu": "###",
            "tenChucVu": "",
            "mucLuong": 0,
            "moTaCongViec": "",
            "phongBanId": ""
        },
        onSubmit,
        validationSchema: positionSchema
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
                                            Thêm chức vụ mới
                                        </Dialog.Title>
                                    </div>
                                    <form onSubmit={formik.handleSubmit} className="mt-5">
                                        <div className="flex flex-col gap-y-4">
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
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
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
                                                    htmlFor="tenChucVu"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Tên chức vụ
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.tenChucVu}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="tenChucVu"
                                                        name="tenChucVu"
                                                        placeholder='Vd: Thư kí'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.tenChucVu && formik.touched.tenChucVu && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.tenChucVu}</p>}
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
                                                        min={0}
                                                        id="mucLuong"
                                                        name="mucLuong"
                                                        placeholder='Vd: abc@gmail.com'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.mucLuong && formik.touched.mucLuong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.mucLuong}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="moTaCongViec"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Mô tả công việc
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.moTaCongViec}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="moTaCongViec"
                                                        name="moTaCongViec"
                                                        placeholder='Vd: Quản lí nhân viên...'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.moTaCongViec && formik.touched.moTaCongViec && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.moTaCongViec}</p>}
                                                </div>

                                            </div>

                                        </div>
                                        <button
                                            type='submit'
                                            className="uppercase w-full mt-8 py-3 px-4 inline-flex justify-center items-center gap-2 rounded-md border border-transparent font-semibold bg-green-600 hover:bg-green-700 text-white transition-all text-sm"
                                            onClick={handleConfirm}
                                        >
                                            THÊM CHỨC VỤ
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