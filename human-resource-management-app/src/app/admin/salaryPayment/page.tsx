"use client";
import { Button } from '@/components/button';
import ConfirmModal from '@/components/comfirm-modal';
import Pagination from '@/components/Pagination';
import Employee from '@/models/employee';
import employeeService from '@/services/employeeService';
import salaryPaymentService from '@/services/salaryPaymentService';
import formatDate from '@/utils/formatDate';
import formatCurrency from '@/utils/formatVND';
import { ChevronRightIcon, HomeIcon } from '@heroicons/react/24/solid';
import Link from 'next/link';
import { ChangeEventHandler, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import AddSalaryPaymentModal from './components/add-salary-payment-modal';
import EditSalaryPaymentModal from './components/edit-salary-payment-modal';
import Status, { EStatus } from '@/components/Status';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowLeft, faArrowRight, faPlus, faSearch } from '@fortawesome/free-solid-svg-icons';

export default function AdminSalaryPayment() {
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [selectedSalaryPayment, setSelectedSalaryPayment] = useState<SalaryPayment | null>(null);

    const [salaryPayments, setSalaryPayments] = useState<any[]>([]);
    const [searchEmployee, setSearchEmployee] = useState('')
    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const [currentMonth, setCurrentMonth] = useState(new Date())

    const getSalaryPayments = async (page = 1, size = 10, employeeName = "", month = new Date()) => {
        const response = await salaryPaymentService.getByPage(page, size, employeeName, month);
        console.log(response);
        if (response?.statusCode === 200) {
            setSalaryPayments(response.data.items);
            setTotalCount(response.data.totalCount);
            setTotalPages(response.data.totalPages);
        }
    }


    useEffect(() => {
        getSalaryPayments();
    }, [])


    const onSalaryPaymentUpdated = () => {
        getSalaryPayments();
        setSelectedSalaryPayment(null)
    }

    const handleRemove = async () => {
        const response = await salaryPaymentService.delete(selectedSalaryPayment?.maTraLuong);
        if (response?.statusCode === 204) {
            getSalaryPayments();
            setShowDeleteModal(false);
            toast.success(response?.message)
        } else toast.error(response?.message)
    }



    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getSalaryPayments(page, pageSize, searchEmployee, currentMonth);
    }

    const handlePagePrevious = async () => {
        handlePageChange(currentPage - 1)
    }

    const handlePageNext = async () => {
        handlePageChange(currentPage + 1)
    }

    const handleSearchChange: ChangeEventHandler<HTMLInputElement> = (e) => {
        setSearchEmployee(e.target.value)
        if (e.target.value.length == 0) {
            getSalaryPayments(currentPage, pageSize, "", currentMonth)
        }
    }

    const handleSearch = async () => {
        getSalaryPayments(currentPage, pageSize, searchEmployee, currentMonth);
    }

    const handlePrevMonth = () => {
        const newMonth = new Date(currentMonth);
        newMonth.setMonth(newMonth.getMonth() - 1);
        setCurrentMonth(newMonth);
        getSalaryPayments(currentPage, pageSize, searchEmployee, newMonth)
    }

    const handleNextMonth = () => {
        const newMonth = new Date(currentMonth);
        newMonth.setMonth(newMonth.getMonth() + 1);
        setCurrentMonth(newMonth);
        getSalaryPayments(currentPage, pageSize, searchEmployee, newMonth)
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
                            <span className="text-gray-900">Danh sách chức vụ</span>
                        </li>
                    </ol>
                </nav>
            </div>

            <div className="bg-white shadow-md p-4 rounded-md">
                <div className="mb-5 pb-3 border-b-2 border-sky-400 flex justify-between">
                    <button className='flex gap-x-2 items-center bg-green-600 hover:bg-green-700 text-white px-3' onClick={() => setShowAddModal(true)}> 
                        <FontAwesomeIcon
                                icon={faPlus}
                            />
                        Thêm thông tin trả lương
                    </button>

                    <div className="flex gap-x-2 items-center">
                        <button onClick={handlePrevMonth} className="px-4 py-2 rounded-sm bg-green-500 text-white flex items-center gap-x-2">
                            <FontAwesomeIcon
                                icon={faArrowLeft}
                            />
                            Prev
                        </button>
                        <span className="text-gray-900 font-normal text-sm py-3 px-4 border-2">Đợt trả lương tháng {currentMonth.getMonth() + 1} / {currentMonth.getFullYear()}</span>
                        <button onClick={handleNextMonth} className="px-4 py-2 rounded-sm bg-green-500 text-white flex items-center gap-x-2">
                            Next
                            <FontAwesomeIcon
                                icon={faArrowRight}
                            />
                        </button>
                    </div>


                    <div className='flex items-center'>
                        <input
                            className='px-2 py-2 outline-none border border-green-500'
                            placeholder='Tìm kiếm thông tin..'
                            value={searchEmployee}
                            onChange={handleSearchChange}
                        />
                        <button onClick={handleSearch} className='bg-green-500 hover:bg-green-600 border flex gap-x-2 items-center border-green-500 hover:border-green-600 text-white px-2 py-2'>
                            <FontAwesomeIcon
                                icon={faSearch}
                            />
                            Tìm kiếm
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
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Đợt trả lương
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Số tiền phải trả
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Số tiền đã trả
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Số tiền nợ
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Ngày trả lương
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Trạng thái
                            </th>


                        </tr>
                    </thead>
                    <tbody className="bg-white divide-y divide-gray-200">
                        {salaryPayments?.map((salaryPayment) => (
                            <tr key={salaryPayment.maTraLuong}>

                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {salaryPayment?.nhanVien?.hoTen}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {salaryPayment?.dotTraLuong?.tenDotTraLuong}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {formatCurrency(salaryPayment.soTienCanPhaiTra)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatCurrency(salaryPayment.soTienDaTra)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatCurrency(salaryPayment.soTienConPhaiTra)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatDate(salaryPayment.ngayTraLuong)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    <Status
                                        title={salaryPayment.trangThai}
                                        status={salaryPayment.trangThai == "Đang nợ lương" ? EStatus.UNAVAILABLE : EStatus.AVAILABLE}
                                    />
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

            <AddSalaryPaymentModal isOpen={showAddModal} handleCancel={() => setShowAddModal(false)} onSalaryPaymentCreated={getSalaryPayments} handleConfirm={() => { }} />
            {selectedSalaryPayment && showEditModal && <EditSalaryPaymentModal isOpen={showEditModal} handleCancel={() => setShowEditModal(false)} selectedSalaryPayment={selectedSalaryPayment} onSalaryPaymentUpdated={onSalaryPaymentUpdated} handleConfirm={() => { }} />}
            {selectedSalaryPayment && showDeleteModal && <ConfirmModal title="Thông báo" description="Bạn có muốn xóa cái này không" isOpen={showDeleteModal} handleCancel={() => setShowDeleteModal(false)} handleConfirm={handleRemove} />}
        </div>
    );
}
