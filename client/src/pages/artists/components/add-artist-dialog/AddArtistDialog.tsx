import {ReactElement, useEffect, useState} from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import {Form} from "react-bootstrap";
import chooseFile from "../../../../utils/chooseFile.ts";
import fileToBase64 from "../../../../utils/fileToBase64.ts";
import useAddArtist from "../../../../hooks/artists/useAddArtist.ts";
import ErrorAlert from "../../../../components/ErrorAlert.tsx";
import Spinner from "../../../../components/spinner/Spinner.tsx";
import {useNavigate} from "react-router-dom";

export interface ArtistInfo {
  name: string;
  description: string;
  imageBase64: string;
}

interface AddArtistDialogProps {
  show: boolean;
  onClose: () => void;
}

export default function AddArtistDialog({ show, onClose }: AddArtistDialogProps): ReactElement {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [image, setImage] = useState("");
  const { addArtist, artist, loading, error } = useAddArtist();
  const navigate = useNavigate();

  useEffect(() => {
    if (artist) {
      onClose();
      navigate(`/artists/${artist.id}`);
    }
  }, [artist, onClose, navigate]);

  const chooseImage = async () => {
   const file = await chooseFile("image/jpeg, image/png");
    const base64 = await fileToBase64(file);
    setImage(base64);
  }

  const onAddClick = async () => {
    await addArtist({ name, description, image });
  }

  return (
    <Modal
      show={show && !artist}
      onHide={onClose}
      backdrop="static"
      keyboard={false}
    >
      <Modal.Header closeButton>
        <Modal.Title>New Artist</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <ErrorAlert message={error} />

        <Form.Group className="mb-3" controlId="add-artist.name">
          <Form.Label>Name</Form.Label>
          <Form.Control type="text" value={name} onChange={(e) => setName(e.target.value)} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="add-artist.description">
          <Form.Label>Description</Form.Label>
          <Form.Control
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            as="textarea"
            rows={3}
            style={{ resize: 'none' }}
          />
        </Form.Group>

        <Button onClick={chooseImage}>Select image</Button>
        { image && <img src={image} height={128} width={128} alt="image" /> }
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onClose}>
          Cancel
        </Button>
        <Button variant="primary" onClick={onAddClick}>
          {loading && <Spinner />}
          <span>Add</span>
        </Button>
      </Modal.Footer>
    </Modal>
  );
}
