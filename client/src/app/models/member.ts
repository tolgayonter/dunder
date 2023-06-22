import { Photo } from './photo';

export interface Member {
  id: number;
  userName: string;
  photoUrl: string;
  age: number;
  knownAs: string;
  joined: Date;
  lastActive: Date;
  bio: string;
  city: string;
  country: string;
  photos: Photo[];
}
