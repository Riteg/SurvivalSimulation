using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalSimulation.Models
{
    public class Enemy : CharacterBase
    {
        private int _position;

        public Enemy() { }

        public Enemy(string name, int health, int damage, int position) : base(damage, health, name)
        {
            _position = position;
        }

        public int Position => _position;

        public void SetPosition(int position)
        {
            _position = position;
        }

        public void Die()
        {
            _health = 0;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }
}
