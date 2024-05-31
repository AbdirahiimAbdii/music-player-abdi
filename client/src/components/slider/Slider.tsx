import {ReactNode} from "react";
import "./Slider.scss";

interface SliderProp<T> {
  items: T[];
  loading: boolean;
  id: (item: T) => string;
  children?: (item: T) => ReactNode;
}

export default function Slider<T>({
  items, children, loading, id
}: SliderProp<T>) {

  if (!children) {
    throw new Error("No children");
  }

  const childrenItems = loading
    ? <div>Loading...</div>
    : items.map((item: T) => <div key={id(item)}>{children(item)}</div>);

  return (
    <div className="slider">
      <div className="slider-container">
        {childrenItems}
      </div>
    </div>
  );
}