using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalSimulation.Models
{
    public class CharacterBase
    {
        private string _name;
        private int _damage;
        protected int _health;

        public CharacterBase() { }

        public CharacterBase(int damage, int health, string name)
        {
            _name = name;
            _health = health;
            _damage = damage;
        }

        public string Name => _name;
        public int Health => _health;
        public int Damage => _damage;
        public bool IsAlive => Health > 0;

        public virtual void SetName(string name)
        {
            _name = name;
        }

        public virtual void SetDamage(int damage)
        {
            _damage = damage;
        }

        public virtual void SetHealth(int health)
        {
            _health = health;
        }
    }
}
