import useApiPost from "../useApiPost.ts";
import {Album} from "../../types/Album.ts";

export interface ArtistAlbumRequest {
  title: string;
  coverImage: string;
  year: number;
  genre: string;
}

export default function useAddArtistAlbum(artistId: string) {
  const { postResource, response, loading, error} = useApiPost<ArtistAlbumRequest, Album>(`/api/v1/artists/${artistId}/discography`);
  return { addAlbum: postResource, album: response, loading, error };
}