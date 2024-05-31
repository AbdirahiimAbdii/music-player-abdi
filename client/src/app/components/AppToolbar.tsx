import {ReactElement} from "react";
import './AppToolbar.scss'
import {ButtonGroup} from "react-bootstrap";
import Icon from "../../components/Icon.tsx";
import {NavLink} from "react-router-dom";

export default function AppToolbar(): ReactElement {
  const getClass = ({ isActive }: { isActive: boolean}) => isActive ? 'btn btn-outline-danger active' : 'btn btn-outline-danger';
  return (
    <ButtonGroup>
      <NavLink to="/" className={getClass}>
        <Icon name="home" />
      </NavLink>
      <NavLink to="/artists" className={getClass}>
        <Icon name="artists" />
      </NavLink>
      <NavLink to="/albums" className={getClass}>
        <Icon name="albums" />
      </NavLink>
      <NavLink to="/search" className={getClass}>
        <Icon name="search" />
      </NavLink>
    </ButtonGroup>
  )

  /*
 return (
   <ButtonGroup>
     <Button variant="danger" title="Home">
       <Icon name="home" />
     </Button>
    <Button variant="danger">
      <Icon name="artists" />
    </Button>
     <Button variant="danger">
       <Icon name="albums" />
     </Button>
     <Button variant="danger">
       <Icon name="search" />
     </Button>
   </ButtonGroup>
 )
  */
}