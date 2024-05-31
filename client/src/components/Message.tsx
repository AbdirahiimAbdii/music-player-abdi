import {Alert} from "react-bootstrap";

interface ErrorAlertProps {
  message: string | null;
}

export default function Message({message}: ErrorAlertProps) {
  return (
    <Alert variant="danger">{message}</Alert>
  );
}