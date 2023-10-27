import React, { useEffect, useState } from 'react'
import { Col, Container, Row } from 'react-bootstrap';
import { Link, useParams } from 'react-router-dom';
import { LeaveTypeService } from '../../../services/LeaveTypeService';
import Spinner from '../../../Spinner/Spinner';

{/* returns a single LeaveType */}

const ViewLeaveType = () => {

  let { leaveTypeId } = useParams(); 

  //"state" is an object and we define it as follows: state = {loading: false, leaveType: [], errorMessage: ''}
  let [state, setState] = useState({
    loading: false,
    leaveType: {},
    errorMessage: ''
  });
  
  /*----------------------------------SECTION: Get/Return a Single Data from LeaveTypeService.js using leaveTypeId------------------------*/
  //"loadData" is an async function
  const loadData = async () => {
    try {
        setState({...state, loading: true}); //Only changes "loading" to true while leaving "leaveType" & "errorMessage" unchanged
        let response = await LeaveTypeService.getSingleLeaveType(leaveTypeId); // gets a single LeaveType data using "leaveTypeId" from the API
        //console.log(response);
        setState({
            ...state,
            loading: false,
            leaveType: response.data
        }); //Changes "loading" to false & "leaveType" is loaded with "response.data" but "errorMessage" is unchanged.
        
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
  }, [leaveTypeId ])
  /*----------------------------------SECTION 1 END: Get/Return a Single Data using leaveTypeId -----------------------------------------------*/

  let { loading, leaveType, errorMessage } = state; //Destructure the object "state" to get its individual objects.
  return (
    <React.Fragment>
        <section className='view-contact-intro p-3'>
            <Container>
                <Row>
                    <Col>
                        <p className='h3 text-warning fw-bold'>View the Leave Type</p>
                        <p className='fst-italic'>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Asperiores laborum modi magnam quasi dolorum corporis exercitationem enim. Delectus, suscipit perspiciatis provident iste placeat, et tempore fuga magnam eaque numquam ab.</p>
                    </Col>
                </Row>
            </Container>
        </section>

        { 
            loading ? <Spinner /> :
            <React.Fragment>
            {
                Object.keys(leaveType).length > 0
                       &&
                <section className='view-contact p-3'>
                    <Container>
                        {/*To vertically Center the Column contents inside the <Row>, use className='align-items-center' */}
                        <Row className='align-items-center'>

                            <Col md={8}>
                                <ul className='list-group'>
                                      <li className='list-group-item list-group-item-action'>
                                          Name : <span className='fw-bold'>{leaveType.name}</span>
                                      </li>
                                      <li className='list-group-item list-group-item-action'>
                                          Default Days : <span className='fw-bold'>{leaveType.defaultDays}</span>
                                      </li>
                                  </ul>
                            </Col>
                        </Row>

                        <Row>
                            <Col>
                                {/*Below we have a Button: NOTE: "btn btn-property" makes any tag into a button*/}
                                <Link to={`/leaveTypes/leaveTypeList`} className='btn btn-warning'>Back</Link>
                            </Col>
                        </Row>
                    </Container>
                </section>
            }
            </React.Fragment>
        }
    </React.Fragment>
  )
}

export default ViewLeaveType