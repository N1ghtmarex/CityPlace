"use client"
import LocationCard from "@/components/LocationCard";
import { Location } from "@/types/location";
import axios from "axios";
import { useSession } from "next-auth/react";
import { useEffect, useState } from "react";

export default function HomePage() {
  const apiUrl = process.env.NEXT_PUBLIC_API_URL ?? "";
  const [locations, setLocations] = useState<Location[]>([]);
  const [favoriteLocations, setFavoriteLocations] = useState<Location[]>([]);
  const { data: session, status } = useSession()

  const getLocations = async () => {
    await axios.get(`${apiUrl}/api/locations`)
      .then(response => {
        setLocations(response.data.items);
        console.log(response.data.items);
      }
    );
  }

  const getFavoriteLocations = async () => {
    await axios.get(`${apiUrl}/api/locations/favorite`, {
      headers: {
        Authorization: `Bearer ${session?.accessToken}`
      }
    })
    .then(response => {
      setFavoriteLocations(response.data.items);
      console.log(response.data.items);
    })
  }

  useEffect(() => {
    getLocations();
  }, []);

  useEffect(() => {
    if (status === 'authenticated') {
      getFavoriteLocations();
    }
  }, [status])
  
  return (
    <div className="locations-container">
      { locations.map((location) => (
        <LocationCard key={location.id} location={location} isFavorite={favoriteLocations.some(x => x.id == location.id)}/>
      ))
      }
    </div>
  );
}