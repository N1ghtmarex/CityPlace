import { Location } from '@/types/location';
import axiosInstance from '../src/axios';
import { useSession } from 'next-auth/react';
import localFont from 'next/font/local';
import { useEffect, useState } from 'react';

const font = localFont({
  src: '../public/fonts/FranksRus-Regular_0.otf',
})

interface LocationCardProps {
  location: Location;
  isFavorite: boolean
}

export default function LocationCard({ location, isFavorite }: LocationCardProps) {
  const { data: session } = useSession()
  const [isFavoriteParameter, setIsFavoriteParameter] = useState<boolean>(isFavorite);
  useEffect(() => {
    setIsFavoriteParameter(isFavorite);
  }, [isFavorite]);
  
  const favorite = async (id: string) => {
    await axiosInstance.post(`${process.env.NEXT_PUBLIC_API_URL}/api/locations/favorite/${id}`, null, {
      headers: {
        Authorization: `Bearer ${session?.accessToken}`
      }
    })
    .then(response => {
      setIsFavoriteParameter(!isFavoriteParameter);
    })
  }

  return (
    <div key={location.id} className="location-card-container">
      <a href={`/location/${location.id}`}>
        <img className="location-avatar"
          src={`${process.env.NEXT_PUBLIC_API_URL}/${location.pictures.filter(x => x.isAvatar)[0]?.path}`}
        />
        <div className={`${font.className} location-name`}>
          {location.name}
        </div>
        <div className="location-description">
          <span>{location.description}</span>
        </div>
      </a>
      <div className="favorite-wrapper" onClick={() => favorite(location.id)}>
        <div className="favorite-container">
          <img className='favorite-icon' src={`${isFavoriteParameter ? '/icons/favoriteFilled.svg' : '/icons/favorite.svg'}`}>
          </img>
        </div>
      </div>
      <div className="location-type">
        <span>{location.type}</span>
      </div>
    </div> 
  );
}