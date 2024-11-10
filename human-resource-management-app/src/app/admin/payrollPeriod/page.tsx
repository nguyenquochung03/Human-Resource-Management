"use client";
import { ChevronRightIcon, HomeIcon } from "@heroicons/react/24/solid";
import { useEffect, useState } from "react";

import Pagination from "@/components/Pagination";
import payrollPeriodService from "@/services/payrollPeriodService";
import formatMonth from "@/utils/formatMonth";
import MoneyUtil from "@/utils/money-util";
import Link from "next/link";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowLeft, faArrowRight } from "@fortawesome/free-solid-svg-icons";


export default function AdminPayrollPeriod() {

    const [payrollPeriods, setPayrollPeriods] = useState<any[]>([]);
    const [currentMonth, setCurrentMonth] = useState(new Date())
    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const getPayrollPeriods = async (page = 1, size = 10, month = new Date()) => {
        const response = await payrollPeriodService.getByPage(page, size, month);
        console.log(response)
        if (response?.statusCode === 200) {
            console.log(response.items)
            setPayrollPeriods(response.data.items);
            setTotalCount(response.data.totalCount);
            setTotalPages(response.data.totalPages);
        }
    }

    useEffect(() => {
        getPayrollPeriods(currentPage, pageSize, currentMonth);
    }, [])

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getPayrollPeriods(page, pageSize, currentMonth);
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
        getPayrollPeriods(currentPage, pageSize, newMonth)
        setCurrentMonth(newMonth);
    }

    const handleNextMonth = async () => {
        const newMonth = new Date(currentMonth);
        newMonth.setMonth(newMonth.getMonth() + 1);
        getPayrollPeriods(currentPage, pageSize, newMonth)
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
                            <span className="text-gray-900">Danh sách đợt trả lương</span>
                        </li>
                    </ol>
                </nav>
            </div>

            <div className="bg-white shadow-md p-4 rounded-md">
                <div className="mb-5 pb-3 border-b-2 border-sky-400 flex gap-x-4">

                    <div className="flex items-center gap-x-2">
                        <button onClick={handlePrevMonth} className="px-4 py-2 rounded-sm bg-green-500 text-white flex items-center gap-x-2">
                            <FontAwesomeIcon
                                icon={faArrowLeft}
                            />
                            Prev
                        </button>
                       
                        <span className="bg-white text-black border-[1px] px-3 py-[8px]">Lương tháng {currentMonth.getMonth() + 1} / {currentMonth.getFullYear()}</span>
                        <button onClick={handleNextMonth} className="px-4 py-2 rounded-sm bg-green-500 text-white flex items-center gap-x-2">
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
                                Đợt
                            </th>
                          
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Số nhân viên cần trả
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Số nhân viên đã trả
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Số nhân viên còn phải trả
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Tiền cần trả
                            </th>

                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Tiền đã trả
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Tiền còn phải trả
                            </th>

                        </tr>
                    </thead>
                    <tbody className="bg-white divide-y divide-gray-200">
                        {payrollPeriods?.map((payrollPeriod) => (
                            <tr key={payrollPeriod.maDotTraLuong}>
                          
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {payrollPeriod.tenDotTraLuong}
                                </td>
                          
                                <td className="px-6 py-4 text-sm text-gray-500">
                                    {payrollPeriod.tongNhanVienCanPhaiTra}
                                </td>
                                <td className="px-6 py-4 text-sm text-gray-500">
                                    {payrollPeriod.tongNhanVienDaTra}
                                </td>
                                <td className="px-6 py-4 text-sm text-gray-500">
                                    {payrollPeriod.tongNhanVienConPhaiTra}
                                </td>
                                <td className="truncate px-6 py-4 text-sm text-gray-500">
                                    {MoneyUtil.formatMoney(Math.round(payrollPeriod.tongTienCanPhaiTra))} đ
                                </td>
                                <td className="truncate px-6 py-4 text-sm text-gray-500">
                                    {MoneyUtil.formatMoney(Math.round(payrollPeriod.tongTienDaTra))} đ
                                </td>
                                <td className="truncate px-6 py-4 text-sm text-gray-500">
                                    {MoneyUtil.formatMoney(Math.round(payrollPeriod.tongTienConPhaiTra))} đ
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