using SurvivalSimulation.Core;
using SurvivalSimulation.UI.Windows.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalSimulation.UI.Windows
{
    public class EndWindow : WindowBase
    {
        public static EndWindow Instance;

        public EndWindow()
        {
            Instance = this;
        }

        public static List<string> Log { get; set; } = new List<string>();

        public override List<Command> Commands { get; set; } = new() { new("Q", "Quit", _ => Environment.Exit(0)) };

        protected override void Draw()
        {
            var lines = new Dictionary<int, string>();

            lines.Add(1, "Simulation Result");

            for (int i = 0; i < Log.Count; i++)
            {
                lines.Add(i + 5, Log[i]);
            }

            this.DrawWindow(lines);
        }
    }
}
