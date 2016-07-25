require 'open-uri'
require 'json'
require 'net/http'
require 'openssl'
require_relative 'api_interface'
class LocationApi <APIInterface

  def initialize(ip)
    @ip = ip
  end

  def call()
    begin
    @location = Net::HTTP.get(URI("http://ip-api.com/json/" + @ip.call ))
    @location = JSON.parse(@location)
    @location = @location['city']
    puts @location
    return @location
    end
  rescue
    puts ("Something went wrong")
  end
end