using SurvivalSimulation.Core;
using SurvivalSimulation.UI;
using SurvivalSimulation.UI.Windows;

namespace SurvivalSimulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InitializeWindows();

            MainWindow.Instance.Show();
        }

        public static void InitializeWindows()
        {
            new MainWindow();
            new InputWindow();
            new SettingsWindow();
            new SimulationWindow();
            new EndWindow();
        }
    }
}
