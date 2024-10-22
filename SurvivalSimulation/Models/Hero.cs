using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SurvivalSimulation.Models
{
    public class Hero : CharacterBase
    {
        private int _targetDistance;
        private int _startHealth;

        public Hero() { }

        public Hero(string name, int health, int damage, int targetDistance) : base(damage, health, name)
        {
            _startHealth = health;
            _targetDistance = targetDistance;
        }

        public int TargetDistance => _targetDistance;

        public int StartHealth => _startHealth;

        public void SetTargetDistance(int targetDistance)
        {
            _targetDistance = targetDistance;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }

        public override void SetHealth(int health)
        {
            _startHealth = health;
            base.SetHealth(health);
        }

    }
}
