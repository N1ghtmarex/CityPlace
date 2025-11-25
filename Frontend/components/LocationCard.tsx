import { Location } from '@/types/location';
import axios from 'axios';
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
  const { data: session, status } = useSession()
  const [isFavoriteParameter, setIsFavoriteParameter] = useState<boolean>(isFavorite);
  useEffect(() => {
    setIsFavoriteParameter(isFavorite);
  }, [isFavorite]);
  
  const favorite = async (id: string) => {
    await axios.post(`${process.env.NEXT_PUBLIC_API_URL}/api/locations/favorite/${id}`, null, {
      headers: {
        Authorization: `Bearer ${session?.accessToken}`
      }
    })
    .then(response => {
      setIsFavoriteParameter(!isFavoriteParameter);
    })

  }

  return (
    <div key={location.id} className="max-w-md relative drop-shadow-lg mb-55">
      <a href={`/location/${location.id}`}>
        {/* Аватар */}
        <img className="w-full h-full object-cover rounded-4xl hover:mix-blend-multiply hover:opacity-90"
          src={`${process.env.NEXT_PUBLIC_API_URL}/${location.pictures.filter(x => x.isAvatar)[0]?.path}`}
        />
        {/* Название локации */}
        <div className={`${font.className} text-black mt-4 text-center text-3xl font-black tracking-wide h-[2.2em] cursor-pointer`}>
          {location.name}
        </div>
        <div className="text-gray-400 text-center mt-6">
          <span>{location.description}</span>
        </div>
      </a>
      {/* Избранное */}
      <div className="absolute top-0 right-0 mr-2 mt-2 w-9 h-9 rounded-full bg-white drop-shadow-lg cursor-pointer" onClick={() => favorite(location.id)}>
        <div className="w-full h-full flex items-center justify-center">
          <svg viewBox="0 0 24 24" className="w-7 h-7"
            fill={`${isFavoriteParameter ? 'red' : 'none'}`} xmlns="http://www.w3.org/2000/svg">
            <path fillRule="evenodd" clipRule="evenodd" d="M12 6.00019C10.2006 3.90317 7.19377 3.2551 4.93923 5.17534C2.68468 7.09558 2.36727 10.3061 
            4.13778 12.5772C5.60984 14.4654 10.0648 18.4479 11.5249 19.7369C11.6882 19.8811 11.7699 19.9532 11.8652 19.9815C11.9483 20.0062 12.0393 
            20.0062 12.1225 19.9815C12.2178 19.9532 12.2994 19.8811 12.4628 19.7369C13.9229 18.4479 18.3778 14.4654 19.8499 12.5772C21.6204 10.3061 
            21.3417 7.07538 19.0484 5.17534C16.7551 3.2753 13.7994 3.90317 12 6.00019Z" 
            stroke={`${isFavoriteParameter ? 'red' : '#000000'}`}
            strokeWidth="1" strokeLinecap="round" strokeLinejoin="round"/>
          </svg>
        </div>
      </div>
      {/* Тип локации */}
      <div className="absolute top-0 ml-2 mt-2 h-9 text-black rounded-4xl bg-white flex items-center justify-center p-3 drop-shadow-lg">
        <span>{location.type}</span>
      </div>
    </div> 
  );
}