using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Character
{
    internal class CharacterStats
    {
        private int _health;
        private int _attack;
        private int _stamina;
        private int _agility;
        private int _defence;

        public CharacterStats(int health, int attack, int stamina, int agility, int defence)
        {
            Health = health;
            Attack = attack;
            Stamina = stamina;
            Agility = agility;
            Defence = defence;
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public int Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }

        public int Stamina
        {
            get { return _stamina; }
            set { _stamina = value; }
        }

        public int Agility
        {
            get { return _agility; }
            set { _agility = value; }
        }

        public int Defence
        {
            get { return _defence; }
            set { _defence = value; }
        }
    }
}
