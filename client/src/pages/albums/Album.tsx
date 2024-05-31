import {ReactElement} from "react";
import {useParams} from "react-router-dom";
import useGetAlbum from "../../hooks/albums/useGetAlbum.ts";
import ErrorAlert from "../../components/ErrorAlert.tsx";
import AlbumHeader from "./component/album-header/AlbumHeader.tsx";
import {ListGroup} from "react-bootstrap";

export default function Album(): ReactElement {
  const { albumId } = useParams();
  const { album, error } = useGetAlbum(albumId ?? '');

  return (
    <div className="sections-container">
      <ErrorAlert message={error} />
      {album && <AlbumHeader album={album} />}

      <ListGroup as="ol" numbered>
        {album && album.tracks.map((t) => (
          <ListGroup.Item key={t.id} as="li" variant="dark">
            {t.title}
          </ListGroup.Item>
        ))}
      </ListGroup>



    </div>);
}