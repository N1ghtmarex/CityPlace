"use client"
import LocationCard from "@/components/LocationCard";
import { Location } from "@/types/location";
import axios from "axios";
import { useSession } from "next-auth/react";
import { useEffect, useState } from "react";
import '../src/styles/index.scss'

export default function HomePage() {
  const apiUrl = process.env.NEXT_PUBLIC_API_URL ?? "";
  const [locations, setLocations] = useState<Location[]>([]);
  const [favoriteLocations, setFavoriteLocations] = useState<Location[]>([]);
  const [searchQuery, setSearchQuery] = useState("");
  const { data: session, status } = useSession()

  const getLocations = async () => {
    await axios.get(`${apiUrl}/api/locations${searchQuery != '' ? `?searchQuery=${searchQuery}` : ''}`)
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
    <div className="min-h-screen bg-gray-50">
      <section className="bg-gradient-to-r from-blue-600 to-purple-700 text-white py-20">
        <div className="container mx-auto px-4 text-center">
          <h2 className="text-5xl font-bold mb-6">Откройте удивительные места рядом с вами</h2>
          <p className="text-xl mb-8 max-w-2xl mx-auto">
            Исследуйте уникальные локации, добавляйте в избранное и планируйте свои приключения
          </p>
          <div className="max-w-md mx-auto">
            <div className="flex">
              <input
                value={searchQuery}
                onChange={e => { setSearchQuery(e.target.value) }}
                type="text" 
                placeholder="Поиск мест, категорий..." 
                className="flex-1 px-4 py-3 rounded-l-lg text-gray-800 focus:outline-none"
              />
              <button onClick={getLocations} className={`bg-yellow-500 text-gray-900 px-6 py-3 rounded-r-lg font-semibold hover:bg-yellow-400 transition-colors`}>
                Найти
              </button>
            </div>
          </div>
        </div>
      </section>

      <section className="py-16">
        <div className="container mx-auto px-4">
          <h2 className="text-3xl font-bold text-gray-800 mb-12 text-center">Популярные локации</h2>

          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 justify-items-center gap-8">
            { locations.map((location) => (
              <LocationCard key={location.id} location={location} isFavorite={favoriteLocations.some(x => x.id == location.id)}/>
            ))
            }
          </div>
        </div>
      </section>
    </div>
  );
}