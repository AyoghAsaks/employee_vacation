import axios from "axios";

const JWT_ISSUER = "JWTAuthenticationServer";
const JWT_AUDIENCE = "JWTServicePostmanClient";
const JWT_SUBJECT = "JWTServiceAccessToken";
/*
let payload = {
    "username": userName,
    "password": password,
    "jwTIssuer": JWT_ISSUER,
    "jwTAudience": JWT_AUDIENCE,
    "jwTSubject": JWT_SUBJECT
};
*/
export class LoginService {
    static serverURL = `https://localhost:7141​/api​/Authentication​`;  


    //POST: https://localhost:7141​/api​/Authentication​/login - We Post userName and password to the server
    static addLogin(employee, payload) {
        let dataURL = `${this.serverURL}/login`;
        return axios.post(dataURL, employee, payload, 
            {
                headers: {
                //'Authorization': `bearer ${tokenData}`,
                'Content-Type': 'application/json'
                }
            }
        );
    } 

    //GET: https://localhost:7141​/api​/Authentication​/login - get userName and password
    static getLogin() {
        let dataURL = `${this.serverURL}/login`;
        //return axios.get(dataURL, { headers: header });
        return axios.get(dataURL);
    }

}