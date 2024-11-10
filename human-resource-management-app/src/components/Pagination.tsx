import { ChevronLeftIcon, ChevronRightIcon } from '@heroicons/react/24/solid';
import React from 'react';

interface PaginationProps {
    currentPage: number;
    totalPages: number;
    totalCount: number;
    handlePageChange: (page: number) => void;
    handlePageNext: () => void;
    handlePagePrevious: () => void;
}

const Pagination: React.FC<PaginationProps> = ({
    currentPage,
    totalPages,
    totalCount,
    handlePageChange,
    handlePageNext,
    handlePagePrevious
}) => {
    return (
        <div className="flex items-center justify-between border-t border-gray-200 bg-white px-4 py-3 sm:px-6">
            <div className="hidden sm:flex sm:flex-1 sm:items-center sm:justify-between mt-4">
                <div>
                    <p className="text-sm text-gray-700">
                        Showing <span className="font-medium">{currentPage}</span> to <span className="font-medium">{totalPages}</span> of{' '}
                        <span className="font-medium">{totalCount}</span> results
                    </p>
                </div>
                <div>
                    <nav className="isolate inline-flex -space-x-px rounded-md shadow-sm" aria-label="Pagination">
                        <button
                            onClick={() => {
                                if (currentPage > 1) {
                                    handlePagePrevious();
                                }
                            }}
                            disabled={currentPage === 1}
                            className={`relative inline-flex items-center rounded-l-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 ${currentPage === 1 ? 'opacity-50 cursor-not-allowed' : 'hover:bg-gray-50 focus:z-20 focus:outline-offset-0'
                                }`}
                        >
                            <span className="sr-only">Previous</span>
                            <ChevronLeftIcon className="h-5 w-5" aria-hidden="true" />
                        </button>
                        {/* 1 */}
                        <button
                            onClick={() => {
                                if ((currentPage !== 1 && currentPage < totalPages) || (totalPages > 1 && totalPages < 3 && currentPage == totalPages)) {
                                    handlePageChange(currentPage - 1)
                                } else if (currentPage === totalPages && totalPages > 2) {
                                    handlePageChange(currentPage - 2)
                                }
                            }}
                            className={`${currentPage === 1 ? 'bg-green-600 focus-visible:outline-green-600 text-white' : 'text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:outline-offset-0'} relative z-10 inline-flex items-center px-4 py-2 text-sm font-semibold focus:z-20 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2`}
                        >
                            {(currentPage === totalPages) && (totalPages !== 2 && currentPage !== 1) ? currentPage - 2 : currentPage === 1 ? currentPage : currentPage - 1}
                        </button>
                        {/* 2 */}
                        {totalPages < 2 ? null : (
                            <button
                                onClick={() => {
                                    if (currentPage === 1) {
                                        handlePageChange(currentPage + 1)
                                        console.log(currentPage)
                                    } else if (currentPage === totalPages && totalPages > 2) {
                                        handlePageChange(currentPage - 1)
                                    }
                                }}
                                className={`relative inline-flex items-center px-4 py-2 text-sm font-semibold focus:z-20 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 ${(currentPage !== 1 && currentPage < totalPages) || (totalPages < 3 && currentPage === 2)
                                    ? 'bg-green-600 focus-visible:outline-green-600 text-white z-20'
                                    : 'text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:outline-offset-0'
                                    } 
                                `}
                            >
                                {(currentPage === totalPages) && totalPages !== 2 ? currentPage - 1 : currentPage === 1 ? currentPage + 1 : currentPage}
                            </button>
                        )}
                        {/* 3 */}
                        {totalPages < 3 ? null : (
                            <button
                                onClick={() => {
                                    if (currentPage + 1 <= totalPages && currentPage !== 1) {
                                        handlePageChange(currentPage + 1)
                                    } else if (currentPage === 1) {
                                        handlePageChange(currentPage + 2)
                                    }
                                }}
                                className={`${currentPage == totalPages ? 'bg-green-600 focus-visible:outline-green-600 text-white z-20' : 'text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0'} relative hidden items-center px-4 py-2 text-sm font-semibold md:inline-flex`}
                            >
                                {currentPage === totalPages ? currentPage : currentPage === 1 ? currentPage + 2 : currentPage + 1}
                            </button>
                        )}

                        <span className="relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-700 ring-1 ring-inset ring-gray-300 focus:outline-offset-0">
                            ...
                        </span>
                        {/* Trang cuối */}
                        <button
                            onClick={() => {
                                if (currentPage !== totalPages) {
                                    handlePageChange(totalPages)
                                }
                            }}
                            className={`relative inline-flex items-center px-4 py-2 text-sm font-semibold text-gray-900 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0`}
                        >
                            {totalPages}
                        </button>
                        <button
                            onClick={() => {
                                if (currentPage < totalPages) {
                                    handlePageNext();
                                }
                            }}
                            disabled={currentPage === totalPages}
                            className={`relative inline-flex items-center rounded-r-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 ${currentPage === totalPages ? 'opacity-50 cursor-not-allowed' : 'hover:bg-gray-50 focus:z-20 focus:outline-offset-0'
                                }`}
                        >
                            <span className="sr-only">Next</span>
                            <ChevronRightIcon className="h-5 w-5" aria-hidden="true" />
                        </button>
                    </nav>
                </div>
            </div>
        </div>
    );
};

export default Pagination;
