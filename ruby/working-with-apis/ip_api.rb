require 'open-uri'
require 'json'
require 'net/http'
require 'openssl'
require_relative 'api_interface'
class IpApi <APIInterface
  def initialize()
    @ip
  end

  def call()
    begin
    @ip = Net::HTTP.get(URI('http://api.ipify.org?format=json'))

    @MyIP = JSON.parse(@ip)
    @MyIP= @MyIP["ip"]
    puts @MyIP
    return @MyIP
  end
rescue
  puts ("Something went wrong")
  end
end