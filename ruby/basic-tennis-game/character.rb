class Character
  attr_reader :strength
  attr_reader :speed
  attr_reader :skill
  attr_reader :luck
  def initialize(strength, speed, skill, luck)
    @strength = strength
    @speed = speed
    @skill = skill
    @luck = luck
  end

  def useWeapon(weapon)

    @strength = @strength + weapon.instance_variable_get("@strength")
    @speed = @speed + weapon.instance_variable_get("@speed")
    @skill = @skill + weapon.instance_variable_get("@skill")
    @luck = @luck + weapon.instance_variable_get("@luck")

  end

  def releaseWeapon(weapon)
    @strength = @strength - weapon.instance_variable_get("@strength")
    @speed = @speed - weapon.instance_variable_get("@speed")
    @skill = @skill - weapon.instance_variable_get("@skill")
    @luck = @luck - weapon.instance_variable_get("@luck")

  end

  def fight(characterTwo)
    characterOnePower = @strength + @speed + @skill + @luck
    characterTwoPower = characterTwo.instance_variable_get("@strength") + characterTwo.instance_variable_get("@speed") + characterTwo.instance_variable_get("@skill") + characterTwo.instance_variable_get("@luck")

    if(characterOnePower > characterTwoPower)
      puts("Final score :" + characterOnePower.to_s + "-" + characterTwoPower.to_s)
      puts("You won the fight!")
    elsif(characterOnePower == characterTwoPower)
      selected = false
      result = 1 + rand(1)
      while(selected == false)
        puts("It's a draw. It's time to flip the coin.. Heads or Tails?")
        selection = gets.chomp
      if(selection == 'Heads' && result == 1)
        puts('You won the fight!')
        selected = true
      elsif(selection == 'Heads' && result == 2)
        puts('You lost the fight!')
        exit
      elsif(selection == 'Tails' && result == 1)
        puts('You lost the fight!')
        exit
      elsif (selection == 'Tails' && result == 2)

        puts('You won the fight')
        selected = true
      else
        puts('Wrong selection!')
      end
    end
    else
      puts("Final score :" + characterOnePower.to_s + "-" + characterTwoPower.to_s)
      puts("You lost the fight!")
      exit
    end
  end
end