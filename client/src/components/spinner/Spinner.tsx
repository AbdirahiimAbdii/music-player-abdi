import BsSpinner from 'react-bootstrap/Spinner';

function Spinner() {
  return (
    <BsSpinner animation="border" role="status" size="sm">
      <span className="visually-hidden">Loading...</span>
    </BsSpinner>
  );
}

export default Spinner;