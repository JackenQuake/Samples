using Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AttackSolverTest
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test1()
        {
            var insts = FindImplementations();
            if (insts.Count == 0)
                throw new Exception(
                    "No implementation of IAttackCounter was found, make sure you add a reference to your project to AttackSolverTest");

            foreach (var inst in insts)
            {
                output.WriteLine("Testing " + inst.GetType().FullName);
                // Rook - ladja
                var res = inst.CountUnderAttack(ChessmanType.Rook, new Size(3, 2), new Point(1, 1),
                    new [] { new Point(2, 2), new Point(3, 1)  });
                Assert.Equal(2, res);

                // Bishop - slon
                res = inst.CountUnderAttack(ChessmanType.Bishop, new Size(4, 5), new Point(2, 2),
                    new [] {new Point(1, 1), new Point(1, 3), });
                Assert.Equal(3, res);

                // ------------------------------------------------------ Tests by Pavlovskij E.V.
                // ---- Check there are no exceptions on single-cell board
                res = inst.CountUnderAttack(ChessmanType.Knight, new Size(1, 1), new Point(1, 1), new Point[0]);
                Assert.Equal(0, res);
                res = inst.CountUnderAttack(ChessmanType.Bishop, new Size(1, 1), new Point(1, 1), new Point[0]);
                Assert.Equal(0, res);
                res = inst.CountUnderAttack(ChessmanType.Rook, new Size(1, 1), new Point(1, 1), new Point[0]);
                Assert.Equal(0, res);
                // ---- Some tests on classic 8x8 board
                res = inst.CountUnderAttack(ChessmanType.Knight, new Size(8, 8), new Point(2, 6),
                    new [] { new Point(4, 5), new Point(4, 6) });
                Assert.Equal(5, res);
                res = inst.CountUnderAttack(ChessmanType.Knight, new Size(8, 8), new Point(4, 5),
                    new [] { new Point(2, 4), new Point(6, 6) });
                Assert.Equal(6, res);
                res = inst.CountUnderAttack(ChessmanType.Rook, new Size(8, 8), new Point(3, 5),
                    new [] { new Point(5, 3), new Point(3, 2), new Point(6, 5) });
                Assert.Equal(9, res);
                res = inst.CountUnderAttack(ChessmanType.Bishop, new Size(8, 8), new Point(3, 5),
                    new [] { new Point(5, 3), new Point(3, 2), new Point(5, 7) });
                Assert.Equal(6, res);
                // ---- Some tests on very big board
                res = inst.CountUnderAttack(ChessmanType.Knight, new Size(10000, 10000), new Point(4000, 6000),
                    new [] { new Point(5000, 6000), new Point(6000, 4000) });
                Assert.Equal(8, res);
                res = inst.CountUnderAttack(ChessmanType.Rook, new Size(10000, 10000), new Point(4000, 6000),
                    new [] { new Point(5000, 6000), new Point(6000, 4000) });
                Assert.Equal(14997, res);
                res = inst.CountUnderAttack(ChessmanType.Bishop, new Size(10000, 10000), new Point(4000, 6000),
                    new [] { new Point(5000, 6000), new Point(6000, 4000) });
                Assert.Equal(13997, res);
                // ------------------------------------------------------
            }
        }

        IList<IAttackCounter> FindImplementations()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(mytype => mytype.GetInterfaces().Contains(typeof(IAttackCounter)))
                .Select(type => (IAttackCounter) Activator.CreateInstance(type)).ToList();
        }
    }
}
