using System;
using System.Collections.Generic;
using System.Text;

namespace leetcode
{
    class Class9
    {
        //判断右边是否和左边相等即可,不需要整个遍历
        public bool IsPalindrome(int x)
        {
            if (x < 0) return false;
            if (x % 10 == 0 && x != 0) return false;
            int newX = 0;
            while (x > newX) {
                int pop = x % 10;
                x /= 10;
                newX = newX * 10 + pop;
            }
            return newX == x || newX / 10 == x;
        }
    }
}
