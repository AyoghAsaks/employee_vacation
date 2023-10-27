import React, { useEffect, useState } from 'react';

import Spinner from '../../../Spinner/Spinner.js';
import Form from 'react-bootstrap/Form';
import { LeaveTypeService } from '../../../services/LeaveTypeService.js';
import { Col, Container, Row } from 'react-bootstrap';
import { Link } from 'react-router-dom';

const LeaveTypeList = () => {
  
  //"state" is an object and we define it as follows: state = {loading: false, leaveTypes: [], errorMessage: ''}
  let [state, setState] = useState({
    loading: false,
    leaveTypes: [],
    errorMessage: ''
  }); 

  /*----------------------------------SECTION 1: Get all Data from LeaveTypeService.js-------------------------------------------*/
  //"loadData" is an async function
  const loadData = async () => {
    try {
        setState({...state, loading: true}); //Only changes "loading" to true while leaving "leaveTypes" & "errorMessage" unchanged
        let response = await LeaveTypeService.getLeaveTypes(); // gets "leaveTypes" data from API
        setState({
            ...state,
            loading: false,
            leaveTypes: response.data
        }); //Changes "loading" to false & "leaveTypes" is loaded with "response.data" but "errorMessage" is unchanged.
        
    }
    catch (error) {
        setState({
            ...state,
            loading: false,
            errorMessage: error.message
          }); //If there is an error, then "loading" is changed to false & "errorMessage" is changed from empty string with "error.message".
    }
  }

  useEffect(() => {
    loadData();
  }, [])
  /*----------------------------------SECTION 1 END: Get all Data from LeaveTypeService.js-----------------------------------------------*/

  /*----------------------------------SECTION 2: Delete contact Data from LeaveTypeService.js using leaveTypeId or id------------------------*/
  let clickDelete = async (leaveTypeId) => {
    try {
      let response = await LeaveTypeService.deleteLeaveType(leaveTypeId); //API from LeaveTypeService.js
      if(response) {
        setState({...state, loading: true}); //Only changes "loading" to true while leaving "leaveTypes" & "errorMessage" unchanged.
        let response = await LeaveTypeService.getLeaveTypes(); // gets "leaveTypes" data from API
        setState({
          ...state,
          loading: false,
          leaveTypes: response.data
        }); //Changes "loading" to false & "leaveTypes" is loaded with "response.data" but "errorMessage" is unchanged.
      }
    }
    catch(error) {
      setState({
        ...state,
        loading: false,
        errorMessage: error.message
      }); //If there is an error, then "loading" is changed to false & "errorMessage" is changed from empty string with "error.message".
    }
  }

  /*----------------------------------SECTION 2 END: Delete leaveType Data from LeaveTypeService.js using leaveTypeId or id------------------------*/

  let { loading, leaveTypes, errorMessage } = state; //Destructure the object "state" to get its individual objects.
  
  return (
    <React.Fragment>
      <pre>{JSON.stringify(leaveTypes)}</pre>
      <section className='contact-search p-3'>
        <Container>
	      <Row> 
            <Col>
              <p className='h3 fw-bolder'>
                Create New Leave Type{" "}<Link to={'/leaveTypes/addLeaveType'} className='btn btn-primary'><i className='fa fa-plus-circle me-2'></i>Create A New Leave Type</Link>
              </p>
              <p className='fst-italic'>
                Create a new leave type by clicking the button above!!!
              </p>
            </Col>
          </Row>

          {
            loading ? <Spinner /> :
            <React.Fragment>
                <section className='contact-list'>
                    <Container>
                        <Row> 
                              {
                                  leaveTypes.length > 0 
                                    && 
                                  leaveTypes.map(leaveType => {
                                      return (
                                        <Col md={6} className='my-2' key={leaveType.id}>
                                              <div className='card'>
                                                <div className='card-body'>
                                                    {/*align-items-center centers an item vertically*/}
                                                    <Row className='align-items-center'>

                                                                                        {/*Name & Default Days Column*/}
                                                          <Col md={7}>
                                                            <ul className='list-group'>
                                                              <li className='list-group-item list-group-item-action'>
                                                                Name : <span className='fw-bold'>{leaveType.name}</span>
                                                              </li>
                                                              <li className='list-group-item list-group-item-action'>
                                                                Default Days : <span className='fw-bold'>{leaveType.defaultDays}</span>
                                                              </li>
                                                            </ul>
                                                          </Col>

                                                                                        {/*Links and Button Column*/}
                                                                                        {/*viewLeaveType returns a single LeaveType*/}
                                                                                        {/*editLeaveType returns routes to a Form to*/}
                                                          <Col md={1} className='d-flex flex-row align-items-center'>
                                                              <Link to={`/leaveTypes/viewLeaveType/${leaveType.id}`} className="btn btn-warning my-1 mx-3">
                                                                <i className="fa fa-eye" />
                                                              </Link>
                                                              <Link to={`/leaveTypes/editLeaveType/${leaveType.id}`} className="btn btn-primary my-1">
                                                                <i className="fa fa-pen" />
                                                              </Link>
                                                              <button className='btn btn-danger my-1 mx-3' onClick={() => clickDelete(leaveType.id)}>
                                                                <i className="fa fa-trash" />
                                                              </button>
                                                        </Col>
                                                    </Row>
                                                </div>
                                              </div>
                                          </Col>
                                      )
                                  })
                              }
                              
                        </Row>
                    </Container>
                </section>
            </React.Fragment>
          }
          
        </Container>
      </section>
    </React.Fragment>
  )
}

export default LeaveTypeList