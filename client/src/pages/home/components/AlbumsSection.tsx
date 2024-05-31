import Slider from "../../../components/slider/Slider.tsx";
import Card from "../../../components/card/Card.tsx";
import {Album} from "../../../types/Album.ts";
import Section from "../../../components/Section.tsx";
import useGetAllAlbums from "../../../hooks/albums/useGetAllAlbums.ts";

interface AlbumsSectionProps {
  id?: string;
  onClick: (album: Album) => void;
}

export default function AlbumsSection({ id, onClick}: AlbumsSectionProps) {
  const { albums, loading } = useGetAllAlbums();

  return (
    <Section id={id} title="Albums">
      <Slider items={albums ?? []} loading={loading} id={(x) => x.id}>
        {(album) => (<Card
          key={album.id}
          onClick={() => onClick(album)}
          imageId={album.coverImageId}
          height="250px"
          width="250px"
        />)}
      </Slider>
    </Section>
  )
}