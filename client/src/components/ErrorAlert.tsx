import {Alert} from "react-bootstrap";
import {useState} from "react";

interface ErrorAlertProps {
  message: string | null;
  dismissible?: boolean;
}
export default function ErrorAlert({message, dismissible}: ErrorAlertProps) {
  const [dismissedError, setDismissedError] = useState(false);

  if (message === null || dismissedError)
    return null;

  return (
    <Alert variant="danger" onClose={() => setDismissedError(true)} dismissible={dismissible}>
      <Alert.Heading>Oh snap! You got an error!</Alert.Heading>
      <p>{message}</p>
    </Alert>
  );
}