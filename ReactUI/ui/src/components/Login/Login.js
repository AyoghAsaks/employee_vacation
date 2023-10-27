import React, { useContext, useRef } from 'react';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import AuthContext from '../Shared/AuthContext';
//import { MDBContainer, MDBRow, MDBCol, MDBInput, MDBBtn } from "mdbreact";
//import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

const Login = () => {
  const userName = useRef('');
  const password = useRef('');
  const { loginApiCall } = useContext(AuthContext);

  const loginHandler = async (e) => {
    e.preventDefault();
    let payload = {
        username: userName.current.value,
        password: password.current.value
    }
    await loginApiCall(payload);
    
  };
  return (
    <>
        {/*
        <Container className='mt-2 mb-2'>
            <Row>
                <Col className='col-md-8 offset-md-2'>
                    <legend>Sign In</legend>
                    <Form.Group className='mb-3' controlId='formBasicUsername'>
                        <Form.Label>Username</Form.Label>
                        <Form.Control type="username" placeholder='Username' ref={userName}></Form.Control>
                    </Form.Group>

                    <Form.Group>
                        <Form.Label>Password</Form.Label>
                        <Form.Control type="password" placeholder='Password' ref={password}></Form.Control>
                    </Form.Group>
                    <Button variant='success' type='button' onClick={loginHandler}>Login</Button>
                </Col>
            </Row>
        </Container>
        */}
        {/* <div className='container min-vh-100 d-flex justify-content-center h-100 align-items-center'> */}
        <div className='d-flex justify-content-center h-100 mt-3'>
            <div className='card'>
                <div className='card-header'>
                    <h3>SIGN IN</h3>
                    <div className='d-flex justify-content-end social-icon'>
                        <span><i className='fab fa-gitlab'></i></span>
                        <span><i className='fab fa-github'></i></span>
                        <span><i className='fab fa-gitkraken'></i></span>
                        {/*fab fa-google*/}
                    </div>
                </div>
                <div className='card-body'>
                    <Form>
                        <div className='input-group form-group'>
                            {/* Username Input Field */}
                            <Form.Group className='mb-3' controlId='formBasicUsername'>
                                {/*<Form.Label>Username</Form.Label>*/}
                                <Container>
                                    <Row>
                                        <Col md={1}><div className='' ><span className=''><i className='fas fa-user'></i></span></div></Col>
                                        <Col md={10}><Form.Control type="username" placeholder='Username' ref={userName}></Form.Control></Col>
                                    </Row>
                                </Container>
                                {/*<Form.Control type="username" placeholder='Username' ref={userName}></Form.Control> */}
                            </Form.Group>
                        </div>
                        {/*  */}
                        <div className='input-group form-group'>
                            {/* Password Input Field */}
                            <Form.Group  className='mb-3' controlId='formBasicPassword'>
                                {/*<Form.Label>Password</Form.Label>*/}
                                 <Container>
                                    <Row>
                                        <Col md={1}><div className='' ><span className=''><i className='fas fa-lock'></i></span></div></Col>
                                        <Col md={10}><Form.Control type="password" placeholder='Password' ref={password}></Form.Control></Col>
                                    </Row>
                                </Container>
                            </Form.Group>
                        </div>
                        {/* Remember Me */}
                        <div className='row align-items-center'>
                            <Container>
                                <Row>
                                    <Col>
                                        <input className='ms-3 me-2' type="checkbox" />Remember Me
                                    </Col>
                                </Row>
                            </Container>
                        </div>
                        {/*Button */}
                        <div className='text-center'><Button className='mt-2' variant='success' type='button' onClick={loginHandler}>Login</Button></div>
                    </Form>
                </div>
                {/* Card Footer */}
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

export default Login