import { AuthContext } from "@/context/AuthContext";
import { useContext } from "react";

export function useAuth() {
    const context = useContext(AuthContext);

    if(!context) {
        throw new Error("Context must be inside auth provider");
    }

    return context;
}