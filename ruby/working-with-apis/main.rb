require_relative 'ip_api'
require_relative 'location_api'
require_relative 'weather_api'
require_relative 'api_interface'
require 'open-uri'
require 'net/https'
require 'json'
require 'openssl'

  class Main
    obj = WeatherApi.new(LocationApi.new(IpApi.new))
    puts obj.call
  end