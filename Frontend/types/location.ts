import { Picture } from "./picture";

export interface Location {
  id: string;
  name: string;
  description: string;
  type: string;
  latitude: number;
  longitude: number;
  pictures: Picture[];
}