import axios from 'axios';
import React, { createContext } from 'react'

const LOGIN_URL = '/login';

const AuthContext = createContext({});

export const AuthContextProvider = ({children}) => {

    const loginApiCall = async (payload) => {
        await axios.post('https://localhost:7141/api/Authentication/login', payload, {
            withCredentials: true
        });
    };

    return <AuthContext.Provider value={{ loginApiCall }}>{children}</AuthContext.Provider>;
};
 
export default AuthContext
