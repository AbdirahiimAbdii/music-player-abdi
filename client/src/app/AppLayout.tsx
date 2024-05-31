import {Outlet} from "react-router-dom";
import Menu from "../components/menu/Menu.tsx";
import './AppLayout.scss'
import AppToolbar from "./components/AppToolbar.tsx";
import SearchNavLink from "../components/search/SearchNavLink.tsx";

function AppLayout() {
  return (
    <div className="app">
      <aside className="sidebar">
        <SearchNavLink to="search" placeholder="Search" />
        <Menu title="Abdi Music" items={[
          { id:'home', icon: 'home', text: 'Home', path: '/' },
          // { id:'playlists', icon: 'playlist', text: 'Playlists', path: 'playlists' }
        ]} />
        <Menu title="Library" items={[
          { id:'artists', icon: 'artists', text: 'Artists', path: 'artists' },
          // { id:'albums', icon: 'albums', text: 'Albums', path: 'albums' },
          // { id:'songs', icon: 'songs', text: 'Songs', path: 'songs' }
        ]} />
      </aside>
      <main>
        <div className="content">
          <Outlet />
        </div>
        <aside className="toolbar">
          <AppToolbar />
        </aside>
      </main>
    </div>
  );
}

export default AppLayout
