using Interface;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AttackSolver
{
    public class MyAttackCounter : IAttackCounter
    {
        // Вспомогательная функция, добавляет клетку к списку атакуемых, если она попадает внутрь доски
        private static void AddToAttackList(List<Point> attacks, Size boardSize, int x, int y)
        {
            if ((x >= 1) && (y >= 1) && (x <= boardSize.Width) && (y <= boardSize.Height))
                attacks.Add(new Point(x, y));
        }

        public int CountUnderAttack(ChessmanType cmType, Size boardSize, Point startCoords, Point[] obstacles)
        {
            // Для коня нужно просто проверить восемь доступных коню клеток
            // Аналогично можно проверять и другие не-дальнобойные фигуры, если понадобится их добавить (пешка, король)
            if (cmType == ChessmanType.Knight) {
                // Создадим список из восьми клеток и будем удалять недоступные
                var attacks = new List<Point>(8);
                AddToAttackList(attacks, boardSize, startCoords.X - 1, startCoords.Y - 2);
                AddToAttackList(attacks, boardSize, startCoords.X + 1, startCoords.Y - 2);
                AddToAttackList(attacks, boardSize, startCoords.X - 1, startCoords.Y + 2);
                AddToAttackList(attacks, boardSize, startCoords.X + 1, startCoords.Y + 2);
                AddToAttackList(attacks, boardSize, startCoords.X - 2, startCoords.Y - 1);
                AddToAttackList(attacks, boardSize, startCoords.X + 2, startCoords.Y - 1);
                AddToAttackList(attacks, boardSize, startCoords.X - 2, startCoords.Y + 1);
                AddToAttackList(attacks, boardSize, startCoords.X + 2, startCoords.Y + 1);
                // Удалим клетки с препятствиями
                foreach (Point p in obstacles) {
                    // Для повышения производительности, если список после предыдущих действий опустел, можно сразу выйти
                    if (attacks.Count == 0) return 0;
                    // И если препятствие слишком далеко от фигуры - пропускаем
                    if ((p.X < startCoords.X - 2) || (p.Y < startCoords.Y - 2) ||
                        (p.X > startCoords.X + 2) || (p.Y > startCoords.Y + 2)) continue;
                    // Удаляем клетку с препятствием из списка, если она там есть
                    attacks.Remove(p);
                }
                return attacks.Count;
            }
            // А для ладьи и слона решение во многом одинаково:
            // нужно определить допустимые диапазоны по двум осям
            // для удобства перейдем в систему координат, в которой в центре находится фигура
            // и для ладьи оси a=x, b=y, а для слона a=x-y, b=x+y
            int a1, a2, b1, b2;
            if (cmType == ChessmanType.Rook)  // Ладья
            {
                a1 = 1 - startCoords.X; a2 = boardSize.Width - startCoords.X;
                b1 = 1 - startCoords.Y; b2 = boardSize.Height - startCoords.Y;
            } else {  // Слон
                a1 = Math.Max(1 - startCoords.X, startCoords.Y - boardSize.Height);
                a2 = Math.Min(boardSize.Width - startCoords.X, startCoords.Y -1);
                b1 = Math.Max(1 - startCoords.X, 1 - startCoords.Y);
                b2 = Math.Min(boardSize.Width - startCoords.X, boardSize.Height - startCoords.Y);
            }
            if ((a1 > 0) || (a2 < 0) || (b1 > 0) || (b2 < 0))
                throw new ArgumentException("Start coordinates outside board");
            foreach (Point p in obstacles) {
                int a, b;  // Координаты препятствия относительно фигуры в выбранных осях
                // Вычисляем координаты в зависимости от типа фигуры
                if (cmType == ChessmanType.Rook)  // Ладья
                {
                    a = p.X - startCoords.X;
                    b = p.Y - startCoords.Y;
                } else {  // Слон
                    a = (p.X - startCoords.X) - (p.Y - startCoords.Y);
                    b = (p.X - startCoords.X) + (p.Y - startCoords.Y);
                    // Эти числа либо оба нечетные, тогда препятствие заведомо не влияет на слона
                    if ((a % 2) != 0) continue;
                    // ... либо оба четные, и тогда их надо поделить на два
                    a /= 2; b /= 2;
                }
                if ((a == 0) && (b == 0))
                    throw new ArgumentException("One of obstacles coincides with the figure");
                // Обновляем диапазоны
                if ((a == 0) && (b < 0) && (b >= b1)) b1 = b+1;
                if ((a == 0) && (b > 0) && (b <= b2)) b2 = b-1;
                if ((b == 0) && (a < 0) && (a >= a1)) a1 = a+1;
                if ((b == 0) && (a > 0) && (a <= a2)) a2 = a-1;
            }
            return (a2-a1) + (b2-b1);
        }
    }
}
