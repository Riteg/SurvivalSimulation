using SurvivalSimulation.UI.Windows.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalSimulation.UI
{

    public static class ConsoleHelper
    {
        private const int WINDOW_WIDTH = 100;
        private const int WINDOW_HEIGHT = 20;
        private const char WINDOW_BORDER_CHAR = '#';
        private const string WINDOW_BORDER_STRING = "\x1b[92m#\x1b[39m";

        private static readonly Dictionary<Colors, string> COLORS = new Dictionary<Colors, string>() {
        { Colors.Normal,"\x1b[39m"},
        {Colors.Red , "\x1b[91m"},
        {Colors.Green , "\x1b[92m"},
        {Colors.Yellow, "\x1b[93m"},
        {Colors.Blue , "\x1b[94m"},
        {Colors.Magenta, "\x1b[95m"},
        {Colors.Cyan , "\x1b[96m"},
        {Colors.Grey , "\x1b[97m"}
        };

        public static void DrawWindow(this WindowBase window, Dictionary<int, string> lines)
        {
            Console.Clear();

            Console.WriteLine(new string(WINDOW_BORDER_CHAR, WINDOW_WIDTH + 2).ChangeColor(Colors.Green));

            lines.Add(20, GetCommandsLine(window));

            for (int y = 0; y < WINDOW_HEIGHT + 1; y++)
            {
                var lineBuilder = new StringBuilder();

                lines.TryGetValue(y, out string? lineText);

                DrawLine(lineText, y == WINDOW_HEIGHT ? Colors.Red : Colors.Normal);
            }

            Console.WriteLine(new string(WINDOW_BORDER_CHAR, WINDOW_WIDTH + 2).ChangeColor(Colors.Green));
        }

        public static string ChangeColor(this string text, Colors color)
        {
            COLORS.TryGetValue(color, out var textColor);
            COLORS.TryGetValue(Colors.Normal, out var textNormal);

            text = textColor + text + textNormal;

            return text;
        }

        private static void DrawLine()
        {
            Console.WriteLine(WINDOW_BORDER_STRING + new string(' ', WINDOW_WIDTH) + WINDOW_BORDER_STRING);
        }

        private static void DrawLine(string? text, Colors color = Colors.Magenta)
        {
            if (text is null)
            {
                DrawLine();
                return;
            }

            int leftPadding = (WINDOW_WIDTH - text.Length) / 2;

            string formattedMessage = new string(' ', leftPadding) + text;

            Console.WriteLine(WINDOW_BORDER_STRING + formattedMessage.ChangeColor(color) + new string(' ', WINDOW_WIDTH - formattedMessage.Length) + WINDOW_BORDER_STRING);
        }

        private static string GetCommandsLine(WindowBase window)
        {
            List<string> commandLabels = new();

            foreach (var command in window.Commands)
            {
                commandLabels.Add($"{command.Key}-{command.Label}");
            }

            return String.Join(" | ", commandLabels);
        }
    }

    public enum Colors
    {
        Normal,
        Red,
        Green,
        Yellow,
        Blue,
        Magenta,
        Cyan,
        Grey,
    }
}
