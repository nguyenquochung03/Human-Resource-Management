"use client";


import ChamCong from '@/models/chamCong';
import Employee from '@/models/employee';
import absenceService from '@/services/absenceService';
import chamCongService from '@/services/chamCongService';
import employeeService from '@/services/employeeService';
import formatDate from '@/utils/formatDate';
import { faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { ChevronRightIcon, HomeIcon } from '@heroicons/react/24/solid'
import { format, startOfMonth, endOfMonth, eachDayOfInterval, getDay, addDays, subMonths, addMonths, isAfter, isBefore, isEqual } from 'date-fns';
import { vi } from 'date-fns/locale';
import Link from 'next/link'
import React, { ChangeEventHandler, useEffect, useState } from 'react'

export default function AdminChamCongNhanVien() {
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [selectedEmployee, setSelectedEmployee] = useState<Employee | null>(null);
    const [absences, setAbsences] = useState<Absence[]>([]);

    const [currentMonth, setCurrentMonth] = useState<Date>(new Date());
    const [attendance, setAttendance] = useState<ChamCong[]>([]);
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
        getChamCongsByEmployee(selectedEmployee, firstDayOffsetDate, endOfPrevMonth);

        if (selectedEmployee)
            getAbsences(selectedEmployee.maNhanVien, prevMonth);
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
        getChamCongsByEmployee(selectedEmployee, firstDayOffsetDate, endOfNextMonth);

        if (selectedEmployee)
            getAbsences(selectedEmployee.maNhanVien, nextMonth);
    };

    const getChamCongsByEmployee = async (employeeId: Employee | null, from: Date, to: Date) => {
        const response = await chamCongService.findAllFromTo(employeeId?.maNhanVien, format(from, 'yyyy-MM-dd'), format(to, 'yyyy-MM-dd'))
        if (response.statusCode === 200) {
            setAttendance(response.data)
        }
    }
    const getAbsences = async (employeeId: string, month: Date) => {
        const response = await absenceService.findAllByMonth(employeeId, month);
        if (response.statusCode === 200) {
            setAbsences(response.data)
        }
    }

    useEffect(() => {

        getChamCongsByEmployee(selectedEmployee, firstDayOffsetDate, end)

        if (selectedEmployee)
            getAbsences(selectedEmployee.maNhanVien, currentMonth);
    }, [])

    const handleEmployeeChange: ChangeEventHandler<HTMLSelectElement> = async (event) => {
        const { value } = event.target;
        if (value === "all") {
            setSelectedEmployee(null)
            getChamCongsByEmployee(null, start, end);
            return;
        }

        setSelectedEmployee(employees[event.target.selectedIndex - 1])
        getChamCongsByEmployee(employees[event.target.selectedIndex - 1], start, end);
        getAbsences(value, currentMonth);

    }

    const getEmployees = async () => {
        const response = await employeeService.findAll();
        if (response?.statusCode === 200) {
            setEmployees(response.data);
        }
    }

    useEffect(() => {
        getEmployees();
    }, [])


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
                            <span className="text-gray-900">Danh sách chấm công</span>
                        </li>
                        <li>
                            <ChevronRightIcon className="h-4 w-4" />
                        </li>
                        <li className="flex items-center">
                            <span className="text-gray-900">Nhân viên</span>
                        </li>
                    </ol>
                </nav>
            </div>

            <div className="bg-white shadow-md p-4 rounded-md">
                <div className="w-full mb-5 flex gap-x-1 items-center">
                    <span className='font-bold'>Chọn nhân viên:</span>
                    <select
                        className="ml-4 px-4 text-sm py-[10px] border border-black rounded-sm outline-none scrollbar-w-2 custom-scrollbar custom-scrollbar"
                        onChange={handleEmployeeChange}
                    >
                        <option className="bg-white" value="all">Tất cả</option>
                        {employees.map(employee => <option value={employee.maNhanVien} key={employee.maNhanVien}>
                            {employee.hoTen}
                        </option>)}
                    </select>
                </div>

                <div>
                    <h2 className='text-center my-4 text-lg font-bold pb-3 border-b-2 border-sky-400'>Dữ liệu chấm công tháng {format(currentMonth, 'MMMM / yyyy', { locale: vi })} của {selectedEmployee?.hoTen}</h2>
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
                            <span className="bg-green-200 text-green-700 px-1 py-[1px] text-sm rounded-md">Đã chấm công</span>
                            <span className="bg-sky-200 px-1 py-[1px] text-sm rounded-md text-sky-700">Nghỉ có phép</span>
                            <span className="bg-red-200 px-1 py-[1px] text-sm rounded-md text-red-700">Nghỉ không phép</span>
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
                            const isPresent = attendance.some(att => (
                                formatDate(att.ngayChamCong.toString()) == formatDate(currentDate.toString())
                            ));
                            const isAbsence = absences.find(absen => {
                                const isStartOnOrBeforeCurrent = isEqual(absen.ngayBatDau, currentDate) || isBefore(absen.ngayBatDau, currentDate);
                                const isEndOnOrAfterCurrent = isEqual(absen.ngayKetThuc, currentDate) || isAfter(absen.ngayKetThuc, currentDate);
                                return isStartOnOrBeforeCurrent && isEndOnOrAfterCurrent;
                            });

                            let cellStyle = '';

                            if (isPresent) {
                                cellStyle = 'bg-green-400';
                            } else {
                                switch (isAbsence?.loaiNghiVang) {
                                    case 'Không được trả lương':
                                        cellStyle = 'bg-red-400';
                                        break;
                                    case 'Được trả lương':
                                        cellStyle = 'bg-sky-400';
                                        break;
                                    default:
                                        cellStyle = 'bg-slate-50';
                                }
                            }


                            return (
                                <div
                                    className={`${cellStyle} text-center py-8 border border-gray-300`}
                                    key={formattedDate}
                                >
                                    <div>{format(currentDate, 'd / MM', { locale: vi })}</div>
                                </div>
                            );
                        })}
                    </div>
                </div>
            </div>
        </div>
    )
}