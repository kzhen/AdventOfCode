using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers
{
    public record Position(int x, int y);

    public record Position<T>(int x, int y, T valueAtPos);
}
