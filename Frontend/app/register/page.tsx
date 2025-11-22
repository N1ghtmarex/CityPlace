'use client';

import { useState } from 'react';
import Link from 'next/link';
import axios from 'axios';

export default function RegisterPage() {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
  });
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, type, checked } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (formData.password != formData.confirmPassword) {
        alert("Пароли не совпадают!");
    }
    else {
        
        setIsLoading(true);

        let data = JSON.stringify({
          username: formData.username,
          email: formData.email,
          firstName: formData.firstName,
          lastName: formData.lastName,
          password: formData.password
        });

        let config = {
          method: 'post',
          maxBodyLength: Infinity,
          url: `${process.env.NEXT_PUBLIC_API_URL}/api/users/registration`,
          headers: { 
            'Content-Type': 'application/json'
          },
          data : data,
        };

        await axios.post(`${process.env.NEXT_PUBLIC_API_URL}/api/users/registration`, {
          username: formData.username,
          email: formData.email,
          firstName: formData.firstName,
          lastName: formData.lastName,
          password: formData.password
        })
        .then((response) => {
          alert("Успешно")
        })
        .catch((error) => {
          alert(error.response.data.Message ?? "Неизвестная ошибка");
        });

        setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 py-8">
      <div className="container mx-auto px-4">

        <header className="text-center mb-8">
          <Link href="/" className="inline-flex items-center space-x-2 text-2xl font-bold text-gray-800">
            <div className="w-8 h-8 bg-blue-600 rounded-lg flex items-center justify-center">
              <span className="text-white font-bold text-sm">CP</span>
            </div>
            <span>CityPlaces</span>
          </Link>
          <p className="text-gray-600 mt-2">Присоединяйтесь к нашему сообществу</p>
        </header>

        <div className="max-w-md mx-auto">
          {/* Основная карточка */}
          <div className="bg-white rounded-2xl shadow-xl overflow-hidden">
            <div className="p-8">
              <div className="text-center mb-8">
                <h1 className="text-3xl font-bold text-gray-800 mb-2">Создать аккаунт</h1>
                <p className="text-gray-600">Заполните информацию для регистрации</p>
              </div>

              <form onSubmit={handleSubmit} className="space-y-6">

                <div className="grid grid-cols-2 gap-4">
                  <div>
                    <label htmlFor="firstName" className="block text-sm font-medium text-gray-700 mb-2">
                      Имя
                    </label>
                    <div className="relative">
                      <input
                        id="firstName"
                        name="firstName"
                        type="text"
                        required
                        value={formData.firstName}
                        onChange={handleChange}
                        className="w-full pl-5 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors text-gray-700"
                        placeholder="Ваше имя"
                      />
                    </div>
                  </div>
                  <div>
                    <label htmlFor="lastName" className="block text-sm font-medium text-gray-700 mb-2">
                      Фамилия
                    </label>
                    <div className="relative">
                      <input
                        id="lastName"
                        name="lastName"
                        type="text"
                        required
                        value={formData.lastName}
                        onChange={handleChange}
                        className="w-full pl-5 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors text-gray-700"
                        placeholder="Ваша фамилия"
                      />
                    </div>
                  </div>
                </div>

                <div>
                    <label htmlFor="username" className="block text-sm font-medium text-gray-700 mb-2">
                      Имя пользователя
                    </label>
                    <div className="relative">
                      <input
                        id="username"
                        name="username"
                        type="text"
                        required
                        value={formData.username}
                        onChange={handleChange}
                        className="w-full pl-5 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors text-gray-700"
                        placeholder="Username"
                      />
                    </div>
                  </div>

                <div>
                  <label htmlFor="email" className="block text-sm font-medium text-gray-700 mb-2">
                    Email адрес
                  </label>
                  <div className="relative">
                    <input
                      id="email"
                      name="email"
                      type="email"
                      required
                      value={formData.email}
                      onChange={handleChange}
                      className="w-full pl-5 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors text-gray-700"
                      placeholder="your@email.com"
                    />
                  </div>
                </div>

                <div>
                  <label htmlFor="password" className="block text-sm font-medium text-gray-700 mb-2">
                    Пароль
                  </label>
                  <div className="relative">
                    <input
                      id="password"
                      name="password"
                      type={showPassword ? 'text' : 'password'}
                      required
                      value={formData.password}
                      onChange={handleChange}
                      className="w-full pl-5 pr-12 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors text-gray-700"
                      placeholder="Пароль"
                      minLength={8}
                    />
                    <button
                      type="button"
                      onClick={() => setShowPassword(!showPassword)}
                      className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600 transition-colors"
                    >
                    </button>
                  </div>
                </div>

                <div>
                  <label htmlFor="confirmPassword" className="block text-sm font-medium text-gray-700 mb-2">
                    Подтвердите пароль
                  </label>
                  <div className="relative">
                    <input
                      id="confirmPassword"
                      name="confirmPassword"
                      type={showConfirmPassword ? 'text' : 'password'}
                      required
                      value={formData.confirmPassword}
                      onChange={handleChange}
                      className="w-full pl-5 pr-12 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors text-gray-700"
                      placeholder="Повторите пароль"
                    />
                    <button
                      type="button"
                      onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                      className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600 transition-colors"
                    >
                    </button>
                  </div>
                </div>

                {/* Кнопка регистрации */}
                <button
                  disabled={isLoading}
                  className="w-full bg-blue-600 text-white py-3 px-6 rounded-lg font-semibold hover:bg-blue-700 focus:ring-4 focus:ring-blue-200 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                >
                  {isLoading ? (
                    <div className="flex items-center justify-center space-x-2">
                      <div className="w-5 h-5 border-2 border-white border-t-transparent rounded-full animate-spin" />
                      <span>Регистрация...</span>
                    </div>
                  ) : (
                    'Создать аккаунт'
                  )}
                </button>
              </form>
            </div>

            {/* Футер формы */}
            <div className="bg-gray-50 px-8 py-6 border-t border-gray-200">
              <div className="text-center">
                <p className="text-gray-600">
                  Уже есть аккаунт?{' '}
                  <Link href="/login" className="text-blue-600 font-semibold hover:underline">
                    Войти
                  </Link>
                </p>
              </div>
            </div>
          </div>

          {/* Дополнительная информация */}
          <div className="mt-8 text-center">
            <p className="text-sm text-gray-600">
              Регистрируясь, вы получаете доступ к:
            </p>
            <div className="flex justify-center space-x-6 mt-4 text-sm text-gray-600">
              <span className="flex items-center space-x-1">
                <div className="w-2 h-2 bg-green-500 rounded-full"></div>
                <span>Избранным локациям</span>
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}