require 'open-uri'
require 'json'
require 'net/http'
require 'openssl'
require_relative 'api_interface'
class WeatherApi < APIInterface
  def initialize(location)
    @location = location
  end
  def call()
    begin
    @locationValue = @location.call
    @weather = Net::HTTP.get(URI('http://api.openweathermap.org/data/2.5/weather?q='+@locationValue+'&units=metric&APPID=7b6f07475972f8618ae99c940f7a6cdc'))
    @weather = JSON.parse(@weather)
    @currentWeather = @weather['main']['temp']
    @currentWeather
      end
    rescue
      puts ("Something went wrong")

  end

end