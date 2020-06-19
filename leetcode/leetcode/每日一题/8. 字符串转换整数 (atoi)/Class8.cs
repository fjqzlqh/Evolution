using System;
using System.Collections.Generic;
using System.Text;
/*
请你来实现一个 atoi 函数，使其能将字符串转换成整数。

首先，该函数会根据需要丢弃无用的开头空格字符，直到寻找到第一个非空格的字符为止。接下来的转化规则如下：

如果第一个非空字符为正或者负号时，则将该符号与之后面尽可能多的连续数字字符组合起来，形成一个有符号整数。
假如第一个非空字符是数字，则直接将其与之后连续的数字字符组合起来，形成一个整数。
该字符串在有效的整数部分之后也可能会存在多余的字符，那么这些字符可以被忽略，它们对函数不应该造成影响。
注意：假如该字符串中的第一个非空格字符不是一个有效整数字符、字符串为空或字符串仅包含空白字符时，则你的函数不需要进行转换，即无法进行有效转换。

在任何情况下，若函数不能进行有效的转换时，请返回 0 。

提示：

本题中的空白字符只包括空格字符 ' ' 。
假设我们的环境只能存储 32 位大小的有符号整数，那么其数值范围为[−231, 231 − 1]。如果数值超过这个范围，请返回 INT_MAX(231 − 1) 或 INT_MIN(−231) 。
 

示例 1:

输入: "42"
输出: 42
示例 2:

输入: "   -42"
输出: -42
解释: 第一个非空白字符为 '-', 它是一个负号。
     我们尽可能将负号与后面所有连续出现的数字组合起来，最后得到 -42 。
示例 3:

输入: "4193 with words"
输出: 4193
解释: 转换截止于数字 '3' ，因为它的下一个字符不为数字。
示例 4:

输入: "words and 987"
输出: 0
解释: 第一个非空字符是 'w', 但它不是数字或正、负号。
     因此无法执行有效的转换。
示例 5:

输入: "-91283472332"
输出: -2147483648
解释: 数字 "-91283472332" 超过 32 位有符号整数范围。 
     因此返回 INT_MIN(−231) 。

*/

namespace leetcode
{
    class Class8
    {
        //普通方法
        public static int MyAtoi1(string str)
        {
            int n = str.Length;

            int ret = 0;
            bool bBegin = false;
            int bFushu = 1;
            for (int i = 0; i < n; i++)
            {
                if (bBegin)
                {
                    if (str[i] >= 48 && str[i] <= 57)
                    {
                        int num = str[i] - 48;
                        if (ret > int.MaxValue / 10 || (ret == int.MaxValue / 10 && num > 7)) return int.MaxValue;
                        if (ret < int.MinValue / 10 || (ret == int.MinValue / 10 && num > 8)) return int.MinValue;

                        ret = ret * 10 + num * bFushu;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (str[i] == ' ') continue;
                    if (str[i] == '-')
                    {
                        bBegin = true;
                        bFushu = -1;
                    }
                    else if (str[i] >= 48 && str[i] <= 57)
                    {
                        bBegin = true;
                        ret = str[i] - 48;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            return ret;
        }

        //状态机
        public class SM
        {
            string state = "start";
            Dictionary<string, List<string>> states = new Dictionary<string, List<string>>();
            public SM()
            {
                //每个状态对应下个字符的状态跳转 " ", "+ -", "0123456789", "其他字符"
                states.Add("start", new List<string>() {"start", "sign", "number", "end"});
                states.Add("sign", new List<string>() {"end", "end", "number", "end"});
                states.Add("number", new List<string>() {"end", "end", "number", "end"});
                states.Add("end", new List<string>() {"end", "end", "end", "end"});
            }

            public int GetState(char c) {
                if (c == ' ') return 0;
                if (c == '+' || c == '-') return 1;
                if (c >= '0' && c <= '9') return 2;
                return 3;
            }

            public void Get(char c) {
                state = states[state][GetState(c)];
                if (state == "number") {
                    int n = c - 48;
                    if (num > int.MaxValue / 10 || (num == int.MaxValue / 10 && n > 7)) {
                        state = "end";
                        num = int.MaxValue;
                        return;
                    }
                    if (num < int.MinValue / 10 || (num == int.MinValue / 10 && n > 8)) {
                        state = "end";
                        num = int.MinValue;
                        return;
                    }

                    num = num * 10 + n * sign;
                } 
                else if (state == "sign") {
                    sign = c == '-' ? -1 : 1;
                }
            }

            public int sign = 1;
            public int num = 0;
        }

        public static int MyAtoi2(string str) {
            SM sm = new SM();
            for (int i = 0; i < str.Length; i++) {
                sm.Get(str[i]);
            }
            return sm.num;
        }

    }
}
