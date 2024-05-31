import Slider from "../../../components/slider/Slider.tsx";
import Card from "../../../components/card/Card.tsx";
import {Album} from "../../../types/Album.ts";
import {useNavigate} from "react-router-dom";
import Message from "../../../components/Message.tsx";

interface ArtistDiscographyProp {
  albums: Album[] | undefined | null;
}

export default function ArtistDiscography({albums}: ArtistDiscographyProp) {
  const navigate = useNavigate();

  return (
    <section>
      <h3>Discography</h3>
      {albums?.length === 0 && <Message message="No albums yet" />}

      <Slider items={albums ?? []} id={(x) => x.id} loading={albums === undefined}>
        {(album) => (<Card
          key={album.id}
          onClick={() => navigate(`/albums/${album.id}`)}
          imageId={album.coverImageId}
          height="200px"
          width="200px"
        />)}
      </Slider>
    </section>
  );
}