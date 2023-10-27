import React, { useState } from 'react'
import { toast } from 'react-toastify';
import { Link } from 'react-router-dom';
import { LoginService } from '../../services/LoginService';

const JWT_ISSUER = "JWTAuthenticationServer";
const JWT_AUDIENCE = "JWTServicePostmanClient";
const JWT_SUBJECT = "JWTServiceAccessToken";

const SignIn = () => {
  //const [userName, userNameUpdate] = useState('');
  //const [password, passwordUpdate] = useState('');

 

  //"state" is an object and we define it as follows. 
  let [state, setState] = useState({
    loading: false,
    employee: {
      userName: '',
      password: ''
    },
    errorMessage: ''
  });

  let { loading, employee, errorMessage } = state; //Destructure the object "state" to get its individual objects.

  let payload = {
    "userName": employee.userName,
    "password": employee.password,
    "jwTIssuer": JWT_ISSUER,
    "jwTAudience": JWT_AUDIENCE,
    "jwTSubject": JWT_SUBJECT
  };

  //This Function receives the "userName" and "password" from the "employee"
  let changeUserNameAndPassword = (event) => {
    setState({
      ...state,
      employee: {
        ...state.employee,
        [event.target.name] : event.target.value
      }

    });
  };

  ////let { loading, employee, errorMessage } = state; //Destructure the object "state" to get its individual objects.

  //POST Request Function: This Function POST the <form></form>
  const submitLogin = async (e) => {
    e.preventDefault();
    if(validate()){
        //implementation
        //console.log('proceed');
        try {
            //let response = await LoginService.addLogin(state.employee); "state.employee" if we had not destructured
            let response = await LoginService.addLogin(employee, payload); //API from LoginService.js
            //console.log(response);

            //Resets userName & password to empty string
            setState({
                ...state,
                loading: false,
                employee: {
                    userName: '',
                    password: ''
                },
                errorMessage: ""
            });
        }
        catch (error) {
            setState({
                ...state,
                loading: false,
                errorMessage: error.message
            }); //If there is an error, then "loading" is changed to false & "errorMessage" is changed from empty string with "error.message".
        }
          
    }
  }


  const validate = () => {
    let result = true;
    if(employee.userName === '' || employee.userName === null){
        result = false;
        toast.warning('Please Enter Username');
    }
    if(employee.password === '' || employee.password === null){
        result = false;
        toast.warning('Please Enter Password');
    }

    return result;
  }

return (
    <div className='row'>
        <div className='offset-lg-3 col-lg-6' style={{ marginTop: '100px' }}>

            <form onSubmit={submitLogin} className="container">
                <div className='card'>
                    <div className='card-header'>
                        <h2>Employee Login</h2>
                    </div>

                    <div className='card-body'>
                        <div className='form-group'>
                            <label>User Name<span className='errmsg'>*</span></label>
                            <input 
                                name='userName'
                                value={employee.userName} 
                                onChange={changeUserNameAndPassword}
                                type="text"
                                placeholder='Username'
                                className='form-control' 
                            />
                        </div>

                        <div className='form-group'>
                            <label>Password<span className='errmsg'>*</span></label>
                            <input 
                                name="password" 
                                value={employee.password}
                                onChange={changeUserNameAndPassword} 
                                type="password"
                                placeholder='Password'
                                className='form-control' 
                            />
                        </div>
                    </div>

                    <div className='card-footer'>
                        <button type='submit' className='btn btn-primary'>Login</button> | {' '}
                        <Link className='btn btn-success' to={'/authentication​/register'}>New Employee</Link>
                    </div>
                </div>
            </form>

        </div>
    </div>
)
}

export default SignIn