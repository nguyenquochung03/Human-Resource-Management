import { logout } from "@/context/reducer";
import { useAuth } from "@/hooks/useAuth";
import { useEffect, useRef, useState } from "react";

export default function Header() {
    const { user, dispatch } = useAuth();
    // const [openModal, setOpenModal] = useState(false)
    const modalRef = useRef<HTMLDivElement>(null);
    const [openInfoAvatar, setOpenInfoAvatar] = useState(false)

    const handleClickOutside = (event: MouseEvent) => {
        if (modalRef.current && modalRef.current instanceof HTMLElement && !modalRef.current.contains(event.target as Node)) {
            setOpenInfoAvatar(false);
        }
    };
    
    useEffect(() => {
        document.addEventListener('click', handleClickOutside, true);
    
        return () => {
            document.removeEventListener('click', handleClickOutside);
        };
    }, [openInfoAvatar]);
    
    const handleToggle = () => {
        setOpenInfoAvatar(!openInfoAvatar)
    }

    return (
        <div className='absolute flex items-center justify-between shadow-md bg-white top-0 left-0 bottom-20 right-0 h-20 z-10'>
            <div></div>
            <p className='text-black font-bold text-2xl'>PHẦN MỀM QUẢN LÍ NHÂN SỰ</p>

            <div className='flex items-center gap-x-3 relative px-8'>
                <span className='text-lg font-bold'>{user?.name}</span>
                <button
                    type='button'
                    onClick={handleToggle}>
                    <img
                        className='rounded-full w-[35px] h-[35px] cover'
                        alt="messi"
                        src="https://encrypted-tbn3.gstatic.com/licensed-image?q=tbn:ANd9GcQFWlv5W-UzytWCKl9wRgsi8kxZH0MVWCvVVWd-vbg4H1z_KXrHJ9CsjyTMeBSrEUPeBdBTW-WSA8XQSaE" />
                </button>

                <div ref={modalRef} className={`${openInfoAvatar ? 'scale-1' : 'scale-0'} transition-all origin-top-right bg-white absolute right-0 top-full mt-2 w-[300px] shadow-xl mr-6 p-6 rounded-md`}>
          
                    <div className='flex gap-x-4 items-center pb-4 border-b-[1px] mt-4 border-teal-800'>
                        <img
                            className='rounded-full w-[70px] h-[70px] cover'
                            alt="messi"
                            src="https://encrypted-tbn3.gstatic.com/licensed-image?q=tbn:ANd9GcQFWlv5W-UzytWCKl9wRgsi8kxZH0MVWCvVVWd-vbg4H1z_KXrHJ9CsjyTMeBSrEUPeBdBTW-WSA8XQSaE" />
                        <div className='flex flex-col text-black'>
                            <span className='text-lg font-bold'>{user?.name}</span>
                            <span>QUẢN TRỊ VIÊN</span>

                        </div>
                    </div>

                    <button
                        className='bg-red-600 text-white rounded-md bg-opacity-80 flex items-center py-1 px-3 gap-x-2 mt-4'>
                       
                        <span onClick={() => dispatch(logout())}>Đăng xuất</span>
                    </button>
                </div>
            </div>
        </div>
    )
}
