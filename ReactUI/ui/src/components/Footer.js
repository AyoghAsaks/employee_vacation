import React from 'react'
import { Col, Container, Row } from 'react-bootstrap';

const Footer = () => {
  return (
    <div className='footer'>
        <Container>
        <Row>
                <Col className='text-center py-3'>
                    Copyright &copy; Ayogh
                </Col>
        </Row>
        </Container>
    </div>
  );
}

export default Footer;