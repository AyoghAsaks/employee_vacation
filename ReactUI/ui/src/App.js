import './App.css';
import { Routes, Route } from 'react-router-dom';
import Layout from './components/Shared/Layout';
import Home from './components/Home/Home';
import Login from './components/Login/Login';
import { AuthContextProvider } from './components/Shared/AuthContext';
/*
import LeaveType from './components/LeaveType';
import Testing from './components/Testing';
import React from 'react';
import LeaveTypeList from './components/LeaveTypes/LeaveTypeList/LeaveTypeList.jsx';
import AddLeaveType from './components/LeaveTypes/AddLeaveType/AddLeaveType.jsx';
import ViewLeaveType from './components/LeaveTypes/ViewLeaveType/ViewLeaveType.jsx';
import EditLeaveType from './components/LeaveTypes/EditLeaveType/EditLeaveType.jsx';
import SignIn from './components/SignIn/SignIn';
import Register from './components/Register/Register';


import LoggedIn from './components/LoggedIn/LoggedIn';
import LoginPage from './components/LoginPage/LoginPage';
import { AuthProvider } from './components/Shared/Contexts/AuthProvider';
*/

function App() {
  return (
    <>
      <AuthContextProvider>
        <Layout>
          <Routes>
            <Route path='/' element={<Home />}></Route>
            <Route path='/login' element={ <Login /> }></Route>
          </Routes>
        </Layout>
      </AuthContextProvider>
    </>
    
  ) 
}

export default App;