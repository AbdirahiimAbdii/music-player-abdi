import {NavLink} from "react-router-dom";
import Icon, {IconName} from "../Icon.tsx";
import './Menu.scss'

interface MenuProps {
  title?: string;
  items: MenuItem[];
}

interface MenuItem {
  id: string;
  icon: IconName;
  path: string;
  text: string;
}


function Menu({title, items}: MenuProps) {
  return (
    <section className="menu">
    {!!title &&
      <h3 className="menu-header">{title}</h3>
    }
      <ul className="menu-items">
        {items.map(item => (
          <li key={item.id}>
            <NavLink to={item.path} className={({ isActive }) => isActive ? 'active' : undefined}>
              <Icon name={item.icon} />
              {item.text}
            </NavLink>
          </li>))}
      </ul>
    </section>
  )
}

export default Menu;