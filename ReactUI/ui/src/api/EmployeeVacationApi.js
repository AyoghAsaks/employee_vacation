import axios from "axios";

const EmployeeVacationApi = axios.create({
    baseURL: "https://localhost:7141"
})
/*
export const getAllLeaveTypes = async () => {
    const response = await EmployeeVacationApi.get("/api/LeaveTypes/GetAllLeaveTypes")
    return response.data
}
*/

export const getAllLeaveTypes = async () => {
    const response = await axios.get(`${process.env.REACT_APP_API_SERVER}/api/LeaveTypes/GetAllLeaveTypes`)
    if(!response.ok) {
        throw new Error("Something went wrong")
    }
    return response.json()
}
/*
export const getSingleLeaveType = async (lvData) => {
    return await EmployeeVacationApi.get(`/api/LeaveTypes/GetById/${lvData.id}`, lvData.id)
}
*/
/*
export const getSingleLeaveType = async (id) => {
    const response = await EmployeeVacationApi.get(`/api/LeaveTypes/GetById/${id}`)
    return response.data
}
*/
export const getLeaveType = async ({ queryKey }) => {
    const [_key, { leaveTypeId }] = queryKey;
    const response = await axios.get(`${process.env.REACT_APP_API_SERVER}/api/LeaveTypes/GetById/${leaveTypeId}`)

    if(!response.ok) {
        throw new Error(response.json().message)
    }

    return response.json()
}

export const getSingleLeaveType = async (id) => {
    const response = await EmployeeVacationApi.get(`/api/LeaveTypes/GetById/${id}`)
    return response.data
}
export const addLeaveType = async (lvData) => {
    return await EmployeeVacationApi.post("/api/LeaveTypes/Create", lvData)
}

export const updateLeaveType = async (lvData) => {
    return await EmployeeVacationApi.patch(`/api/LeaveTypes/${lvData.id}`, lvData)
}

/*
export const updateLeaveType = ({id, ...updateLeaveType}) => {
    EmployeeVacationApi.put(`/api/LeaveTypes/${id}`, updateLeaveType).then((response) => response.data);
}
*/
export const deleteLeaveType = async ({ id }) => {
    return await EmployeeVacationApi.delete(`/api/LeaveTypes/${id}`, id)
}

export default EmployeeVacationApi;