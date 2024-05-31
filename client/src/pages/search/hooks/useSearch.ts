import useApiGet from "../../../hooks/useApiGet.ts";

export interface SearchResults {
  artists: {
    id: string;
    name: string;
    imageId: string;
  }[];
  albums: {
    id: string;
    title: string;
    imageId: string;
  }[];
  tracks: {
    id: string;
    title: string;
  }[];
}

export default function useSearch(searchTerm: string) {
  return useApiGet<SearchResults>(
    `/api/v1/search?q=${encodeURIComponent(searchTerm)}`,
    {
      artists: [],
      albums: [],
      tracks: []
    }
  );
}