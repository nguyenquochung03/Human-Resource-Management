import { Dialog, Transition } from '@headlessui/react';
import { ChangeEventHandler, Fragment, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import allowanceService from '@/services/allowanceService';
import AddAllowanceForEmployeeModalProps from './add-allowance-for-employee-modal-props';
import Allowance from '@/models/allowance';
import employeeService from '@/services/employeeService';
import Employee from '@/models/employee';



export default function AddAllowanceForEmployeeModal({ isOpen, handleCancel }: AddAllowanceForEmployeeModalProps) {
    const [allowances, setAllowances] = useState<Allowance[]>([])
    const [employees, setEmployees] = useState<Employee[]>([])
    const [result, setResult] = useState<string[]>([])
    const [allowance, setAllowance] = useState<string>("default");

    const toggleOption = (option: string) => {
        const selectedIndex = result.some(item => item === option);
        if (selectedIndex) {
            setResult(prevSelectedOptions =>
                prevSelectedOptions.filter(item => item !== option)
            );
        } else {
            setResult(prevSelectedOptions => [...prevSelectedOptions, option]);
        }
    };


    const getAllowances = async () => {
        const response = await allowanceService.findAll();

        if (response.statusCode === 200)
            setAllowances(response.data);
    }



    const handleSubmit = async () => {
        if (allowance == "default") {
            toast.error("Vui lòng chọn phụ cấp");
            return;
        }

        if (result.length == 0) {
            toast.error("Vui lòng chọn nhân viên");
            return;
        }

        const payload = {
            'phuCapId': allowance,
            'nhanVienIds': result
        }

        const response = await allowanceService.addAllowanceForEmployee(payload);

        if (response.statusCode === 200) {
            toast.success(response.message)
            handleCancel();
            setAllowance('default')
            setResult([])
        } else {
            toast.error(response.message)
        }
    }


    useEffect(() => {
        getAllowances();

    }, []);

    const handleAllowanceChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        setAllowance(event.target.value)
        const response = await employeeService.findAll()
        if (response.statusCode === 200) {

            const temp = response.data
                .filter((item: any) => !item.phuCaps.some((pc: any) => pc.maPhuCap === event.target.value));


            setEmployees(temp);
        }


    }

    const handleChangeAll: ChangeEventHandler<HTMLInputElement> = (event) => {
        if (event.target.checked) {
            const newResults = employees.map(employee => employee.maNhanVien)
            setResult(newResults)
        } else setResult([])

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
                        <Dialog.Panel className="relative transform rounded-lg bg-white text-left shadow-xl transition-all sm:my-4 sm:w-full sm:max-w-2xl">
                            <div className="mt-2 rounded-xl shadow-lg">
                                <div className="p-4 sm:p-7">
                                    <div className="text-center">
                                        <Dialog.Title as="h1" className="block text-2xl font-bold text-gray-800">
                                            Thêm phụ cấp cho nhân viên
                                        </Dialog.Title>
                                    </div>
                                    <form className="mt-5">
                                        <div className="grid grid-cols-1 gap-x-6 gap-y-4">
                                            <div>
                                                <label
                                                    htmlFor="tenPhuCap"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    Chọn phụ cấp
                                                </label>
                                                <div className="relative">
                                                    <select
                                                        value={allowance}
                                                        onChange={handleAllowanceChange}
                                                        id="tenPhuCap"
                                                        name="tenPhuCap"
                                                        className="py-3 px-4 block w-full border-[1px] border-slate-400 outline-none rounded-md text-sm"
                                                    >
                                                        <option>----Chọn phụ cấp----</option>
                                                        {
                                                            allowances.map(allowance =>
                                                                <option value={allowance.maPhuCap} key={allowance.maPhuCap}>
                                                                    {allowance.tenPhuCap}
                                                                </option>)
                                                        }
                                                    </select>

                                                </div>

                                            </div>
                                            <div>
                                                <label
                                                    htmlFor="nhanVienIds"
                                                    className="block text-sm font-bold ml-1 mb-2"
                                                >
                                                    {result.length === 0 ? 'Chọn nhân viên' : `Đã chọn ${result.length}`}
                                                </label>


                                                {employees.length > 0 &&
                                                    <div className='flex gap-x-1 items-center ml-2'>

                                                        <input
                                                            type="checkbox"
                                                            className="h-5 w-5 cursor-pointer"
                                                            onChange={handleChangeAll}
                                                        />
                                                        <label className="block px-4 py-2 cursor-pointer hover:bg-gray-100">Chọn tất cả</label>
                                                    </div>

                                                }

                                                <div className="w-full mt-2 p-2 bg-white border h-[400px] overflow-y-scroll border-gray-300 rounded-md shadow-lg scrollbar-w-2 custom-scrollbar custom-scrollbar">

                                                    {employees.length > 0 ? employees.map(option => (
                                                        <div key={option.maNhanVien} className='flex gap-x-1 items-center'>
                                                            <input
                                                                type="checkbox"
                                                                className="h-5 w-5 cursor-pointer text-green-600 border-green-600"
                                                                checked={result.includes(option.maNhanVien)}
                                                                onChange={() => toggleOption(option.maNhanVien)}
                                                            />
                                                         
                                                            <label className="block px-4 py-2 cursor-pointer hover:bg-gray-100"> {option.hoTen}</label>

                                                        </div>
                                                    )) : <i className='p-4'>Không có dữ liệu nào</i>}
                                                </div>
                                            </div>


                                        </div>
                                        <button
                                            type='button'
                                            onClick={handleSubmit}
                                            className="uppercase w-full mt-8 py-3 px-4 inline-flex justify-center items-center gap-2 rounded-md border border-transparent font-semibold bg-green-600 hover:bg-green-700 text-white transition-all text-sm"
                                        >
                                            Thêm phụ cấp
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