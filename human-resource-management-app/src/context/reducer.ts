import { AuthActionType, PayLoadAction } from "./AuthContext";
import { AuthState } from "./AuthState";

export interface ReducerHandler {
    INITIALIZE(state: AuthState, action: PayLoadAction<AuthState>) : AuthState;
    LOGIN(state: AuthState, action: PayLoadAction<AuthState>) : AuthState;
    LOGOUT(state: AuthState) : AuthState;
}

const reducerHandlers: ReducerHandler = {
    INITIALIZE(state: AuthState, action: PayLoadAction<AuthState>): AuthState {
        const { isAuthenticated, user } = action.payload;

        return {
            ...state,
            isAuthenticated,
            isInitialized: true,
            user
        };
    },
    LOGIN: function (state: AuthState, action: PayLoadAction<AuthState>): AuthState {
        const { user } = action.payload;

        return {
            ...state,
            isAuthenticated: true,
            user
        };
    },
    LOGOUT: function (state: AuthState): AuthState {
        return {
            ...state,
            isAuthenticated: false,
            user: null
        }
    }
}

export function reducer(state: AuthState, action: PayLoadAction<AuthState>) {
    if(!reducerHandlers[action.type]) return state;

    return reducerHandlers[action.type](state, action);
}

export function initialize(payload: AuthState): PayLoadAction<AuthState> { 
    return {
        type: AuthActionType.INITIALIZE, 
        payload
    }
}

export function login(payload: AuthState): PayLoadAction<AuthState> { 
    return {
        type: AuthActionType.LOGIN, 
        payload
    }
}

export function logout(): PayLoadAction<AuthState> { 
    localStorage.removeItem('ACCESS_TOKEN')
    return {
        type: AuthActionType.LOGOUT, 
        payload: {
            user: null
        }
    }
}