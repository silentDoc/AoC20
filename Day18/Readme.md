﻿## --- Day 18: Operation Order ---

As you look out the window and notice a heavily-forested continent slowly appear over the horizon, you are interrupted by the child sitting next to you. They're curious if you could help them with their  math  homework.

Unfortunately, it seems like this "math"  [follows different rules](https://www.youtube.com/watch?v=3QtRK7Y2pPU&t=15)  than you remember.

The homework (your puzzle input) consists of a series of expressions that consist of addition (`+`), multiplication (`*`), and parentheses (`(...)`). Just like normal math, parentheses indicate that the expression inside must be evaluated before it can be used by the surrounding expression. Addition still finds the sum of the numbers on both sides of the operator, and multiplication still finds the product.

However, the rules of  _operator precedence_  have changed. Rather than evaluating multiplication before addition, the operators have the  _same precedence_, and are evaluated left-to-right regardless of the order in which they appear.

For example, the steps to evaluate the expression  `1 + 2 * 3 + 4 * 5 + 6`  are as follows:

```
1 + 2 * 3 + 4 * 5 + 6
  3   * 3 + 4 * 5 + 6
      9   + 4 * 5 + 6
         13   * 5 + 6
             65   + 6
                 71

```

Parentheses can override this order; for example, here is what happens if parentheses are added to form  `1 + (2 * 3) + (4 * (5 + 6))`:

```
1 + (2 * 3) + (4 * (5 + 6))
1 +    6    + (4 * (5 + 6))
     7      + (4 * (5 + 6))
     7      + (4 *   11   )
     7      +     44
            51

```

Here are a few more examples:

-   `2 * 3 + (4 * 5)`  becomes  _`26`_.
-   `5 + (8 * 3 + 9 + 3 * 4 * 3)`  becomes  _`437`_.
-   `5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))`  becomes  _`12240`_.
-   `((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2`  becomes  _`13632`_.

Before you can help with the homework, you need to understand it yourself.  _Evaluate the expression on each line of the homework; what is the sum of the resulting values?_