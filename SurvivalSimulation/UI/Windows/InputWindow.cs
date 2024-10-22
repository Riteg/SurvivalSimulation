using SurvivalSimulation.Core;
using SurvivalSimulation.Models;
using SurvivalSimulation.UI.Windows.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalSimulation.UI.Windows
{
    public class InputWindow : WindowBase
    {
        public static InputWindow Instance;

        public InputWindow()
        {
            Instance = this;
        }

        private readonly string[] _enemyNames = { "Bug", "Lion", "Zombie", "Mutant", "Zombie Dog" };

        private static List<InputState> _inputs = new List<InputState> {

            new("Enter the distance to the sources (in meters)", SetTargetDistance),

            new("Enter the hero's health", SetHeroHealth),

            new("Enter the hero's damage", SetHeroDamage),

            new("Enter the number of enemies", SetEnemyCount),
        };

        private int _inputIndex = 0;

        private static int _enemyCount = 3;
        private int _enemyIndex = 0;

        public override List<Command> Commands { get; set; } = new List<Command>() {
            //new("B","Back",(_)=>MainWindow.Instance.Show()),
            new("Q", "Quit", (_)=> Environment.Exit(0))
        };

        public override void Show()
        {
            if (!SimulationManager.GenerateRandom)
                base.Show();

            var random = new Random();

            SetTargetDistance(random.Next(1000, 7500).ToString());

            SetHeroHealth(random.Next(250, 1500).ToString());

            SetHeroDamage(random.Next(15, 100).ToString());

            _enemyCount = random.Next(1, 5);

            for (int i = 0; i < _enemyCount; i++)
            {
                SimulationManager.tempEnemy.SetName(_enemyNames[random.Next(0, 5)]);

                SimulationManager.tempEnemy.SetHealth(random.Next(100, 500));

                SimulationManager.tempEnemy.SetDamage(random.Next(10, 50));

                SimulationManager.tempEnemy.SetPosition(random.Next(1, SimulationManager.Hero.TargetDistance));

                SimulationManager.InitializeTempEnemy();
            }

            SimulationManager.EnemyList = SimulationManager.EnemyList.OrderBy(x => x.Position).ToList();

            if (SimulationManager.ShowFights)
                SimulationWindow.Instance.Show();
            else
                SimulationManager.FastSimulate();
        }

        protected override void Draw()
        {
            var lines = new Dictionary<int, string>();

            lines.Add(1, "Inputs");

            if (_inputIndex < _inputs.Count)
            {
                lines.Add(10, _inputs[_inputIndex].RequestedInputLabel);
            }

            this.DrawWindow(lines);
        }

        protected override void InputChanged(string input)
        {
            if (_inputIndex >= _inputs.Count)
                return;

            _inputs[_inputIndex].Execute(input);
            _inputIndex++;

            if (_inputIndex != _inputs.Count)
                return;


            _inputs.Clear();
            _inputIndex = 0;

            _inputs.Add(new("Enter the name of the enemy", SetEnemyName));

            _enemyIndex++;

            if (_enemyIndex <= _enemyCount)
                return;

            if (SimulationManager.ShowFights)
                SimulationWindow.Instance.Show();
            else
                SimulationManager.FastSimulate();
        }


        private static void SetTargetDistance(string response)
        {
            if (int.TryParse(response, out int value) && value > 0)
                SimulationManager.Hero.SetTargetDistance(value);
            else
                SimulationManager.Hero.SetTargetDistance(5000);
        }

        private static void SetHeroHealth(string response)
        {
            if (int.TryParse(response, out int value) && value > 0)
                SimulationManager.Hero.SetHealth(value);
            else
                SimulationManager.Hero.SetHealth(1000);
        }

        private static void SetHeroDamage(string response)
        {
            if (int.TryParse(response, out int value) && value > 0)
                SimulationManager.Hero.SetDamage(value);
            else
                SimulationManager.Hero.SetDamage(25);
        }

        private static void SetEnemyCount(string response)
        {
            if (int.TryParse(response, out int value) && value > 0)
                _enemyCount = value;
            else
                _enemyCount = 1;
        }


        private static void SetEnemyName(string response)
        {
            if (string.IsNullOrEmpty(response))
                response = "Enemy";

            SimulationManager.tempEnemy.SetName(response);

            _inputs.Add(new($"Enter the {response}'s health points", SetEnemyHealth));

            _inputs.Add(new($"Enter the {response}'s hit points", SetEnemyDamage));

            _inputs.Add(new($"Enter the {response}'s position (in meters)", SetEnemyPosition));
        }

        private static void SetEnemyHealth(string response)
        {
            if (int.TryParse(response, out int value) && value > 0)
                SimulationManager.tempEnemy.SetHealth(value);
            else
                SimulationManager.tempEnemy.SetHealth(150);
        }

        private static void SetEnemyDamage(string response)
        {
            if (int.TryParse(response, out int value) && value > 0)
                SimulationManager.tempEnemy.SetDamage(value);
            else
                SimulationManager.tempEnemy.SetDamage(10);
        }

        private static void SetEnemyPosition(string response)
        {
            var random = new Random();

            if (int.TryParse(response, out int value) && value > 0)
                SimulationManager.tempEnemy.SetPosition(value);
            else
                SimulationManager.tempEnemy.SetPosition(random.Next(1, SimulationManager.Hero.TargetDistance));

            SimulationManager.InitializeTempEnemy();
        }

        public class InputState
        {
            public string RequestedInputLabel { get; set; }
            public Action<string> Execute { get; set; }

            public InputState(string requestedInputLabel, Action<string> execute)
            {
                RequestedInputLabel = requestedInputLabel;
                Execute = execute;
            }
        }
    }
}


//inputs!.Add(new("Enter the name of the enemy", response =>
//{
//    SimulationManager.tempEnemy.SetName(response);

//    inputs.Add(new($"Enter the {response}'s health points", response =>
//    {
//        if (int.TryParse(response, out int value))
//            SimulationManager.tempEnemy.SetHealth(value);
//    }));

//    inputs.Add(new($"Enter the {response}'s hit points", response =>
//    {
//        if (int.TryParse(response, out int value))
//            SimulationManager.tempEnemy.SetDamage(value);
//    }));

//    inputs.Add(new($"Enter the position of the {response} (meters)", response =>
//    {
//        if (int.TryParse(response, out int value))
//            SimulationManager.tempEnemy.SetPosition(value);

//        SimulationManager.InitializeTempEnemy();
//    }));
//}));