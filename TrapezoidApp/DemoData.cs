using TrapezoidLib;

namespace TrapezoidApp;


public static class DemoData
{
    public static Trapezoid[] CreateTrapezoids()
    {
        return new Trapezoid[]
        {
            new Trapezoid("Червоний", "Чорний", 10, 6, 4),
            new Trapezoid("Синій", "Білий", 8, 4, 3),
            new Trapezoid("Зелений", "Жовтий", 14, 8, 5),
            new Trapezoid("Жовтий", "Синій", 3, 1, 12)
        };
    }

    public static Trapezoid CreateExtra()
    {
        return new Trapezoid("Помаранчевий", "Сірий", 12, 9, 2);
    }
}