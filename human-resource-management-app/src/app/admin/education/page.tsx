"use client";
import ConfirmModal from "@/components/comfirm-modal";
import Employee from "@/models/employee";
import educationService from "@/services/educationService";
import employeeService from "@/services/employeeService";
import { ChevronRightIcon, HomeIcon } from "@heroicons/react/24/solid";
import Link from "next/link";
import { ChangeEventHandler, useEffect, useState } from "react";
import toast from "react-hot-toast";
import AddEducationModal from "./components/add-education-modal";
import EditEducationModal from "./components/edit-education-modal";
import Pagination from "@/components/Pagination";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";

export default function AdminEducation() {
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [selectedEducation, setSelectedEducation] = useState<Education | null>(null);

    const [educations, setEducations] = useState<any[]>([]);
    const [employees, setEmployees] = useState<Employee[]>([]);

    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const getEducations = async (page = 1, size = 10) => {
        const response = await educationService.getByPage(page, size);
        if (response?.statusCode === 200) {
            setEducations(response.data.items);
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
        getEducations();
        getEmployees();
    }, [])

    const handleClickEdit = (chamCong: any) => {
        setShowEditModal(true)

        const { nhanVien, ...newEducation } = chamCong;
        setSelectedEducation({
            ...newEducation,
            nhanVienId: nhanVien.maNhanVien
        })
    }

    const handleDeleteEdit = (chamCong: any) => {
        setShowDeleteModal(true)
        const { nhanVien, ...newEducation } = chamCong;
        setSelectedEducation({
            ...newEducation,
            nhanVienId: nhanVien.maNhanVien
        })
    }

    const handleRemove = async () => {
        const response = await educationService.delete(selectedEducation?.maTrinhDo);
        if (response?.statusCode === 204) {
            getEducations();
            setShowDeleteModal(false);
            toast.success(response?.message)
        }
    }

    const handleEmployeeChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        const { value } = event.target;
        if (value === "all") {

            getEducations();
            return;
        }
        const response = await educationService.findAllByEmployeeId(value);

        if (response.statusCode === 200) {
            setEducations(response.data)
        }
    }

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getEducations(page, pageSize);
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
                            <span className="text-gray-900">Danh sách trình độ học vấn</span>
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
                        Thêm trình độ học vấn
                    </button>
                    <select
                        className="ml-2 px-4 text-sm py-[10px] border border-black rounded-sm outline-none"
                        onChange={handleEmployeeChange}
                    >
                        <option className="bg-white" value="all">Tất cả</option>
                        {employees?.map(employee => <option value={employee.maNhanVien} key={employee.maNhanVien}>
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
                                Tên trình độ học vấn
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
                                Chuyên ngành
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Tên trường
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Năm tốt nghiệp
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Bằng cấp
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
                        {educations?.map((education) => (
                            <tr key={education.maTrinhDo}>

                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {education.tenTrinhDoHocVan}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {education.nhanVien.hoTen}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {education.chuyenNganh}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {education.tenTruong}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {education.namTotNghiep}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {education.bangCap}
                                </td>

                                <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                                    <div className="flex gap-x-2 items-center">
                                        <button
                                            onClick={() => handleClickEdit(education)}
                                            className="bg-sky-400 hover:bg-sky-500 px-4 rounded-sm py-1 text-white"
                                        >
                                            Sửa
                                        </button>
                                        <button
                                            onClick={() => handleDeleteEdit(education)}
                                            className="bg-red-400 hover:bg-red-500 px-4 rounded-sm py-1 text-white"
                                        >
                                            Xóa
                                        </button>

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

            <AddEducationModal isOpen={showAddModal} handleCancel={() => setShowAddModal(false)} onEducationCreated={getEducations} handleConfirm={() => { }} />
            {selectedEducation && showEditModal && <EditEducationModal isOpen={showEditModal} handleCancel={() => setShowEditModal(false)} selectedEducation={selectedEducation} onEducationUpdated={getEducations} handleConfirm={() => { }} />}
            {selectedEducation && showDeleteModal && <ConfirmModal title="Thông báo" description="Bạn có muốn xóa cái này không" isOpen={showDeleteModal} handleCancel={() => setShowDeleteModal(false)} handleConfirm={handleRemove} />}
        </div>
    );
}