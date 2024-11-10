import Department from '@/models/department';
import { departmentSchema } from '@/schemas/departmentSchema';
import departmentService from '@/services/departmentService';
import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import { Fragment } from 'react';
import toast from 'react-hot-toast';


export default function AddDepartmentModal({ isOpen, handleCancel, handleConfirm, onDepartmentCreated }: AddDepartmentModalProps) {

    const onSubmit = async (values: Department, actions: FormikHelpers<Department>) => {
        const response = await departmentService.add(values);

        if(response?.statusCode === 201) {
            actions.resetForm();
            toast.success(response?.message);
            handleCancel();
            onDepartmentCreated();
        }
    }

    const formik = useFormik<Department>({
        initialValues: {
            soPhong: "####",
            tenPhong: "",
            nguoiQuanLy: "",
            email: "",
            diaDiem: "",
            moTa: ""
        },
        onSubmit,
        validationSchema: departmentSchema
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
                                            Thêm phòng ban mới
                                        </Dialog.Title>
                                    </div>
                                    <form onSubmit={formik.handleSubmit} className="mt-5">
                                        <div className="flex flex-col gap-y-4">
                                            <div>
                                                <label
                                                    htmlFor="tenPhong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Tên phòng
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.tenPhong}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="tenPhong"
                                                        name="tenPhong"
                                                        placeholder='Vd: Phòng nhân sự'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.tenPhong && formik.touched.tenPhong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.tenPhong}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="nguoiQuanLy"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Tên trưởng phòng
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.nguoiQuanLy}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="nguoiQuanLy"
                                                        name="nguoiQuanLy"
                                                        placeholder='Vd: Nguyễn Duy Ben'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.nguoiQuanLy && formik.touched.nguoiQuanLy && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.nguoiQuanLy}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="email"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Email
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.email}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="email"
                                                        name="email"
                                                        placeholder='Vd: abc@gmail.com'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.email && formik.touched.email && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.email}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="diaDiem"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Địa điểm
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.diaDiem}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="diaDiem"
                                                        name="diaDiem"
                                                        placeholder='Vd: Quận 7'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.diaDiem && formik.touched.diaDiem && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.diaDiem}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="moTa"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Mô tả
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.moTa}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="moTa"
                                                        name="moTa"
                                                        placeholder='Vd: Phòng ban quản lí ...'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.moTa && formik.touched.moTa && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.moTa}</p>}
                                                </div>

                                            </div>

                                        </div>
                                        <button
                                            type='submit'
                                            className="uppercase w-full mt-8 py-3 px-4 inline-flex justify-center items-center gap-2 rounded-md border border-transparent font-semibold bg-green-600 hover:bg-green-700 text-white transition-all text-sm"
                                            onClick={handleConfirm}
                                        >
                                            Thêm phòng ban
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