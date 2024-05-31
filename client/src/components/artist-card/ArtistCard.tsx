import {ReactElement} from "react";
import {Artist} from "../../types/Artist.ts";
import Card from "../card/Card.tsx";
import './ArtistCard.scss';

interface ArtistCardProps {
  artist: Artist;
  height: string;
  width: string;
  onClick?: () => void;
}

function ArtistCard({ artist, width, height, onClick }: ArtistCardProps): ReactElement {
  return (
    <div className="artist-card" style={{height, width}}>
      <div className="content" onClick={onClick}>
        <h3 className="text-danger">{artist.name}</h3>
        <p>{artist.description}</p>
      </div>
      <Card imageId={artist.imageId} height={height} width={width}/>
    </div>
  );
}

export default ArtistCard;