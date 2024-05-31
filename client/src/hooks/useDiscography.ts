import { useState, useEffect } from 'react';
import axios, { AxiosRequestConfig } from 'axios';

interface Album {
  id: string;
  name: string;
  description: string;
  imageId: string;
}

function useDiscography(artistId: string) {
  const [albums, setAlbums] = useState<Album[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>('');

  useEffect(() => {
    const url = `/api/v1/artists/${artistId}/discography`;
    const options: AxiosRequestConfig = {}
    const fetchData = async () => {
      setLoading(true);
      try {
        const response = await axios.get<Album[]>(url, options);
        setAlbums(response.data);
        setError('');
      } catch (err) {
        if (axios.isAxiosError(err)) {
          setError(err.message || 'An unknown error occurred');
        } else {
          setError('An unexpected error occurred');
        }
      } finally {
        setLoading(false);
      }
    };

    fetchData();

    // Optional: Return a cleanup function to cancel the request on component unmount
    return () => {
      const source = axios.CancelToken.source();
      options.cancelToken = source.token;
      source.cancel('API call cancelled');
    };
  }, [artistId]);

  return { albums, loading, error };
}

export default useDiscography;