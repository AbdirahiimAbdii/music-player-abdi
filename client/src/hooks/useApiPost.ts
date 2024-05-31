import {useState} from "react";
import axios, {AxiosRequestConfig} from "axios";

type UsePostResourceResult<TData, TResponse> = {
  postResource: (data: TData) => Promise<void>;
  response: TResponse | null;
  loading: boolean;
  error: string | null;
};

export default function useApiPost<TData, TResponse>(url: string): UsePostResourceResult<TData, TResponse> {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [response, setResponse] = useState<TResponse | null>(null);

  const postResource = async (data: TData, config?: AxiosRequestConfig) => {
    setLoading(true);
    setError(null);
    try {
      const res = await axios.post<TResponse>(url, data, config);

      setResponse(res.data);
    } catch (error) {
      if (axios.isAxiosError(error) && error.response) {
        setError(error.response.data.message || error.message);
      } else {
        setError((error as Error).message);
      }
    } finally {
      setLoading(false);
    }
  };

  return { postResource, response, loading, error };
}


