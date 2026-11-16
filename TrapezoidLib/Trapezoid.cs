using System;
using System.Globalization;

namespace TrapezoidLib;

public class Trapezoid : IComparable<Trapezoid>
{

    private string _fillColor = string.Empty;
    private string _outlineColor = string.Empty;
    private double _baseA;
    private double _baseB;
    private double _height;

    public Trapezoid(string fillColor, string outlineColor, double baseA, double baseB, double height)
    {
        FillColor = fillColor;
        OutlineColor = outlineColor;
        BaseA = baseA;
        BaseB = baseB;
        Height = height;
    }

    public string FillColor
    {
        get => _fillColor;
        set => _fillColor = CheckText(value, "Колір заповнення");
    }

    public string OutlineColor
    {
        get => _outlineColor;
        set => _outlineColor = CheckText(value, "Колір контуру");
    }

    public double BaseA
    {
        get => _baseA;
        set => _baseA = CheckPositive(value, "Основа A");
    }

    public double BaseB
    {
        get => _baseB;
        set => _baseB = CheckPositive(value, "Основа B");
    }

    public double Height
    {
        get => _height;
        set => _height = CheckPositive(value, "Висота");
    }

    public double CalculateArea()
    {
        return (BaseA + BaseB) / 2.0 * Height;
    }

    public double CalculatePerimeter()
    {
        double halfDifference = Math.Abs(BaseA - BaseB) / 2.0;
        double side = Math.Sqrt(Height * Height + halfDifference * halfDifference);
        return BaseA + BaseB + 2.0 * side;
    }


    public string GetInfo()
    {
        return string.Format(
            CultureInfo.InvariantCulture,
            "Трапеція: {0}/{1}, основи {2:F2} і {3:F2}, висота {4:F2}, площа {5:F2}, периметр {6:F2}",
            FillColor, OutlineColor, BaseA, BaseB, Height, CalculateArea(), CalculatePerimeter());
    }

    public int CompareTo(Trapezoid? other)
    {
        if (other is null)
        {
            return 1;
        }

        return CalculateArea().CompareTo(other.CalculateArea());
    }

    public override string ToString()
    {
        return GetInfo();
    }

    private static string CheckText(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(name + " не може бути порожнім.");
        }

        return value.Trim();
    }

    private static double CheckPositive(double value, string name)
    {
        if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0)
        {
            throw new ArgumentOutOfRangeException(name, value, name + " має бути додатним числом.");
        }

        return value;
    }
}