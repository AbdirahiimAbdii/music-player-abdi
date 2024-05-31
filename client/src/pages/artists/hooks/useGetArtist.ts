import useApiGet from "../../../hooks/useApiGet.ts";
import {Album} from "../../../types/Album.ts";

interface ArtistResponse {
  id: string;
  imageId: string;
  name: string;
  description: string;
  discography: Album[];
}

export default function useGetArtist(id: string) {
  const { results, loading, error} = useApiGet<ArtistResponse>(`/api/v1/artists/${id}`);
  return { artist: results, loading, error };
}