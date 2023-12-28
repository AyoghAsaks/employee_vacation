/*
https://www.youtube.com/watch?v=IR7f9tzDWns
React and JWT by Coding Tutorials
*/

/*

import { useEffect, useState } from "react";
import axios from "axios";
import config from '../../../api/Config.json';

import axioz from '../../../services/LoginApiService';

const LOGIN_URL = '/login';

//let isLoggedIn;
const logout = () => {}

const AuthenticationContext = createContext({isLoggedIn: false, logout: ()=>{}}); //the context is called "AuthContext"

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

export const AuthenticationProvider = ({ children }) => {
    const JWT_KEY = 'JWT_KEY';

    const [jwt, setJwt] = useState(sessionStorage.getItem(JWT_KEY) ?? undefined);
    const [isLoggedIn, setIsLoggedIn] = useState(!!jwt);
    const [username, setUsername] = useState(jwtToUsername(jwt));

    useEffect(()=>{
        if(isLoggedIn){
            axios.interceptors.request.use(req =>{
                const isToServer = req.url?.startsWith(config.serverUrl);

                if (isToServer){
                    req.headers['Authorization'] = `Bearer ${jwt}`;
                }

                return req;
            });
        }
        else{
            axios.interceptors.request.clear();
        }    
    }, [isLoggedIn,jwt]);

    const login = async (payload)=> {
        //const response = await axios.post(`${config.serverUrl}/login`);
        const response = await axioz.post(
            LOGIN_URL,
            payload
        );

        if (response.status !== 200) {
            throw new Error('Login failed.');
        }

        sessionStorage.setItem(JWT_KEY, response.data.jwtToken);
        setJwt(response.data.jwtToken);
        setIsLoggedIn(true);
        setUsername(jwtToUsername(response.data.jwtToken));

        return new Date(response.data.expiration);
    }

    const logout = () =>{
        setIsLoggedIn(false);
        setUsername('');
        setJwt(undefined);
        sessionStorage.removeItem(JWT_KEY);
    }

    return (
        <AuthenticationContext.Provider value={{
            login, isLoggedIn, username, logout
        }}>
            {children}
        </AuthenticationContext.Provider>
    )
};

export default AuthenticationContext;

*/