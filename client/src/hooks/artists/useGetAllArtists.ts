import {Artist} from "../../types/Artist.ts";
import useApiGet from "../useApiGet.ts";

export default function useGetAllArtists() {
  const {results, loading, error} = useApiGet<Artist[]>('/api/v1/artists');
  return { artists: results, loading, error };
}
