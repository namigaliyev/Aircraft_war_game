using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Aircraft_war_game
{
    class Player:Control
    {
        
        private int xsolsinir;
        private int xsagsinir;
        public int Xsolsinir
        {
            get
            {
                return xsolsinir;
            }

            set
            {
                xsolsinir = value;
            }
        }
        public int Xsagsinir
        {
            get
            {
                return xsagsinir;
            }

            set
            {
                xsagsinir = value;
            }
        }

        //Oyuncubilgileri 
        public Player()
        {
            Resim = Image.FromFile("player.png");
            X = 300;
            Y = 500;
            Width = 64;
            Height = 64;
            Xsolsinir = 1;
            Xsagsinir = 612;
        }
        //saga ve sola gitme fonksiyonlari oyuncunun X'ini artirib azaltiyor
        public void SagaGit()
        {
            if (X < Xsagsinir)
            {
                X += 10;
            }
        }
        public void SolaGit()
        {
            if (X > Xsolsinir)
            {
                X -= 10;
            }
        }
    }
}
