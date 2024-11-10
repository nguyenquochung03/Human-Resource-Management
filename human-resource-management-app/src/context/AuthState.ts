import User from "@/models/user";

export interface AuthState {
    isAuthenticated?: boolean;
    isInitialized?: boolean;
    user: User | null;
}