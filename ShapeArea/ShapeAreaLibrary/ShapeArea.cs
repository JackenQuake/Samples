using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeArea {
    /// <summary>
    /// Интерфейс для классов геометрических фигур, реализующий вычисление площади.
    /// </summary>
    // Новые фигуры можно добавлять реализуя данный интерфейс,
    // при этом далее можно использовать метод GetArea() не зная, какая фигура была создана ранее.
    public interface IShapeArea {
        double GetArea();
    }
    /// <summary>
    /// Класс Окружность, реализующий интерфейс вычисления площади
    /// </summary>
    public class Circle : IShapeArea {
        private double r;
        public Circle(double _r) {
            r = _r;
        }
        public double GetArea() {
            return Math.PI * r * r;
        }
    }
    /// <summary>
    /// Класс Треугольник, реализующий интерфейс вычисления площади и проверку на прямоугольность
    /// </summary>
    public class Triangle : IShapeArea {
        private double a, b, c;
        public Triangle(double _a, double _b, double _c) {
            a = _a;
            b = _b;
            c = _c;
        }
        // для треугольника - Формула Герона
        public double GetArea() {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
        // из теоремы косинусов следует, что равенство a*a + b*b = c*c выполняется только для прямоугольных треугольников,
        // но аккуратно сравненпие дробных чисел делается с точностью дл 10^(-10)
        public bool IsSquare() {
            return (Math.Abs(a * a + b * b - c * c) < 1e-10);
        }
    }
}
