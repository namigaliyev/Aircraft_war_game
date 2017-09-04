using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft_war_game
{
    class RandomNumber
    {
        private static Random rastgele;
        public static int SayiUret(int min, int max)
        {
            if (rastgele == null)
                rastgele = new Random();

            return rastgele.Next(min, max);
        }
    }
}
