"use client";
import Allowance from '@/models/allowance';
import Bonus from '@/models/bonus';
import Employee from '@/models/employee';
import Salary from '@/models/Salary';
import Tax from '@/models/tax';
import allowanceService from '@/services/allowanceService';
import bonusService from '@/services/bonusService';
import employeeService from '@/services/employeeService';
import salaryService from '@/services/salaryService';
import taxService from '@/services/taxService';
import formatDate from '@/utils/formatDate';
import formatDateForInput from '@/utils/formatDateInput';
import formatCurrency from '@/utils/formatVND';
import { getMonth, getYear } from 'date-fns';
import React, { useEffect, useState } from 'react'

interface SalaryDetailProps {
    salaryId: string;
}
interface CurrentDepartment {
    soPhong: string;
    tenPhong: string;
    nguoiQuanLy: string;
    moTa: string;
    diaDiem: string;
    email: string;
}

interface CurrentPosition {
    maChucVu: string;
    tenChucVu: string;
    moTaCongViec: string;
    mucLuong: number;
}


const SalaryDetail: React.FC<{ params: SalaryDetailProps }> = ({ params }) => {

    const [tax, setTax] = useState<Tax>({} as Tax)
    const [allowances, setAllowances] = useState<Allowance[]>([])
    const [bonuses, setBonuses] = useState<Bonus[]>([])
    const [salary, setSalary] = useState<Salary>({} as Salary);
    const [employye, setEmployee] = useState<Employee | null>(null)
    const [department, setDepartment] = useState<CurrentDepartment | null>(null)
    const [position, setPosition] = useState<CurrentPosition | null>(null)

    useEffect(() => {
        getSalary();
    }, [])



    const getSalary = async () => {
        const response = await salaryService.findById(params.salaryId);
        if (response?.statusCode === 200) {
            setSalary(response.data)

            const employeeId = response.data.nhanVien?.maNhanVien;

            const responseEmployee = await employeeService.searchById(employeeId);

            if (responseEmployee.statusCode === 200) {
                const { phongBan, chucVu, ...restEmployee } = responseEmployee.data;

                setEmployee(restEmployee);
                setDepartment(phongBan);
                setPosition(chucVu);
            }

            const time = new Date(formatDateForInput(response.data?.thoiGian))
            const responseTax = await taxService
                .findByEmployeeAndTime(response.data?.nhanVien.maNhanVien, time.getMonth() + 1, time.getFullYear());

            if (responseTax.statusCode === 200) {
                setTax(responseTax.data)
            }

            const responseBonus = await bonusService.findAllByEmployeeId(response.data?.nhanVien.maNhanVien);
            if (responseBonus.statusCode === 200) {
                setBonuses(responseBonus.data)
            }

            const responseAllowance = await allowanceService.findAllByEmployeeId(response.data?.nhanVien.maNhanVien);
            if (responseAllowance.statusCode === 200) {
                setAllowances(responseAllowance.data)
            }
        }
    }

    return (
        <div className="bg-white shadow-md p-4 rounded-md">
            <div className="flex flex-col gap-x-4">
                <span className='mb-5 pb-3 border-b-2 border-sky-400 text-center font-bold text-lg uppercase'>THÔNG TIN CHI TIẾT BẢNG LƯƠNG THÁNG {getMonth(salary?.thoiGian)} / {getYear(salary?.thoiGian)} CỦA {employye?.hoTen}</span>
                <div>

                    <table className='w-full text-left border-[1px] border-gray-700'>
                        <tbody>
                            <tr className='border-[1px] border-gray-700'>
                                <td className='border-2 border-gray px-4 py-2' colSpan={4}>
                                    Họ tên: <span className='font-bold'>{employye?.hoTen}</span>
                                </td>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    Mã NV: <span className='font-bold'>{employye?.maNhanVien}</span>
                                </td>
                            </tr>
                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    Chức vụ: <span className='font-bold'>{position?.tenChucVu}</span>
                                </td>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    TTHN: <span className='font-bold'>{employye?.trangThaiHonNhan}</span>
                                </td>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    Mã PB: <span className='font-bold'>{department?.soPhong}</span>
                                </td>
                            </tr>
                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    Ngày vào làm: <span className='font-bold'>{formatDate(employye?.ngayVaoLam.toString()!)}</span>
                                </td>
                                <td className='border-2 border-gray px-4 py-2'>
                                    MST TNCN: <span className='font-bold'>{tax?.maSoThue}</span>
                                </td>
                                <td className='border-2 border-gray px-4 py-2'>
                                    Số người phụ thuộc: <span className='font-bold'>{employye?.soNguoiPhuThuoc}</span>
                                </td>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    Lương HĐLĐ: <span className='font-bold'>{formatCurrency(employye?.mucLuong!)}</span>
                                </td>
                            </tr>
                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 bg-gray-200 py-2' colSpan={8}>
                                    Khấu trừ
                                </td>
                            </tr>
                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    BHXH (8%)
                                </td>
                                <td className='border-2 border-gray px-4 py-2'>
                                    {formatCurrency(tax?.baoHiemXaHoi)}
                                </td>
                                <td className='border-2 border-gray px-4 py-2'>
                                    Thuế suất
                                </td>
                                <td className='border-2 border-gray px-4 py-2'>
                                    {tax?.thueSuat}
                                </td>
                            </tr>

                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    BHYT (1.5%)
                                </td>
                                <td className='border-2 border-gray px-4 py-2'>
                                    {formatCurrency(tax?.baoHiemYTe)}
                                </td>
                                <td className='border-2 border-gray px-4 py-2'>
                                    Thuế TNCN
                                </td>
                                <td className='border-2 border-gray px-4 py-2'>
                                    {formatCurrency(tax?.thueThuNhapCaNhanThucTe)}
                                </td>
                            </tr>
                            <tr className='border-1'>

                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    BHTN (1%)
                                </td>
                                <td className='border-2 border-gray px-4 py-2'>
                                    {formatCurrency(tax?.baoHiemThatNghiep)}
                                </td>
                                <td className='border-2 border-gray px-4 py-2'>
                                    Mức thu nhập chịu thuế
                                </td>
                                <td className='border-2 border-gray px-4 py-2'>
                                    {formatCurrency(tax?.mucThuNhapChiuThue)}
                                </td>
                            </tr>
                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 bg-gray-50 py-2 text-right' colSpan={8}>
                                    <div className='flex gap-x-2 justify-end'>
                                        <span>Tổng khấu trừ: </span>
                                        <span className='font-bold'>{formatCurrency(salary?.cacKhoanKhauTru)}</span>
                                    </div>
                                </td>
                            </tr>

                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 bg-gray-200 py-2' colSpan={8}>
                                    Phụ cấp
                                </td>
                            </tr>
                            {allowances.map(allowance => <tr key={allowance.maPhuCap} className='border-1'>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    {allowance.tenPhuCap}
                                </td>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    {formatCurrency(allowance.soTienPhuCap)}
                                </td>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    {allowance.tanSuat}
                                </td>

                            </tr>)}
                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 bg-gray-50 py-2 text-right' colSpan={8}>
                                    <div className='flex gap-x-2 justify-end'>
                                        <span>Tổng phụ cấp: </span>
                                        <span className='font-bold'>{formatCurrency(salary?.tongTienPhuCap)}</span>
                                    </div>
                                </td>
                            </tr>
                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 bg-gray-200 py-2' colSpan={8}>
                                    Khoản thưởng
                                </td>
                            </tr>
                            {bonuses.map(bonus => <tr key={bonus.maKhoanThuong} className='border-1'>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    {bonus.maKhoanThuong}
                                </td>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    {formatCurrency(bonus.soTienThuong)}
                                </td>
                                <td className='border-2 border-gray px-4 py-2' colSpan={2}>
                                    {bonus.lyDoKhenThuong}
                                </td>

                            </tr>)}
                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 bg-gray-50 py-2 text-right' colSpan={8}>
                                    <div className='flex gap-x-2 justify-end'>
                                        <span>Tổng tiền thưởng: </span>
                                        <span className='font-bold'>{formatCurrency(salary?.tongTienThuong)}</span>
                                    </div>
                                </td>
                            </tr>
                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 bg-gray-200 py-2' colSpan={8}>
                                    Tổng lương
                                </td>

                            </tr>
                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 bg-gray-50 py-2 text-right' colSpan={8}>
                                    <div className='flex gap-x-2 justify-end'>
                                        <span>Tổng lương: </span>
                                        <span className='font-bold'>{formatCurrency(salary?.tongTien)}</span>
                                    </div>
                                </td>
                            </tr>
                            <tr className='border-1'>
                                <td className='border-2 border-gray px-4 bg-gray-50 py-2 text-right' colSpan={8}>
                                    <div className='flex gap-x-2 justify-end'>
                                        <span>Thu nhập ròng: </span>
                                        <span className='font-bold'>{formatCurrency(salary?.thuNhapRong)}</span>
                                    </div>
                                </td>
                            </tr>

                            
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

    )
}

export default SalaryDetail;