'use client';

import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';
import { 
  MapPin, 
  Image, 
  Tag, 
  Navigation, 
  Building, 
  Home,
  ArrowLeft,
  Plus,
  X
} from 'lucide-react';
import axios from 'axios';
import { signIn, useSession } from 'next-auth/react';
import { CreateLocationRequset } from '@/src/models/createLocationRequestModel';
import { createLocation } from '@/app/services/location.service';

interface LocationFormData {
  title: string;
  description: string;
  type: string;
  latitude: number;
  longitude: number;
  images: File[];
}

interface LocationType {
  value: number;
  name: string;
  description: string;
}

export default function NewLocationPage() {
  const router = useRouter();
  const [isLoading, setIsLoading] = useState(false);
  const [imagePreviews, setImagePreviews] = useState<string[]>([]);
  const { data: session, status } = useSession();
  const [locationTypes, setLocationTypes] = useState<LocationType[]>([]);
  const [images, setImages] = useState<File[]>([]);
  
  const [formData, setFormData] = useState<CreateLocationRequset>({
    name: '',
    description: '',
    locationType: '',
    latitude: 0,
    longitude: 0
  });

  useEffect(() => {
    if (status === 'unauthenticated') {
      signIn("keycloak");
    }
  }, [status])

  useEffect(() => {
    axios.get(`${process.env.NEXT_PUBLIC_API_URL}/api/locations/types`)
    .then(response => {
        setLocationTypes(response.data);
    })
  }, []);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleImageUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
    const files = e.target.files;
    if (!files) return;

    const newImages = Array.from(files);
    const newPreviews: string[] = [];

    newImages.forEach(file => {
      const reader = new FileReader();
      reader.onload = (e) => {
        if (e.target?.result) {
          newPreviews.push(e.target.result as string);
          if (newPreviews.length === newImages.length) {
            setImagePreviews(prev => [...prev, ...newPreviews]);
          }
        }
      };
      reader.readAsDataURL(file);
    });

    setImages(newImages);
  };

  const removeImage = (index: number) => {
    setImagePreviews(prev => prev.filter((_, i) => i !== index));
    setImages(images.filter((_, i) => i !== index));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);

    try {
      const locationId = (await createLocation(formData)).id;

      images.forEach((file, index) => {
        // В дальнейшем здесь будет загрузка изображений в MinIO
      });
      
    } catch (error) {
      console.error('Error creating location:', error);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-50 py-8">
      <div className="container mx-auto px-4 max-w-4xl">
        <div className="mb-8">
          <button
            onClick={() => router.back()}
            className="inline-flex items-center space-x-2 text-gray-600 hover:text-gray-900 mb-4 transition-colors"
          >
            <ArrowLeft className="w-5 h-5" />
            <span>Назад</span>
          </button>
          <h1 className="text-3xl font-bold text-gray-900">Добавить новую локацию</h1>
          <p className="text-gray-600 mt-2">Заполните информацию о новой локации</p>
        </div>

        <form onSubmit={handleSubmit} className="space-y-8">
          <div className="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
            <h2 className="text-xl font-semibold text-gray-900 mb-6 flex items-center space-x-2">
              <MapPin className="w-5 h-5 text-blue-600" />
              <span>Основная информация</span>
            </h2>
            
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
              <div className="lg:col-span-2">
                <label htmlFor="name" className="block text-sm font-medium text-gray-700 mb-2">
                  Название локации *
                </label>
                <input
                  type="text"
                  id="name"
                  name="name"
                  required
                  value={formData.name}
                  onChange={handleChange}
                  className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors text-gray-700"
                  placeholder="Введите название локации"
                />
              </div>

              <div>
                <label htmlFor="locationType" className="block text-sm font-medium text-gray-700 mb-2">
                  Тип локации *
                </label>
                <div className="relative">
                  <Tag className="absolute left-3 top-1/2 transform -translate-y-1/2 w-5 h-5 text-gray-400" />
                  <select
                    id="locationType"
                    name="locationType"
                    required
                    value={formData.locationType}
                    onChange={handleChange}
                    className="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 appearance-none bg-white transition-colors text-gray-400"
                  >
                    <option value="">Выберите тип</option>
                    {locationTypes.map(type => (
                    <option key={type.value} value={type.value}>
                        {type.description}
                    </option>
                    ))}
                  </select>
                </div>
              </div>

              <div className="lg:col-span-2">
                <label htmlFor="description" className="block text-sm font-medium text-gray-700 mb-2">
                  Описание *
                </label>
                <textarea
                  id="description"
                  name="description"
                  required
                  rows={4}
                  value={formData.description}
                  onChange={handleChange}
                  className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors resize-vertical text-gray-700"
                  placeholder="Опишите локацию, её особенности и чем она примечательна..."
                />
              </div>
            </div>
          </div>

          <div className="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
            <h2 className="text-xl font-semibold text-gray-900 mb-6 flex items-center space-x-2">
              <Navigation className="w-5 h-5 text-blue-600" />
              <span>Адрес</span>
            </h2>
            
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <label htmlFor="latitude" className="block text-sm font-medium text-gray-700 mb-2">
                  Широта
                </label>
                <input
                  type="text"
                  id="latitude"
                  name="latitude"
                  required
                  value={formData.latitude}
                  onChange={handleChange}
                  className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors text-gray-700"
                  placeholder="Широта"
                />
              </div>

              <div>
                <label htmlFor="longitude" className="block text-sm font-medium text-gray-700 mb-2">
                  Долгота
                </label>
                <input
                  type="text"
                  id="longitude"
                  name="longitude"
                  value={formData.longitude}
                  onChange={handleChange}
                  className="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 transition-colors text-gray-700"
                  placeholder="Долгота"
                />
              </div>
            </div>
          </div>

          <div className="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
            <h2 className="text-xl font-semibold text-gray-900 mb-6 flex items-center space-x-2">
              <Image className="w-5 h-5 text-blue-600" />
              <span>Изображения</span>
            </h2>

            <div className="space-y-4">
              <div className="border-2 border-dashed border-gray-300 rounded-lg p-8 text-center hover:border-gray-400 transition-colors">
                <input
                  type="file"
                  id="images"
                  multiple
                  accept="image/*"
                  onChange={handleImageUpload}
                  className="hidden"
                />
                <label htmlFor="images" className="cursor-pointer">
                  <Image className="w-12 h-12 text-gray-400 mx-auto mb-4" />
                  <p className="text-lg font-medium text-gray-900 mb-2">Добавить фотографии</p>
                  <p className="text-gray-500">Перетащите файлы или нажмите для выбора</p>
                  <p className="text-sm text-gray-400 mt-1">PNG, JPG, JPEG до 10MB</p>
                </label>
              </div>

              {imagePreviews.length > 0 && (
                <div>
                  <h3 className="text-sm font-medium text-gray-700 mb-3">Загруженные изображения:</h3>
                  <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
                    {imagePreviews.map((preview, index) => (
                      <div key={index} className="relative group">
                        <img
                          src={preview}
                          alt={`Preview ${index + 1}`}
                          className="w-full h-32 object-cover rounded-lg"
                        />
                        <button
                          type="button"
                          onClick={() => removeImage(index)}
                          className="absolute -top-2 -right-2 bg-red-500 text-white rounded-full p-1 opacity-0 group-hover:opacity-100 transition-opacity"
                        >
                          <X className="w-4 h-4" />
                        </button>
                      </div>
                    ))}
                  </div>
                </div>
              )}
            </div>
          </div>

          <div className="flex flex-col sm:flex-row gap-4 justify-end pt-6">
            <button
              type="button"
              onClick={() => router.back()}
              className="px-6 py-3 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50 transition-colors font-medium"
            >
              Отмена
            </button>
            <button
              type="submit"
              disabled={isLoading}
              className="px-6 py-3 bg-blue-600 text-white rounded-lg hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors font-medium flex items-center space-x-2"
            >
              {isLoading ? (
                <>
                  <div className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin" />
                  <span>Создание...</span>
                </>
              ) : (
                <>
                  <Plus className="w-5 h-5" />
                  <span>Создать локацию</span>
                </>
              )}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}