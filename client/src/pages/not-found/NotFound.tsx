import {ReactElement} from "react";
import {Alert} from "react-bootstrap";
import {Link} from "react-router-dom";

const styles = {
  display: 'grid',
  height: '100vh',
  margin: 'auto',
  placeItems: 'center',
  width: '80%',
  maxWidth: '400px',
}
export default function NotFound(): ReactElement {

  return (
    <div style={styles}>
      <Alert variant="danger" >
        <Alert.Heading>Oops! Page Not Found</Alert.Heading>
        <p>
          It looks like you’ve taken a wrong turn somewhere in the depths of the internet.
          But don’t worry, even the best explorers get lost sometimes.
        </p>
        <p>
          🔍 What Now?
          <ul>
            <li>You can <Link to="/">go back</Link> to the homepage and start your journey anew</li>
            <li>If you need help, feel free to contact us.</li>
          </ul>
          Remember, adventure awaits just a click away!
        </p>
        <div>
          <span><Link to="/">Take Me Home!</Link></span>
        </div>
      </Alert>
    </div>);
}