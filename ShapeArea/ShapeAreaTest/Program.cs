using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeArea;

namespace ShapeAreaTest {
    class Program {
        static void TestDoubleValue(string comment, double result, double expected) {
            if (Math.Abs(result - expected) < 1e-10) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("ВЕРНО   ");
                Console.ForegroundColor = ConsoleColor.Gray;
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("НЕВЕРНО ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine($": {comment}, ожидалось {expected}, получено {result}.");
        }
        static void TestBooleanValue(string comment, bool result, bool expected) {
            if (result == expected) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("ВЕРНО   ");
                Console.ForegroundColor = ConsoleColor.Gray;
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("НЕВЕРНО ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine($": {comment}, ожидалось {expected}, получено {result}.");
        }
        static void TestCircle() {
            var circle1 = new ShapeArea.Circle(10);
            var circle2 = new ShapeArea.Circle(15);
            TestDoubleValue("Площадь круга радиуса 10", circle1.GetArea(), 100 * Math.PI);
            TestDoubleValue("Площадь круга радиуса 15", circle2.GetArea(), 225 * Math.PI);
        }
        static void TestTriangle() {
            var triangle1 = new Triangle(1, 2, 3);
            var triangle2 = new Triangle(3, 4, 5);
            TestDoubleValue("Площадь треугольника со сторонами 1, 2, 3", triangle1.GetArea(), 0);
            TestBooleanValue("Проверка на прямоугольность треугольника со сторонами 1, 2, 3", triangle1.IsSquare(), false);
            TestDoubleValue("Площадь треугольника со сторонами 3, 4, 5", triangle2.GetArea(), 6);
            TestBooleanValue("Проверка на прямоугольность треугольника со сторонами 3, 4, 5", triangle2.IsSquare(), true);
        }
        static void Main(string[] args) {
            TestCircle();
            TestTriangle();
            Console.WriteLine(); Console.Write("Press any key to exit..."); Console.ReadKey(true);
        }
    }
}
