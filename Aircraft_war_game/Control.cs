using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Aircraft_war_game
{
    class Control
    {
        //Bu abstact sinifinda diger siniflarin ayni degisken ve fonksiyonlari tekrar tekrar yazdirilmamasi icin  biraraya topluyarak
        //o siniflara kalitim veriyor
        private int x;
        private int y;
        private int width;
        private int height;
        private Image resim;

        public Image Resim
        {
            get
            {
                return resim;
            }

            set
            {
                resim = value;
            }
        }
        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }
        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }
        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }
        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public void Ciz(Graphics g)
        {
            g.DrawImage(Resim, X, Y, Width, Height);
        }

    }
}
