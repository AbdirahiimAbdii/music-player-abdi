import useApiGet from "../useApiGet.ts";
import {Album} from "../../types/Album.ts";

export default function useGetAlbum(albumId: string) {
  const {results, loading, error} = useApiGet<Album>(`/api/v1/albums/${albumId}`);
  return {album: results, loading, error};
}

