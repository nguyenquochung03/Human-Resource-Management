"use client";
import Pagination from "@/components/Pagination";
import Employee from "@/models/employee";
import Tax from "@/models/tax";
import employeeService from "@/services/employeeService";
import taxService from "@/services/taxService";
import formatMonth from "@/utils/formatMonth";
import formatCurrency from "@/utils/formatVND";
import { ChevronRightIcon, HomeIcon } from "@heroicons/react/24/solid";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Link from "next/link";
import { ChangeEventHandler, useEffect, useState } from "react";
import { faArrowLeft, faArrowRight } from "@fortawesome/free-solid-svg-icons";



export default function AdminTax() {
    const [selectedTax, setSelectedTax] = useState<Tax | null>(null);

    const [taxs, setTaxs] = useState<any[]>([]);
    const [employees, setEmployees] = useState<Employee[]>([]);

    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [currentMonth, setCurrentMonth] = useState(new Date())
    const [currentEmployee, setCurrentEmployee] = useState('')

    const getTaxs = async (page = 1, size = 10, employeeId = "", time = new Date()) => {
        const response = await taxService.getByPage(page, size, employeeId, time);
        if (response?.statusCode === 200) {
            setTaxs(response.data.items);
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
        getTaxs();
        getEmployees();
    }, [])



    const handleEmployeeChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        const { value } = event.target;
        if (value === "all") {
            setCurrentEmployee('')
            getTaxs(currentPage, pageSize, "", currentMonth);
            return;
        }

        setCurrentEmployee(value)
        const response = await taxService.getByPage(currentPage, pageSize, value, currentMonth);
        console.log(response)
        if (response.statusCode === 200) {
            setTaxs(response.data.items);
            setTotalCount(response.data.totalCount);
            setTotalPages(response.data.totalPages);
        }
    }

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getTaxs(page, pageSize);
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
        getTaxs(currentPage, pageSize, currentEmployee, newMonth)
        setCurrentMonth(newMonth);
    }

    const handleNextMonth = async () => {
        const newMonth = new Date(currentMonth);
        newMonth.setMonth(newMonth.getMonth() + 1);
        getTaxs(currentPage, pageSize, currentEmployee, newMonth)
        setCurrentMonth(newMonth);
    }

    return (
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
                            <span className="text-gray-900">Danh sách thuế</span>
                        </li>
                    </ol>
                </nav>
            </div>

            <div className="bg-white shadow-md p-4 rounded-md">
                <div className="mb-5 pb-3 border-b-2 border-sky-400 flex items-center justify-between">
                   <div className="flex gap-x-1 items-center">
                        <p>Chọn nhân viên: </p>
                        <select
                            className="ml-4 px-4 text-sm py-[10px] border border-green-600 rounded-sm outline-none"
                            onChange={handleEmployeeChange}
                        >
                            <option className="bg-white" value="all">Tất cả</option>
                            {employees?.map(employee => <option value={employee.maNhanVien} key={employee.maNhanVien}>
                                {employee.hoTen}
                            </option>)}
                        </select>
                    </div>

                    <div className="flex items-center gap-x-2">
                        <button onClick={handlePrevMonth} className="bg-green-600 text-white px-3 py-2 flex items-center gap-x-2">
                            <FontAwesomeIcon 
                                icon={faArrowLeft}
                            />
                            Prev
                        </button>
                        <span className="bg-white text-black border-[1px] px-3 py-[8px]">Thuế tháng {currentMonth.getMonth() + 1} / {currentMonth.getFullYear()}</span>
                        <button onClick={handleNextMonth} className="bg-green-600 text-white px-3 py-2 flex items-center gap-x-2">
                            Next
                            <FontAwesomeIcon 
                                icon={faArrowRight}
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
                                Mã số thuế
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Nhân viên
                            </th>

                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Bảo hiểm xã hội
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Bảo hiểm y tế
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Bảo hiểm thất nghiệp
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Mức thu nhập chịu thuế
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Thuế suất
                            </th>

                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Thuế thu nhập cá nhân thực tế
                            </th>

                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Thời gian tính
                            </th>

                        </tr>
                    </thead>
                    <tbody className="bg-white divide-y divide-gray-200">
                        {taxs?.map((tax) => (
                            <tr key={tax.maSoThue}>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {tax.maSoThue}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {tax?.nhanVien?.hoTen}
                                </td>

                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatCurrency(tax.baoHiemXaHoi)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatCurrency(tax.baoHiemYTe)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatCurrency(tax.baoHiemThatNghiep)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatCurrency(tax.mucThuNhapChiuThue)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {tax.thueSuat}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatCurrency(tax.thueThuNhapCaNhanThucTe)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatMonth(tax.thoiGianTinh)}
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