"use client";
import ConfirmModal from '@/components/comfirm-modal';
import Pagination from '@/components/Pagination';
import Employee from '@/models/employee';
import WorkSchedule from '@/models/workSchedule';
import employeeService from '@/services/employeeService';
import workScheduleService from '@/services/workScheduleService';
import { ChevronRightIcon, HomeIcon } from '@heroicons/react/24/solid';
import Link from 'next/link';
import { ChangeEventHandler, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import AddWorkScheduleForEmployee from './components/add-work-schedule-for-employee';
import AddWorkSchedule from './components/add-work-schedule-modal';
import EditWorkScheduleModal from './components/edit-work-schedule-modal';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowLeft, faArrowRight, faPlus } from '@fortawesome/free-solid-svg-icons';
import { format, addDays, addMonths, eachDayOfInterval, endOfMonth, getDay, startOfMonth, subMonths, isSameDay } from 'date-fns';
import { vi } from 'date-fns/locale';
import formatDate from '@/utils/formatDate';

export default function AdminWorkSchedule() {
    const [showAddModal, setShowAddModal] = useState(false);
    const [showAddScheduleModal, setShowAddScheduleModal] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [selectedWorkSchedule, setSelectedWorkSchedule] = useState<WorkSchedule | null>(null);

    const [workSchedules, setWorkSchedules] = useState<WorkSchedule[]>([]);
    const [employees, setEmployees] = useState<Employee[]>([]);

    const [totalCount, setTotalCount] = useState(0);
    const [totalPages, setTotalPages] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [isGrid, setIsGrid] = useState(true)


    const [currentMonth, setCurrentMonth] = useState<Date>(new Date());
    const [start, setStart] = useState(startOfMonth(currentMonth))
    const [end, setEnd] = useState(endOfMonth(currentMonth))
    const [days, setDays] = useState(eachDayOfInterval({ start, end }))
    const [startDayOffset, setStartDayOffset] = useState((getDay(start) + 6) % 7);
    const [firstDayOffsetDate, setFirstDayOffsetDate] = useState(addDays(start, -startDayOffset));


    const handlePrevMonth = (): void => {
        const prevMonth = subMonths(currentMonth, 1);
        setCurrentMonth(prevMonth);

        const startOfPrevMonth = startOfMonth(prevMonth);
        setStart(startOfPrevMonth)

        const startDayOffset = (getDay(startOfPrevMonth) + 6) % 7;
        setStartDayOffset(startDayOffset)

        const firstDayOffsetDate = addDays(startOfPrevMonth, -startDayOffset);
        setFirstDayOffsetDate(firstDayOffsetDate)

        const endOfPrevMonth = endOfMonth(prevMonth);
        setEnd(endOfPrevMonth)

        setDays(eachDayOfInterval({ start: firstDayOffsetDate, end: endOfPrevMonth }))
        getWorkSchedulesFromTo(firstDayOffsetDate, endOfPrevMonth);

    };

    const handleNextMonth = (): void => {
        const nextMonth = addMonths(currentMonth, 1);
        setCurrentMonth(nextMonth);

        const startOfNextMonth = startOfMonth(nextMonth);
        setStart(startOfNextMonth)

        const startDayOffset = (getDay(startOfNextMonth) + 6) % 7;
        setStartDayOffset(startDayOffset)

        const firstDayOffsetDate = addDays(startOfNextMonth, -startDayOffset);
        setFirstDayOffsetDate(firstDayOffsetDate)

        const endOfNextMonth = endOfMonth(nextMonth);
        setEnd(endOfNextMonth)

        setDays(eachDayOfInterval({ start: firstDayOffsetDate, end: endOfNextMonth }))
        getWorkSchedulesFromTo(firstDayOffsetDate, endOfNextMonth);

    };

    const getEmployees = async () => {
        const response = await employeeService.findAll();
        if (response?.statusCode === 200) {
            setEmployees(response.data);
        }
    }

    const getWorkSchedules = async (page = 1, size = 10) => {
        const response = await workScheduleService.getByPage(page, size);
        console.log(response.data)
        if (response?.statusCode === 200) {
            setWorkSchedules(response.data.items);
            setTotalCount(response.data.totalCount);
            setTotalPages(response.data.totalPages);
        }
    }

    const getWorkSchedulesFromTo = async (from: Date, to: Date) => {
        const response = await workScheduleService.findAllFromTo(from, to);
        console.log(response.data)
        if (response?.statusCode === 200) {
            setWorkSchedules(response.data);

        }
    }

    useEffect(() => {
        if (isGrid)
            getWorkSchedulesFromTo(start, end);
        else getWorkSchedules(currentPage, pageSize)
        getEmployees();
    }, [isGrid])

    const handleClickEdit = (workSchedule: any) => {
        setShowEditModal(true)
        setSelectedWorkSchedule(workSchedule)
    }

    const handleDeleteEdit = (workSchedule: any) => {
        setShowDeleteModal(true)
        setSelectedWorkSchedule(workSchedule)
    }

    const onWorkScheduleUpdated = () => {
        getWorkSchedules();
        setSelectedWorkSchedule(null)
    }

    const handleRemove = async () => {
        const response = await workScheduleService.delete(selectedWorkSchedule?.maLichLam);
        if (response?.statusCode === 204) {
            getWorkSchedules();
            setShowDeleteModal(false);
            toast.success(response?.message)
        } else toast.error(response?.message)
    }

    const handleEmployeeChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        const { value } = event.target;
        if (value === "all") {

            getWorkSchedules();
            return;
        }
        const response = await workScheduleService.findAllByEmployeeId(value);

        if (response.statusCode === 200) {
            setWorkSchedules(response.data)
        }
    }

    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        getWorkSchedules(page, pageSize);
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
                            <span className="text-gray-900">Danh sách lịch làm</span>
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
                        Thêm mới
                    </button>
                    <button
                        className="bg-green-600 hover:bg-green-700 px-3 py-2 rounded-sm text-white flex items-center gap-x-1"
                        onClick={() => setShowAddScheduleModal(true)}
                    >
                        <FontAwesomeIcon
                            icon={faPlus}
                        />
                        Thêm lịch làm cho nhân viên
                    </button>
                    {!isGrid && <select
                        className="px-4 py-1 border border-black rounded-sm outline-none"
                        onChange={handleEmployeeChange}
                    >
                        <option className="bg-white py-2" value="all">Tất cả</option>
                        {employees?.map(employee => <option value={employee.maNhanVien} key={employee.maNhanVien}>
                            {employee.hoTen}
                        </option>)}
                    </select>}
                    <button
                        className="bg-green-600 hover:bg-green-700 px-3 py-2 rounded-sm text-white flex items-center gap-x-1"
                        onClick={() => setIsGrid(!isGrid)}
                    >{isGrid ? "Xem dạng bảng" : "Xem dạng lịch"}</button>
                </div>

                {!isGrid && <table className="min-w-full divide-y divide-gray-200 overflow-x-auto">
                    <thead className="bg-gray-50">
                        <tr>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Mã lịch làm
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Ngày làm
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Loại lịch trình
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Giờ bắt đầu
                            </th>
                            <th
                                scope="col"
                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                            >
                                Giờ kết thúc
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
                        {workSchedules?.map((workSchedule) => (
                            <tr key={workSchedule.maLichLam}>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {workSchedule.maLichLam}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {formatDate(workSchedule.ngayLam.toString())}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                    {workSchedule.loaiLich}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {workSchedule.loaiLich !== "Nghỉ lễ" ? workSchedule.gioBatDau : "###"}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {workSchedule.loaiLich !== "Nghỉ lễ" ? workSchedule.gioKetThuc : "###"}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {workSchedule.moTaCongViec}
                                </td>
                                <td className="max-w-[200px] truncate px-6 py-4 text-sm text-gray-500">
                                    {workSchedule.ghiChu}
                                </td>

                                <td className="px-6 py-4 whitespace-nowrap  text-sm font-medium">

                                    <div className="flex gap-x-2">
                                        <button className="bg-sky-500 hover:bg-sky-600 px-4 py-2 rounded-sm text-white" onClick={() => handleClickEdit(workSchedule)} >Sửa</button>
                                        <button className="bg-red-500 hover:bg-red-600 px-4 py-2 rounded-sm text-white" onClick={() => handleDeleteEdit(workSchedule)} >Xóa</button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>}

                {isGrid && <>
                    <div className='flex justify-between mb-4 items-start'>
                        <div className='flex gap-x-2'>
                            <button onClick={handlePrevMonth} className="py-2 px-4 rounded-sm text-white bg-green-500 flex gap-x-2 items-center">
                                <FontAwesomeIcon
                                    icon={faArrowLeft}
                                />
                                Prev
                            </button>
                            <button onClick={handleNextMonth} className="py-2 px-4 rounded-sm text-white bg-green-500 flex gap-x-2 items-center">
                                Next
                                <FontAwesomeIcon
                                    icon={faArrowRight}
                                />
                            </button>
                        </div>

                        <div className='flex gap-x-2'>
                            <span className="bg-green-200 text-green-700 px-1 py-[1px] text-sm rounded-md">Ngày làm việc</span>
                            <span className="bg-sky-200 px-1 py-[1px] text-sm rounded-md text-sky-700">Nghỉ lễ</span>
                        </div>
                    </div>

                    <div className="grid grid-cols-7">
                        <div className="px-2 py-2 text-center font-semibold text-lg">Thứ Hai</div>
                        <div className="px-2 py-2 text-center font-semibold text-lg">Thứ Ba</div>
                        <div className="px-2 py-2 text-center font-semibold text-lg">Thứ Tư</div>
                        <div className="px-2 py-2 text-center font-semibold text-lg">Thứ Năm</div>
                        <div className="px-2 py-2 text-center font-semibold text-lg">Thứ Sáu</div>
                        <div className="px-2 py-2 text-center font-semibold text-lg">Thứ Bảy</div>
                        <div className="px-2 py-2 text-center font-semibold text-lg">Chủ nhật</div>

                        {days.map((day, index) => {
                            const currentDate = addDays(firstDayOffsetDate, index);
                            const formattedDate = format(currentDate, 'yyyy-MM-dd');
                            const findWorkDay = workSchedules.find(workDay => {

                                return isSameDay(workDay.ngayLam, currentDate)
                            });


                            let cellStyle = 'bg-slate-100';

                            if (findWorkDay?.loaiLich === "Nghỉ lễ") {
                                console.log(findWorkDay)
                                cellStyle = 'bg-sky-500';
                            } else if (findWorkDay?.loaiLich === "Làm việc") {
                                cellStyle = 'bg-green-500';
                            }


                            return (
                                <div
                                    className={`${cellStyle} text-center py-8 border border-gray-300`}
                                    key={formattedDate}
                                    title={findWorkDay?.moTaCongViec}
                                >
                                    <div>{format(currentDate, 'd / MM', { locale: vi })}</div>
                                </div>
                            );
                        })}
                    </div>
                </>}

                {!isGrid && <Pagination
                    currentPage={currentPage}
                    totalPages={totalPages}
                    totalCount={totalCount}
                    handlePageChange={handlePageChange}
                    handlePageNext={handlePageNext}
                    handlePagePrevious={handlePagePrevious}
                />}

            </div>

            {showAddScheduleModal && <AddWorkScheduleForEmployee
                handleCancel={() => setShowAddScheduleModal(false)}
                isOpen={showAddScheduleModal}
            />}
            <AddWorkSchedule isOpen={showAddModal} handleCancel={() => setShowAddModal(false)} onWorkScheduleCreated={() => getWorkSchedulesFromTo(start, end)} handleConfirm={() => { }} />
            {selectedWorkSchedule && showEditModal && <EditWorkScheduleModal isOpen={showEditModal} handleCancel={() => setShowEditModal(false)} selectedWorkSchedule={selectedWorkSchedule} onWorkScheduleUpdated={onWorkScheduleUpdated} handleConfirm={() => { }} />}
            {selectedWorkSchedule && showDeleteModal && <ConfirmModal title="Thông báo" description="Bạn có muốn xóa cái này không" isOpen={showDeleteModal} handleCancel={() => setShowDeleteModal(false)} handleConfirm={handleRemove} />}
        </div>
    );
}
