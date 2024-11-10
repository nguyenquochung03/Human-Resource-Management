"use client";
import { Button } from "@/components/button";
import Pagination from "@/components/Pagination";
import TableSkeleton from "@/components/skeleton/TableSkeleton";
import ChamCong from "@/models/chamCong";
import Employee from "@/models/employee";
import chamCongService from "@/services/chamCongService";
import employeeService from "@/services/employeeService";
import formatDate from "@/utils/formatDate";
import { ChevronRightIcon, HomeIcon } from "@heroicons/react/24/solid";
import { addDays } from "date-fns";
import Link from "next/link";
import { useEffect, useState } from "react";
import AddChamCongModal from "./components/add-chamcong-modal";
import EditChamCongModal from "./components/edit-chamcong-modal";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowLeft, faArrowRight, faPlus, faUser } from "@fortawesome/free-solid-svg-icons";

export default function AdminChamCong() {
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [selectedChamCong, setSelectedChamCong] = useState<ChamCong | null>(null);

    const [chamCongs, setChamCongs] = useState<any[]>([]);
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const [isLoading, setIsLoading] = useState(false)

    const [currentDate, setCurrentDate] = useState(new Date())

    const getChamCongs = async (page = 1, size = 10, day = new Date()) => {
        const response = await chamCongService.getByPage(page, size, day);
        if (response?.statusCode === 200) {
            setChamCongs(response.data.items);
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
        getChamCongs(currentPage, pageSize);
        getEmployees();
    }, [])

    const handleClickEdit = (chamCong: any) => {
        setShowEditModal(true)

        const { nhanVien, ...newChamCong } = chamCong;
        setSelectedChamCong({
            ...newChamCong,
            nhanVienId: nhanVien.maNhanVien
        })
    }

    const handleDeleteEdit = (chamCong: any) => {
        setShowDeleteModal(true)
        const { nhanVien, ...newChamCong } = chamCong;
        setSelectedChamCong({
            ...newChamCong,
            nhanVienId: nhanVien.maNhanVien
        })
    }


    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getChamCongs(page, pageSize, currentDate);
    }

    const handlePagePrevious = async () => {
        handlePageChange(currentPage - 1)
    }

    const handlePageNext = async () => {
        handlePageChange(currentPage + 1)
    }

    const handleNextDay = () => {
        const newDate = addDays(currentDate, 1)
        setCurrentDate(newDate)
        getChamCongs(currentPage, pageSize, newDate);
    }

    const handlePrevDay = () => {
        const newDate = addDays(currentDate, -1)
        setCurrentDate(newDate)
        getChamCongs(currentPage, pageSize, newDate);
    }

    useEffect(() => {

    }, [])

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
                                <span className="text-gray-900">Danh sách chấm công</span>
                            </li>
                        </ol>
                    </nav>
                </div>




                <div className="bg-white shadow-md p-4 rounded-md">
                    <div className="mb-5 pb-3">
                        <div className="w-full mb-5 pb-3 border-b-2 border-sky-400 flex justify-between items-center">
                            <div className="flex gap-x-2">
                                <button
                                    className="bg-green-600 hover:bg-green-700 px-3 py-2 rounded-sm text-white flex items-center gap-x-1"
                                    onClick={() => setShowAddModal(true)}
                                >
                                    <FontAwesomeIcon
                                        icon={faPlus}
                                    />
                                    Thêm mới
                                </button>
                                <Link className="px-3 py-2 rounded-sm bg-green-600 hover:bg-green-700 text-white flex items-center gap-x-2" href="/admin/chamcong/nhanvien"> 
                                    <FontAwesomeIcon 
                                        icon={faUser}
                                    />
                                    Xem theo nhân viên
                                </Link>
                            </div>
                            <div className="flex gap-x-2 items-center">
                                <button onClick={handlePrevDay} className="px-4 py-2 rounded-sm bg-green-500 text-white flex items-center gap-x-2">
                                    <FontAwesomeIcon
                                        icon={faArrowLeft}
                                    />
                                    Prev
                                </button>
                                <span className="text-gray-900 font-normal text-sm py-3 px-4 border-2">Chấm công {formatDate(currentDate.toString())}</span>
                                <button onClick={handleNextDay} className="px-4 py-2 rounded-sm bg-green-500 text-white flex items-center gap-x-2">
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
                                        Mã chấm công
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
                                        Giờ vào làm
                                    </th>
                                    <th
                                        scope="col"
                                        className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                    >
                                        Giờ ra làm
                                    </th>
                                    <th
                                        scope="col"
                                        className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                    >
                                        Số giờ làm việc
                                    </th>
                                    <th
                                        scope="col"
                                        className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                    >
                                        Số giờ làm thêm
                                    </th>
                                    <th
                                        scope="col"
                                        className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                    >
                                        Số giờ nghỉ phép
                                    </th>
                                    <th
                                        scope="col"
                                        className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                    >
                                        Số giờ nghỉ không phép
                                    </th>
                                    <th
                                        scope="col"
                                        className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                    >
                                        Tổng giờ làm
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
                                {chamCongs?.map((chamCong) => (
                                    <tr key={chamCong.maChamCong}>
                                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                            {chamCong.maChamCong}
                                        </td>
                                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                            {chamCong.nhanVien.hoTen}
                                        </td>
                                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                            {chamCong.gioVaoLam}
                                        </td>
                                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                            {chamCong.gioRaLam}
                                        </td>
                                        <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                            {chamCong.soGioLamViec}
                                        </td>
                                        <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                            {chamCong.soGioLamThem}
                                        </td>
                                        <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                            {chamCong.soGioNghiPhep}
                                        </td>
                                        <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                            {chamCong.soGioNghiKhongPhep}
                                        </td>
                                        <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                            {chamCong.tongGioLam}
                                        </td>
                                        <td className="px-6 py-4 whitespace-nowrap  text-sm font-medium">

                                            <div className="flex gap-x-2">
                                                <button className="bg-sky-500 hover:bg-sky-600 px-4 py-2 rounded-sm text-white" onClick={() => handleClickEdit(chamCong)} >Sửa</button>
                                                <button className="bg-red-500 hover:bg-red-600 px-4 py-2 rounded-sm text-white" onClick={() => handleDeleteEdit(chamCong)} >Xóa</button>
                                            </div>
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

                    <AddChamCongModal isOpen={showAddModal} handleCancel={() => setShowAddModal(false)} onChamCongCreated={() => getChamCongs(1, pageSize)} handleConfirm={() => { }} />
                    {selectedChamCong && showEditModal && <EditChamCongModal isOpen={showEditModal} handleCancel={() => setShowEditModal(false)} selectedChamCong={selectedChamCong} onChamCongUpdated={() => getChamCongs(1, pageSize)} handleConfirm={() => { }} />}
                </div>

            </div>
    );
}