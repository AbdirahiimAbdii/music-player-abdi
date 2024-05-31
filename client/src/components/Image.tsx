interface ImageProps {
  id: string;
  alt: string;
  height?: number;
  width?: number;
}

export default function Image({ id, alt, height, width }: ImageProps) {
  return (
    <img key={id} src={`/api/v1/images/${id}`} height={height} width={width} alt={alt}/>
  );
}