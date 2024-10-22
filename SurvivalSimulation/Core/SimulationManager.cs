using SurvivalSimulation.Models;
using SurvivalSimulation.Services;
using SurvivalSimulation.UI;
using SurvivalSimulation.UI.Windows;
using SurvivalSimulation.UI.Windows.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalSimulation.Core
{
    public static class SimulationManager
    {
        private static bool _showFights = true;
        private static bool _generateRandom = false;

        public static bool ShowFights => _showFights;
        public static bool GenerateRandom => _generateRandom;

        public static Hero Hero { get; set; } = new("Hero", 1000, 25, 5000);

        public static List<Enemy> EnemyList { get; set; } = new List<Enemy>();

        public static Enemy tempEnemy = new("Enemy", 150, 12, 1500);

        private static int _enemyIndex = 0;

        private static bool _isFirstRender = true;

        public static bool SimulateOneFrame(out Hero hero, out Enemy enemy)
        {
            hero = Hero;
            enemy = new();

            if (_enemyIndex >= EnemyList.Count)
            {
                EndWindow.Log.Add($"Hero survived and reached the resources.");

                EndWindow.Instance.Show();

                return false;
            }

            enemy = EnemyList[_enemyIndex];

            if (_isFirstRender)
            {
                EndWindow.Log.Add($"Hero encountered a {enemy.Name} at {enemy.Position} meters.");

                _isFirstRender = false;
                return true;
            }

            Hero.TakeDamage(enemy.Damage);

            enemy.TakeDamage(Hero.Damage);


            if (Hero.IsAlive && enemy.IsAlive)
            {
                hero = Hero;
                EnemyList[_enemyIndex] = enemy;

                return true;
            }
            else if (Hero.IsAlive && !enemy.IsAlive)
            {
                hero = Hero;
                EnemyList[_enemyIndex] = enemy;

                _enemyIndex++;

                EndWindow.Log.Add($"Hero killed the {enemy.Name} with {Hero.Health} HP.");

                _isFirstRender = true;

                return true;
            }
            else if (!Hero.IsAlive && enemy.IsAlive)
            {
                EndWindow.Log.Add($"{enemy.Name} killed the Hero with {enemy.Health} HP.");

                EndWindow.Log.Add($"Hero is dead! He was last seen at {enemy.Position} meters.");

                EndWindow.Instance.Show();
            }

            return false;
        }

        public static void ToggleShowFights()
        {
            _showFights = !_showFights;
        }

        public static void ToggleGenerateRandom()
        {
            _generateRandom = !_generateRandom;
        }

        public static void InitializeTempEnemy()
        {
            EnemyList.Add(tempEnemy);

            tempEnemy = new();
        }

        public static void FastSimulate()
        {
            EnemyList = EnemyList.OrderBy(x => x.Position).ToList();

            EndWindow.Log.Add($"Hero started the journey with {Hero.StartHealth} HP.");

            int lastSeenPosition = 0;

            foreach (Enemy enemy in EnemyList)
            {
                if (enemy.Position >= Hero.TargetDistance)
                    continue;

                lastSeenPosition = enemy.Position;

                EndWindow.Log.Add($"Hero encountered a {enemy.Name} at {enemy.Position} meters.");

                if (CombatService.SimulateFight(Hero, enemy))
                {
                    // hero killed the enemy

                    EndWindow.Log.Add($"Hero killed the {enemy.Name} with {Hero.Health} HP.");
                }
                else
                {
                    // hero died to the enemy

                    EndWindow.Log.Add($"{enemy.Name} killed the Hero with {enemy.Health} HP.");

                    break;
                }
            }

            if (Hero.IsAlive)
                EndWindow.Log.Add($"Hero survived and reached the resources.");
            else
                EndWindow.Log.Add($"Hero is dead! He was last seen at {lastSeenPosition} meters.");

            EndWindow.Instance.Show();
        }
    }
}
