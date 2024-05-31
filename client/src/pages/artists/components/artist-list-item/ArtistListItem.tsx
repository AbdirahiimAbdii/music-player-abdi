import {Artist} from "../../../../types/Artist.ts";
import {ReactElement} from "react";
import Image from "../../../../components/Image.tsx";
import './ArtistListItem.scss'

interface ArtistListItemProps {
  artist: Artist;
}

export default function ArtistListItem({artist}: ArtistListItemProps): ReactElement {
  return (
    <div className="artist-list-item">
      <Image id={artist.imageId} alt={artist.name} height={128} width={128}/>
      <div className="content">
        <h3>{artist.name}</h3>
        <p>{artist.description}</p>
      </div>
    </div>
  )
}