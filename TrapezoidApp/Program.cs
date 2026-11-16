using System;
using System.Globalization;
using System.Text;

namespace TrapezoidApp;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        ConsoleUi ui = new ConsoleUi();
        CollectionsDemo collectionsDemo = new CollectionsDemo(ui);
        TreeDemo treeDemo = new TreeDemo(ui);

        bool exit = false;
        while (!exit)
        {
            ui.ShowMenu();

            switch (ui.ReadMenuChoice())
            {
                case 1:
                    collectionsDemo.Run();
                    break;
                case 2:
                    treeDemo.RunTask3();
                    ui.WaitForEnter();
                    break;
                case 3:
                    treeDemo.RunTask4();
                    ui.WaitForEnter();
                    break;
                case 0:
                    exit = true;
                    break;
                default:
                    ui.PrintLine("Невірний вибір. Введіть 1, 2, 3 або 0.");
                    break;
            }
        }

        ui.PrintLine("Роботу завершено.");
    }
}