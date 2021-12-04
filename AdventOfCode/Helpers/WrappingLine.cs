namespace AdventOfCode.Helpers
{
    public class WrappingLine
    {
        private readonly string _line;

        public WrappingLine(string line)
        {
            _line = line;
        }

        internal string GetValueAt(int x)
        {
            if (x >= _line.Length)
            {
                //wrap around
                var res = x / _line.Length;
                var pos = x - res * _line.Length;

                return _line[pos].ToString();
            }

            return _line[x].ToString();
        }
    }
}
