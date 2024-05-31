import './Card.scss';
import {CSSProperties, ReactElement} from "react";

interface CardProps {
  imageId: string;
  height: string;
  width: string;
  onClick?: () => void;
}

function Card({ imageId, width, height, onClick }: CardProps): ReactElement {
  const styles: CSSProperties = {
    width, height,
    backgroundImage: `url("/api/v1/images/${imageId}")`,
  }

  if(onClick) {
    styles['cursor'] = 'pointer';
  }

  return (
    <div className="card" style={styles} onClick={onClick}></div>
  );
}

export default Card;