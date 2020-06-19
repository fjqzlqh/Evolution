using System;
using System.Collections.Generic;
using System.Text;

namespace leetcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine(Class8.MyAtoi2("-91283472332"));
            Console.ReadKey();
        }

        public static int LengthOfLongestSubstring(string s)
        {
            int[] bit = new int[256];
            int begin = 0;
            int max = 0;
            int t = 0;
            int t2 = 0;
            for (int i = 0; i < s.Length; i++)
            {
                t = bit[s[i]];
                if (t > 0) {
                    for (int j = begin; j < t; j++) {
                        bit[s[j]] = 0;
                    }
                    t2 = i - begin;
                    if (t2 > max)
                    {
                        max = t2;
                    }
                    begin = t;
                }
                bit[s[i]] = i + 1;
            }
            return max;
        }

        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length > nums2.Length)
            {
                return FindMedianSortedArrays(nums2, nums1);
            }
            int len1 = nums1.Length;
            int len2 = nums2.Length;
            
            int begin = 0;
            int end = len1 - 1;
            int i, j;
            int totalLen = len1 + len2 - 1;
            bool bFind = false;
            bool bEvenNumber = totalLen % 2 != 0;
            while (begin <= end)
            {
                i = (begin + end) / 2;
                j = totalLen / 2 - i;

                if (i != 0 && j != len2 - 1 && nums1[i] < nums2[j - 1])
                {
                    begin = i + 1;
                }
                else if (i != len1 - 1 && j != 0 && nums2[j] < nums1[i - 1])
                {
                    end = i - 1;
                }
                else 
                {
                    
                }

                if (i == 0) {

                }
                else if(i == len1 - 1)
                { 

                }

            }

            return 0.0;
        }

        //扩散法查找回文子串
        public static string LongestPalindrome(string s)
        {
            int begin = 0;
            int max = 0;
            for (int i = 0; i < s.Length; i++) 
            {
                int left = i;
                int right = i;
                int len1 = 0;
                while (left >= 0 && right < s.Length && s[left] == s[right])
                {
                    left--;
                    right++;
                }
                len1 = right - left - 1;

                left = i;
                right = i + 1;
                int len2 = 0;
                while (left >= 0 && right < s.Length && s[left] == s[right])
                {
                    left--;
                    right++;
                }
                len2 = right - left - 1;
                int len = Math.Max(len1, len2);
                if (len > max) {
                    begin = i - (len -1) / 2;
                    max = len;
                }
            }
            return s.Substring(begin, max);
        }

        //动态规划查找回文子串
        public static string LongestPalindrome2(string s)
        {
            if (s == "") return s;
            int n = s.Length;
            int begin = 0;
            int max = 1;
            bool[,] dp = new bool[n, n];
            for(int len = 0; len < n; len++)
            {
                dp[len, len] = true;
            }
            for (int len = 1; len < n; len++)
            {
                for (int i = 0; i + len < n; i++) {
                    int j = i + len;
                    if(len == 1)
                    {
                        dp[i, j] = s[i] == s[j];
                    }
                    else
                    {
                        dp[i, j] = s[i] == s[j] && dp[i + 1, j - 1];
                    }
                    if (dp[i, j] && len >= max) {
                        begin = i;
                        max = len + 1;
                    }
                }
            }
            return s.Substring(begin, max);
        }

        private static Dictionary<int, int> dp = new Dictionary<int, int>();
        public static int ChouLingQian(int[] coins, int num) 
        {
            if (num == 0) return 0;
            if (num < 0) return -1;
            if (dp.ContainsKey(num))
                return dp[num];
            int ret = int.MaxValue;
            for (int i = 0; i < coins.Length; i++) 
            {
                int count = ChouLingQian(coins, num - coins[i]);
                if (count == -1) continue;
                ret = Math.Min(ret, 1 + count);
            }
            ret = ret == int.MaxValue ? -1 : ret;
            dp[num] = ret;
            return ret;
        }

        //Z字变换
        public static string Convert(string s, int numRows)
        {
            int n = s.Length;
            if (n <= numRows) return s;
            List<StringBuilder> rows = new List<StringBuilder>();
            for (int i = 0; i < numRows; i++) {
                rows.Add(new StringBuilder());
            }
            bool bDown = false;
            int rowIndex = 0;
            for (int i = 0; i < n; i++) {
                rows[rowIndex].Append(s[i]);

                if (rowIndex == numRows - 1 || rowIndex == 0)
                {
                    bDown = !bDown;
                }
                rowIndex = bDown ? rowIndex + 1 : rowIndex - 1;
            }
            StringBuilder ret = new StringBuilder();
            foreach (var item in rows) {
                ret.Append(item);
            }
            return ret.ToString();
        }

        public static int Reverse(int x)
        {
            long ret = 0;
            while (x != 0)
            {
                long num = x % 10;
                x = x / 10;
                ret = ret * 10 + num;
            }
            if (ret < int.MinValue || ret > int.MaxValue) return 0;

            return (int)ret;
        }

        public static int MyAtoi(string str)
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
                        ret = ret *10 + (str[i] - 48) * bFushu;
                    }
                    else {
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
                    else {
                        break;
                    }
                    
                }
            }
            return ret;
        }

        public static int Binary(int[] arr, int target)
        {

            return -1;
        }
    }
}
