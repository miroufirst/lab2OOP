using System;
using System.Collections;
using System.Collections.Generic;
using TrapezoidLib;

namespace TrapezoidApp;

public class CollectionsDemo
{
    private const string SearchColor = "Синій";
    private const string RemoveColor = "Зелений";
    private const string NewColor = "Фіолетовий";

    private readonly ConsoleUi _ui;

    public CollectionsDemo(ConsoleUi ui)
    {
        _ui = ui;
    }

    public void Run()
    {
        DemoArray();
        _ui.WaitForEnter();

        DemoArrayList();
        _ui.WaitForEnter();

        DemoGenericList();
        _ui.WaitForEnter();

        PrintComparison();
    }

    private void DemoArray()
    {
        _ui.PrintTitle("1. МАСИВ Trapezoid[] (розмір задається один раз)");

        Trapezoid[] array = DemoData.CreateTrapezoids();
        _ui.PrintItems("Створено масив із 4 трапецій", array);

        _ui.PrintStep("Додавання: розмір масиву змінити не можна, тому створюється новий, більший (Array.Resize)");
        Array.Resize(ref array, array.Length + 1);
        array[array.Length - 1] = DemoData.CreateExtra();
        _ui.PrintItems("Після додавання", array);

        _ui.PrintStep("Оновлення: змінюємо колір заповнення першої трапеції на \"" + NewColor + "\"");
        array[0].FillColor = NewColor;
        _ui.PrintItems("Після оновлення", array);

        _ui.PrintStep("Пошук: Array.Find шукає трапецію з кольором заповнення \"" + SearchColor + "\"");
        Trapezoid? found = Array.Find(array, t => t.FillColor == SearchColor);
        _ui.PrintLine(found is null ? "  Не знайдено" : "  Знайдено: " + found.GetInfo());

        _ui.PrintStep("Видалення: у масиві немає Remove, тому копіюємо всі елементи, крім \"" + RemoveColor + "\"");
        int index = Array.FindIndex(array, t => t.FillColor == RemoveColor);
        if (index >= 0)
        {
            Trapezoid[] smaller = new Trapezoid[array.Length - 1];
            Array.Copy(array, 0, smaller, 0, index);
            Array.Copy(array, index + 1, smaller, index, array.Length - index - 1);
            array = smaller;
        }

        _ui.PrintItems("Після видалення", array);

        _ui.PrintStep("Прохід: цикл for (індекс від 0 до Length - 1)");
        for (int i = 0; i < array.Length; i++)
        {
            _ui.PrintLine("  array[" + i + "]: " + ConsoleUi.Brief(array[i]));
        }
    }

    private void DemoArrayList()
    {
        _ui.PrintTitle("2. ArrayList (звичайна, non-generic колекція: зберігає object)");

        ArrayList list = new ArrayList(DemoData.CreateTrapezoids());
        _ui.PrintItems("Створено ArrayList із 4 трапецій", list);

        _ui.PrintStep("Додавання: метод Add, розмір росте автоматично");
        list.Add(DemoData.CreateExtra());
        _ui.PrintItems("Після додавання", list);

        _ui.PrintStep("Недолік ArrayList: він приймає БУДЬ-ЯКИЙ об'єкт, навіть рядок");
        list.Add("Це рядок, а не трапеція");
        try
        {
            foreach (Trapezoid item in list)
            {
                _ui.PrintLine("  Прочитано: " + ConsoleUi.Brief(item));
            }
        }
        catch (InvalidCastException ex)
        {
            _ui.PrintLine("  ПОМИЛКА ПІД ЧАС РОБОТИ ПРОГРАМИ: " + ex.Message);
        }

        list.RemoveAt(list.Count - 1);
        _ui.PrintLine("  Зайвий рядок прибрано (RemoveAt).");

        _ui.PrintStep("Оновлення: щоб змінити об'єкт, його треба привести до типу Trapezoid");
        Trapezoid first = (Trapezoid)list[0]!;
        first.FillColor = NewColor;
        _ui.PrintItems("Після оновлення", list);

        _ui.PrintStep("Пошук: власний цикл з перевіркою типу (is), кольору \"" + SearchColor + "\"");
        Trapezoid? found = FindInArrayList(list, SearchColor);
        _ui.PrintLine(found is null ? "  Не знайдено" : "  Знайдено: " + found.GetInfo());

        _ui.PrintStep("Видалення: Remove видаляє знайдений об'єкт (\"" + RemoveColor + "\")");
        Trapezoid? toRemove = FindInArrayList(list, RemoveColor);
        if (toRemove is not null)
        {
            list.Remove(toRemove);
        }

        _ui.PrintItems("Після видалення", list);

        _ui.PrintStep("Прохід: foreach із явним приведенням типу кожного елемента");
        foreach (Trapezoid trapezoid in list)
        {
            _ui.PrintLine("  " + ConsoleUi.Brief(trapezoid));
        }
    }

    private static Trapezoid? FindInArrayList(ArrayList list, string fillColor)
    {
        foreach (object? item in list)
        {
            if (item is Trapezoid candidate && candidate.FillColor == fillColor)
            {
                return candidate;
            }
        }

        return null;
    }

    private void DemoGenericList()
    {
        _ui.PrintTitle("3. List<Trapezoid> (узагальнена, generic колекція)");

        List<Trapezoid> list = new List<Trapezoid>(DemoData.CreateTrapezoids());
        _ui.PrintItems("Створено List<Trapezoid> із 4 трапецій", list);

        _ui.PrintStep("Додавання: метод Add (рядок додати не вийде - компілятор не дозволить)");
        list.Add(DemoData.CreateExtra());

        _ui.PrintItems("Після додавання", list);

        _ui.PrintStep("Оновлення: звертаємось за індексом, приведення типу не потрібне");
        list[0].FillColor = NewColor;
        _ui.PrintItems("Після оновлення", list);

        _ui.PrintStep("Пошук: метод Find з умовою, колір \"" + SearchColor + "\"");
        Trapezoid? found = list.Find(t => t.FillColor == SearchColor);
        _ui.PrintLine(found is null ? "  Не знайдено" : "  Знайдено: " + found.GetInfo());

        _ui.PrintStep("Видалення: FindIndex знаходить номер, RemoveAt видаляє елемент (\"" + RemoveColor + "\")");
        int index = list.FindIndex(t => t.FillColor == RemoveColor);
        if (index >= 0)
        {
            list.RemoveAt(index);
        }

        _ui.PrintItems("Після видалення", list);

        _ui.PrintStep("Прохід: foreach, тип елемента відомий заздалегідь");
        foreach (Trapezoid trapezoid in list)
        {
            _ui.PrintLine("  " + ConsoleUi.Brief(trapezoid));
        }
    }

    private void PrintComparison()
    {
        _ui.PrintTitle("ПОРІВНЯННЯ: масив, ArrayList, List<T>");
        PrintRow("Критерій", "Масив", "ArrayList", "List<T>");
        PrintRow("Розмір", "фіксований", "росте сам", "росте сам");
        PrintRow("Тип елементів", "Trapezoid", "object (будь-який)", "Trapezoid");
        PrintRow("Перевірка типів", "при компіляції", "лише під час роботи", "при компіляції");
        PrintRow("Читання елемента", "array[i]", "(Trapezoid)list[i]", "list[i]");
        PrintRow("Додавання", "новий масив", "Add", "Add");
        PrintRow("Видалення", "новий масив", "Remove, RemoveAt", "Remove, RemoveAt");
        PrintRow("Пошук", "Array.Find", "власний цикл", "Find, FindIndex");
        PrintRow("Упаковка (boxing)", "немає", "є (для int тощо)", "немає");

        _ui.PrintLine("");
        _ui.PrintLine("Висновок: масив найпростіший, але розмір незмінний. ArrayList гнучкий, але небезпечний:");
        _ui.PrintLine("помилку типу видно лише під час роботи програми. List<T> гнучкий і безпечний:");
        _ui.PrintLine("компілятор перевіряє типи, приведення і упаковка не потрібні. Зазвичай обирають List<T>.");
    }

    private void PrintRow(string criterion, string array, string arrayList, string genericList)
    {
        _ui.PrintLine($"  {criterion,-20}{array,-17}{arrayList,-23}{genericList}");
    }
}