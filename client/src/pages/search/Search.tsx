import {ReactElement, useEffect, useState} from "react";
import {useNavigate, useSearchParams} from "react-router-dom";
import useSearch from "./hooks/useSearch.ts";
import ErrorAlert from "../../components/ErrorAlert.tsx";
import SearchNavLink from "../../components/search/SearchNavLink.tsx";
import {ListGroup} from "react-bootstrap";
import Message from "../../components/Message.tsx";

export default function Search(): ReactElement {
  const [searchParams] = useSearchParams();
  const [query, setQuery] = useState("");
  const { results, loading, error } = useSearch(query);
  const navigate = useNavigate();

  useEffect(() => {
    const handler = setTimeout(() => {
      const value = searchParams.get('query') ?? '';
      setQuery(value);
    }, 200);

    return () => {
      clearTimeout(handler)
    }
  }, [searchParams]);

  const artistResults = results?.artists?.length ?? 0;
  const albumResults = results?.albums?.length ?? 0;
  const trackResults = results?.tracks?.length ?? 0;
  const totalResults = artistResults + albumResults + trackResults;

  return (<div className="sections-container">
    <ErrorAlert message={error} />
    <SearchNavLink to="." />

    {!loading && artistResults > 0 &&
      <section>
        <h2>Artists</h2>
        <ListGroup>
          {results?.artists.map((artist) => (
            <ListGroup.Item key={artist.id} as="li" action variant="dark" onClick={() => navigate(`/artists/${artist.id}`)}>
              {artist.name}
            </ListGroup.Item>
          ))}
        </ListGroup>
      </section>
    }

    {!loading && albumResults > 0 &&
      <section>
        <h2>Albums</h2>
        <ListGroup>
          {results?.albums.map((album) => (
            <ListGroup.Item key={album.id} as="li" action variant="dark" onClick={() => navigate(`/albums/${album.id}`)}>
              {album.title}
            </ListGroup.Item>
          ))}
        </ListGroup>
      </section>
    }

    {!loading && trackResults > 0 &&
      <section>
        <h2>Tracks</h2>
        <ListGroup>
          {results?.tracks.map((track) => (
            <ListGroup.Item key={track.id} as="li" action variant="dark" onClick={() => navigate(`/tracks/${track.id}`)}>
              {track.title}
            </ListGroup.Item>
          ))}
        </ListGroup>
      </section>
    }

    { totalResults === 0 &&
      <Message message="No results" />
    }
  </div>);
}
