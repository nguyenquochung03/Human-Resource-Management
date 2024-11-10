import React, { useState, useRef, useEffect } from 'react';

interface Option {
    value: string,
    label: string
}

interface DropdownMultiSelect {
    options: Option[],
    selected: string[],
    disabled: boolean,
    onChange: (option: Option) => void
}

const DropdownMultiSelect: React.FC<DropdownMultiSelect> = ({ options, selected, onChange, disabled }) => {
    const [isOpen, setIsOpen] = useState(false);
    const dropdownRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
                setIsOpen(false);
            }
        };

        document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, []);

    const toggleDropdown = () => {
        setIsOpen(!isOpen);
    };

    return (
        <div className="relative z-20" ref={dropdownRef}>
            <button
                disabled={disabled}
                type='button'
                className="w-full text-sm px-4 py-3 text-left bg-white border-2 border-slate-900 rounded-md focus:outline-none"
                onClick={toggleDropdown}
            >
                {selected.length === 0 ? 'Chọn nhân viên' : `Đã chọn ${selected.length}`}
            </button>
            {isOpen && (
                <div className="absolute z-10 w-full mt-2 p-2 bg-white border max-h-[500px] overflow-y-scroll border-gray-300 rounded-md shadow-lg">
                    {options.length > 0 ? options.map(option => (
                        <label key={option.value} className="block px-4 py-2 cursor-pointer hover:bg-gray-100">
                            <input
                                type="checkbox"
                                className="mr-2 cursor-pointer"
                                checked={selected.includes(option.value)}
                                onChange={() => onChange(option)}
                            />
                            {option.label}

                        </label>
                    )) : <i className='p-4'>Không có dữ liệu nào</i>}
                </div>
            )}
        </div>
    );
};

export default DropdownMultiSelect;

