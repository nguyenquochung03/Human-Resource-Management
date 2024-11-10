import React, { useState, useEffect } from 'react';
import { format, startOfMonth, endOfMonth, eachDayOfInterval, getDay, addDays, subMonths, addMonths, isEqual } from 'date-fns';
import { vi } from 'date-fns/locale'; 
import chamCongService from '@/services/chamCongService';
import ChamCong from '@/models/chamCong';


interface AttendanceCalendarProps {
    employeeId: string;
}

const AttendanceCalendar: React.FC<AttendanceCalendarProps> = ({
    employeeId
}) => {
    const [currentMonth, setCurrentMonth] = useState<Date>(new Date());
    const [attendance, setAttendance] = useState<ChamCong []>([]);
    const [start, setStart] = useState(startOfMonth(currentMonth))
    const [end, setEnd] = useState(endOfMonth(currentMonth))
    const [days, setDays] = useState(eachDayOfInterval({ start, end }))

    
    const handlePrevMonth = (): void => {
        const prevMonth = subMonths(currentMonth, 1);
        const startOfPrevMonth = startOfMonth(prevMonth);

        const startDayOffset = (getDay(startOfPrevMonth) + 6) % 7; 
        const firstDayOffsetDate = addDays(startOfPrevMonth, -startDayOffset);

        const endOfPrevMonth = endOfMonth(prevMonth);

        setDays(eachDayOfInterval({ start: startOfPrevMonth, end: endOfPrevMonth }))
        setCurrentMonth(prevMonth);
        getChamCongsByEmployee(firstDayOffsetDate, endOfPrevMonth);
    };

    const handleNextMonth = (): void => {
        const nextMonth = addMonths(currentMonth, 1);

        const startOfNextMonth = startOfMonth(nextMonth);
        const startDayOffset = (getDay(startOfNextMonth) + 6) % 7; 
        const firstDayOffsetDate = addDays(startOfNextMonth, -startDayOffset);
        const endOfNextMonth = endOfMonth(nextMonth);

        setDays(eachDayOfInterval({ start: startOfNextMonth, end: endOfNextMonth }))
        setCurrentMonth(nextMonth);
        getChamCongsByEmployee(firstDayOffsetDate, endOfNextMonth);
    };

    const getChamCongsByEmployee = async (from: Date, to: Date) => {
        const response = await chamCongService.findAllFromTo(employeeId, format(from, 'yyyy-MM-dd'), format(to, 'yyyy-MM-dd'))
        console.log(response)
        if(response.statusCode === 200) {
            setAttendance(response.data)
        }
    }

    const startDayOffset = (getDay(start) + 6) % 7; 
    const firstDayOffsetDate = addDays(start, -startDayOffset);
    useEffect(() => {
        
        getChamCongsByEmployee(firstDayOffsetDate, end)
    }, [])


    return (
        <div>
            <h2 className='text-center my-4 text-lg font-bold pb-3 border-b-2 border-sky-400'>Lịch chấm công cho tháng {format(currentMonth, 'MMMM yyyy', { locale: vi })}</h2>
            <div className='flex gap-x-2 mb-4'>
                <button onClick={handlePrevMonth} className="py-2 px-4 rounded-md text-white bg-green-500">Prev</button>
                <button onClick={handleNextMonth} className="py-2 px-4 rounded-md text-white bg-green-500">Next</button>
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
                    const isPresent = attendance.some(att => isEqual(att.ngayChamCong,currentDate))
                    console.log(isPresent)
                    return (
                        <div
                            className={`${isPresent ? 'bg-green-500' : 'bg-gray-100'} text-center py-10 px-8 border-2 border-grey cursor-pointer tex-white`}
                            key={formattedDate}
                            
                        >
                             <div>{format(currentDate, 'd / MM', { locale: vi })}</div>
                        </div>
                      
                    );
                })}
            </div>
            <button data-tip="Tooltip text" data-for="tooltip1">
                Hover me
            </button>
           
           
        </div>
    );
};

export default AttendanceCalendar;
