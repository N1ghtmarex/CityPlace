import { Address } from "./address";
import { Picture } from "./picture";

export interface Location {
  id: string;
  name: string;
  description: string;
  type: string;
  address: Address;
  pictures: Picture[];
}