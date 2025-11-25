import { Location } from '@/types/location';

interface LocationCardProps {
  location: Location;
}

export default function LocationCard({ location }: LocationCardProps) {
  return (
    <div className="bg-white rounded-xl shadow-lg overflow-hidden hover:shadow-xl transition-shadow duration-300">
      <div className="relative">
        <img
          src={`${process.env.NEXT_PUBLIC_API_URL}/${location.pictures.filter(x => x.isAvatar == true)[0]?.path}`}
          className="w-full h-48 object-cover"
        />
        <div className="absolute top-4 right-4 bg-blue-600 text-white px-3 py-1 rounded-full text-sm font-semibold">
          {location.type}
        </div>
        <div className="absolute top-4 left-4 bg-white bg-opacity-90 px-2 py-1 rounded flex items-center">
          <span className="text-yellow-500 mr-1">★</span>
          <span className="text-gray-400 font-semibold">5.00</span>
        </div>
      </div>
      
      <div className="p-6">
        <h3 className="text-xl font-bold text-gray-800 mb-2">{location.name}</h3>
        <p className="text-gray-600 mb-4 line-clamp-2">{location.description}</p>
        
        <div className="mb-4">
          <div className="flex items-center text-gray-500 mb-2">
            <svg className="w-4 h-4 mr-2" fill="currentColor" viewBox="0 0 20 20">
              <path fillRule="evenodd" d="M5.05 4.05a7 7 0 119.9 9.9L10 18.9l-4.95-4.95a7 7 0 010-9.9zM10 11a2 2 0 100-4 2 2 0 000 4z" clipRule="evenodd" />
            </svg>
            <span className="text-sm">{location.address.planningStructure}</span>
          </div>
        </div>

        <div className="flex justify-between items-center">
          <button className="bg-blue-600 text-white px-6 py-2 rounded-lg hover:bg-blue-700 transition-colors font-semibold">
            <a href={`/location/${location.id}`}>Подробнее</a>
          </button>
          <button className="text-gray-400 hover:text-red-500 transition-colors">
            <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" />
            </svg>
          </button>
        </div>
      </div>
    </div>
  );
}