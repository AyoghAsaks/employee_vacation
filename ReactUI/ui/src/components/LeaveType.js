import React from 'react';
//import Spinner from '../../Spinner/Spinner';
import { useQuery, useMutation, useQueryClient } from "react-query";
import { getAllLeaveTypes, addLeaveType, updateLeaveType, deleteLeaveType } from "../api/EmployeeVacationApi";
import { useState } from 'react';

const LeaveType = () => {
  const [newLeaveTypeName, setNewLeaveTypeName] = useState('');
  const [newDefaultDays, setNewDefaultDays] = useState(0);

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
    e.preventDefault()
    addLeaveTypeMutation.mutate({ id: 1, name: newLeaveTypeName, defaultDays: newDefaultDays })
    setNewLeaveTypeName('')
    setNewDefaultDays()
  }
  
  let content;

  if(isLoading) {
    content = <p>Loading...</p>
  } else if (isError){
    content = <p>{error.message}</p>
  }else {
    content = JSON.stringify(leaveTypes)
    //content = leaveTypes; 
  }

  //console.log(content);

  return (
    <div>
        <h1>LeaveTypes</h1>
        {content}
    </div>
  )
}

export default LeaveType;