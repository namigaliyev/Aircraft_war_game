using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Aircraft_war_game
{
    class Bullet:Control
    {
        public Bullet(Player o)
        {

            Resim = Image.FromFile("bullet.png");
            //Rocket sinifinin kurucu fonksiyonu Oyuncu nesnesinden referans alarak ayni kordinatdan hareket etmesini sagliyor
            X = o.X + 15;
            Y = o.Y + 3;
            Width = 32;
            Height = 32;
        }
        //yukarigit fonksiyonu rocketin Y degerini azaltiyor
        public void YukariGit()
        {
            Y -= 5;
        }
    }
}
