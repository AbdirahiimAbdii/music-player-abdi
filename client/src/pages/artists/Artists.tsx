import {ReactElement, useState} from "react";
import useGetAllArtists from "../../hooks/artists/useGetAllArtists.ts";
import './Artists.scss';
import AddArtistDialog from "./components/add-artist-dialog/AddArtistDialog.tsx";
import {Button, ListGroup} from "react-bootstrap";
import {useNavigate} from "react-router-dom";
import ArtistListItem from "./components/artist-list-item/ArtistListItem.tsx";

export default function Artists(): ReactElement {
  const navigate = useNavigate();
  const [show, setShow] = useState(false);
  const { artists, loading } = useGetAllArtists();

  if (loading) {
    return <div>loading...</div>
  }

  return (
    <div className="sections-container">
      <header>
        <h1>Artists</h1>
        <Button variant="primary" onClick={() => setShow(true)}>
          Add
        </Button>
      </header>

      {loading && <div>loading...</div> }

      {!loading && artists && <ListGroup>
        {artists.map((artist) => (
          <ListGroup.Item key={artist.id} as="li" action variant="dark" onClick={() => navigate(`/artists/${artist.id}`)}>
            <ArtistListItem artist={artist} />
          </ListGroup.Item>
        ))}
      </ListGroup>}

      <AddArtistDialog
        show={show}
        onClose={() => setShow(false)}
      />
    </div>);
}
