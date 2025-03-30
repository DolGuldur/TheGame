using Main.Gear.Weapon;

namespace Main.Gear.Weapon
{
    class Sword : Weapon
    {
        public Sword(string name, string rarity, int damage, int level, int durability, bool twoHanded)
            : base(name, rarity, damage, level, durability, twoHanded)
        {
            ;
        }
    }
}
