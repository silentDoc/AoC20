namespace AoC20.Day18
{
    class MathExpression
    {
        string Expr = "";

        public MathExpression(string expr)
            => Expr = expr;

        public long Solve()
        {
            long res = 0;
            char op = '_';

            for (int i = 0; i < Expr.Length; i++)
            {
                if (Expr[i] == ' ')
                    continue;

                if (Expr[i] == '(')
                {
                    var level = 1;
                    var pos = i+1;
                    while (level != 0)
                    {
                        if (Expr[pos] == '(')
                            level++;
                        if (Expr[pos] == ')')
                            level--;
                        pos++;
                    }

                    var subExpression = Expr.Substring(i + 1, pos - (i + 1));
                    var subResult = new MathExpression(subExpression).Solve();

                    res = (op == '_') ? subResult : ( (op == '+') ? res + subResult : res * subResult);
                    i = pos;
                    continue;
                }

                if (char.IsDigit(Expr[i]))
                {
                    var number = long.Parse(Expr[i].ToString());
                    res = (op == '_') ? number : ((op == '+') ? res + number : res * number);
                }

                if (Expr[i] == '*' || Expr[i] == '+')
                    op = Expr[i];
            }
            
            return res;
        }
    }

    internal class MathParser
    {
        List<MathExpression> expressions = [];
        public void ParseInput(List<string> lines)
            => lines.ForEach(x => expressions.Add(new MathExpression(x)));

        public long Solve(int part = 1)
            => expressions.Sum(x => x.Solve());

    }
}
