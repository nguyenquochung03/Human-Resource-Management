"use client";
import {
  AcademicCapIcon,
  BanknotesIcon,
  BriefcaseIcon,
  CalendarDaysIcon,
  CalendarIcon,
  ChevronDoubleUpIcon,
  CpuChipIcon,
  CubeIcon,
  CurrencyDollarIcon,
  DocumentCheckIcon,
  ScaleIcon,
  UserCircleIcon,
  UserGroupIcon,
  ClipboardIcon,
  ClipboardDocumentListIcon,
  DocumentMagnifyingGlassIcon
} from "@heroicons/react/24/solid";
import Link from "next/link";
import { useState } from "react";

import '../../../styles/global.css';

const Sidebar = () => {
  const [submenuOpen, setSubmenuOpen] = useState(false);
  const [selectedMenu, setSelectedMenu] = useState(-1);

  const toggleSubmenu = () => {
    setSubmenuOpen(!submenuOpen);
  };

  return (
    <div
      className={`bg-white sidebar flex-none top-0 bottom-0 lg:left-0 left-[-300px] duration-1000 p-2 w-[300px] overflow-y-auto scrollbar-w-2 custom-scrollbar custom-scrollbar text-center shadow-md h-screen`}
    >
      <div className="text-sky-800 text-xl">
        <div className="p-2.5 mt-1 flex items-center rounded-md ">
          <UserCircleIcon className="h-6 w-6 " />
          <h1 className="text-[15px]  ml-3 text-xl text-sky-800 font-semibold">
            Xin chào, Admin
          </h1>
        </div>
        <hr className="my-2 text-gray-600" />
        <div>
          <Link href="/admin/employee">
            <div onClick={() => setSelectedMenu(1)} className={`${selectedMenu === 1 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <UserGroupIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Nhân Viên</span>
            </div>
          </Link>
          <Link href="/admin/department">
          <div onClick={() => setSelectedMenu(2)} className={`${selectedMenu === 2 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <CubeIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Phòng Ban</span>
            </div>
          </Link>
          <Link href="/admin/position">
          <div onClick={() => setSelectedMenu(3)} className={`${selectedMenu === 3 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <BriefcaseIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Chức Vụ</span>
            </div>
          </Link>
          <Link href="/admin/chamcong">
          <div onClick={() => setSelectedMenu(4)} className={`${selectedMenu === 4 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <DocumentCheckIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Chấm công</span>
            </div>
          </Link>
          <hr className="my-4 text-gray-600" />
          <Link href="/admin/payrollPeriod">
          <div onClick={() => setSelectedMenu(5)} className={`${selectedMenu === 5 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <ClipboardIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Đợt trả lương</span>
            </div>
          </Link>
          <Link href="/admin/hieusuat">
          <div onClick={() => setSelectedMenu(6)} className={`${selectedMenu === 6 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <CpuChipIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Hiệu Suất</span>
            </div>
          </Link>
          <Link href="/admin/salary">
          <div onClick={() => setSelectedMenu(7)} className={`${selectedMenu === 7 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <CurrencyDollarIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Lương</span>
            </div>
          </Link>
          <Link href="/admin/allowance">
          <div onClick={() => setSelectedMenu(8)} className={`${selectedMenu === 8 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <ChevronDoubleUpIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Phụ Cấp</span>
            </div>
          </Link>
          <hr className="my-4 text-gray-600" />
          <Link href="/admin/tax">
          <div onClick={() => setSelectedMenu(9)} className={`${selectedMenu === 9 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <ScaleIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Thuế</span>
            </div>
          </Link>
          <Link href="/admin/salaryPayment">
          <div onClick={() => setSelectedMenu(10)} className={`${selectedMenu === 10 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <CalendarIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Trả lương</span>
            </div>
          </Link>
          <Link href="/admin/workSchedule">
          <div onClick={() => setSelectedMenu(11)} className={`${selectedMenu === 11 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <CalendarDaysIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">
                Lịch trình
              </span>
            </div>
          </Link>
          <Link href="/admin/contract">
          <div onClick={() => setSelectedMenu(12)} className={`${selectedMenu === 12 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <ClipboardDocumentListIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Hợp đồng</span>
            </div>
          </Link>
          <Link href="/admin/absence">
          <div onClick={() => setSelectedMenu(13)} className={`${selectedMenu === 13 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <DocumentMagnifyingGlassIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Nghỉ vắng</span>
            </div>
          </Link>
          <Link href="/admin/bonus">
          <div onClick={() => setSelectedMenu(14)} className={`${selectedMenu === 14 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <BanknotesIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Thưởng</span>
            </div>
          </Link>
          <Link href="/admin/education">
          <div onClick={() => setSelectedMenu(15)} className={`${selectedMenu === 15 ? 'bg-sky-200 border-r-[6px] border-sky-800':'hover:bg-sky-200 hover:border-r-[6px] hover:border-sky-800'} p-2.5 mt-2 flex items-center rounded-md px-4 duration-300 cursor-pointer`}>
              <AcademicCapIcon className="h-6 w-6" />
              <span className="text-[15px] ml-4 text-sky-800 font-semibold">Trình độ học vấn</span>
            </div>
          </Link>
        </div>
      </div>
    </div>
  );
};

export default Sidebar;
