using SurvivalSimulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalSimulation.Services
{
    public static class CombatService
    {
        public static bool SimulateFight(Hero hero, Enemy enemy)
        {
            int enemyTurnsToDie = enemy.Health / hero.Damage;
            int heroTurnsToDie = hero.Health / enemy.Damage;

            if (enemyTurnsToDie < heroTurnsToDie)
            {
                hero.TakeDamage(enemyTurnsToDie * enemy.Damage);

                enemy.Die();

                return true;
            }

            enemy.TakeDamage(heroTurnsToDie * hero.Damage);

            return false;
        }
    }
}
