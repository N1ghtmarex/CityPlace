"use client"
import { useState, useEffect } from 'react';
import Link from 'next/link';
import LocationCard from '@/components/LocationCard';
import { Location } from '@/types/location';
import { signIn, useSession } from 'next-auth/react';
import { getFavoriteLocations } from '../services/location.service';

export default function FavoritesPage() {
  const [favoriteLocations, setFavoriteLocations] = useState<Location[]>([]);
  const [loading, setLoading] = useState(false);
  const { data: session, status } = useSession();

  useEffect(() => {
    if (status == 'authenticated') {
      setLoading(true);

      const loadFavorites = async () => {
      try {
        const result = await getFavoriteLocations();
        setFavoriteLocations(result.items);
      } catch (error) {
        console.error('Ошибка загрузки избранного:', error);
      } finally {
        setLoading(false);
      }
    };

    loadFavorites();
    }
    else {
    }
  }, [status])

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50 py-8">
        <div className="container mx-auto px-4">
          <div className="text-center">
            <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600 mx-auto"></div>
            <p className="mt-4 text-gray-600">Загрузка избранных локаций...</p>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50 py-8">
      <div className="container mx-auto px-4">
        <div className="mb-8">
          <h1 className="text-3xl font-bold text-gray-900 mb-2">Избранные локации</h1>
          <p className="text-gray-600">
            {favoriteLocations.length > 0 
              ? `У вас ${favoriteLocations.length} избранных локаций`
              : status == 'authenticated' 
                ? 'У вас пока нет избранных локаций' 
                : <><Link href='/register' className='text-blue-600'>Зарегистрируйтесь </Link>или <a className='text-blue-600 cursor-pointer' onClick={() => signIn("keycloak")}>войдите </a>в аккаунт чтобы не потерять понравившиеся места</>
            }
          </p>
        </div>

        {favoriteLocations.length === 0 ? (
          <div className="text-center py-12">
            <div className="max-w-md mx-auto">
              <svg 
                className="w-24 h-24 text-gray-300 mx-auto mb-4" 
                fill="none" 
                stroke="currentColor" 
                viewBox="0 0 24 24"
              >
                <path 
                  strokeLinecap="round" 
                  strokeLinejoin="round" 
                  strokeWidth={1} 
                  d="M4.318 6.318a4.5 4.5 0 000 6.364L12 20.364l7.682-7.682a4.5 4.5 0 00-6.364-6.364L12 7.636l-1.318-1.318a4.5 4.5 0 00-6.364 0z" 
                />
              </svg>
              <h3 className="text-xl font-semibold text-gray-900 mb-2">Нет избранных локаций</h3>
              <p className="text-gray-600 mb-6">Добавляйте локации в избранное, чтобы легко находить их позже</p>
            </div>
          </div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {favoriteLocations.map((location) => (
              <LocationCard key={location.id} location={location} isFavorite={true}/>
            ))}
          </div>
        )}
        <div className="mt-8 text-center">
          <Link 
            href="/"
            className="text-blue-600 hover:text-blue-800 font-semibold inline-flex items-center"
          >
            <svg className="w-4 h-4 mr-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M10 19l-7-7m0 0l7-7m-7 7h18" />
            </svg>
            Вернуться ко всем локациям
          </Link>
        </div>
      </div>
    </div>
  );
}