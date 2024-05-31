import {BrowserRouter, Route, Routes} from "react-router-dom";
import AppLayout from "./AppLayout.tsx";
import Home from "../pages/home/Home.tsx";
import Artists from "../pages/artists/Artists.tsx";
import Albums from "../pages/albums/Albums.tsx";
import Songs from "../pages/songs/Songs.tsx";
import Playlists from "../pages/playlists/Playlists.tsx";
import Search from "../pages/search/Search.tsx";
import Artist from "../pages/artists/Artist.tsx";
import Album from "../pages/albums/Album.tsx";
import NotFound from "../pages/not-found/NotFound.tsx";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<AppLayout />}>
          <Route index element={<Home />} />
          <Route path="/playlists" element={<Playlists />} />
          <Route path="/artists">
            <Route path="" element={<Artists />} />
            <Route path=":artistId" element={<Artist />} />
          </Route>
          <Route path="/albums">
            <Route path="" element={<Albums />} />
            <Route path=":albumId" element={<Album />} />
          </Route>
          <Route path="/songs" element={<Songs />} />
          <Route path="/search" element={<Search />} />
        </Route>
        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App
