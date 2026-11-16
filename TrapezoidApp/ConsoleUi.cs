using System;
using System.Collections;
using TrapezoidLib;

namespace TrapezoidApp;

public class ConsoleUi
{
    public void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== Лабораторна робота 3.2, варіант 6: Трапеція ===");
        Console.WriteLine("1 - Завдання 2: масив, ArrayList, List<T>");
        Console.WriteLine("2 - Завдання 3: узагальнене бінарне дерево");
        Console.WriteLine("3 - Завдання 4: порівняння, сортування, ітератор (центрований обхід)");
        Console.WriteLine("0 - Вихід");
        Console.Write("Ваш вибір: ");
    }

    public int ReadMenuChoice()
    {
        string? text = Console.ReadLine();
        return int.TryParse(text, out int choice) ? choice : -1;
    }

    public void PrintTitle(string title)
    {
        Console.WriteLine();
        Console.WriteLine("==============================================================");
        Console.WriteLine(title);
        Console.WriteLine("==============================================================");
    }

    public void PrintStep(string text)
    {
        Console.WriteLine();
        Console.WriteLine("-> " + text);
    }

    public void PrintLine(string text)
    {
        Console.WriteLine(text);
    }

    // Друкує багаторядковий текст з відступом (використовується для структури дерева).
    public void PrintBlock(string text)
    {
        string[] lines = text.Split('\n');
        foreach (string line in lines)
        {
            Console.WriteLine("    " + line.TrimEnd('\r'));
        }
    }

    public void PrintItems(string title, IEnumerable items)
    {
        Console.WriteLine("  " + title + ":");
        int number = 1;
        foreach (object? item in items)
        {
            Console.WriteLine("    " + number + ") " + item);
            number++;
        }
    }

    public void WaitForEnter()
    {
        Console.WriteLine();
        Console.Write("Натисніть Enter, щоб продовжити...");
        Console.ReadLine();
    }

    public static string Brief(Trapezoid trapezoid)
    {
        return trapezoid.FillColor + ", S=" + trapezoid.CalculateArea().ToString("F2")
            + ", P=" + trapezoid.CalculatePerimeter().ToString("F2");
    }
}