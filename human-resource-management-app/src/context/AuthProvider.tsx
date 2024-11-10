import { FC, ReactNode, useEffect, useReducer } from "react";
import { initialize, reducer } from "./reducer";
import userService from "@/services/userService";
import { AuthContext, initState } from "./AuthContext";

interface ChildrenProps {
    children: ReactNode
}

export const AuthProvider: FC<ChildrenProps> = ({ children }) => {
    const [state, dispatch] = useReducer(reducer, initState);
    useEffect(() => {
        (async () => {
            const accessToken = localStorage.getItem('ACCESS_TOKEN');
            if (!accessToken) {
                return dispatch(initialize({
                    isAuthenticated: false, user: null
                }));
            }

            try {
                const response = await userService.getProfile();
                if(response.statusCode === 200)
                    dispatch(initialize({ isAuthenticated: true, user: response.data }));
                else dispatch(initialize({ isAuthenticated: false, user: null }));
            } catch {
                dispatch(initialize({ isAuthenticated: false, user: null }));
            }
        })();
    }, []);
    return (
        <AuthContext.Provider value={{ ...state, dispatch }}>
            {children}
        </AuthContext.Provider>
    );
}