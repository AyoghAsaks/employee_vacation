import Card from 'react-bootstrap/Card';

const Home = () => {

  return (
    <div className='d-flex justify-content-center align-items-center' style={{ minHeight: "500px", minWidth: "600px" }}>
      <Card>
        <Card.Body>
          <Card.Text>
            Some quick example text to build on the card title and make up the
            bulk of the card's content.
          </Card.Text>
        </Card.Body>
      </Card>
    </div>
  )
}

export default Home