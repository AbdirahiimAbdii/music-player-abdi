export interface Album {
  id: string;
  artist: {
    id: string;
    name: string;
    imageId: string;
  };
  coverImageId: string;
  title: string;
  year: number;
  genre: string;
  tracks: Track[];
}

export interface Track {
  id: string;
  albumId: string;
  title: string;
  position: number;
  duration: number;
}
