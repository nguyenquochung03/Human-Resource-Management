import { createContext, Dispatch } from "react";
import { AuthState } from "./AuthState";

export enum AuthActionType {
    INITIALIZE = 'INITIALIZE',
    LOGIN = 'LOGIN',
    LOGOUT = 'LOGOUT',
}

export interface PayLoadAction<T> {
    type: AuthActionType;
    payload: T;
}

export const initState : AuthState = {
    isAuthenticated: false,
    isInitialized: false,
    user: null
}

export interface AuthContextType extends AuthState {
    dispatch: Dispatch<PayLoadAction<AuthState>>;
}

export const AuthContext = createContext<AuthContextType>({
    ...initState,
    dispatch: () => null
});
