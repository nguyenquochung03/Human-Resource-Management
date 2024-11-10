'use client'
import { useRouter } from 'next/navigation';
import Loading from "@/components/Loading";
import { useAuth } from "@/hooks/useAuth";
import { Toaster } from "react-hot-toast";

export default function AuthLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {

    const router = useRouter();
    const { isAuthenticated, isInitialized } = useAuth();
    
    if(!isInitialized) return <Loading />;

    if (isAuthenticated) router.push('/admin/employee');
    return (
        <div>
            {children}
            <Toaster position="top-right" />
        </div>
    );
}
