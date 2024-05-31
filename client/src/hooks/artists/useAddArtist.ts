import {Artist} from "../../types/Artist.ts";
import useApiPost from "../useApiPost.ts";

export interface ArtistRequest {
  name: string;
  description: string;
  image: string;
}

export default function useAddArtist() {
  const { postResource, response, loading, error} = useApiPost<ArtistRequest, Artist>('/api/v1/artists');
  return { addArtist: postResource, artist: response, loading, error };
}