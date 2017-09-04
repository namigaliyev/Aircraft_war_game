using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Drawing;

namespace Aircraft_war_game
{
    class Mainwindow:Form
    {
        private int numberofenemies;
        private int numberofbullet;
        private int counter;
        private int score;
        private string drawString;
        private string path;
        private Player player;
        private List<Bullet> bullets = new List<Bullet>();
        private List<Foe> foes = new List<Foe>();
        private Timer zamanla;
        private Font drawFont;
        private SolidBrush drawBrush;
        private StringFormat drawFormat;
        private SoundPlayer player1;
        private Label lbl;

        public Mainwindow(int genislik, int yukseklik)
        {
            Width = genislik;
            Height = yukseklik;
            DoubleBuffered = true;
            Paint += Mainwindow_Paint;
            counter = 0;
            score = 0;
            Score_updated(score);

            zamanla = new Timer();
            player = new Player();
            lbl = new Label();
            //Label'in ozellikleri
            lbl.Text = "Oyunu Baslatmak icin ENTER tusuna basiniz \n Oyuncuyu hareket etdirmek icin SAG/SOl yon tuslarini kulllanin \n Ates etmek icin BOSLUK tusuna basin";
            lbl.Location = new Point(0, 0);
            lbl.Size = new Size(700, 40);
            lbl.BackColor = Color.Red;
            lbl.ForeColor = Color.White;
            //Bilgi Labeli ekrana cikariliyor
            Controls.Add(lbl);

            //Arkaplandaki score nin ekrana cizdirilmesi icin verilen ozellikler
            drawFont = new Font("Arial", 200);
            drawBrush = new SolidBrush(Color.Red);
            drawFormat = new StringFormat();

            //Score artdiginda cikan sesi ekliyoruz
            player1= new SoundPlayer();
            path = "point.wav";
            player1.SoundLocation = path;


            zamanla.Tick += Zamanla_Tick;
            //Tick olayina interval veriyoruz
            zamanla.Interval = 20;
            KeyDown += Mainwindow_KeyDown;
        }
        public void Foe_create()
        {
            //dusman olusturma satirlari
            foes.Add(new Foe());
            numberofenemies++;
        }
        public void Bullet_create()
        {
            //rocket olusturma satirlari
            //Burda onemli kisim Rocket sinifinin Kurucu fonksiyonu Oyuncu  sinifdan referans almasi
            //Buna sebep Rocket firlatildigi an oyuncuyla ayni kordinatdan baslamasi lazim
            bullets.Add(new Bullet(player));
            numberofbullet++;
        }

        public void Score_updated(int score)
        {
            //score nin guncellenmesi satiri Tick olayinda arttirilmis score degerini parametre alarak
            //draw string degiskenine atiyoruz
            //drawstring degiskenide asagida paint olayinda ekrana guncel score ni ekrana ciziyor
            drawString = Convert.ToString(score);
        }

        private void Mainwindow_KeyDown(object sender, KeyEventArgs e)
        {
            //Keydown tusa basildigian olayi

            //enter tusuna basildiginda oyun basliyor
            if (e.KeyCode == Keys.Return)
            {

                zamanla.Start();
            }
            //Sag yon tusuna basildiginda oyuncu saga dogru hareket ediyor
            if (e.KeyCode == Keys.Right)
            {
                player.SagaGit();
            }
            //Sol yon tusuna basildiginda oyuncu sola dogru hareket ediyor
            if (e.KeyCode == Keys.Left)
            {
                player.SolaGit();
            }
            //Bosluk tusuna basildiginda oyuncu rocket firlatiyor 
            //yani rocket olusturma islemleri gerceklesiyor
            if (e.KeyCode == Keys.Space)
            {
                Bullet_create();
            }
        }

        private void Zamanla_Tick(object sender, EventArgs e)
        {
            //sayac degiskeni bu Tcik olayinin asagisinda 1 arttiriliyor
            //bu if in amaci dusmanin rastgele zamanlarda olusturulmasini sagliyor

            if (counter % 45 == 0)
            {
                Foe_create();
            }
            //dusmanin hareket etmesi
            for (int i = 0; i < numberofenemies; i++)
            {
                //bu if kosulunda dusman alt sinira kadar gidiyor
                //Y 490 dan kucuk oldugu surece asagiya dogru gidecek
                if (foes[i].Y < 490)
                {
                    foes[i].AsagiGit();

                }
                //eger artik Y 490 dan fazlaysa bu satirlar calisacak
                else
                {
                    //oyun durdurulacak
                    zamanla.Stop();
                    //ekrana bidirim cikarilacak ve score gozukecek
                    MessageBox.Show("Game Over:::SCORE " + score);
                    //dusmanlar ve rocketler temizlenecek
                    foes.Clear();
                    bullets.Clear();
                    //dusmansayisi ve rocketsayisi 0 yapilacak
                    numberofenemies = 0;
                    numberofbullet = 0;
                    //score 0 olacak
                    score = 0;
                    Score_updated(score);
                }
            }
            //rocketlerin yukariya dogru hareket etmesi
            for (int i = 0; i < numberofbullet; i++)
            {
                bullets[i].YukariGit();
            }
            //bu for dongusu icerisinde dusmanlarla rocketlerin carpismasi kontrol edilecek
            for (int i = 0; i < numberofenemies; i++)
            {
                for (int j = 0; j < numberofbullet; j++)
                {
                    try
                    {
                        if (foes[i].X < bullets[j].X + bullets[j].Width &&
                            foes[i].X + foes[i].Width > bullets[j].X &&
                            foes[i].Y < bullets[j].Y + bullets[j].Height &&
                            foes[i].Height + foes[i].Y > bullets[j].Y)
                        {
                            //eger carpistiysa carpisan dusmanla rocket silinecek ve dusmansayisi degiskeniyle
                            //rocketsayisi degiskeni 1 azaltilacak
                            foes.RemoveAt(i); numberofenemies--;
                            bullets.RemoveAt(j); numberofbullet--;
                            //carpisma aninda score sayisi 1 artacak
                            score++;
                            //score guncellenecek
                            Score_updated(score);
                            //carpisma aninda puan sesi cikartilacak
                            player1.Play();
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            //bu for dongusundede carpisma kontrol ediliyor fakat burda dusmanlarla oyuncu carpismasi kontrol ediliyor
            for (int i = 0; i < numberofenemies; i++)
            {
                try
                {
                    if (foes[i].X < player.X + player.Width &&
                        foes[i].X + foes[i].Width > player.X &&
                        foes[i].Y < player.Y + player.Height &&
                        foes[i].Height + foes[i].Y > player.Y)
                    {
                        //carpisma varsa yukaridaki islemler gibi burdada ayni islemler calisacak
                        zamanla.Stop();
                        MessageBox.Show("Game Over:::SCORE " + score);
                        foes.Clear();
                        bullets.Clear();
                        numberofenemies = 0;
                        numberofbullet = 0;
                        score = 0;
                        Score_updated(score);
                    }
                }
                catch (Exception ex) { }
            }
            //yukarida soyledigim gibi sayac 1 artacak
            counter++;
            //her degisiklikte yeniden cizilmesi icin kullanilan Invalidate
            Invalidate();
        }

        private void Mainwindow_Paint(object sender, PaintEventArgs e)
        {
            //buradada cizdirme olaylari gerceklesiyor
            //oyuncu cizdiriliyor
            player.Ciz(e.Graphics);
            //Guncel score ekrana cizdiriliyor
            e.Graphics.DrawString(drawString, drawFont, drawBrush, 200, 150, drawFormat);
            //dusmanlar cizdiriliyor
            for (int i = 0; i < numberofenemies; i++)
            {
                foes[i].Ciz(e.Graphics);
            }
            //rocketler cizdiriliyor
            for (int j = 0; j < numberofbullet; j++)
            {
                bullets[j].Ciz(e.Graphics);
            }

        }
    }
}
