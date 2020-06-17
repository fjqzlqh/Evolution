using System;
using System.Collections.Generic;
using System.Text;

namespace leetcode.每日一题._9._回文数
{
    class Class9
    {
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
