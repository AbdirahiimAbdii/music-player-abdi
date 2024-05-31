import './Artist.scss';
import {ReactElement, useState} from "react";
import {useParams} from "react-router-dom";
import useGetArtist from "./hooks/useGetArtist.ts";
import ErrorAlert from "../../components/ErrorAlert.tsx";
import AddAlbumDialogProps from "./components/add-album-dialog/AddAlbumDialog.tsx";
import ArtistHeader from "./components/artist-header/ArtistHeader.tsx";
import ArtistDiscography from "./components/ArtistDiscography.tsx";

export default function Artist(): ReactElement {
  const { artistId } = useParams();
  const {artist, loading, error } = useGetArtist(artistId ?? '');
  const [show, setShow] = useState(false);

  if (loading)
    return <span>Loading...</span>

  return (
    <div className="artist-page">
      <ErrorAlert message={error}/>
      <ArtistHeader artist={artist} onClick={() => setShow(true)}/>

      <ArtistDiscography albums={artist?.discography} />

      { artistId &&
          <AddAlbumDialogProps
            show={show}
            artistId={artistId}
            onClose={() => setShow(false)}
          />
      }
    </div>
  );
}
