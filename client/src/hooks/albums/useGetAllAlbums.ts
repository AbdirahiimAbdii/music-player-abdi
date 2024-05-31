import useApiGet from "../useApiGet.ts";
import {Album} from "../../types/Album.ts";

export default function useGetAllAlbums() {
  const {results, loading, error} = useApiGet<Album[]>('/api/v1/albums');
  return {albums: results, loading, error};
}

