using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Gear.WeaponSerializer
{
    internal class WeaponDataSerializer
    {
        public List<WeaponData> StartingWeapon {  get; set; }
        public List<WeaponData> Swords { get; set; }
        public List<WeaponData> Axes { get; set; }
        public List<WeaponData> Spears { get; set; }
    }

    internal class WeaponData
    {
        public string Name { get; set; }
        public string Rarity { get; set; }
        public int Damage { get; set; }
        public int Accessibility { get; set; }
        public int AgilityBoost { get; set; }
        public int Durability { get; set; }
        public int TwoHanded { get; set; }
    }
}
