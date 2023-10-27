import axios from "axios";

export class LeaveTypeService {
    static serverURL = `https://localhost:7141/api/LeaveTypes`; 

    //GET: https://localhost:7141 - get all leaveTypes
    static getLeaveTypes() {
        let dataURL = `${this.serverURL}/GetAllLeaveTypes`;
        //return axios.get(dataURL, { headers: header });
        return axios.get(dataURL);
    }

    //GET by id: https://localhost:7141/api/LeaveTypes/GetById/{id} - get one leaveType using "id"
    static getSingleLeaveType(id) {
        let dataURL = `${this.serverURL}/GetById/${id}`;
        return axios.get(dataURL);
    }

    //POST: https://localhost:7141​/api​/LeaveTypes​/Create - We Post "LeaveType" object to the server
    static addLeaveType(leaveType) {
        let dataURL = `${this.serverURL}/Create`;
        return axios.post(dataURL, leaveType);
    } 

    //PUT: Update/Edit a leaveType : PUT --> https://localhost:7141​/api​/LeaveTypes​/Update/{id}
    static updateLeaveType(leaveType, id) {
        let dataURL = `${this.serverURL}/Update/${id}`;
        return axios.put(dataURL, leaveType);
    } 

    //DELETE: Delete a leaveType : DELETE --> https://localhost:7141​​​/api​/LeaveTypes​/Delete/{id}
    static deleteLeaveType(id) {
        let dataURL = `${this.serverURL}/Delete/${id}`;
        return axios.delete(dataURL);
    } 
    
}
