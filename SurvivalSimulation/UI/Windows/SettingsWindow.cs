using SurvivalSimulation.Core;
using SurvivalSimulation.UI.Windows.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalSimulation.UI.Windows
{
    public class SettingsWindow : WindowBase
    {
        public static SettingsWindow Instance;

        public SettingsWindow()
        {
            Instance = this;
        }

        public override List<Command> Commands { get; set; } = new List<Command>() {
            new("B","Back",(_)=>MainWindow.Instance.Show()),
            new("H","Toggle Show Fights",(_)=>{
                SimulationManager.ToggleShowFights();
            }),
            new("R","Toggle Generate Random",(_)=>{
                SimulationManager.ToggleGenerateRandom();
            }),
            new("Q", "Quit", (_) => Environment.Exit(0))
        };

        protected override void Draw()
        {
            var lines = new Dictionary<int, string>();

            lines.Add(1, "Settings");

            lines.Add(7, $"Show Fights : {SimulationManager.ShowFights}");
            lines.Add(9, $"Generate Random : {SimulationManager.GenerateRandom}");

            this.DrawWindow(lines);
        }
    }
}
