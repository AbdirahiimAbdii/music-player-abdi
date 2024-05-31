import {ReactElement} from 'react';

export type IconName = 'home' | 'browse' | 'search' | 'radio' | 'artists' | 'albums' | 'songs' | 'github' | 'close' | 'playlist';

interface IconProps {
  name: IconName | string;
  size?: number;
}

const iconMap: Record<string, string> = {
  'home': 'house',
  'browse': 'grid',
  'search': 'search',
  'radio': 'broadcast',
  'artists': 'mic',
  'albums': 'collection',
  'songs': 'music-note',
  'github': 'github',
  'close': 'x',
  'playlist': 'music-note-list'
};

function Icon({ name, size = 16 }: IconProps): ReactElement {
  const bsIconName: string = name in iconMap ? iconMap[name.toString()] : name;

  return (<i className={`bi bi-${bsIconName}`} style={{ fontSize: `${size}px` }}></i>);
}

export default Icon;