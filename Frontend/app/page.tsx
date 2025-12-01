"use client"
import LocationCard from "@/components/LocationCard";
import { Location } from "@/types/location";
import { useSession } from "next-auth/react";
import { useEffect, useState } from "react";
import { getFavoriteLocations, getLocations } from "./services/location.service";


export default function HomePage() {
  const [locations, setLocations] = useState<Location[]>([]);
  const [favoriteLocations, setFavoriteLocations] = useState<Location[]>([]);
  const { data: session, status } = useSession()

  const loadLocations = async () => {
    const result = await getLocations();
    setLocations(result.items);
  }

  const loadFavoriteLocations = async () => {
    const result = await getFavoriteLocations();
    setFavoriteLocations(result.items);
  }

  useEffect(() => {
    loadLocations();
  }, []);

  useEffect(() => {
    if (status == 'authenticated') {
      loadFavoriteLocations();
    }
  }, [session, status])
  
  return (
    <div className="locations-container">
      { locations.map((location) => (
        <LocationCard key={location.id} location={location} isFavorite={favoriteLocations.some(x => x.id == location.id)}/>
      ))
      }
    </div>
  );
}