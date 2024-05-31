import Slider from "../../../components/slider/Slider.tsx";
import useGetAllArtists from "../../../hooks/artists/useGetAllArtists.ts";
import {Artist} from "../../../types/Artist.ts";
import Section from "../../../components/Section.tsx";
import ArtistCard from "../../../components/artist-card/ArtistCard.tsx";

interface ArtistsSectionProps {
  id?: string;
  onClick: (artist: Artist) => void;
}

export default function ArtistsSection({id, onClick}: ArtistsSectionProps) {
  const { artists, loading } = useGetAllArtists();

  return (
    <Section id={id} title="Artists">
      <Slider items={artists ?? []} loading={loading} id={(x) => x.id}>
        {(artist) => (<ArtistCard
          key={artist.id}
          onClick={() => onClick(artist)}
          artist={artist}
          height="250px"
          width="250px"
        />)}
      </Slider>
    </Section>
  )
}