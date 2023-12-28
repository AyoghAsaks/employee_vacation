import React, { useContext } from 'react'

import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { Link } from 'react-router-dom';
import AuthContext from './AuthContext';

import './Layout.css';

const Layout = ({ children }) => {
  const { userName } = useContext(AuthContext);
  return (
    <>
    {/*
        <Navbar expand="lg"  className="bg-body-tertiary">
            <Container>
                <Navbar.Brand href="#home">AYOGH </Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="me-auto">
                        <Nav.Link href="#home">Home</Nav.Link>
                        <Nav.Link href="#link">Link</Nav.Link>
                        <NavDropdown title="Dropdown" id="basic-nav-dropdown">
                            <NavDropdown.Item href="#action/3.1">Action</NavDropdown.Item>
                            <NavDropdown.Item href="#action/3.2">
                                Another action
                            </NavDropdown.Item>
                            <NavDropdown.Item href="#action/3.3">Something</NavDropdown.Item>
                            <NavDropdown.Divider />
                            <NavDropdown.Item href="#action/3.4">
                                Separated link
                            </NavDropdown.Item>
                        </NavDropdown>
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
  */}
        <div className='Layout'>
            <Navbar expand="lg" bg='dark' variant='dark' collapseOnSelect>
                <Container>
                <Navbar.Brand href="/">AYOGH</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className='ms-auto'>
                            <Nav.Link as={Link} to='/'>Home</Nav.Link>
                            {/* <Nav.Link as={Link} to='/login'>Login</Nav.Link> */}
                            {!userName && (<Nav.Link as={Link} to='/login'>Login</Nav.Link>)}
                            {userName && (<Nav.Link as={Link} to='/login'>{userName}</Nav.Link>)}
                            <Nav.Link href="/applyforvacation">Apply for Vacation</Nav.Link>
                            <Nav.Link href="/myvacation">My Vacation</Nav.Link>
                            <NavDropdown title="Vacation Management" id="basic-nav-dropdown">
                                <NavDropdown.Item href="#action/3.1">Types of Vacations</NavDropdown.Item>
                                <NavDropdown.Item href="#action/3.2">Vacation Allocations</NavDropdown.Item>
                                <NavDropdown.Item href="#action/3.3">Vacation Requests</NavDropdown.Item>
                                <NavDropdown.Item href="#action/3.3">Employee Management</NavDropdown.Item>
                                <NavDropdown.Divider />
                                <NavDropdown.Item href="#action/3.4">Separated link</NavDropdown.Item>
                            </NavDropdown>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>

            <Container>{children}</Container>

            <footer className=" foot text-center text-white" style={{backgroundColor: "#caced1"}}>
                {/*!-- Grid container --*/}
                <div className="container p-4">
                    {/*!-- Section: Images --*/}
                    <section className="">
                        <div className="row">
                            <div className="col-lg-2 col-md-12 mb-4 mb-md-0">
                                <div
                                className="bg-image hover-overlay ripple shadow-1-strong rounded"
                                data-ripple-color="light"
                                >
                                <img
                                    src="https://mdbcdn.b-cdn.net/img/new/fluid/city/113.webp"
                                    className="w-100"
                                    alt=''
                                />
                                <a href="#!">
                                    <div
                                    className="mask"
                                    style={{backgroundColor: "rgba(251, 251, 251, 0.2)"}}
                                    ></div>
                                </a>
                                </div>
                            </div>
                            <div className="col-lg-2 col-md-12 mb-4 mb-md-0">
                                <div
                                className="bg-image hover-overlay ripple shadow-1-strong rounded"
                                data-ripple-color="light"
                                >
                                <img
                                    src="https://mdbcdn.b-cdn.net/img/new/fluid/city/111.webp"
                                    className="w-100"
                                    alt=''
                                />
                                <a href="#!">
                                    <div
                                    className="mask"
                                    style={{backgroundColor: "rgba(251, 251, 251, 0.2)"}}
                                    ></div>
                                </a>
                                </div>
                            </div>
                            <div className="col-lg-2 col-md-12 mb-4 mb-md-0">
                                <div
                                    className="bg-image hover-overlay ripple shadow-1-strong rounded"
                                    data-ripple-color="light"
                                >
                                    <img
                                        src="https://mdbcdn.b-cdn.net/img/new/fluid/city/112.webp"
                                        className="w-100"
                                        alt=''
                                    />
                                    <a href="#!">
                                        <div
                                        className="mask"
                                        style={{backgroundColor: "rgba(251, 251, 251, 0.2)"}}
                                        ></div>
                                    </a>
                                </div>
                            </div>
                            <div className="col-lg-2 col-md-12 mb-4 mb-md-0">
                                <div
                                    className="bg-image hover-overlay ripple shadow-1-strong rounded"
                                    data-ripple-color="light"
                                >
                                    <img
                                        src="https://mdbcdn.b-cdn.net/img/new/fluid/city/114.webp"
                                        className="w-100"
                                        alt=''
                                    />
                                    <a href="#!">
                                        <div
                                            className="mask"
                                            style={{backgroundColor: "rgba(251, 251, 251, 0.2)"}}
                                        ></div>
                                    </a>
                                </div>
                            </div>
                            <div className="col-lg-2 col-md-12 mb-4 mb-md-0">
                                <div
                                    className="bg-image hover-overlay ripple shadow-1-strong rounded"
                                    data-ripple-color="light"
                                >
                                    <img
                                        src="https://mdbcdn.b-cdn.net/img/new/fluid/city/115.webp"
                                        className="w-100"
                                        alt=''
                                    />
                                    <a href="#!">
                                        <div
                                            className='mask'
                                            style={{backgroundColor: "rgba(251, 251, 251, 0.2)"}}
                                        ></div>
                                    </a>
                                </div>
                            </div>
                            <div className="col-lg-2 col-md-12 mb-4 mb-md-0">
                                <div
                                    className="bg-image hover-overlay ripple shadow-1-strong rounded"
                                    data-ripple-color="light"
                                >
                                    <img
                                        src="https://mdbcdn.b-cdn.net/img/new/fluid/city/116.webp"
                                        className="w-100"
                                        alt=''
                                    />
                                    <a href="#!">
                                        <div
                                            className="mask"
                                            style={{backgroundColor: "rgba(251, 251, 251, 0.2)"}}
                                        ></div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </section>
                    {/*!-- Section: Images --*/}
                </div>
                {/*!-- Grid container --*/}

                {/*!-- Copyright --*/}
                <div className="text-center p-3" style={{backgroundColor: "rgba(0, 0, 0, 0.2)"}}>
                    © 2023 Copyright:
                    <a className="text-white" href="https://mdbootstrap.com/" style={{textDecoration: "none"}}>{" "}aayogh@yahoo.com</a>
                </div>
                {/*!-- Copyright --*/}
            </footer>
        </div>
    </>
  )
}

export default Layout