using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests_LongRoadHome
{
    class StringMaker
    {
        private static Random rnd = new Random();

        public static String makeItemStr(int id)
        {
            String item = "ID:" + id + ",Name:TestItem,Amount:1,Description:test item " + id + ",ActiveEffect,PassiveEffect,Requirements";
            int requirements = rnd.Next(4);
            for (int i = 0; i < requirements; i++)
            {
                item += ":" + rnd.Next(1, 21);
            }
            return item;
        }
    }
}
