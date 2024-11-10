"use client";
import React, { ChangeEvent, ChangeEventHandler, useEffect, useState } from 'react'
import AddPositionModal from './components/addPositionModal';
import { ChevronRightIcon, HomeIcon } from '@heroicons/react/24/solid';
import { Button } from '@/components/button';
import positionService from '@/services/positionService';
import Position from '@/models/position';
import Link from 'next/link';
import ConfirmModal from '@/components/comfirm-modal';
import EditPositionModal from './components/editPositionModal';
import toast from 'react-hot-toast';
import Department from '@/models/department';
import departmentService from '@/services/departmentService';
import Pagination from '@/components/Pagination';
import formatCurrency from '@/utils/formatVND';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

export default function AdminPosition() {
    const [showAddModal, setShowAddModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [selectedPosition, setSelectedPosition] = useState<Position | null>(null);
    const [departments, setDepartments] = useState<Department[]>([]);
    const [selectedDepartment, setSelectedDepartment] = useState('')

    const [positions, setPositions] = useState<any[]>([]);
    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [searchValue, setSearchValue] = useState('')

    const getPositions = async (page: number, size: number, phongBanId: string = "", searchChucVu: string = "") => {
        const response = await positionService.getByPage(page, size, phongBanId, searchChucVu);
        if (response?.statusCode === 200) {
            setPositions(response.data.items);
            setTotalCount(response.data.totalCount);
            setTotalPages(response.data.totalPages);
        }
    }

    const getDepartments = async () => {
        const response = await departmentService.findAll();
        if (response?.statusCode === 200) {
            setDepartments(response.data);
        }
    }

    useEffect(() => {
        getPositions(currentPage, pageSize);
        getDepartments();
    }, [])

    const handleClickEdit = (position: any) => {
        setShowEditModal(true)
        const { phongBan, ...newPosition } = position
        setSelectedPosition({
            ...newPosition,
            phongBanId: phongBan.soPhong
        })
    }

    const handleDeleteEdit = (position: any) => {
        setShowDeleteModal(true)

        const { phongBan, ...newPosition } = position
        setSelectedPosition({
            ...newPosition,
            phongBanId: phongBan.soPhong
        })
    }

    const onPositionUpdated = () => {
        getPositions(currentPage, pageSize);
        setSelectedPosition(null)
    }

    const handleRemove = async () => {
        const response = await positionService.delete(selectedPosition?.maChucVu);
        if (response?.statusCode === 204) {
            getPositions(1, pageSize);
            setShowDeleteModal(false);
            toast.success(response?.message)
        } else toast.error(response?.message)
    }

    const handleDepartmentChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        const { value } = event.target;
        if (value === "all") {
            setSelectedDepartment('')
            getPositions(currentPage, pageSize);
            return;
        }

        getPositions(currentPage, pageSize, value);
        setSelectedDepartment(value)

    }

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getPositions(page, pageSize);
    }

    const handlePagePrevious = async () => {
        handlePageChange(currentPage - 1)
    }

    const handlePageNext = async () => {
        handlePageChange(currentPage + 1)
    }

    const handleSearch = async () => {
        console.log(selectedDepartment)
        getPositions(currentPage, pageSize, selectedDepartment, searchValue)
    }

    const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
        console.log(e.target.value)
        setSearchValue(e.target.value);

        if (e.target.value.length === 0) {
            getPositions(currentPage, pageSize, selectedDepartment, e.target.value)
        }


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
                <div className="mb-5 pb-3 border-b-2 border-sky-400 flex items-center justify-between">
                    <button
                        className="bg-green-600 hover:bg-green-700 px-3 py-2 rounded-sm text-white flex items-center gap-x-1"
                        onClick={() => setShowAddModal(true)}
                    >
                        <FontAwesomeIcon
                            icon={faPlus}
                        />
                        Thêm mới
                    </button>
                    <div className='flex gap-x-6'>
                        <div className='flex items-center'>
                            <p className='font-semibold'>Chọn phòng ban:</p>
                            <select
                                className="ml-4 px-4 text-sm py-[10px] border border-black rounded-sm outline-none"
                                onChange={handleDepartmentChange}
                            >
                                <option className="bg-white" value="all">Tất cả</option>
                                {departments?.map(department => <option value={department.soPhong} key={department.soPhong}>
                                    {department.tenPhong}
                                </option>)}
                            </select>
                        </div>

                        <div className="flex">
                            <input
                                value={searchValue}
                                className='px-2 rounded-sm outline-none border-[1px] border-green-600'
                                placeholder="Tìm kiếm ở đây"
                                onChange={handleSearchChange}
                            />

                            <button
                                disabled={searchValue === ''}
                                onClick={handleSearch}
                                className='bg-green-600 mx-[1px] text-sm py-1 px-2 rounded-sm text-white'>Tìm kiếm</button>
                        </div>
                    </div>
                </div>

                <table className="min-w-full divide-y divide-gray-200 overflow-x-auto">
                    <thead className="bg-gray-50">
                        <tr>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Mã chức vụ
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Tên chức vụ
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Phòng ban
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Mô tả công việc
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
                                Thao tác
                            </th>
                        </tr>
                    </thead>
                    <tbody className="bg-white divide-y divide-gray-200">
                        {positions.map((pos) => (
                            <tr key={pos.maChucVu}>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {pos.maChucVu}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {pos.tenChucVu}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {pos?.phongBan?.tenPhong}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {pos.moTaCongViec}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {formatCurrency(pos.mucLuong)}
                                </td>

                                <td className="px-6 py-4 whitespace-nowrap  text-sm font-medium">

                                    <div className="flex gap-x-2">
                                        <button className="bg-sky-500 hover:bg-sky-600 px-4 py-2 rounded-sm text-white" onClick={() => handleClickEdit(pos)} >Sửa</button>
                                        <button className="bg-red-500 hover:bg-red-600 px-4 py-2 rounded-sm text-white" onClick={() => handleDeleteEdit(pos)} >Xóa</button>
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

            <AddPositionModal isOpen={showAddModal} handleCancel={() => setShowAddModal(false)} onPositionAdded={() => getPositions(1, pageSize)} handleConfirm={() => { }} />
            {selectedPosition && showEditModal && <EditPositionModal isOpen={showEditModal} handleCancel={() => setShowEditModal(false)} selectedPosition={selectedPosition} onPositionUpdated={onPositionUpdated} handleConfirm={() => { }} />}
            {selectedPosition && showDeleteModal && <ConfirmModal title="Thông báo" description="Bạn có muốn xóa cái này không" isOpen={showDeleteModal} handleCancel={() => setShowDeleteModal(false)} handleConfirm={handleRemove} />}
        </div>
    );
}
