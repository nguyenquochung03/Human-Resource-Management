"use client";
import html2canvas from "html2canvas";
import jsPDF from "jspdf";
import Contract from "@/models/contract";
import contractService from "@/services/contractService";
import employeeService from "@/services/employeeService";
import formatDate from "@/utils/formatDate";
import { useEffect, useRef, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faDownload } from "@fortawesome/free-solid-svg-icons";


interface ContractDetailProps {
    contractId: string;
}

const ContractDetail: React.FC<{ params: ContractDetailProps }> = ({ params }) => {

    const [contract, setContract] = useState<Contract | null>(null);
    const [employee, setEmployee] = useState<any>(null);
    const divRef = useRef<HTMLDivElement>(null)

    useEffect(() => {
        getContract();
    }, [])

    const getContract = async () => {
        const response = await contractService.findById(params.contractId);

        if (response.statusCode === 200) {
            const data = response.data;
            setContract(data);
            const employeeId = data?.nhanVien?.maNhanVien;
            const responseEmployee = await employeeService.searchById(employeeId);

            if (responseEmployee.statusCode === 200) {
                setEmployee(responseEmployee.data);
            }


        }
    }

    const generatePdf = () => {
        const input = divRef.current;
        if (!input) return; 
      
        html2canvas(input)
          .then((canvas) => {
            const imgData = canvas.toDataURL('image/png');
            const pdf = new jsPDF();
            const pdfWidth = pdf.internal.pageSize.getWidth();
            const pdfHeight = pdf.internal.pageSize.getHeight();
            pdf.addImage(imgData, 'PNG', 0, 0, pdfWidth, pdfHeight); 
            pdf.save(`${employee.hoTen}_${contract?.maHopDong}.pdf`);
          });
      };

    return <div className="flex items-center justify-center z-10">
        <button onClick={generatePdf} className="fixed flex gap-x-2 items-center right-10 bottom-10 shadow-lg px-3 py-1 rounded-sm bg-green-500 hover:bg-green-600 text-white">
            <FontAwesomeIcon
                icon={faDownload}
            />
             <span>Tải về</span>
        </button>
        <div ref={divRef} className="bg-white py-24 px-32 shadow-lg m-10 w-[1000px]">
            <div className="flex flex-col items-center">
                <span className="text-xl font-bold">CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</span>
                <span className="mt-2 font-semibold">Độc lập - Tự do - Hạnh phúc</span>
                <span className="my-4 font-semibold">-----------</span>
                <span className="font-bold text-2xl mb-12">HỢP ĐỒNG LAO ĐỘNG</span>
            </div>

            <div className="flex flex-col gap-y-2">
                <div className="flex flex-col gap-y-2">
                    <p className="italic">Căn cứ Bộ luật lao động ngày 20 tháng 11 năm 2019</p>
                    <p className="italic">Căn cứ vào nhu cầu của các Bên</p>
                    <p>Hôm nay ngày {formatDate(contract?.ngayKy.toString()!)} tại Công ty TNHH Zăn Biên, chúng tôi gồm:</p>
                </div>

                <div className="flex flex-col gap-y-2">
                    <p className="font-semibold underline">BÊN A: Người sử dụng lao động</p>
                    <div className="flex relative">
                        <span className="w-20">Công ty:</span>
                        <span className="flex-1 font-semibold">TNHH Zăn Biên</span>
                        <div className="absolute left-20 right-0 top-[calc(100%+2px)] border-b-2 border-dotted border-black"></div>
                    </div>
                    <div className="flex relative">
                        <span className="w-20">Địa chỉ:</span>
                        <span className="font-semibold">Số 19, Nguyễn Hữu Thọ, Tân Phong, Q.7, TPHCM</span>
                        <div className="absolute left-20 right-0 top-[calc(100%+2px)] border-b-2 border-dotted border-black"></div>
                    </div>
                    <div className="flex relative">
                        <span className="w-28">Điện thoại:</span>
                        <span className="font-semibold">0948634657</span>
                        <div className="absolute left-28 right-0 top-[calc(100%+2px)] border-b-2 border-dotted border-black"></div>
                    </div>
                    <div className="flex justify-between">
                        <div className="relative">
                            <span className="w-24 inline-block">Đại diện:</span>
                            <span className="font-semibold">Nguyễn Quốc Hưng</span>
                            <div className="absolute left-24 right-0 top-[calc(100%+2px)] border-b-2 border-dotted border-black"></div>
                        </div>
                        <div className="relative">
                            <span className="w-24 inline-block">Chức vụ:</span>
                            <span className="font-semibold">Giám đốc điều hành</span>
                            <div className="absolute left-24 right-0 top-[calc(100%+2px)] border-b-2 border-dotted border-black"></div>
                        </div>
                        <div className="relative">
                            <span className="w-24 inline-block">Quốc tịch:</span>
                            <span className="font-semibold">Việt Nam</span>
                            <div className="absolute left-24 right-0 top-[calc(100%+2px)] border-b-2 border-dotted border-black"></div>
                        </div>

                    </div>
                </div>

                <div className="flex flex-col gap-y-2">
                    <p className="font-semibold underline">BÊN B: Người lao động</p>
                    <div className="flex relative">
                        <span className="w-28">Ông/bà:</span>
                        <span className="font-semibold">{employee?.hoTen}</span>
                        <div className="absolute left-28 right-0 top-[calc(100%+2px)] border-b-2 border-dotted border-black"></div>
                    </div>
                    <div className="flex relative">
                        <span className="w-28">Quốc tịch:</span>
                        <span className="font-semibold">Việt Nam</span>
                        <div className="absolute left-28 right-0 top-[calc(100%+2px)] border-b-2 border-dotted border-black"></div>
                    </div>
                    <div className="flex relative">
                        <span className="w-28">Ngày sinh:</span>
                        <span className="font-semibold">{formatDate(employee?.ngaySinh.toString()!)}</span>
                        <div className="absolute left-28 right-0 top-[calc(100%+2px)] border-b-2 border-dotted border-black"></div>
                    </div>
                    <div className="flex relative">
                        <span className="w-36">Địa chỉ thường trú:</span>
                        <span className="font-semibold">{employee?.diaChi}</span>
                        <div className="absolute left-36 right-0 top-[calc(100%+2px)] border-b-2 border-dotted border-black"></div>
                    </div>
                    <div className="flex relative">
                        <span className="w-36">Số CMND, CCCD:</span>
                        <span className="font-semibold">{employee?.cccd}</span>
                        <div className="absolute left-36 right-0 top-[calc(100%+2px)] border-b-2 border-dotted border-black"></div>
                    </div>
                </div>

                <i>Cùng thỏa thuận kí kết Hợp đồng lao động (HĐLĐ) về cam kết làm đúng những điều khoản sau đây</i>
                <div className="flex flex-col gap-y-2">
                    <p className="font-semibold">Điều 1: Thời hạn và công việc</p>
                    <p>Ông/bà: {employee?.hoTen} làm việc theo loại hợp động {contract?.loaiHopDong} từ ngày {formatDate(contract?.ngayBatDau.toString()!)} đến ngày {formatDate(contract?.ngayKetThuc.toString()!)}</p>
                    <p>Phòng ban: {employee?.phongBan?.tenPhong}</p>
                    <p>Chức vụ: {employee?.chucVu?.tenChucVu}</p>
                    <p>Công việc phải làm: {employee?.chucVu?.moTaCongViec}</p>
                </div>
                <div className="flex flex-col gap-y-2">
                    <p className="font-semibold">Điều 2: Chế độ làm việc</p>
                    <p>Thời giờ làm việc: Giờ hành chính</p>
                    <p>Phụ cấp: {employee?.phuCaps.map((pc: { tenPhuCap: string; }) => pc.tenPhuCap + ', ')}</p>
                    <p>Điều kiện an toàn và vệ sinh lao động tại nơi làm việc theo quy định hiện hành của nhà nước</p>

                </div>

                <div className="flex justify-between px-10 mt-12">
                    <span className="font-bold text-lg uppercase">Người lao động</span>
                    <span className="font-bold text-lg uppercase">Người sử dụng lao động</span>
                </div>
            </div>
        </div>
    </div>
}

export default ContractDetail;