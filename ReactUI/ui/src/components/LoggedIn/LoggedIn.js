/*
https://www.youtube.com/watch?v=DPuQahfe7nw
.NET Core Web API & JWT Token handling in React JS | React JS Authentication by Nihira Techiees
*/

/*
import React, { useEffect, useState } from 'react';
import { Button, Col, Container, Row } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import axios from '../../services/LoginApiService';

const LOGIN_URL = '/login';

const LoggedIn = () => {

  const [userName, userNameUpdate] = useState(''); 
  const [password, passwordUpdate] = useState('');
  
  const usenavigate = useNavigate();

  useEffect(()=>{
    sessionStorage.clear();
  },[]); 

  const ProceedLogin = (e) => {
    e.preventDefault();
    if (validate()) {
        let inputobj = {userName, password};
        
       //'https://localhost:7141/api/Authentication/login'
        fetch('https://localhost:7141/api/Authentication/login',
            {
              method: 'POST',
              headers: {
                  'Content-Type': 'application/json'
              },
            
             body:JSON.stringify(inputobj)
            }).then((res) => {
            return res.JSON();
          }).then((resp) => {
            //console.log(resp);
            toast.success('Success');
            sessionStorage.setItem('userName', userName);
            sessionStorage.setItem('jwtToken', resp.jwtToken);
            usenavigate('/');
            
          }).catch((err) => {
            toast.error('Login Failed due to :' + err.message);
          });
    }
    
  }

  const validate = () => {
    let result = true;
    if (userName === '' || userName === null) {
        result = false;
        toast.warning('Please Enter Username');
    }
    if (password === '' || password === null) {
        result = false;
        toast.warning('Please Enter Password');
    }
    return result;
  }

  return (
    <>
        <div className='d-flex justify-content-center h-100 mt-3'>
            <div className='card'>
                <div className='card-header'>
                    <h3>SIGN IN</h3>
                    <div className='d-flex justify-content-end social-icon'>
                        <span><i className='fab fa-gitlab'></i></span>
                        <span><i className='fab fa-github'></i></span>
                        <span><i className='fab fa-gitkraken'></i></span>
                    </div>
                </div>

                <div className='card-body'>

                    <form onSubmit={ProceedLogin}>
                        <div className='input-group form-group'>
                            <div className='mb-3'>
                                <Container>
                                    <Row>
                                        <Col md={1}><div className='' ><span className=''><i className='fas fa-user'></i></span></div></Col>
                                        <Col md={10}>
                                          <div className="form-group">
                                            <label>User Name <span className="errmsg">*</span></label>
                                            <input value={userName} onChange={e => userNameUpdate(e.target.value)} className="form-control"></input>
                                          </div>
                                        </Col>
                                    </Row>
                                </Container>
                
                            </div>
                        </div>
                        
                        <div className='input-group form-group'>
                            <div className='mb-3'>
                                 <Container>
                                    <Row>
                                        <Col md={1}><div className='' ><span className=''><i className='fas fa-lock'></i></span></div></Col>
                                        <Col md={10}>
                                          <div className="form-group">
                                            <label>Password <span className="errmsg">*</span></label>
                                            <input type="password" value={password} onChange={e => passwordUpdate(e.target.value)} className="form-control"></input>
                                          </div>
                                        </Col>
                                    </Row>
                                </Container>
                            </div>
                        </div>
                        
                        <div className='row align-items-center'>
                            <Container>
                                <Row>
                                    <Col>
                                        <input className='ms-3 me-2' type="checkbox" />Remember Me
                                    </Col>
                                </Row>
                            </Container>
                        </div>
                    
                        <div className='text-center'>
                            <Button className='mt-2' variant="success" type="submit">
                                Login
                            </Button>
                        </div>

                    </form>
                    
                </div>

                
                <div className='card-footer'>
                    <div className='d-flex justify-content-center links'>
                        Don't Have An Account?<Link to={`/`} className='ms-1' style={{textDecoration: "none"}}>Sign Up</Link>
                    </div>
                    <div className='d-flex justify-content-center'>
                        <Link style={{textDecoration: "none"}} to={`/`}>Forgot Your Password</Link>
                    </div>
                </div>
            </div>
        </div>
    </>
  )
}

export default LoggedIn

*/

