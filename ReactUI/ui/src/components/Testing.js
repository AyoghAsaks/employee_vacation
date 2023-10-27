import React from 'react';
import { useQuery, useMutation, useQueryClient } from "react-query";
import { getAllLeaveTypes, addLeaveType, updateLeaveType, deleteLeaveType } from "../api/EmployeeVacationApi";
import { useState } from 'react';

//Just for the Testing.js Component
//import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
//import { faTrash, faUpload } from "@fortawesome/free-solid-svg-icons"

const Testing = () => {
  const [newLeaveType, setNewLeaveType] = useState('');
  const [newDefaultDays, setNewDefaultDays] = useState('');
  const queryClient = useQueryClient();

  const {
    isLoading,
    isError,
    error, 
    data: leaveTypes
} = useQuery('leaveTypes', getAllLeaveTypes);

const addLeaveTypeMutation = useMutation(addLeaveType, {
    onSuccess: () => {
        //Invalidates cache abd refresh
        queryClient.invalidateQueries("leaveTypes")
    }
})
const updateLeaveTypeMutation = useMutation(updateLeaveType, {
    onSuccess: () => {
        //Invalidates cache abd refresh
        queryClient.invalidateQueries("leaveTypes")
    }
})
const deleteLeaveTypeMutation = useMutation(deleteLeaveType, {
    onSuccess: () => {
        //Invalidates cache abd refresh
        queryClient.invalidateQueries("leaveTypes")
    }
})

const handleSubmit = (e) => {
    e.preventDefault();
    addLeaveTypeMutation.mutate({ name: newLeaveType, defaultDays: newDefaultDays });
    setNewLeaveType('');
    setNewDefaultDays();
}

const newLeaveTypeSection = (
    <form onSubmit={handleSubmit}>
        <label htmlFor="new-leavetypename">Enter a new leave type</label>
        <div className="new-leavetypename">
            <input
                type="text"
                id="new-leavetypename"
                value={newLeaveType}
                onChange={(e) => setNewLeaveType(e.target.value)}
                placeholder="Enter a new leave type"
            />
        </div>

        <label htmlFor="new-defaultdays">Enter the default number of days</label>
        <div className="new-defaultdays">
            <input
                type="text"
                id="new-defaultdays"
                value={newDefaultDays}
                onChange={(e) => setNewDefaultDays(e.target.value)}
                placeholder="Enter the default number of days"
            />
        </div>
        <button className="submit">
            {/*<FontAwesomeIcon icon={faUpload} /> */} Submit
        </button>
    </form>
)

let content;

if(isLoading) {
  content = <p>Loading...</p>
} else if (isError){
  content = <p>{error.message}</p>
}else {
  //content = JSON.stringify(leaveTypes)
  //content = leaveTypes; 
  content = (leaveTypes) => {
        return (
        
        
            <div className="leaveType">
                <label htmlFor={leaveTypes.id}>{leaveTypes.name}</label>
                    <input
                        id={leaveTypes.id}
                        onChange={() =>
                            updateLeaveTypeMutation.mutate({ ...leaveTypes, name: leaveTypes.name })
                        }
                    />

                <label htmlFor={leaveTypes.id}>{leaveTypes.defaultDays}</label>
                    <input
                        id={leaveTypes.id}
                        onChange={() =>
                            updateLeaveTypeMutation.mutate({ ...leaveTypes, defaultDays: leaveTypes.defaultDays })
                        }
                    />

                <button className="trash" onClick={() => deleteLeaveTypeMutation.mutate({ id: leaveTypes.id })}>
                    Click
                </button>
            </div>
        
        )
}

}

//console.log(content);

  return (
    <div>
        Testing
        {newLeaveTypeSection}
        {content}
    </div>
  )
}

export default Testing;