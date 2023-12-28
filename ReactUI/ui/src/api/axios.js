import axios from "axios";

//From AuthApi: "https://localhost:7141/api​/Authentication​/login"

const BASE_URL = 'https://localhost:7141/api/Authentication';

export default axios.create({
    baseURL: BASE_URL
});
/*
export const axiosPrivate = axios.create({
    baseURL: BASE_URL,
    headers: { 'Content-Type': 'application/json'},
    withCredentials: true
});
*/