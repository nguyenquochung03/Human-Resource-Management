"use client";
import Pagination from "@/components/Pagination";
import TableSkeleton from "@/components/skeleton/TableSkeleton";
import Employee from "@/models/employee";
import employeeService from "@/services/employeeService";
import salaryService from "@/services/salaryService";
import formatMonth from "@/utils/formatMonth";
import formatCurrency from "@/utils/formatVND";
import { faArrowLeftLong, faArrowRightLong } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { ChevronRightIcon, HomeIcon } from "@heroicons/react/24/solid";
import Link from "next/link";
import { ChangeEventHandler, useEffect, useState } from "react";



export default function AdminSalary() {

    const [salarys, setSalarys] = useState<any[]>([]);
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [isLoading, setIsLoading] = useState(false)

    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [currentMonth, setCurrentMonth] = useState(new Date())
    const [selectedEmployee, setSelectedEmployee] = useState('')

    const getSalarys = async (page = 1, size = 10, month = new Date(), employeeId: string = "") => {
        const response = await salaryService.getByPage(page, size, month, employeeId);
        if (response?.statusCode === 200) {
            setSalarys(response.data.items);
            setTotalCount(response.data.totalCount);
            setTotalPages(response.data.totalPages);
        }
    }

    const getEmployees = async () => {
        const response = await employeeService.findAll();
        if (response?.statusCode === 200) {
            setEmployees(response.data);
        }
    }


    useEffect(() => {
        getSalarys();
        getEmployees();
    }, [])

    const handleEmployeeChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        const { value } = event.target;

        if (value === "all") {
            setSelectedEmployee('')
            getSalarys(currentPage, pageSize, currentMonth, '');
            return;
        }

        setSelectedEmployee(value)
        getSalarys(currentPage, pageSize, currentMonth, value);
    }

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getSalarys(page, pageSize, currentMonth, selectedEmployee);
    }

    const handlePagePrevious = async () => {
        handlePageChange(currentPage - 1)
    }

    const handlePageNext = async () => {
        handlePageChange(currentPage + 1)
    }

    const handlePrevMonth = async () => {
        const newMonth = new Date(currentMonth);
        newMonth.setMonth(newMonth.getMonth() - 1);
        getSalarys(currentPage, pageSize, newMonth, selectedEmployee)
        setCurrentMonth(newMonth);
    }

    const handleNextMonth = async () => {
        const newMonth = new Date(currentMonth);
        newMonth.setMonth(newMonth.getMonth() + 1);
        getSalarys(currentPage, pageSize, newMonth, selectedEmployee)
        setCurrentMonth(newMonth);
    }


    return (
        isLoading ? <TableSkeleton /> :
            <div className="flex-auto p-2 mt-2">
                <div className="mx-auto pb-4">
                    <nav className="text-sm font-medium" aria-label="Breadcrumb">
                        <ol className="list-none p-0 inline-flex gap-x-2">
                            <HomeIcon className="h-4 w-4 mr-1" />
                            <li className="flex items-center">
                                <Link href="#" className="text-gray-500 hover:text-gray-700">
                                    Trang chủ
                                </Link>
                            </li>
                            <li>
                                <ChevronRightIcon className="h-4 w-4" />
                            </li>
                            <li className="flex items-center">
                                <span className="text-gray-900">Danh sách lương</span>
                            </li>
                        </ol>
                    </nav>
                </div>

                <div className="bg-white shadow-md p-4 rounded-md">
                    <div className="mb-5 pb-3 border-b-2 border-sky-400 flex justify-between">
                        <div className="flex gap-x-2 items-center">
                            <span className="font-bold">Chọn nhân viên: </span>
                            <select
                                className="px-4 text-sm py-[10px] border border-black rounded-sm outline-none"
                                onChange={handleEmployeeChange}
                            >
                                <option className="bg-white" value="all">Tất cả</option>
                                {employees?.map(employee => <option value={employee.maNhanVien} key={employee.maNhanVien}>
                                    {employee.hoTen}
                                </option>)}
                            </select>
                        </div>

                        <div className="flex items-center gap-x-2">
                            <button onClick={handlePrevMonth} className="px-4 py-2 rounded-sm bg-green-500 hover:bg-green-600 text-white flex items-center gap-x-2">
                                <FontAwesomeIcon
                                    icon={faArrowLeftLong}
                                />
                                Prev
                            </button>

                            <span className="bg-white text-black border-[1px] px-3 py-[8px]">Lương tháng {currentMonth.getMonth() + 1} / {currentMonth.getFullYear()}</span>
                            <button onClick={handleNextMonth} className="px-4 py-2 rounded-sm bg-green-500 hover:bg-green-600 text-white flex items-center gap-x-2">
                                Next
                                <FontAwesomeIcon
                                    icon={faArrowRightLong}
                                />
                            </button>
                        </div>
                    </div>

                    <table className="min-w-full divide-y divide-gray-200 overflow-x-auto">
                        <thead className="bg-gray-50">
                            <tr>
                                <th
                                    scope="col"
                                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                >
                                    Nhân viên
                                </th>
                                {/* <th
                                    scope="col"
                                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                >
                                    Lương cơ bản
                                </th> */}

                                <th
                                    scope="col"
                                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                >
                                    Tổng tiền
                                </th>
                                <th
                                    scope="col"
                                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                >
                                    Các khoản khấu trừ
                                </th>
                                <th
                                    scope="col"
                                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                >
                                    Thu nhập ròng
                                </th>
                                <th
                                    scope="col"
                                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                >
                                    Thời gian tính lương
                                </th>

                                <th
                                    scope="col"
                                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                >
                                    Thao tác
                                </th>
                            </tr>
                        </thead>
                        <tbody className="bg-white divide-y divide-gray-200">
                            {salarys?.map((salary) => (
                                <tr key={salary.maLuong}>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {salary?.nhanVien?.hoTen}
                                    </td>
                                    {/* <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {formatCurrency(salary.luongCoBan)}
                                    </td>
                                     */}

                                    <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                        {formatCurrency(salary.tongTien)}
                                    </td>
                                    <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                        {formatCurrency(salary.cacKhoanKhauTru)}
                                    </td>
                                    <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                        {formatCurrency(salary.thuNhapRong)}
                                    </td>
                                    <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                        {formatMonth(salary.thoiGian)}
                                    </td>

                                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                                        <Link className="bg-green-500 px-4 py-2 rounded-sm text-white" href={`/admin/salary/${salary.maLuong}`}>Xem</Link>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>

                    <Pagination
                        currentPage={currentPage}
                        totalPages={totalPages}
                        totalCount={totalCount}
                        handlePageChange={handlePageChange}
                        handlePageNext={handlePageNext}
                        handlePagePrevious={handlePagePrevious}
                    />

                </div>


            </div>
    );
}