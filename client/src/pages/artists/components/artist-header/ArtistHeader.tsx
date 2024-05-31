import {Artist} from "../../../../types/Artist.ts";
import {ReactElement} from "react";
import Image from "../../../../components/Image.tsx";
import './ArtistHeader.scss';
import {Button} from "react-bootstrap";

interface ArtistHeader {
  artist: Artist | null | undefined;
  onClick?: () => void;
}

export default function ArtistHeader({artist, onClick}: ArtistHeader): ReactElement {
  return (
    <header className="artist-header">
      <Image id={artist?.imageId ?? ''} height={200} width={200} alt=""/>
      <div className="description">
        <h1>{artist?.name}</h1>
        <p>{artist?.description}</p>

        <Button variant="primary" onClick={onClick}>
          Add album
        </Button>
      </div>
    </header>
  );
}