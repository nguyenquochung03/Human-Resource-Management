
import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import React, { Fragment, useState } from 'react'
import toast from 'react-hot-toast';
import AddWorkScheduleModal from './add-work-schedule-modal-props';
import WorkSchedule from '@/models/workSchedule';
import workScheduleService from '@/services/workScheduleService';
import workScheduleSchema from '@/schemas/workScheduleSchema';
import formatDateForInput from '@/utils/formatDateInput';
import Loading from '@/components/Loading';

const NGHI_LE = "Nghỉ lễ";
const LAM_VIEC = "Làm việc";


export default function AddWorkSchedule({ isOpen, handleCancel, handleConfirm, onWorkScheduleCreated }: Readonly<AddWorkScheduleModal>) {
    const [isLoading, setIsLoading] = useState(false)

    const onSubmit = async (values: WorkSchedule, actions: FormikHelpers<WorkSchedule>) => {

        setIsLoading(true)
        const response = await workScheduleService.add(values);
        setIsLoading(false)
        if (response?.statusCode === 201) {
            onWorkScheduleCreated();
            handleCancel();
            actions.resetForm();
            toast.success(response.message);
        } else toast.error(response.message);
    }

    const formik = useFormik<WorkSchedule>({
        initialValues: {
            "maLichLam": "#####",
            "ngayLam": new Date(),
            "gioBatDau": "08:00",
            "gioKetThuc": "17:00",
            "moTaCongViec": "",
            "ghiChu": "",
            "loaiLich": "default",
        },
        onSubmit,
        validationSchema: workScheduleSchema
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
                {isLoading && <Loading />}
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
                                            Thêm lịch trình mới
                                        </Dialog.Title>
                                    </div>
                                    <form onSubmit={formik.handleSubmit} className="mt-5">
                                        <div className="flex flex-col gap-y-4">
                                            <div>
                                                <label
                                                    htmlFor="ngayLam"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Chọn ngày
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formatDateForInput(formik.values.ngayLam.toString())}
                                                        onChange={formik.handleChange}
                                                        type='date'
                                                        id="ngayLam"
                                                        name="ngayLam"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.ngayLam && formik.touched.ngayLam && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.ngayLam as string}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="loaiLich"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Chọn loại lịch trình
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={formik.values.loaiLich}
                                                        onChange={formik.handleChange}
                                                        id="loaiLich"
                                                        name="loaiLich"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    >
                                                        <option value="default">-----Chọn loại lịch trình-------</option>
                                                        <option value={LAM_VIEC}>Làm việc</option>
                                                        <option value={NGHI_LE}>Nghỉ lễ</option>
                                                    </select>
                                                    {formik.errors.loaiLich && formik.touched.loaiLich && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.loaiLich}</p>}
                                                </div>

                                            </div>

                                            {formik.values.loaiLich !== NGHI_LE && <div>
                                                <label
                                                    htmlFor="gioBatDau"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Giờ bắt đầu
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.gioBatDau}
                                                        onChange={formik.handleChange}
                                                        type="time"
                                                        id="gioBatDau"
                                                        disabled={formik.values.loaiLich === NGHI_LE}
                                                        name="gioBatDau"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.gioBatDau && formik.touched.gioBatDau && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.gioBatDau}</p>}
                                                </div>

                                            </div>}
                                            {formik.values.loaiLich !== NGHI_LE && <div>
                                                <label
                                                    htmlFor="gioKetThuc"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Giờ kết thúc
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.gioKetThuc}
                                                        onChange={formik.handleChange}
                                                        type="time"
                                                        id="gioKetThuc"
                                                        disabled={formik.values.loaiLich === NGHI_LE}
                                                        name="gioKetThuc"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.gioKetThuc && formik.touched.gioKetThuc && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.gioKetThuc}</p>}
                                                </div>

                                            </div>}


                                            <div>
                                                <label
                                                    htmlFor="moTaCongViec"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    {formik.values.loaiLich === NGHI_LE ? 'Mô tả ngày lễ' : ' Mô tả công việc'}
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.moTaCongViec}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="moTaCongViec"
                                                        name="moTaCongViec"
                                                        placeholder={`${formik.values.loaiLich === NGHI_LE ? 'Mô tả ngày lễ' : ' Mô tả công việc'}`}
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.moTaCongViec && formik.touched.moTaCongViec && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.moTaCongViec}</p>}
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
                                            THÊM LỊCH TRÌNH
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