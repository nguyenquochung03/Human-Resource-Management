import Loading from '@/components/Loading';
import ChamCong from '@/models/chamCong';
import Employee from '@/models/employee';
import { chamCongSchema } from '@/schemas/chamCongSchema';
import chamCongService from '@/services/chamCongService';
import employeeService from '@/services/employeeService';
import formatDateForInput from '@/utils/formatDateInput';
import { Dialog, Transition } from '@headlessui/react';
import { FormikHelpers, useFormik } from 'formik';
import { Fragment, useEffect, useState } from 'react';
import toast from 'react-hot-toast';


export default function AddChamCongModal({ isOpen, handleCancel, handleConfirm, onChamCongCreated }: AddChamCongModalProps) {

    const [employees, setEmployees] = useState<Employee[]>([]);
    const [isLoading, setIsLoading] = useState(false)

    const onSubmit = async (values: ChamCong, actions: FormikHelpers<ChamCong>) => {
        setIsLoading(true)
        const response = await chamCongService.add(values);
        setIsLoading(false)

        if (response?.statusCode === 201) {
            actions.resetForm();
            toast.success(response?.message);
            handleCancel();
            onChamCongCreated();
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

    const formik = useFormik<ChamCong>({
        initialValues: {
            "maChamCong": "string",
            "ngayChamCong": new Date(),
            "gioVaoLam": "07:00",
            "gioRaLam": "07:00",
            "soGioLamViec": 0,
            "soGioLamThem": 0,
            "soGioNghiPhep": 0,
            "soGioNghiKhongPhep": 0,
            "tongGioLam": 0,
            "nhanVienId": "default"
        },
        onSubmit,
        validationSchema: chamCongSchema
    });

    return <Transition.Root show={isOpen} as={Fragment}>

        <Dialog as="div" className="relative z-10" onClose={handleCancel}>
            {isLoading && <Loading />}
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
                                                    htmlFor="ngayChamCong"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Ngày chấm công
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formatDateForInput(formik.values.ngayChamCong.toString())}
                                                        onChange={formik.handleChange}
                                                        type="date"
                                                        id="ngayChamCong"
                                                        name="ngayChamCong"
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.ngayChamCong && formik.touched.ngayChamCong && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.ngayChamCong as string}</p>}
                                                </div>

                                            </div>
                                           

                                            <div>
                                                <label
                                                    htmlFor="soGioLamThem"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Số giờ làm thêm
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.soGioLamThem}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="soGioLamThem"
                                                        name="soGioLamThem"
                                                        placeholder='Nhập số giờ làm thêm'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.soGioLamThem && formik.touched.soGioLamThem && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.soGioLamThem}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="soGioNghiPhep"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Số giờ nghỉ phép
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.soGioNghiPhep}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="soGioNghiPhep"
                                                        name="soGioNghiPhep"
                                                        placeholder='Nhập số giờ nghỉ có phép'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.soGioNghiPhep && formik.touched.soGioNghiPhep && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.soGioNghiPhep}</p>}
                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="soGioNghiKhongPhep"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Số giờ nghỉ không phép
                                                </label>
                                                <div className="relative">
                                                    <input
                                                        value={formik.values.soGioNghiKhongPhep}
                                                        onChange={formik.handleChange}
                                                        type="text"
                                                        id="soGioNghiKhongPhep"
                                                        name="soGioNghiKhongPhep"
                                                        placeholder='Nhập số giờ nghỉ không phép'
                                                        className="py-3 px-4 block w-full border-[1px] outline-none border-slate-400 rounded-md text-sm"
                                                    />
                                                    {formik.errors.soGioNghiKhongPhep && formik.touched.soGioNghiKhongPhep && <p className='text-[13px] text-red-600 mt-1 pl-2'>{formik.errors.soGioNghiKhongPhep}</p>}
                                                </div>

                                            </div>

                                        </div>
                                        <button
                                            type='submit'
                                            className="uppercase w-full mt-8 py-3 px-4 inline-flex justify-center items-center gap-2 rounded-md border border-transparent font-semibold bg-green-600 hover:bg-green-700 text-white transition-all text-sm"
                                            onClick={handleConfirm}
                                        >
                                            Thêm chấm công
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