using SurvivalSimulation.UI.Windows.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalSimulation.UI.Windows
{
    public class MainWindow : WindowBase
    {
        public static MainWindow Instance;

        public MainWindow()
        {
            Instance = this;
        }

        public override List<Command> Commands { get; set; } = new List<Command>() {
            new("S", "Start", (_)=> InputWindow.Instance.Show()),
            new("E", "Settings", (_)=> SettingsWindow.Instance.Show()),
            new("Q", "Quit", (_)=> Environment.Exit(0)) };

        protected override void Draw()
        {
            var lines = new Dictionary<int, string>();

            lines.Add(9, "Welcome To Survival Simulation");

            this.DrawWindow(lines);
        }
    }
}
