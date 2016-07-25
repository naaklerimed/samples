class Weapon
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

end