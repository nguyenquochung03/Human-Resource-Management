"use client";
import { Button } from "@/components/button";
import ConfirmModal from "@/components/comfirm-modal";
import Pagination from "@/components/Pagination";
import Employee from "@/models/employee";
import HieuSuat from "@/models/hieuSuat";
import employeeService from "@/services/employeeService";
import hieuSuatService from "@/services/hieuSuatService";
import { ChevronRightIcon, HomeIcon } from "@heroicons/react/24/solid";
import Link from "next/link";
import { ChangeEventHandler, useEffect, useState } from "react";
import toast from "react-hot-toast";
import AddHieuSuatModal from "./components/add-hieusuat-modal";
import EditHieuSuatModal from "./components/edit-hieusuat-modal";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";


export default function AdminHieuSuat() {
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [selectedHieuSuat, setSelectedHieuSuat] = useState<HieuSuat | null>(null);

    const [hieuSuats, setHieuSuats] = useState<any[]>([]);
    const [employees, setEmployees] = useState<Employee[]>([]);

    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const getHieuSuats = async (page = 1, size = 10) => {
        const response = await hieuSuatService.getByPage(page, size);
        if (response?.statusCode === 200) {
            setHieuSuats(response.data.items);
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
        getHieuSuats();
        getEmployees();
    }, [])

    const handleClickEdit = (hieuSuat: any) => {
        setShowEditModal(true)

        const { nhanVien, ...newHieuSuat } = hieuSuat;
        setSelectedHieuSuat({
            ...newHieuSuat,
            nhanVienId: nhanVien.maNhanVien
        })
    }

    const handleDeleteEdit = (hieuSuat: any) => {
        setShowDeleteModal(true)
        const { nhanVien, ...newHieuSuat } = hieuSuat;
        setSelectedHieuSuat({
            ...newHieuSuat,
            nhanVienId: nhanVien.maNhanVien
        })
    }

    const handleRemove = async () => {
        const response = await hieuSuatService.delete(selectedHieuSuat?.maHieuSuat);
        if (response?.statusCode === 204) {
            getHieuSuats(1, pageSize);
            setShowDeleteModal(false);
            toast.success(response?.message)
        }
    }

    const handleEmployeeChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        const { value } = event.target;
        if (value === "all") {

            getHieuSuats();
            return;
        }
        const response = await hieuSuatService.findAllByEmployeeId(value);

        if (response.statusCode === 200) {
            setHieuSuats(response.data)
        }
    }

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getHieuSuats(page, pageSize);
    }

    const handlePagePrevious = async () => {
        handlePageChange(currentPage - 1)
    }

    const handlePageNext = async () => {
        handlePageChange(currentPage + 1)
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
                            <span className="text-gray-900">Danh sách hiệu suất</span>
                        </li>
                    </ol>
                </nav>
            </div>

            <div className="bg-white shadow-md p-4 rounded-md">
                <div className="mb-5 pb-3 border-b-2 border-sky-400 flex gap-x-2">
                    <button
                        className="bg-green-600 hover:bg-green-700 px-3 py-2 rounded-sm text-white flex items-center gap-x-1"
                        onClick={() => setShowAddModal(true)}
                    >
                        <FontAwesomeIcon
                            icon={faPlus}
                        />
                        Thêm mới
                    </button>
                    <select
                        className="ml-4 px-4 text-sm py-[10px] border border-black rounded-sm outline-none"
                        onChange={handleEmployeeChange}
                    >
                        <option className="bg-white" value="all">Tất cả</option>
                        {employees.map(employee => <option value={employee.maNhanVien} key={employee.maNhanVien}>
                            {employee.hoTen}
                        </option>)}
                    </select>
                </div>

                <table className="min-w-full divide-y divide-gray-200 overflow-x-auto">
                    <thead className="bg-gray-50">
                        <tr>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Mã hiệu suất
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
                                Người đánh giá
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Kì đánh giá
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Mục tiêu hiệu suất
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Đánh giá hiệu suất
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Phản hồi
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Kế hoạch phát triển
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
                        {hieuSuats?.map((hieuSuat) => (
                            <tr key={hieuSuat.maHieuSuat}>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {hieuSuat.maHieuSuat}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {hieuSuat?.nhanVien?.hoTen}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {hieuSuat.nguoiDanhGia}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {hieuSuat.kyDanhGia}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {hieuSuat.mucTieuHieuSuat}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {hieuSuat.danhGiaHieuSuat}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {hieuSuat.phanHoi}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {hieuSuat.keHoachPhatTrien}
                                </td>

                                <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                                    <div className="flex gap-x-2">
                                        <button className="bg-sky-500 hover:bg-sky-600 px-4 py-2 rounded-sm text-white" onClick={() => handleClickEdit(hieuSuat)} >Sửa</button>
                                        <button className="bg-red-500 hover:bg-red-600 px-4 py-2 rounded-sm text-white" onClick={() => handleDeleteEdit(hieuSuat)} >Xóa</button>
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

            <AddHieuSuatModal
                isOpen={showAddModal} handleCancel={() => setShowAddModal(false)} onHieuSuatCreated={getHieuSuats} handleConfirm={() => { }} />
            {selectedHieuSuat && showEditModal && <EditHieuSuatModal isOpen={showEditModal} handleCancel={() => setShowEditModal(false)} selectedHieuSuat={selectedHieuSuat} onHieuSuatUpdated={getHieuSuats} handleConfirm={() => { }} />}
            {selectedHieuSuat && showDeleteModal && <ConfirmModal title="Thông báo" description="Bạn có muốn xóa cái này không" isOpen={showDeleteModal} handleCancel={() => setShowDeleteModal(false)} handleConfirm={handleRemove} />}
        </div>
    );
}