import axios from '../../api/axios';
import React, { createContext, useState } from 'react';
import { jwtDecode } from "jwt-decode";

//await axios.post('https://localhost:7141/api/Authentication/login', payload,
const LOGIN_URL = '/login';

function jwtToUsername(jwt) {
    if(jwt) {
        const payload = jwt.split('.')[1];
        const asJson = atob(payload);
        const asObj = JSON.parse(asJson);

        return asObj['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']; 
    }
    else {
        return '';
    } 
}

//payload = { "userName": userName, "password": password }

//const AuthContext = createContext({});
const AuthContext = createContext(); //the context is called "AuthContext"

export const AuthContextProvider = ({ children }) => {

        const [userName, setUserName] = useState(null);
        const login = async (payload) => {
        const response = await axios.post(LOGIN_URL,
            JSON.stringify(payload),
            {
                headers: { 'Content-Type': 'application/json' },
                withCredentials: true
            }
        );
        console.log(JSON.stringify(response?.data));
        //console.log(JSON.stringify(response));
        const accessToken = response?.data?.jwtToken;
        //console.log('Decode Access Token: ' + jwtDecode((response?.data?.jwtToken)));
        //const decodedAccessToken = jwtDecode(accessToken, { header: true });
        //console.log(response?.data?.jwtToken); //---Unecessary
        //console.log('Object: ' + (decodedAccessToken));
        //setUser(decodedAccessToken);

        //Try the code.
        const obj = JSON.parse(atob(accessToken.split('.')[1]));
        
        //destructure "jwtToken" and "refreshToken"
        const { jwtToken, refreshToken } = response?.data; 
        console.log('accessToken: ' + accessToken); //OK
        console.log('jwtToken: ' + jwtToken); //OK
        console.log('refreshToken: ' + refreshToken); //OK
        console.log(jwtToUsername(response?.data?.jwtToken));

        setUserName(jwtToUsername(response?.data?.jwtToken));

        //Try the code.
        console.log(obj);
    };

    return (
        <AuthContext.Provider value={{ login, userName }}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthContext;