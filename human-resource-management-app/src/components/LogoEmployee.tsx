import images from '@/assets';
import React from 'react'

interface EmployeeProps {
    employee : any
}

const LogoEmployee:React.FC<EmployeeProps> = ({ employee }) => {
    return (
        <div className='p-4 w-[250px] flex flex-col items-center'>
            <img src={`${employee.gioiTinh == "Nam" ? images.boyImage.src : images.girlImage.src}`} />
            <p className='font-semibold text-md mt-4 text-center'>{employee.hoTen}</p>
            <p className='font-normal text-sm mt-2 text-center'>{employee?.chucVu?.tenChucVu || "Chưa có dữ liệu"}</p>

        </div>
    )
}

export default LogoEmployee;
