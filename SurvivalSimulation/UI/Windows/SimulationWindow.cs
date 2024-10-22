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
    public class SimulationWindow : WindowBase
    {
        public static SimulationWindow Instance;

        public SimulationWindow()
        {
            Instance = this;
        }

        public override List<Command> Commands { get; set; } = new()
        {
            //new("Q","Quit",(_)=>Environment.Exit(0))
        };

        protected override void Draw()
        {
            SimulationManager.EnemyList = SimulationManager.EnemyList.OrderBy(x => x.Position).ToList();

            EndWindow.Log.Add($"Hero started the journey with {SimulationManager.Hero.StartHealth} HP.");

            while (SimulationManager.SimulateOneFrame(out Hero hero, out Enemy enemy))
            {
                var lines = new Dictionary<int, string>();

                lines.Add(1, "Simulation");

                lines.Add(3, $"Target Position:{hero.TargetDistance} | Current Position:{enemy.Position}");

                lines.Add(8, $"Hero's Health:{hero.Health}");

                lines.Add(12, $"{enemy.Name}'s Health:{enemy.Health}");

                this.DrawWindow(lines);

                Thread.Sleep(1000);
            }
        }
    }
}
