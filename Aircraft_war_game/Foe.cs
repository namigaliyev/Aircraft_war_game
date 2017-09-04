using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Aircraft_war_game
{
    class Foe:Control
    {
        public Foe()
        {
            Resim = Image.FromFile("foe.png");
            //dusmanin rastgele X kordinatlarinda olusmasi icin rasgele degerleri ataniyor
            X = RandomNumber.SayiUret(1, 600);
            Y = -60;
            Width = 64;
            Height = 64;
        }
        //Asagigit fonksiyonu dusmanin Y degerini artiriyor
        public void AsagiGit()
        {
            Y += 3;
        }
    }
}
