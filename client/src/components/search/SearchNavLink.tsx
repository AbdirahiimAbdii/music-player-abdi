import './Search.scss';
import {NavLink} from "react-router-dom";
import {useState} from "react";
import Search from "./Search.tsx";
import './Search.scss';

interface SearchProps {
  to: string;
  onClick?: ()=> void;
  onFocus?: () => void;
  onBlur?: () => void;
  placeholder?: string;
  ariaLabel?: string;
}

function SearchNavLink({ to, onClick, placeholder, ariaLabel}: SearchProps) {
  const [focused, setFocused] = useState(false);

  const computeClassName = ({ isActive }: { isActive: boolean }): string => {
    const classes = ['search-container'];
    isActive && classes.push('active');
    focused && classes.push('focus');
    return classes.join(" ");
  }

  return (
    <NavLink
      to={to}
      className={computeClassName}
      onBlur={() => setFocused(false)}>
      <Search
        placeholder={placeholder}
        onFocus={() => setFocused(true)}
        ariaLabel={ariaLabel}
        onClick={onClick} />
    </NavLink>)
}

export default SearchNavLink;