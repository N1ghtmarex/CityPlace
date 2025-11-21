"use client"
import Footer from "@/components/layout/Footer";
import Header from "@/components/layout/Header";
import { Location } from "@/types/location";
import Link from "next/link";
import { use, useEffect, useState } from "react";
import axios from 'axios';
import { load } from "@2gis/mapgl";

interface Props {
  params: Promise<{
    id: string;
  }>;
}

export default function LocationPage({ params }: Props) {
  const { id } = use(params);
  const apiUrl = process.env.NEXT_PUBLIC_API_URL ?? "";

  const [location, setLocation] = useState<Location>();
  const [lat, setLat] = useState(Number);
  const [lon, setLon] = useState(Number);
  const [coordinates, setCoordinates] = useState<[number, number] | null>(null);

  const getLocation = () => {
    axios.get(`${apiUrl}/api/locations/${id}`)
        .then(response => {
            setLocation(response.data);
            axios.get(`https://catalog.api.2gis.com/3.0/items/geocode?q=${response.data.address.settlement}, ${response.data.address.planningStructure}, ${response.data.address.house}&fields=items.point&key=9a5ff141-c68c-4a4b-8e28-0612a9894449`)
                .then(res => {
                    console.log(res)
                    const lon = res.data.result.items[0].point.lon;
                    const lat = res.data.result.items[0].point.lat;
                    setCoordinates([lon, lat]);
                }
            );
        }
    )
  }

  useEffect(() => {
    getLocation();
  }, []);

  useEffect(() => {
    if (coordinates) {
        load().then((mapglAPI) => {
            const map = new mapglAPI.Map('map', {
                center: coordinates,
                zoom: 13,
                key: '9a5ff141-c68c-4a4b-8e28-0612a9894449',
            });

            const marker = new mapglAPI.Marker(map, {
                coordinates: coordinates,
            });
        });
    };
  }, [coordinates]);
  
  if (!location) {
    return (
        <div>
            <Header />
            <div className="min-h-screen bg-gray-50 py-8">
                <div className="container mx-auto px-4">
                    <div className="bg-white rounded-lg shadow-lg p-8 text-center">
                        <h1 className="text-3xl font-bold text-red-600 mb-4">Локация не найдена</h1>
                        <p className="text-gray-600 mb-6">Локация с ID {id} не существует</p>
                        <Link href="/" className="bg-blue-600 text-white px-6 py-3 rounded-lg hover:bg-blue-700 transition-colors">
                            Вернуться к списку локаций
                        </Link>
                    </div>
                </div>
            </div>
            <Footer />
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
        <Header />
        {/* Хлебные крошки */}
        <nav className="bg-white border-b border-gray-200">
            <div className="container mx-auto px-4 py-4">
            <div className="flex items-center space-x-2 text-sm text-gray-600">
                <Link href="/" className="hover:text-blue-600 transition-colors">Главная</Link>
                <span>›</span>
                <Link href="/locations" className="hover:text-blue-600 transition-colors">Локации</Link>
                <span>›</span>
                <span className="text-gray-900 font-medium">{location.name}</span>
            </div>
            </div>
        </nav>

        <div className="container mx-auto px-4 py-8">
            <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
                {/* Основной контент */}
                <div className="lg:col-span-2">
                    {/* Галерея изображений */}
                    <div className="bg-white rounded-xl shadow-lg overflow-hidden mb-6">
                        <div className="grid grid-cols-2 gap-2 p-2">
                            <div className="col-span-2">
                                <img src={`${process.env.NEXT_PUBLIC_API_URL}/${location.pictures.filter(x => x.isAvatar == true)[0].path}`} 
                                    className="w-full h-80 object-cover rounded-lg"/>
                            </div>
                            {location.pictures.slice(1, 5).map((picture, index) => (
                            <img 
                                key={index}
                                src={`${process.env.NEXT_PUBLIC_API_URL}/${picture.path}`}
                                className="w-full h-40 object-cover rounded-lg"
                            />
                        ))}
                        </div>
                    </div>

                    {/* Основная информация */}
                    <div className="bg-white rounded-xl shadow-lg p-6 mb-6">
                        <div className="flex items-start justify-between mb-4">
                            <div>
                                <h1 className="text-3xl font-bold text-gray-800 mb-2">{location.name}</h1>
                                <div className="flex items-center space-x-4 mb-4">
                                    <span className="bg-blue-100 text-blue-800 px-3 py-1 rounded-full text-sm font-semibold">
                                        {location.type}
                                    </span>
                                    <div className="flex items-center space-x-1">
                                        <span className="font-semibold">5.0</span>
                                        <span className="text-gray-500">(0 отзывов)</span>
                                    </div>
                                </div>
                            </div>
                            <button className="text-gray-400 hover:text-red-500 transition-colors p-2">
                                <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
                                </svg>
                            </button>
                        </div>

                        <div className="flex items-center text-gray-600 mb-6">
                            <span>{location.address.region}, {location.address.settlement}, {location.address.district}<br/>
                                {location.address.planningStructure}, {location.address.house}</span>
                        </div>

                        <div className="prose max-w-none">
                            <p className="text-gray-700 text-lg leading-relaxed">{location.description}</p>
                        </div>
                    </div>
                </div>

                {/* Боковая панель */}
                <div className="space-y-6">

                    {/* Карта */}
                    <div className="bg-white rounded-xl shadow-lg p-6">
                        <h3 className="text-xl font-bold text-gray-800 mb-4">Расположение</h3>
                        <div id="map" className="bg-gray-200 rounded-lg h-48 flex items-center justify-center">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
  );
}