"use client";
import { Button } from "@/components/button";
import ConfirmModal from "@/components/comfirm-modal";
import Pagination from "@/components/Pagination";
import Employee from "@/models/employee";
import absenceService from "@/services/absenceService";
import employeeService from "@/services/employeeService";
import { ChevronRightIcon, HomeIcon } from "@heroicons/react/24/solid";
import Link from "next/link";
import { ChangeEventHandler, useEffect, useState } from "react";
import toast from "react-hot-toast";
import AddAbsenceModal from "./components/add-absence-modal";
import EditAbsenceModal from "./components/edit-absence-modal";
import formatDate from "@/utils/formatDate";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";

export default function AdminAbsence() {
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [selectedAbsence, setSelectedAbsence] = useState<Absence | null>(null);

    const [absences, setAbsences] = useState<any[]>([]);
    const [employees, setEmployees] = useState<Employee[]>([]);

    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const getAbsences = async (page = 1, size = 10) => {
        const response = await absenceService.getByPage(page, size);
        if (response?.statusCode === 200) {
            setAbsences(response.data.items);
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
        getAbsences();
        getEmployees();
    }, [])

    const handleClickEdit = (absence: any) => {
        setShowEditModal(true)

        const { nhanVien, ...newAbsence } = absence;
        setSelectedAbsence({
            ...newAbsence,
            nhanVienId: nhanVien.maNhanVien
        })
    }

    const handleDeleteEdit = (chamCong: any) => {
        setShowDeleteModal(true)
        const { nhanVien, ...newAbsence } = chamCong;
        setSelectedAbsence({
            ...newAbsence,
            nhanVienId: nhanVien.maNhanVien
        })
    }

    const handleRemove = async () => {
        const response = await absenceService.delete(selectedAbsence?.maNghiVang);
        if (response?.statusCode === 204) {
            getAbsences();
            setShowDeleteModal(false);
            toast.success(response?.message)
        }
    }

    const handleEmployeeChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        const { value } = event.target;
        if (value === "all") {

            getAbsences();
            return;
        }
        const response = await absenceService.findAllByEmployeeId(value);

        if (response.statusCode === 200) {
            setAbsences(response.data)
        }
    }

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getAbsences(page, pageSize);
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
                            <span className="text-gray-900">Danh sách nghỉ vắng</span>
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
                        Thêm nghỉ vắng
                    </button>
                    <select
                        className="ml-2 px-4 text-sm py-[10px] border border-black rounded-sm outline-none"
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
                                Nhân viên
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Loại nghỉ vắng
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Ngày bắt đầu
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Ngày kết thúc
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Số ngày nghỉ
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Lí do
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Trạng thái
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Ghi chú
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
                        {absences?.map((absence) => (
                            <tr key={absence.maNghiVang}>

                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {absence.nhanVien.hoTen}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {absence.loaiNghiVang}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {formatDate(absence.ngayBatDau)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatDate(absence.ngayKetThuc)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {absence.soNgayNghi}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {absence.lyDo}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {absence.trangThai}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {absence.ghiChu}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap  text-sm font-medium">

                                    <div className="flex gap-x-2">
                                        <button className="bg-sky-500 hover:bg-sky-600 px-4 py-2 rounded-sm text-white" onClick={() => handleClickEdit(absence)} >Sửa</button>
                                        <button className="bg-red-500 hover:bg-red-600 px-4 py-2 rounded-sm text-white" onClick={() => handleDeleteEdit(absence)} >Xóa</button>
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

            <AddAbsenceModal isOpen={showAddModal} handleCancel={() => setShowAddModal(false)} onAbsenceCreated={getAbsences} handleConfirm={() => { }} />
            {selectedAbsence && showEditModal && <EditAbsenceModal isOpen={showEditModal} handleCancel={() => setShowEditModal(false)} selectedAbsence={selectedAbsence} onAbsenceUpdated={getAbsences} handleConfirm={() => { }} />}
            {selectedAbsence && showDeleteModal && <ConfirmModal title="Thông báo" description="Bạn có muốn xóa cái này không" isOpen={showDeleteModal} handleCancel={() => setShowDeleteModal(false)} handleConfirm={handleRemove} />}
        </div>
    );
}