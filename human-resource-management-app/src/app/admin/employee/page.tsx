"use client";
import { ChevronRightIcon, HomeIcon } from "@heroicons/react/24/solid";
import { ChangeEvent, useEffect, useState } from "react";
import ConfirmModal from "../../../components/comfirm-modal";
import AddEmployeeModal from "./components/add-employee-modal";
import EditEmployeeModal from "./components/edit-employee-modal";
import { Button } from "@/components/button";
import Employee from "@/models/employee";
import employeeService from "@/services/employeeService";
import Link from "next/link";
import toast from "react-hot-toast";
import Pagination from "@/components/Pagination";
import TableSkeleton from "@/components/skeleton/TableSkeleton";
import formatCurrency from "@/utils/formatVND";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import images from "@/assets";

export default function AdminEmployee() {
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [selectedEmployee, setSelectedEmployee] = useState<Employee | null>(null);
    const [searchValue, setSearchValue] = useState('')
    const [isLoading, setIsLoading] = useState(true)

    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const getEmployees = async (page = 1, size = 10, queryType = "", queryValue = "") => {
        
        const response = await employeeService.getByPage(page, size, queryType, queryValue);
        setIsLoading(false)
        if (response?.statusCode === 200) {
            setEmployees(response.data.items)
            setTotalCount(response.data.totalCount);
            setTotalPages(response.data.totalPages);
        }
    }

    useEffect(() => {
        getEmployees(currentPage, pageSize);
    }, [])

    const handleClickEdit = (employee: any) => {
        setShowEditModal(true)
        const { phongBan, chucVu, ...newEployee } = employee
        setSelectedEmployee({
            ...newEployee,
            phongBanId: phongBan.soPhong,
            chucVuId: chucVu.maChucVu,
        })

    }

    const handleClickDelete = (employee: any) => {
        setShowDeleteModal(true)
        const { phongBan, chucVu, ...newEployee } = employee
        setSelectedEmployee({
            ...newEployee,
            phongBanId: phongBan.soPhong,
            chucVuId: chucVu.maChucVu,
        })
    }

    const handleRemove = async () => {
        const response = await employeeService.delete(selectedEmployee?.maNhanVien);

        if (response?.statusCode === 204) {
            getEmployees(1, pageSize);
            setShowDeleteModal(false);
            toast.success(response.message);

        } else toast.error(response.message);
    }

    const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
        if (e.target.value === '') {
            setSearchValue(e.target.value);
            getEmployees(1, pageSize);
        } else
            setSearchValue(e.target.value);
    }

    const handleSearchByPhone = async () => {
        await getEmployees(currentPage, pageSize, "phone", searchValue)
    }

    const handleSearchByName = async () => {
        await getEmployees(currentPage, pageSize, "name", searchValue);
    }

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getEmployees(page, pageSize);
    }

    const handlePagePrevious = async () => {
        handlePageChange(currentPage - 1)
    }

    const handlePageNext = async () => {
        handlePageChange(currentPage + 1)
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
                                <span className="text-gray-900">Danh sách nhân viên</span>
                            </li>
                        </ol>
                    </nav>
                </div>

                <div className="bg-white shadow-md p-4 rounded-md">
                    <div className="mb-5 pb-3 border-b-2 border-sky-400 flex justify-between">
                        <button
                            className="bg-green-600 hover:bg-green-700 px-2 py-2 rounded-sm text-white flex items-center gap-x-2"
                            onClick={() => setShowAddModal(true)}
                        >
                            <FontAwesomeIcon
                                icon={faPlus}
                            />
                            Thêm mới
                        </button>

                        <div className="flex gap-x-2">
                            <input
                                value={searchValue}
                                className='px-2 rounded-sm outline-none border-[1px] border-green-600'
                                placeholder="Tìm kiếm ở đây"
                                onChange={handleSearchChange}
                            />

                            <button
                                disabled={searchValue === ''}
                                type="button"
                                onClick={handleSearchByPhone}
                                className='bg-green-600 hover:bg-green-700 mx-[1px] text-sm py-1 px-2 rounded-sm text-white'>Tìm theo SDT</button>
                            <button
                                disabled={searchValue === ''}
                                type="button"
                                onClick={handleSearchByName}
                                className='bg-green-600 hover:bg-green-700 mx-[1px] text-sm py-1 px-2 rounded-sm text-white'>Tìm theo tên</button>
                        </div>
                    </div>

                    <table className="min-w-full divide-y divide-gray-200 overflow-x-auto">
                        <thead className="bg-gray-50">
                            <tr>
                                <th
                                    scope="col"
                                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                >
                                    Họ tên
                                </th>
                                <th
                                    scope="col"
                                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                >
                                    Mã nhân viên
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
                                    Mức lương
                                </th>
                                <th
                                    scope="col"
                                    className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                >
                                    Số điện thoại
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
                            {employees?.length > 0 ? employees.map((employee) => (
                                <tr key={employee.maNhanVien}>
                                    <td className="px-6 py-4 whitespace-nowrap">
                                        <div className="flex items-center">
                                            <div className="flex-shrink-0 h-10 w-10">
                                                <picture><img
                                                    className="h-10 w-10 rounded-full"
                                                    src={`${employee.gioiTinh == "Nam" ? images.boyImage.src : images.girlImage.src}`}
                                                    alt=""
                                                /></picture>
                                            </div>
                                            <div className="ml-4">
                                                <div className="text-sm font-medium text-gray-900">{employee.hoTen}</div>
                                                <div className={`text-sm ${employee.gioiTinh === "Nam" ? "text-blue-500" : "text-pink-500"}`}>{employee.gioiTinh}</div>
                                            </div>
                                        </div>
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap">
                                        <div className="text-sm text-gray-900">
                                            {employee.maNhanVien}
                                        </div>
                                        <div className="text-sm text-gray-500">{employee.email}</div>
                                    </td>

                                    <td className="px-6 py-4 whitespace-nowrap">
                                        <span className={`px-2 inline-flex text-xs leading-5 font-semibold rounded-full ${employee.trangThai === "Nhân viên thực tập" ? "bg-green-100 text-green-800" : "bg-red-100 text-red-800"}`}>
                                            {employee.trangThai}
                                        </span>
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {formatCurrency(employee.mucLuong)}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {employee.soDienThoai}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                                        <div className="flex gap-x-2">
                                            <button className='px-4 py-1 rounded-sm bg-green-500 hover:bg-green-600 text-white'>
                                                <Link className="block" href={`/admin/employee/${employee.maNhanVien}`}>Xem</Link>
                                            </button>
                                            <button className='px-4 py-1 rounded-sm bg-sky-500 hover:bg-sky-600 text-white' onClick={() => handleClickEdit(employee)} >Sửa</button>
                                            <button className='px-4 py-1 rounded-sm bg-red-500 hover:bg-red-600 text-white' onClick={() => handleClickDelete(employee)} >Xóa</button>
                                        </div>
                                    </td>
                                </tr>
                            )) : <tr>
                                <td className="text-center" colSpan={7}><i className="text-sm my-2">Không có nhân viên nào</i></td>
                            </tr>}
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
                <AddEmployeeModal isOpen={showAddModal} handleCancel={() => setShowAddModal(false)} handleConfirm={() => { }} onEmployeeCreated={getEmployees} />
                {
                    selectedEmployee && showEditModal && <EditEmployeeModal isOpen={showEditModal} handleCancel={() => setShowEditModal(false)} selectedEmployee={selectedEmployee} handleConfirm={() => { }} onEmployeeUpdated={getEmployees} />
                }
                {selectedEmployee && showDeleteModal && <ConfirmModal title="Thông báo" description="Bạn có muốn xóa cái này không" isOpen={showDeleteModal} handleCancel={() => setShowDeleteModal(false)} handleConfirm={handleRemove} />}
            </div>

    );
}