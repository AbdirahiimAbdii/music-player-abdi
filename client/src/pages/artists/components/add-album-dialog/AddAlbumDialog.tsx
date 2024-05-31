import {ReactElement, useEffect, useState} from "react";
import chooseFile from "../../../../utils/chooseFile.ts";
import fileToBase64 from "../../../../utils/fileToBase64.ts";
import Modal from "react-bootstrap/Modal";
import ErrorAlert from "../../../../components/ErrorAlert.tsx";
import {Form} from "react-bootstrap";
import Button from "react-bootstrap/Button";
import Spinner from "../../../../components/spinner/Spinner.tsx";
import useAddArtistAlbum from "../../../../hooks/artists/useAddArtistAlbum.ts";
import {useNavigate} from "react-router-dom";

interface AddAlbumDialogProps {
  show: boolean;
  artistId: string;
  onClose: () => void;
}

export default function AddAlbumDialogProps({
  show,
  onClose,
  artistId
}: AddAlbumDialogProps): ReactElement {
  const [title, setTitle] = useState("");
  const [genre, setGenre] = useState("");
  const [year, setYear] = useState(Date);
  const [coverImage, setCoverImage] = useState("");
  const { addAlbum, album, loading, error } = useAddArtistAlbum(artistId);
  const navigate = useNavigate();

  useEffect(() => {
    if (album) {
      navigate(`/albums/${album.id}`);
    }
  }, [album, onClose, navigate]);
  
  const chooseImage = async () => {
    const file = await chooseFile("image/jpeg, image/png");
    const base64 = await fileToBase64(file);
    setCoverImage(base64);
  }

  const onAddClick = async () => {
    await addAlbum({ title, year: parseInt(year), coverImage, genre });
  }

  return (
    <Modal
      show={show}
      onHide={onClose}
      backdrop="static"
      keyboard={false}
    >
      <Modal.Header closeButton>
        <Modal.Title>New Album</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <ErrorAlert message={error} />

        <Form.Group className="mb-3" controlId="add-album.title">
          <Form.Label>Title</Form.Label>
          <Form.Control type="text" value={title} onChange={(e) => setTitle(e.target.value)} />
        </Form.Group>

        <Form.Group className="mb-3" controlId="add-album.genre">
          <Form.Label>Genre</Form.Label>
          <Form.Control type="text" value={genre} onChange={(e) => setGenre(e.target.value)} />
        </Form.Group>

        <Form.Group className="mb-3" controlId="add-album.genre">
          <Form.Label>Year</Form.Label>
          <Form.Control type="number" value={year} onChange={(e) => setYear(e.target.value)} />
        </Form.Group>

        <Button onClick={chooseImage}>Select image</Button>
        { coverImage && <img src={coverImage} height={128} width={128} alt="image" /> }
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