'use client';

import axios from 'axios';
import { useSession, signOut, signIn } from 'next-auth/react';
import Link from 'next/link';
import { useEffect, useState } from 'react';

export default function Header() {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const { data: session, status, update } = useSession()
  const [role, setRole] = useState<string | null>(null);

  useEffect(() => {
    if (status == 'authenticated' && role == null) {
      axios.get(`${process.env.NEXT_PUBLIC_API_URL}/api/users/current`, {
        headers: {
          Authorization: `Bearer ${session.accessToken}`
        }
      })
      .then(response => {
        setRole(response.data.role);
      })
    }
  }, [status, session, role]);
  

  return (
    <header className="bg-white shadow-sm sticky top-0 z-50">
      <div className="container mx-auto px-4 py-4">
        <div className="flex justify-between items-center">

          <Link href="/" className="flex items-center space-x-2">
            <div className="w-10 h-10 bg-blue-600 rounded-lg flex items-center justify-center">
              <span className="text-white font-bold text-lg">CP</span>
            </div>
            <span className="text-2xl font-bold text-gray-800">CityPlaces</span>
          </Link>

          {/* Навигация для десктопа */}
          <nav className="hidden md:flex space-x-8">
            <Link 
              href="/" 
              className="text-gray-600 hover:text-blue-600 transition-colors font-medium"
            >
              Главная
            </Link>
            {
              role == 'Admin' && (
                <span className="text-gray-600 hover:text-blue-600 transition-colors font-medium">
                  Добавить локацию
                </span>
              )
            }
            <Link 
              href="/locations" 
              className="text-gray-600 hover:text-blue-600 transition-colors font-medium"
            >
              Все локации
            </Link>
            <Link 
              href="/about" 
              className="text-gray-600 hover:text-blue-600 transition-colors font-medium"
            >
              О проекте
            </Link>
            
            {
              status == 'unauthenticated' && (
                <>
                  <Link 
                    href="/register" 
                    className="text-gray-600 hover:text-blue-600 transition-colors font-medium"
                  >
                    Зарегистрироваться
                  </Link>
                  <button className="text-gray-600 hover:text-blue-600 transition-colors font-medium" onClick={() => signIn()}>
                    Войти
                  </button>
                </>
              )
            }
            {
              status == 'authenticated' && (
                <>
                <span className="text-gray-800 hover:text-blue-600 transition-colors font-medium">Добро пожаловать, { session.user?.name?.split(" ")[0] }</span>
                <button className="text-gray-600 hover:text-blue-600 transition-colors font-medium" onClick={() => signOut()}>
                  Выйти
                </button>
                </>
                
              )
            }
          </nav>

          {/* Кнопка мобильного меню */}
          <button 
            className="md:hidden p-2"
            onClick={() => setIsMenuOpen(!isMenuOpen)}
          >
            <div className="w-6 h-6 flex flex-col justify-between">
              <span className={`block h-0.5 w-full bg-gray-600 transition-transform ${isMenuOpen ? 'rotate-45 translate-y-2.5' : ''}`}></span>
              <span className={`block h-0.5 w-full bg-gray-600 transition-opacity ${isMenuOpen ? 'opacity-0' : ''}`}></span>
              <span className={`block h-0.5 w-full bg-gray-600 transition-transform ${isMenuOpen ? '-rotate-45 -translate-y-2.5' : ''}`}></span>
            </div>
          </button>
        </div>

        {/* Мобильное меню */}
        {isMenuOpen && (
          <div className="md:hidden mt-4 pb-4 border-t border-gray-200">
            <nav className="flex flex-col space-y-4 mt-4">
              <Link 
                href="/" 
                className="text-gray-600 hover:text-blue-600 transition-colors font-medium py-2"
                onClick={() => setIsMenuOpen(false)}
              >
                Главная
              </Link>
              <Link 
                href="/locations" 
                className="text-gray-600 hover:text-blue-600 transition-colors font-medium py-2"
                onClick={() => setIsMenuOpen(false)}
              >
                Все локации
              </Link>
              <Link 
                href="/categories" 
                className="text-gray-600 hover:text-blue-600 transition-colors font-medium py-2"
                onClick={() => setIsMenuOpen(false)}
              >
                Категории
              </Link>
              <Link 
                href="/about" 
                className="text-gray-600 hover:text-blue-600 transition-colors font-medium py-2"
                onClick={() => setIsMenuOpen(false)}
              >
                О проекте
              </Link>
            </nav>
          </div>
        )}
      </div>
    </header>
  );
}