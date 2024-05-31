import {ReactElement} from "react";
import {useNavigate} from "react-router-dom";
import ArtistsSection from "./components/ArtistsSection.tsx";
import AlbumsSection from "./components/AlbumsSection.tsx";
import './Home.scss'

export default function Home(): ReactElement {
  const navigate = useNavigate();

  return (
    <main className="home-page">
      <h1>Home</h1>
      <div className="sections">
        <ArtistsSection onClick={(artist) => navigate(`/artists/${artist.id}`)} />
        <AlbumsSection onClick={(album) => navigate(`/albums/${album.id}`)} />
      </div>
    </main>
  );
}