'use client'
import { useRouter } from 'next/navigation';
import { Toaster } from "react-hot-toast";
import Header from "./components/header";
import Sidebar from "./components/sidebar";
import { useAuth } from "@/hooks/useAuth";
import Loading from "@/components/Loading";

export default function AdminLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {

    const router = useRouter();
    const { isAuthenticated, isInitialized } = useAuth();

    if(!isInitialized) return <Loading />;

    if (!isAuthenticated) router.push('/auth/login/admin');

    return (
        <div className="flex">
            <Sidebar />
            <div className="relative flex flex-col w-full">
                <Header />
                <div className="h-screen overflow-y-scroll pt-24 px-4 bg-lime-50 bg-opacity-35">
                    {children}
                </div>
            </div>
            <Toaster position="top-right" />
        </div>
    );
}
