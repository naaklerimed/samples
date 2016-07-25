require_relative 'Forehand'
require_relative 'Backhand'
require_relative 'Federer'
require_relative 'Nadal'
require_relative 'Dropshot'
require_relative 'Murray'
require_relative 'Djokovic'
require_relative 'Volley'
require_relative 'Weapon'
require_relative 'Character'

federer = Federer.new(70,60,95,70+rand(30))
djokovic = Djokovic.new(80,80,80,70+rand(30))
nadal = Nadal.new(85,90,75,70+rand(30))
murray = Murray.new(80,75,70,70+rand(30))
forehand = Forehand.new(5,-5,10,15)
backhand = Backhand.new(10,5,20,-5)
volley = Volley.new(-5,10,10,10)
dropshot = Dropshot.new(10,10,15,-20)

opponentArray = [ federer, djokovic, nadal, murray ]
weaponArray = [ forehand, backhand, volley, dropshot ]
characterSelected = false
weaponSelected = false
opponentSelected = false

while(opponentArray.count != 0)

  while(characterSelected == false)
  puts('Select your Character')
  puts('1 for Federer ( Excellent skills, low speed and strength )')
  puts('2 for Djokovic ( All-Around player )')
  puts('3 for Nadal ( Incredible speed and strength, but not skillful )')
  puts('4 for Murray ( All-Around player but lacks skill ')
  cSelection = gets.to_i

  if(cSelection == 1)
    cCharacter = federer
    characterSelected = true
  elsif(cSelection == 2)
    cCharacter = djokovic
    characterSelected= true
  elsif(cSelection == 3)
    cCharacter = nadal
    characterSelected= true
  elsif(cSelection == 4)
    cCharacter = murray
    characterSelected = true
  else
    puts('Wrong selection!')
  end
    end
  while(weaponSelected == false)
  puts('Select your Weapon')
  puts('1 for Forehand ( Improves luck )')
  puts('2 for Backhand ( Improves skill, decreases luck ) ')
  puts('3 for Volley ( Decreases strength but improves others )')
  puts('4 for Dropshot( Decreases luck by a lot, but improves the others more than other weapons ) ')
  wSelection = gets.to_i
  if(wSelection == 1)
    wWeapon = forehand
    weaponSelected=true
  elsif(wSelection == 2)
    wWeapon = backhand
    weaponSelected = true
  elsif(wSelection == 3)
    wWeapon = volley
    weaponSelected= true
  elsif(wSelection == 4)
    wWeapon = dropshot
    weaponSelected = true
  else
    puts('Wrong Selection!')
  end
end
  while(opponentSelected == false)
  puts('Select opponent Character')
  puts('1 for Federer ( Excellent skills, low speed and strength )')
  puts('2 for Djokovic ( All-Around player )')
  puts('3 for Nadal ( Incredible speed and strength, but not skillful )')
  puts('4 for Murray ( All-Around player but lacks skill ')
  coSelection = gets.to_i
  if(coSelection == 1)
    coCharacter = federer
    opponentArray = opponentArray - [federer]
    opponentSelected = true
  elsif(coSelection == 2)
    coCharacter = djokovic
    opponentArray = opponentArray - [djokovic]
    opponentSelected = true
  elsif(coSelection == 3)
    coCharacter = nadal
    opponentArray = opponentArray - [nadal]
    opponentSelected = true
  elsif(coSelection == 4)
    coCharacter = murray
    opponentArray = opponentArray - [murray]
    opponentSelected = true
  end
end
woSelectionArray = [1,2,3,4]
woSelection = woSelectionArray.sample
 if(woSelection == 1)
    woWeapon = forehand
  elsif(woSelection == 2)
    woWeapon = backhand
  elsif(woSelection == 3)
    woWeapon = volley
  elsif(woSelection == 4)
    woWeapon = dropshot

 end

  cCharacter.useWeapon(wWeapon)
  coCharacter.useWeapon(woWeapon)
  cCharacter.fight(coCharacter)
  cCharacter.releaseWeapon(wWeapon)
  coCharacter.releaseWeapon(woWeapon)
  opponentSelected = false
  weaponSelected = false

end

puts("You won the game!")