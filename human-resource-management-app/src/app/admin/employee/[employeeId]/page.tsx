"use client";

import LogoEmployee from '@/components/LogoEmployee';
import TextInput from '@/components/TextInput';
import bonusService from '@/services/bonusService';
import educationService from '@/services/educationService';
import employeeService from '@/services/employeeService';
import formatDate from '@/utils/formatDate';
import React, { useEffect, useState } from 'react'

interface EmployeeDetailProps {
    employeeId: string;
}

interface AllowanceProps {
    maPhuCap: string,
    tenPhuCap: string,
    soTienPhuCap: string,
    tanSuat: string,
    trangThai: string
}

interface BonusProps {
    maKhoanThuong: string,
    ngayKhenThuong: Date,
    soTienThuong: string,
    lyDoKhenThuong: string
}

const EmployeeDetail: React.FC<{ params: EmployeeDetailProps }> = ({ params }) => {

    const [employee, setEmployee] = useState<any>(null);
    const [bonuses, setBonuses] = useState<BonusProps[]>([]);
    const [educationLevels, setEducationLevels] = useState<Education[]>([])

    const getEmployee = async () => {
        const response = await employeeService.searchById(params.employeeId);
        console.log(response)
        if (response.statusCode === 200) {
            setEmployee(response.data)
        }
    }

    const getBonus = async () => {
        const response = await bonusService.findAllByEmployeeId(params.employeeId);
        console.log(response)
        if(response.statusCode === 200) {
            setBonuses(response.data)
        }
    }

    const getEducationLevels = async () => {
        const response = await educationService.findAllByEmployeeId(params.employeeId);
        if(response.statusCode === 200) {
            setEducationLevels(response.data)
        }
    }

    useEffect(() => {
        getEmployee();
        getBonus();
        getEducationLevels();
    }, [])

    return (
        <div className='p-4 bg-white text-black rounded-sm shadow-sm'>
            <div className='flex flex-col gap-y-8'>
                <div className='flex flex-col'>
                    <div className='text-center text-lg uppercase font-semibold py-1 text-white bg-teal-400'>THÔNG TIN NHÂN VIÊN</div>
                    <div className='flex justify-center'>
                        {employee && <LogoEmployee employee={employee} />}
                    </div>
                </div>
                <div className='flex flex-col'>
                    <div className='text-center text-lg uppercase font-semibold py-1 text-white bg-teal-400'>THÔNG TIN CHI TIẾT</div>

                    {employee && (
                        <div className='grid grid-cols-3 gap-y-4 gap-x-8 mt-4'>

                            <div className='flex flex-col gap-y-2'>
                                <span>Căn cước công dân</span>
                                <TextInput
                                    value={employee.cccd ? employee.cccd : 'Chưa có dữ liệu'}
                                />
                            </div>
                            <div className='flex flex-col gap-y-2'>
                                <span>Ngày sinh</span>
                                <TextInput
                                    value={employee.ngaySinh ? formatDate(employee.ngaySinh) : 'Chưa có dữ liệu'}
                                />
                            </div>
                            <div className='flex flex-col gap-y-2'>
                                <span>Số điện thoại</span>
                                <TextInput
                                    value={employee.soDienThoai ? employee.soDienThoai : 'Chưa có dữ liệu'}
                                />
                            </div>
                            <div className='flex flex-col gap-y-2'>
                                <span>Email</span>
                                <TextInput
                                    value={employee.email ? employee.email : 'Chưa có dữ liệu'}
                                />
                            </div>
                            <div className='flex flex-col gap-y-2'>
                                <span>Giới tính</span>
                                <TextInput
                                    value={employee.gioiTinh ? employee.gioiTinh : 'Chưa có dữ liệu'}
                                />
                            </div>

                            <div className='flex flex-col gap-y-2'>
                                <span>Tôn giáo</span>
                                <TextInput
                                    value={employee.tonGiao ? employee.tonGiao : 'Chưa có dữ liệu'}
                                />
                            </div>
                            <div className='flex flex-col gap-y-2'>
                                <span>Địa chỉ</span>
                                <TextInput
                                    value={employee.diaChi ? employee.diaChi : 'Chưa có dữ liệu'}
                                />
                            </div>
                            <div className='flex flex-col gap-y-2'>
                                <span>Ngày vào làm</span>
                                <TextInput
                                    value={employee.ngayVaoLam ? formatDate(employee.ngayVaoLam) : 'Chưa có dữ liệu'}
                                />
                            </div>

                            <div className='flex flex-col gap-y-2'>
                                <span>Trạng thái</span>
                                <TextInput
                                    value={employee.trangThai ? employee.trangThai : 'Chưa có dữ liệu'}
                                />
                            </div>

                        </div>

                    )}
                </div>

                <div className='flex flex-col'>
                    <div className='text-center text-lg uppercase font-semibold py-1 text-white bg-teal-400'>THÔNG TIN HỌC VẤN</div>

                    {employee && (
                        <table className="min-w-full divide-y divide-gray-200 overflow-x-auto mt-4">
                            <thead className="bg-gray-50">
                                <tr>
                                    <th
                                        scope="col"
                                        className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                    >
                                        Trình độ học vấn
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
                                </tr>
                            </thead>

                            <tbody>
                               {educationLevels.map((education) =>  <tr key={education.maTrinhDo}>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {education.tenTrinhDoHocVan}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {education.bangCap}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {education.chuyenNganh}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {education.tenTruong}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {education.namTotNghiep}
                                    </td>
                                </tr>)}
                            </tbody>
                        </table>
                    )}
                </div>

                <div className='flex flex-col'>
                    <div className='text-center text-lg uppercase font-semibold py-1 text-white bg-teal-400'>THÔNG TIN PHỤ CẤP</div>

                    {employee && (
                        <table className="min-w-full divide-y divide-gray-200 overflow-x-auto mt-4">
                            <thead className="bg-gray-50">
                                <tr>
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
                                </tr>
                            </thead>

                            <tbody>
                               {employee?.phuCaps?.map((phuCap : AllowanceProps) =>  <tr key={phuCap?.maPhuCap}>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {phuCap.tenPhuCap}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {phuCap.soTienPhuCap}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {phuCap.tanSuat}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {phuCap.trangThai}
                                    </td>
                                </tr>)}
                            </tbody>
                        </table>
                    )}
                </div>

                <div className='flex flex-col'>
                    <div className='text-center text-lg uppercase font-semibold py-1 text-white bg-teal-400'>THÔNG TIN KHEN THƯỞNG</div>

                    {employee && (
                        <table className="min-w-full divide-y divide-gray-200 overflow-x-auto mt-4">
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
                                        Lí do
                                    </th>
                                    <th
                                        scope="col"
                                        className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                    >
                                        Ngày kí
                                    </th>
                                    <th
                                        scope="col"
                                        className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                    >
                                        Số tiền khen thưởng
                                    </th>
                                </tr>
                            </thead>

                            <tbody>
                               {bonuses.map((bonus : BonusProps) =>  <tr key={bonus?.maKhoanThuong}>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {bonus.maKhoanThuong}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {bonus.lyDoKhenThuong}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {formatDate(bonus?.ngayKhenThuong?.toString())}
                                    </td>
                                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                        {bonus.soTienThuong}
                                    </td>
                                </tr>)}
                            </tbody>
                        </table>
                    )}
                </div>
            </div>
        </div >
    );
};

export default EmployeeDetail;
