import Icon from "../Icon.tsx";
import './Search.scss';
import {useSearchParams} from "react-router-dom";
import {ChangeEvent, useRef} from "react";

interface SearchProps {
  onClick?: ()=> void;
  placeholder?: string;
  ariaLabel?: string;
  onFocus?: () => void;
}

function Search({ onFocus, placeholder, ariaLabel}: SearchProps) {
  const inputRef = useRef<HTMLInputElement>(null);
  const [searchParams, setSearchParams] = useSearchParams();
  const query = searchParams.get('query') || '';

  const onChange = (e: ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setSearchParams(value ? { query: e.target.value } : {});
  };
  const onClick = () => {
    inputRef.current?.focus();
  }

  return (
    <fieldset onClick={onClick}>
      <Icon name="search"/>
      <input
        ref={inputRef}
        value={query}
        onFocus={onFocus}
        onChange={onChange}
        placeholder={placeholder}
        aria-label={ariaLabel}/>
        <span className="clear-search">
          <Icon name="close" size={16}/>
        </span>
    </fieldset>
  )
}

export default Search;