using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalSimulation.UI.Windows.Interface
{
    public abstract class WindowBase
    {
        public abstract List<Command> Commands { get; set; }

        protected abstract void Draw();

        protected virtual void InputChanged(string input) { }

        public virtual void Show()
        {
            while (true)
            {
                Draw();

                Console.Write("Input : ".ChangeColor(Colors.Cyan));
                var input = Console.ReadLine() ?? "";

                var matchedCommand = Commands.FirstOrDefault(c => !string.IsNullOrEmpty(input) && c.Key.ToUpper() == input.Trim().ToUpper());

                if (matchedCommand != null)
                {
                    matchedCommand.Execute(input);
                }
                else
                {
                    InputChanged(input);
                }
            }
        }
    }

    public class Command
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public Action<string> Execute { get; set; }

        public Command(string key, string label, Action<string> navigateTo)
        {
            Execute = navigateTo;
            Label = label;
            Key = key;
        }
    }
}
