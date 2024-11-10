"use client";
import { FormikHelpers, useFormik } from 'formik';
import styles from './styles.module.css';
import userService from '@/services/userService';
import toast from 'react-hot-toast';
import loginSchema from '@/schemas/loginSchema';
import { useAuth } from '@/hooks/useAuth';
import { login } from '@/context/reducer';
import { useState } from 'react';
import Loading from '@/components/Loading';

export default function AdminLogin() {
    const { dispatch } = useAuth();
    const [isLoading, setIsLoading] = useState(false);
    const onSubmit = async (values: Login, actions: FormikHelpers<Login>) => {
        setIsLoading(true)
        const response = await userService.login(values);
        setIsLoading(false)
        if (response?.statusCode === 200) {
            const { accessToken, user } = response.data;
            console.log(user)
            localStorage.setItem('ACCESS_TOKEN', accessToken);
            dispatch(login({
                user
            }));
            actions.resetForm();
            toast.success(response?.message);
        } else toast.error(response.message)
    }


    const formik = useFormik<Login>({
        initialValues: {
            "username" : "",
            "password" : "",
        },
        onSubmit,
        validationSchema: loginSchema
    });

    return (
        <>
            {isLoading && <Loading />}
            <div className={`${styles.body_bg} min-h-screen pt-12 md:pt-20 pb-6 px-2 md:px-0`}>
                <header className="max-w-lg mx-auto">
                    <h1 className="text-4xl font-bold text-white text-center">Đăng nhập</h1>
                </header>
                <main className="bg-white max-w-lg mx-auto p-8 md:p-12 my-10 rounded-lg shadow-2xl">
                    <section>
                        <h3 className="font-bold text-2xl">PHẦN MỀM QUẢN LÍ NHÂN SỰ</h3>
                        <p className="text-gray-600 pt-2">Vui lòng đăng nhập bằng tài khoản admin.</p>
                    </section>
                    <section className="mt-10">
                        <form onSubmit={formik.handleSubmit} className="flex flex-col">
                            <div className="mb-6 pt-3 rounded">
                                <label
                                    className="block text-gray-700 text-sm font-bold mb-2 ml-3"
                                    htmlFor="username"
                                >
                                    Tên người dùng
                                </label>
                                <input
                                    type="text"
                                    id="username"
                                    value={formik.values.username}
                                    onChange={formik.handleChange}
                                    placeholder='Nhập username'
                                    className="pt-2 bg-gray-200 rounded w-full text-gray-700 focus:outline-none border-b-4 border-gray-300 focus:border-green-600 transition duration-500 px-3 pb-3"
                                />
                                {formik.touched.username && formik.errors.username && <span className='text-sm text-red-600 pl-2'>{formik.errors.username}</span>}
                            </div>
                            <div className="mb-6 pt-3 rounded">
                                <label
                                    className="block text-gray-700 text-sm font-bold mb-2 ml-3"
                                    htmlFor="password"
                                >
                                    Mật khẩu
                                </label>
                                <input
                                    type="password"
                                    id="password"
                                    value={formik.values.password}
                                    onChange={formik.handleChange}
                                    placeholder='Nhập mật khẩu'
                                    className="pt-2 bg-gray-200 rounded w-full text-gray-700 focus:outline-none border-b-4 border-gray-300 focus:border-green-600 transition duration-500 px-3 pb-3"
                                />
                                 {formik.touched.password && formik.errors.password && <span className='text-sm text-red-600 pl-2'>{formik.errors.password}</span>}
                            </div>
                       
                            <button
                                type="submit"
                                className="bg-green-600 hover:bg-green-700 text-white font-bold py-2 rounded shadow-lg hover:shadow-xl transition duration-200"
                            >
                                Đăng nhập
                            </button>
                        </form>
                    </section>

                </main>
            </div>
            {/* <ResetPasswordModal isVisible={showModal} onClose={() => setShowModal(false)} /> */}
        </>
    )
}

