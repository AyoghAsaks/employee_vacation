import React, { useEffect, useState } from 'react'
import { Col, Container, Row } from 'react-bootstrap';
import { Link, useNavigate, useParams } from 'react-router-dom';
import { LeaveTypeService } from '../../../services/LeaveTypeService';
import Spinner from '../../../Spinner/Spinner';

const EditLeaveType = () => {

  //To Edit/Update, you need the "id" from useParams
  let { leaveTypeId } = useParams();

  let navigate = useNavigate();

  //"state" is an object and we define it as follows.
  //NOTE: the "leaveType" object's properties must be exactly the same as that in the API.
  let [state, setState] = useState({
    loading: false,
    leaveType: {
      name: '',
      defaultDays: ''
    },
    errorMessage: ''
  });

  //Function to Update/Edit the Form, i.e., change properties inside the "leaveType" object, 
  //leaveType = {name, defaultDays}
  let updateInput = (event) => {
    setState({
      ...state,
      leaveType: {
        ...state.leaveType,
        [event.target.name] : event.target.value
      }

    });
  };

  /*----------------------------------SECTION: Get One/Single Data from LeaveTypeService.js using leaveTypeId-----------------------------------------------------------*/
  //"loadData" is an async function
  const loadData = async () => {
    try {
      setState({...state, loading: true}); //Only changes "loading" to true while leaving "leaveType" & "errorMessage" unchanged
      let response = await LeaveTypeService.getSingleLeaveType(leaveTypeId); // gets single "leaveType" data from API
      console.log(response);
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
  }, [leaveTypeId])

  /*------------------------------SECTION END: Get One Data from LeaveTypeService.js using leaveTypeId---------------------------*/

  //POST Request Function: This Function POST the <form></form>
  let submitForm = async (event) => {
    event.preventDefault();
    try {
      let response = await LeaveTypeService.updateLeaveType(state.leaveType, leaveTypeId); //API from LeaveTypeService.js
      if (response) {
        navigate('/leaveTypes/leaveTypeList', { replace: true } );
      }
    }
    catch (error) {
      setState({
        ...state,
        loading: false,
        errorMessage: error.message
      }); //If there is an error, then "loading" is changed to false & "errorMessage" is changed from empty string with "error.message".

      navigate(`/leaveTypes/editLeaveType/${leaveTypeId}`, { replace: false } );
    }
  }

  let { loading, leaveType, errorMessage } = state;

  return (
    <React.Fragment>
        {
          loading ? <Spinner /> :
          <React.Fragment>
              <pre>{JSON.stringify(leaveType)}</pre>
              <section className='add-contact p-3'>
                  <Container>
                      <Row>
                        <Col>
                          <p className='h3 text-primary fw-bold'>Edit the Leave Type</p>
                          <p className='fst-italic'>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Id fugit voluptas cumque quia, repellat perspiciatis modi harum nostrum velit asperiores, corporis delectus ipsum quasi! Dolore, quis ducimus! Voluptatibus, animi placeat.</p>
                        </Col>
                      </Row>

                            {/*To vertically Center the Column contents inside the <Row>, use className='align-items-center' */}
                      <Row className='align-items-center'>
                        <Col md={4}>
                          <form onSubmit={submitForm}>
                              <div className='mb-2'>
                                <input type="text" className='form-control' placeholder='Name' 
                                  name='name'
                                  value={leaveType.name}
                                  onChange={updateInput}
                                  required={true}
                                />
                              </div>

                              <div className='mb-2'>
                                <input type="number" className='form-control' placeholder='DefaultDays' 
                                  name='defaultDays'
                                  value={leaveType.defaultDays}
                                  onChange={updateInput}
                                  required={true}
                                />
                              </div>

                                  {/*Below we have two Buttons: NOTE: "btn btn-property" makes any tag into a button*/}
                              <div className='mb-2'>
                                  <input type="submit" className='btn btn-primary me-2' value='Update' />
                                  <Link to={`/leaveTypes/leaveTypeList`} className="btn btn-dark">Cancel</Link>
                              </div>
                          </form>
                        </Col>
                      </Row>
                  </Container>
              </section>
          </React.Fragment>
        }
    </React.Fragment>
  )
}

export default EditLeaveType