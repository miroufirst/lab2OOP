using System;
using System.Collections.Generic;
using TrapezoidLib;

namespace TrapezoidApp;

public class TreeDemo
{
    private readonly ConsoleUi _ui;

    public TreeDemo(ConsoleUi ui)
    {
        _ui = ui;
    }

    public void RunTask3()
    {
        _ui.PrintTitle("ЗАВДАННЯ 3. Узагальнене бінарне дерево BinaryTree<T>");

        _ui.PrintStep("Дерево з 4 трапецій (додаємо: Червона, Синя, Зелена, Жовта)");
        BinaryTree<Trapezoid> tree = new BinaryTree<Trapezoid>();
        FillTree(tree);
        _ui.PrintLine("  Елементів у дереві: " + tree.Count);
        _ui.PrintLine("  Структура (менше - ліворуч, більше - праворуч; порівняння за площею):");
        _ui.PrintBlock(tree.GetStructure(ConsoleUi.Brief));

        _ui.PrintStep("Те саме дерево, але для рядків: BinaryTree<string> (тип T - будь-який клас)");
        BinaryTree<string> words = new BinaryTree<string>();
        words.Add("banana");
        words.Add("apple");
        words.Add("cherry");
        words.Add("date");
        _ui.PrintBlock(words.GetStructure(word => word));

        _ui.PrintStep("Структури (int, double...) у дереві заборонені: обмеження where T : class");
    }

    public void RunTask4()
    {
        _ui.PrintTitle("ЗАВДАННЯ 4. Порівняння (IComparable<T>, IComparer<T>) та власний ітератор");

        _ui.PrintStep("1) Array.Sort(array): сортує через IComparable<Trapezoid> (за ПЛОЩЕЮ)");
        Trapezoid[] array = DemoData.CreateTrapezoids();
        _ui.PrintItems("До сортування", array);
        Array.Sort(array);
        _ui.PrintItems("Після Array.Sort(array)", array);

        _ui.PrintStep("2) Array.Sort(array, comparer): сортує через IComparer<Trapezoid> (за ПЕРИМЕТРОМ)");
        Array.Sort(array, new TrapezoidPerimeterComparer());
        _ui.PrintItems("Після Array.Sort(array, comparer)", array);

        _ui.PrintStep("3) List<T>.Sort(comparer): те саме для списку (за ПЕРИМЕТРОМ)");
        List<Trapezoid> list = new List<Trapezoid>(DemoData.CreateTrapezoids());
        list.Sort(new TrapezoidPerimeterComparer());
        _ui.PrintItems("Після list.Sort(comparer)", list);

        _ui.PrintStep("4) Дерево, порівняння за ПЛОЩЕЮ (IComparable<T>, порівнювач не вказано)");
        BinaryTree<Trapezoid> byArea = new BinaryTree<Trapezoid>();
        FillTree(byArea);
        _ui.PrintBlock(byArea.GetStructure(ConsoleUi.Brief));
        _ui.PrintLine("  Центрований обхід (foreach із власним ітератором):");
        foreach (Trapezoid trapezoid in byArea)
        {
            _ui.PrintLine("    " + ConsoleUi.Brief(trapezoid));
        }

        _ui.PrintStep("5) Дерево, порівняння за ПЕРИМЕТРОМ (IComparer<T> передано в конструктор)");
        BinaryTree<Trapezoid> byPerimeter = new BinaryTree<Trapezoid>(new TrapezoidPerimeterComparer());
        FillTree(byPerimeter);
        _ui.PrintBlock(byPerimeter.GetStructure(ConsoleUi.Brief));
        _ui.PrintLine("  Центрований обхід:");
        foreach (Trapezoid trapezoid in byPerimeter)
        {
            _ui.PrintLine("    " + ConsoleUi.Brief(trapezoid));
        }

        _ui.PrintStep("6) Ітератор вручну: MoveNext() і Current (саме це робить foreach)");
        using IEnumerator<Trapezoid> enumerator = byArea.GetEnumerator();
        int number = 1;
        while (enumerator.MoveNext())
        {
            _ui.PrintLine("    крок " + number + ": " + enumerator.Current.GetInfo());
            number++;
        }

        _ui.PrintLine("    MoveNext() повернув false - елементи закінчились.");
    }

    private static void FillTree(BinaryTree<Trapezoid> tree)
    {
        foreach (Trapezoid trapezoid in DemoData.CreateTrapezoids())
        {
            tree.Add(trapezoid);
        }
    }
}