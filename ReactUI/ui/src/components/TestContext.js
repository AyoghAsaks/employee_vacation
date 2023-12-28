import axios from 'axios';
import React, { createContext } from 'react'

//await axios.post('https://localhost:7141/api/Authentication/login', payload,
const LOGIN_URL = '/login';

const TestContext = createContext({});

export const TestContextProvider = ({children}) => {

    const loginApiCallPrivate = async (payload) => {
        await axios.post(LOGIN_URL, payload,
        
         {
            headers: { 
                'Authorization': `bearer ${tokenData}`,
                'Content-Type': 'application/json'
            },
            withCredentials: true
        });
    };

    return <TestContext.Provider value={{ loginApiCallPrivate }}>{children}</TestContext.Provider>;
};
 
export default TestContext