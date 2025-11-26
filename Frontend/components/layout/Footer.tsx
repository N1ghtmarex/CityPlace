export default function Footer() {
    return(
        <footer className="bg-gray-800 text-white py-12">
        <div className="container mx-auto px-4">
          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            <div>
              <h3 className="text-xl font-bold mb-4">CityPlaces</h3>
              <p className="text-gray-400">
                Откройте для себя лучшие места для посещения в вашем регионе
              </p>
            </div>
            <div>
              <h4 className="font-semibold mb-4">Навигация</h4>
              <ul className="space-y-2 text-gray-400">
                <li><a href="#" className="hover:text-white transition-colors">Главная</a></li>
                <li><a href="#" className="hover:text-white transition-colors">О проекте</a></li>
              </ul>
            </div>
            <div>
              <h4 className="font-semibold mb-4">Контакты</h4>
              <ul className="space-y-2 text-gray-400">
                <li>Email: info@cityplaces.ru</li>
                <li>Телефон: +7 (999) 123-45-67</li>
              </ul>
            </div>
          </div>
        </div>
      </footer>
    )
}