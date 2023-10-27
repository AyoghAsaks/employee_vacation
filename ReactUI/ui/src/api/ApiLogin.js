import axios from "axios";

//From AuthApi: "https://localhost:7141/api​/Authentication​/login"

export default axios.create({
    baseURL: 'https://localhost:7141/api/Authentication'
});