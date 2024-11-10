import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import { Fragment } from 'react';
import toast from 'react-hot-toast';
import Allowance from '@/models/allowance';
import { allowanceSchema } from '@/schemas/allowanceSchema';
import bonusService from '@/services/bonusService';
import Bonus from '@/models/bonus';
import { bonusSchema } from '@/schemas/bonusSchema';
import formatDateForInput from '@/utils/formatDateInput';


export default function AddBonusModal({ isOpen, handleCancel, handleConfirm, onBonusCreated }: AddBonusModalProps) {

    const onSubmit = async (values: Bonus, actions: FormikHelpers<Bonus>) => {
        const response = await bonusService.add(values);

        if (response?.statusCode === 201) {
            actions.resetForm();
            toast.success(response?.message);
            handleCancel();
            onBonusCreated();
        } else toast.error(response.message)
    }


    const formik = useFormik<Bonus>({
        initialValues: {
            "maKhoanThuong": "#####",
            "ngayKhenThuong": new Date(),
            "soTienThuong": 0,
            "lyDoKhenThuong": ""
        },
        onSubmit,
        validationSchema: bonusSchema
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
                                            Thêm thưởng
                                        </Dialog.Title>
                                    </div>
                                    <form onSubmit={formik.handleSubmit} className="mt-5">
                                        <div className="grid grid-cols-2 gap-x-6 gap-y-4">
                                            <div>
                                                <label
                                                    htmlFor="ngayKhenThuong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Ngày khen thưởng
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formatDateForInput(formik.values.ngayKhenThuong.toString())}
                                                        onChange={formik.handleChange}
                                                        type="date"
                                                        id="ngayKhenThuong"
                                                        name="ngayKhenThuong"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.ngayKhenThuong && formik.touched.ngayKhenThuong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.ngayKhenThuong as string}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="soTienThuong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Số tiền thưởng
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.soTienThuong}
                                                        onChange={formik.handleChange}
                                                        type="number"
                                                        id="soTienThuong"
                                                        name="soTienThuong"
                                                        placeholder='Vd: 100000'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.soTienThuong && formik.touched.soTienThuong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.soTienThuong}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="lyDoKhenThuong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Lí do khen thưởng
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.lyDoKhenThuong}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="lyDoKhenThuong"
                                                        name="lyDoKhenThuong"
                                                        placeholder='Nhập lí do khen thưởng'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.lyDoKhenThuong && formik.touched.lyDoKhenThuong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.lyDoKhenThuong}</p>}
                                                </div>

                                            </div>
                                        </div>
                                        <button
                                            type='submit'
                                            className="uppercase w-full mt-8 py-3 px-4 inline-flex justify-center items-center gap-2 rounded-md border border-transparent font-semibold bg-green-600 hover:bg-green-700 text-white transition-all text-sm"
                                        >
                                            Thêm thưởng
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