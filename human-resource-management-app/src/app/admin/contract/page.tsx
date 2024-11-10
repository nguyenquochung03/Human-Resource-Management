"use client";
import { Button } from "@/components/button";
import ConfirmModal from "@/components/comfirm-modal";
import { ChevronLeftIcon, ChevronRightIcon, HomeIcon } from "@heroicons/react/24/solid";
import { ChangeEventHandler, useEffect, useState } from "react";

import toast from "react-hot-toast";

import Link from "next/link";
import Contract from "@/models/contract";
import contractService from "@/services/contractService";
import AddContractModal from "./components/add-contract-modal";
import EditContractModal from "./components/edit-contract-modal";
import formatDate from "@/utils/formatDate";
import Employee from "@/models/employee";
import employeeService from "@/services/employeeService";
import Pagination from "@/components/Pagination";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";


export default function AdminContract() {
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [selectedContract, setSelectedContract] = useState<Contract | null>(null);

    const [contracts, setContracts] = useState<any[]>([]);
    const [employees, setEmployees] = useState<Employee[]>([]);

    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const getContracts = async (page = 1, size = 10) => {
        const response = await contractService.getByPage(page, size);
        if (response?.statusCode === 200) {
            setContracts(response.data.items);
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
        getContracts();
        getEmployees();
    }, [])

    const handleClickEdit = (contract: any) => {
        setShowEditModal(true)

        const { nhanVien, ...newContract } = contract;
        setSelectedContract({
            ...newContract,
            nhanVienId: nhanVien.maNhanVien
        })
    }

    const handleDeleteEdit = (contract: any) => {
        setShowDeleteModal(true)

        const { nhanVien, ...newContract } = contract;
        setSelectedContract({
            ...newContract,
            nhanVienId: nhanVien.maNhanVien
        })
    }

    const handleRemove = async () => {
        const response = await contractService.delete(selectedContract?.maHopDong);
        if (response?.statusCode === 204) {
            getContracts();
            setShowDeleteModal(false);
            toast.success(response?.message)
        }
    }

    const handleEmployeeChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        const { value } = event.target;
        if (value === "all") {

            getContracts();
            return;
        }
        const response = await contractService.findAllByEmployeeId(value);

        if (response.statusCode === 200) {
            setContracts(response.data)
        }
    }

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getContracts(page, pageSize);
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
                            <span className="text-gray-900">Danh sách hợp đồng</span>
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
                        Thêm hợp đồng
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

                <table className="divide-y divide-gray-200 overflow-x-scroll">
                    <thead className="bg-gray-50">
                        <tr>

                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Loại hợp đồng
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
                                Nội dung hợp đồng
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Trạng thái hợp đồng
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
                        {contracts?.map((contract) => (
                            <tr key={contract.maHopDong}>

                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {contract.loaiHopDong}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {contract.nhanVien.hoTen}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {formatDate(contract.ngayBatDau)}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatDate(contract.ngayKetThuc)}
                                </td>

                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {contract.noiDungHopDong}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {contract.trangThaiHopDong}
                                </td>

                                <td className="px-6 py-4 whitespace-nowrap  text-sm font-medium">

                                    <div className="flex gap-x-2">
                                        <Link className="bg-green-500 hover:bg-green-600 px-4 py-2 rounded-sm text-white" href={`/admin/contract/${contract.maHopDong}`} >Xem</Link>
                                        <button className="bg-sky-500 hover:bg-sky-600 px-4 py-2 rounded-sm text-white" onClick={() => handleClickEdit(contract)} >Sửa</button>
                                        <button className="bg-red-500 hover:bg-red-600 px-4 py-2 rounded-sm text-white" onClick={() => handleDeleteEdit(contract)} >Xóa</button>
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

            <AddContractModal isOpen={showAddModal} handleCancel={() => setShowAddModal(false)} onContractCreated={getContracts} handleConfirm={() => { }} />
            {selectedContract && showEditModal && <EditContractModal isOpen={showEditModal} handleCancel={() => setShowEditModal(false)} selectedContract={selectedContract} onContractUpdated={getContracts} handleConfirm={() => { }} />}
            {selectedContract && showDeleteModal && <ConfirmModal title="Thông báo" description="Bạn có muốn xóa cái này không" isOpen={showDeleteModal} handleCancel={() => setShowDeleteModal(false)} handleConfirm={handleRemove} />}
        </div>
    );
}