"use client";
import { Button } from "@/components/button";
import { ChevronRightIcon, HomeIcon } from "@heroicons/react/24/solid";
import React, { ChangeEvent, useEffect, useRef, useState } from "react";
import toast from "react-hot-toast";
import Link from "next/link";
import ConfirmModal from "@/components/comfirm-modal";
import Bonus from "@/models/bonus";
import bonusService from "@/services/bonusService";
import AddBonusModal from "./components/add-bonus-modal";
import EditBonusModal from "./components/edit-bonus-modal";
import AddBonusForEmployeeModal from "./components/add-bonus-for-employee";
import formatDate from "@/utils/formatDate";
import formatCurrency from "@/utils/formatVND";
import Pagination from "@/components/Pagination";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMoneyBill, faPlus } from "@fortawesome/free-solid-svg-icons";


export default function AdminBonus() {
    const [showAddModal, setShowAddModal] = useState(false);
    const [showAddBonusModal, setShowAddBonusModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [selectedBonus, setSelectedBonus] = useState<Bonus | null>(null);

    const [bonuses, setBonuses] = useState<any[]>([]);

    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);
   

    const getBonuses = async (page = 1, size = 10) => {
        const response = await bonusService.getByPage(page, size);
        console.log(response)
        if (response?.statusCode === 200) {
            setBonuses(response.data.items);
            setTotalCount(response.data.totalCount);
            setTotalPages(response.data.totalPages);
        }
    }

    useEffect(() => {
        getBonuses();
    }, [])

    const handleClickEdit = (bonus: any) => {
        setShowEditModal(true)
        setSelectedBonus(bonus)
    }

    const handleDeleteEdit = (bonus: any) => {
        setShowDeleteModal(true)
        setSelectedBonus(bonus)
    }

    const handleRemove = async () => {
        const response = await bonusService.delete(selectedBonus?.maKhoanThuong);
        if (response?.statusCode === 204) {
            getBonuses();
            setShowDeleteModal(false);
            toast.success(response?.message)
        } else toast.error(response?.message)
    }

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getBonuses(page, pageSize);
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
                            <span className="text-gray-900">Danh sách thưởng</span>
                        </li>
                    </ol>
                </nav>
            </div>

            <div className="bg-white shadow-md p-4 rounded-md">
                <div className="mb-5 pb-3 border-b-2 border-sky-400 flex gap-x-4">
                    <button
                        className="bg-green-600 hover:bg-green-700 px-3 py-2 rounded-sm text-white flex items-center gap-x-1"
                        onClick={() => setShowAddModal(true)}
                    >
                        <FontAwesomeIcon
                            icon={faPlus}
                        />
                        Thêm thưởng
                    </button>
                    <button
                        className="bg-green-600 hover:bg-green-700 px-3 py-2 rounded-sm text-white flex items-center gap-x-1"
                        onClick={() => setShowAddBonusModal(true)}
                    >
                        <FontAwesomeIcon
                            icon={faMoneyBill}
                        />
                        Cấp thưởng cho nhân viên
                    </button>
                
                </div>

                <table className="min-w-full divide-y divide-gray-200 overflow-x-auto">
                    <thead className="bg-gray-50">
                        <tr>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Mã khoản thưởng
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Ngày khen thưởng
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Số tiền thưởng
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Lý do khen thưởng
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
                        {bonuses?.map((bonus) => (
                            <tr key={bonus.maKhoanThuong}>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {bonus.maKhoanThuong}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {formatDate(bonus.ngayKhenThuong)}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {formatCurrency(bonus.soTienThuong)}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {bonus.lyDoKhenThuong}
                                </td>

                                <td className="px-6 py-4 whitespace-nowrap  text-sm font-medium">
                                    <div className="flex gap-x-2">
                                        <button className="bg-sky-500 hover:bg-sky-600 px-4 py-2 rounded-sm text-white" onClick={() => handleClickEdit(bonus)} >Sửa</button>
                                        <button className="bg-red-500 hover:bg-red-600 px-4 py-2 rounded-sm text-white" onClick={() => handleDeleteEdit(bonus)} >Xóa</button>
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

            {showAddBonusModal && <AddBonusForEmployeeModal
                handleCancel={() => setShowAddBonusModal(false)}
                isOpen={showAddBonusModal}
            />}
            <AddBonusModal
                isOpen={showAddModal} handleCancel={() => setShowAddModal(false)} onBonusCreated={getBonuses} handleConfirm={() => { }} />
            {selectedBonus && showEditModal && <EditBonusModal isOpen={showEditModal} handleCancel={() => setShowEditModal(false)} selectedBonus={selectedBonus} onBonusUpdated={getBonuses} handleConfirm={() => { }} />}
            {selectedBonus && showDeleteModal && <ConfirmModal title="Thông báo" description="Bạn có muốn xóa cái này không" isOpen={showDeleteModal} handleCancel={() => setShowDeleteModal(false)} handleConfirm={handleRemove} />}
        </div>
    );
}