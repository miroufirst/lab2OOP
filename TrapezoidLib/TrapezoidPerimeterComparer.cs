using System.Collections.Generic;

namespace TrapezoidLib;

public class TrapezoidPerimeterComparer : IComparer<Trapezoid>
{
    public int Compare(Trapezoid? x, Trapezoid? y)
    {
        if (ReferenceEquals(x, y))
        {
            return 0;
        }

        if (x is null)
        {
            return -1;
        }

        if (y is null)
        {
            return 1;
        }

        return x.CalculatePerimeter().CompareTo(y.CalculatePerimeter());
    }
}