using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;

namespace AllLottery.Business.Cathectic.LiuHeCai
{
    public abstract class BaseCathecticLiuHeCai : BaseCathectic
    {
        protected List<int> GetColorNumber(string color)
        {

            switch (color)
            {
                case "红":
                    return new List<int>() { 1, 2, 7, 8, 12, 13, 18, 19, 23, 24, 29, 30, 34, 35, 40, 45, 46 };
                case "绿":
                    return new List<int>() { 5, 6, 11, 16, 17, 21, 22, 27, 28, 32, 33, 38, 39, 43, 44, 49 };
                case "蓝":
                    return new List<int>() { 3, 4, 9, 10, 14, 15, 20, 25, 26, 31, 36, 37, 41, 42, 47, 48 };
                default:
                    break;
            }
            return null;
        }

        protected readonly string[] Shuxiang = { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };

        protected List<int> GetAnimalNumber(string animal)
        {
            var number = Shuxiang.IndexOf(animal);
            var begin = Shuxiang.IndexOf(ToyearShuXiang());
            int key = 0;
            if (begin >= number)
            {
                key = begin - number + 1;
            }
            else
            {
                key = begin - number + 13;
            }

            List<int> list = new List<int>();
            while (key < 50)
            {
                list.Add(key);
                key += 12;
            }
            return list;
        }

        protected string ShuXiang(int year)
        {
            int tmp = year - 2008;
            if (year < 2008)
            {

                return Shuxiang[tmp % 12 + 12];
            }
            else
            {
                return Shuxiang[tmp % 12];
            }

        }

        protected string ToyearShuXiang()
        {
            return ShuXiang(DateTime.Today.Year);
        }
    }
}