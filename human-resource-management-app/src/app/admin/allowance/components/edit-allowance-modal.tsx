import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import { Fragment, useState } from 'react';
import toast from 'react-hot-toast';
import Allowance from '@/models/allowance';
import allowanceService from '@/services/allowanceService';
import { allowanceSchema } from '@/schemas/allowanceSchema';
import EditAllowanceModalProps from './edit-allowance-modal-prop';
import Loading from '@/components/Loading';


export default function EditAllowanceModal({ isOpen, handleCancel, handleConfirm, selectedAllowance, onAllowanceUpdated }: EditAllowanceModalProps) {
    const [isLoading, setIsLoading] = useState(false);
    const onSubmit = async (values: Allowance, actions: FormikHelpers<Allowance>) => {
        setIsLoading(true)
        const response = await allowanceService.edit(values.maPhuCap ,values);
        setIsLoading(false)
        if (response?.statusCode === 204) {
            actions.resetForm();
            toast.success(response?.message);
            handleCancel();
            onAllowanceUpdated();
        } else toast.error(response.message)
    }


    const formik = useFormik<Allowance>({
        initialValues: {
            "maPhuCap": "####",
            "tenPhuCap": "",
            "soTienPhuCap": 0,
            "tanSuat": "",
            "trangThai": "default",
            ...selectedAllowance
        },
        onSubmit,
        validationSchema: allowanceSchema
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
            {isLoading && <Loading />}
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
                                            Chỉnh sửa thông tin phụ cấp
                                        </Dialog.Title>
                                    </div>
                                    <form onSubmit={formik.handleSubmit} className="mt-5">
                                        <div className="grid grid-cols-2 gap-x-6 gap-y-4">
                                            <div>
                                                <label
                                                    htmlFor="tenPhuCap"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Tên phụ cấp
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.tenPhuCap}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="tenPhuCap"
                                                        name="tenPhuCap"
                                                        placeholder='Vd: Di chuyển'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.tenPhuCap && formik.touched.tenPhuCap && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.tenPhuCap}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="soTienPhuCap"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Số tiền phụ cấp
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.soTienPhuCap}
                                                        onChange={formik.handleChange}
                                                        type="number"
                                                        id="soTienPhuCap"
                                                        name="soTienPhuCap"
                                                        placeholder='Vd: 100000'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.soTienPhuCap && formik.touched.soTienPhuCap && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.soTienPhuCap}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="tanSuat"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Tần suất
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.tanSuat}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="tanSuat"
                                                        name="tanSuat"
                                                        placeholder='Nhập tần suất'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.tanSuat && formik.touched.tanSuat && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.tanSuat}</p>}
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
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    >
                                                        <option value="default">-----------------Chọn trạng thái----------------</option>
                                                        <option value="Đang hiệu lực">Đang hiệu lực</option>
                                                        <option value="Không hiệu lực">Không hiệu lực</option>
                                                    </select>
                                                    {formik.errors.trangThai && formik.touched.trangThai && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.trangThai}</p>}
                                                </div>

                                            </div>

                                        </div>
                                        <button
                                            type='submit'
                                            className="uppercase w-full mt-8 py-3 px-4 inline-flex justify-center items-center gap-2 rounded-md border border-transparent font-semibold bg-green-600 hover:bg-green-700 text-white transition-all text-sm"
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