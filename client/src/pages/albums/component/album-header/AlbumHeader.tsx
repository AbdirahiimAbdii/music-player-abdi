import {ReactElement} from "react";
import {Album} from "../../../../types/Album.ts";
import Image from "../../../../components/Image.tsx";
import './AlbumHeader.scss';

interface AlbumHeaderProp {
  album: Album;
}

export default function AlbumHeader({album}: AlbumHeaderProp): ReactElement {
  return (
    <header className="album-header">
      <Image id={album.coverImageId} alt={album.title} height={200} width={200} />
      <div>
        <h1>{album.title}</h1>
        <h3>{album.artist.name}</h3>
        <p>{album.genre}</p>
        <p>{album.year}</p>
      </div>
    </header>
  );
}