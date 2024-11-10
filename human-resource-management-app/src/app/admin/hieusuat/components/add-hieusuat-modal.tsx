import HieuSuat from '@/models/hieuSuat';
import HieuSuatService from '@/services/hieuSuatService';
import employeeService from '@/services/employeeService';
import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import { Fragment, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import Employee from '@/models/employee';
import { hieuSuatSchema } from '@/schemas/hieuSuatSchema';


export default function AddHieuSuatModal({ isOpen, handleCancel, handleConfirm, onHieuSuatCreated } : AddHieuSuatModalProps) {

    const [employees, setEmployees] = useState<Employee[]>([]);

    const onSubmit = async (values: HieuSuat, actions: FormikHelpers<HieuSuat>) => {
        const response = await HieuSuatService.add(values);

        if (response?.statusCode === 201) {
            actions.resetForm();
            toast.success(response?.message);
            handleCancel();
            onHieuSuatCreated();
        } else toast.error(response.message)
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

    const formik = useFormik<HieuSuat>({
        initialValues: {
            "maHieuSuat": "string",
            "nguoiDanhGia": "",
            "kyDanhGia": "",
            "mucTieuHieuSuat": "",
            "danhGiaHieuSuat": "",
            "phanHoi": "",
            "keHoachPhatTrien": "",
            "nhanVienId": "default"
        },
        onSubmit,
        validationSchema: hieuSuatSchema
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
                                            Thêm thông tin chấm công
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
                                                    htmlFor="nguoiDanhGia"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Người đánh giá
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.nguoiDanhGia}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="nguoiDanhGia"
                                                        name="nguoiDanhGia"
                                                        placeholder='Vd: Hứa Biên'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.nguoiDanhGia && formik.touched.nguoiDanhGia && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.nguoiDanhGia}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="kyDanhGia"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Kì đánh giá
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.kyDanhGia}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="kyDanhGia"
                                                        name="kyDanhGia"
                                                        placeholder='Nhập kì đánh giá'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.kyDanhGia && formik.touched.kyDanhGia && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.kyDanhGia}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="mucTieuHieuSuat"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Mục tiêu hiệu suất
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.mucTieuHieuSuat}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="mucTieuHieuSuat"
                                                        name="mucTieuHieuSuat"
                                                        placeholder='Vd: Hoàn thành tốt nhiệm vụ'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.mucTieuHieuSuat && formik.touched.mucTieuHieuSuat && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.mucTieuHieuSuat}</p>}
                                                </div>

                                            </div>

                                            <div>
                                                <label
                                                    htmlFor="danhGiaHieuSuat"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Đánh giá hiệu suất
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.danhGiaHieuSuat}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="danhGiaHieuSuat"
                                                        name="danhGiaHieuSuat"
                                                        placeholder='Vd: Đạt 90% ...'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.danhGiaHieuSuat && formik.touched.danhGiaHieuSuat && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.danhGiaHieuSuat}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="phanHoi"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Phản hồi
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.phanHoi}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="phanHoi"
                                                        name="phanHoi"
                                                        placeholder='Nhập phản hồi ...'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.phanHoi && formik.touched.phanHoi && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.phanHoi}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="keHoachPhatTrien"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Kế hoạch phát triển
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.keHoachPhatTrien}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="keHoachPhatTrien"
                                                        name="keHoachPhatTrien"
                                                        placeholder='Vd: Tìm hiểu các công nghệ tài liệu mới ...'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.keHoachPhatTrien && formik.touched.keHoachPhatTrien && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.keHoachPhatTrien}</p>}
                                                </div>

                                            </div>

                                        </div>
                                        <button
                                            type='submit'
                                            className="uppercase w-full mt-8 py-3 px-4 inline-flex justify-center items-center gap-2 rounded-md border border-transparent font-semibold bg-green-600 hover:bg-green-700 text-white transition-all text-sm"
                                        >
                                            Thêm hiệu suất
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