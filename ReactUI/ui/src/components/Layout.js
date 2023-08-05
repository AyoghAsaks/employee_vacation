import React from 'react'
import { Col, Container, Row } from 'react-bootstrap';
import Footer from './Footer';
import Header from './Header';

const Layout = () => {
  return (
    <>
        <Header />
            <main>
                <Container>
                    <Row>
                        <Col className='text-center'>
                            <h1>Ayogh welcome. The company.</h1>
                        </Col>
                    </Row>
                </Container>
            </main>
        <Footer />
    </>
  )
}

export default Layout;
