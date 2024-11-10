"use client";
import { Button } from "@/components/button";
import { ChevronRightIcon, HomeIcon } from "@heroicons/react/24/solid";
import { ChangeEventHandler, useEffect, useState } from "react";
import toast from "react-hot-toast";
import Link from "next/link";
import Allowance from "@/models/allowance";
import allowanceService from "@/services/allowanceService";
import AddAllowanceModal from "./components/add-allowance-modal";
import EditAllowanceModal from "./components/edit-allowance-modal";
import ConfirmModal from "@/components/comfirm-modal";
import AddAllowanceForEmployeeModal from "./components/add-allowance-for-employee";
import Employee from "@/models/employee";
import employeeService from "@/services/employeeService";
import Pagination from "@/components/Pagination";
import Status, { EStatus } from "@/components/Status";
import formatCurrency from "@/utils/formatVND";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";


export default function AdminAllowance() {
    const [showAddModal, setShowAddModal] = useState(false);
    const [showAddAllowanceModal, setShowAddAllowanceModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [selectedAllowance, setSelectedAllowance] = useState<Allowance | null>(null);

    const [allowances, setAllowances] = useState<any[]>([]);
    const [employees, setEmployees] = useState<Employee[]>([]);

    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const getAllowances = async (page = 1, size = 10) => {
        const response = await allowanceService.getByPage(page, size);
        if (response?.statusCode === 200) {
            setAllowances(response.data.items);
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
        getAllowances();
        getEmployees();
    }, [])

    const handleClickEdit = (allowance: any) => {
        setShowEditModal(true)
        setSelectedAllowance(allowance)
    }

    const handleDeleteEdit = (allowance: any) => {
        setShowDeleteModal(true)
        setSelectedAllowance(allowance)
    }

    const handleRemove = async () => {
        const response = await allowanceService.delete(selectedAllowance?.maPhuCap);
        if (response?.statusCode === 204) {
            getAllowances();
            setShowDeleteModal(false);
            toast.success(response?.message)
        } else toast.error(response?.message)
    }

    const handleEmployeeChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        const { value } = event.target;
        if (value === "all") {

            getAllowances();
            return;
        }
        const response = await allowanceService.findAllByEmployeeId(value);

        if (response.statusCode === 200) {
            setAllowances(response.data)
        }
    }

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getAllowances(page, pageSize);
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
                            <span className="text-gray-900">Danh sách phụ cấp</span>
                        </li>
                    </ol>
                </nav>
            </div>

            <div className="bg-white shadow-md p-4 rounded-md">
                <div className="flex gap-x-4 mb-5 pb-3 border-b-2 border-sky-400">
                    <button
                        className="bg-green-600 hover:bg-green-700 px-3 py-2 rounded-sm text-white flex items-center gap-x-1"
                        onClick={() => setShowAddModal(true)}
                    >
                        <FontAwesomeIcon
                            icon={faPlus}
                        />
                        Thêm mới
                    </button>
                    <button
                        className="bg-green-600 hover:bg-green-700 px-3 py-2 rounded-sm text-white flex items-center gap-x-1"
                        onClick={() => setShowAddAllowanceModal(true)}
                    >
                        <FontAwesomeIcon
                            icon={faPlus}
                        />
                        Thêm phụ cấp cho nhân viên
                    </button>
                    <select
                        className="px-4 py-1 border border-black rounded-sm outline-none"
                        onChange={handleEmployeeChange}
                    >
                        <option className="bg-white py-2" value="all">Tất cả</option>
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
                                Mã phụ cấp
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Tên phụ cấp
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Số tiền phụ cấp
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Tần suất
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
                                Thao tác
                            </th>
                        </tr>
                    </thead>
                    <tbody className="bg-white divide-y divide-gray-200">
                        {allowances?.map((allowance) => (
                            <tr key={allowance.maPhuCap}>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {allowance.maPhuCap}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {allowance.tenPhuCap}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {formatCurrency(allowance.soTienPhuCap)}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {allowance.tanSuat}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    <Status
                                        title={allowance.trangThai}
                                        status={allowance.trangThai == "Đang hiệu lực"
                                            ? EStatus.AVAILABLE
                                            : EStatus.UNAVAILABLE
                                        }
                                    />
                                </td>

                                <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                                    <div className="flex gap-x-2">
                                        <button className="bg-sky-500 hover:bg-sky-600 px-4 py-2 rounded-sm text-white" onClick={() => handleClickEdit(allowance)} >Sửa</button>
                                        <button className="bg-red-500 hover:bg-red-600 px-4 py-2 rounded-sm text-white" onClick={() => handleDeleteEdit(allowance)} >Xóa</button>
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

            {showAddAllowanceModal && <AddAllowanceForEmployeeModal
                isOpen={showAddAllowanceModal}
                handleCancel={() => setShowAddAllowanceModal(false)}
            />}
            <AddAllowanceModal
                isOpen={showAddModal} handleCancel={() => setShowAddModal(false)} onAllowanceCreated={getAllowances} handleConfirm={() => { }} />
            {selectedAllowance && showEditModal && <EditAllowanceModal isOpen={showEditModal} handleCancel={() => setShowEditModal(false)} selectedAllowance={selectedAllowance} onAllowanceUpdated={getAllowances} handleConfirm={() => { }} />}
            {selectedAllowance && showDeleteModal && <ConfirmModal title="Thông báo" description="Bạn có muốn xóa cái này không" isOpen={showDeleteModal} handleCancel={() => setShowDeleteModal(false)} handleConfirm={handleRemove} />}
        </div>
    );
}