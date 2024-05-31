import {useEffect, useState} from "react";
import axios, {CancelTokenSource} from "axios";

type UseApiGetResponse<TResponse> = {
  results: TResponse | null;
  loading: boolean;
  error: string | null;
};

export default function useApiGet<TResponse>(url: string, defaultValue: TResponse | null = null): UseApiGetResponse<TResponse> {
  const [results, setResults] = useState<TResponse | null>(defaultValue);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!url) {
      setResults(defaultValue);
      return;
    }

    const source: CancelTokenSource = axios.CancelToken.source();

    const fetchData = async () => {
      setLoading(true);
      setError(null);

      try {
        const response = await axios.get(url, {
          cancelToken: source.token
        });
        setResults(response.data);
      } catch (error) {
        if (axios.isCancel(error)) {
          console.log('Request canceled', error.message);
        } else {
          setError(error instanceof Error ? error.message : 'An error occurred');
        }
      } finally {
        setLoading(false);
      }
    };

    fetchData();

    return () => {
      source.cancel('Operation canceled by the user.');
    };
  }, [url]);

  return { results, loading, error };
}