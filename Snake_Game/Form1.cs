using System.Windows.Forms;
using Snake_Class;
using System.IO;
using System.Drawing;
using System;
using System.Linq;
using System.Media;
using Snake_Game.Properties;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        private Timer m_pRefreshTimer;
        cGame pGame;
        byte n=4;
        SoundPlayer Sound = new SoundPlayer(Resources.Untitled);
        
       
        
        public Form1()
        {
            InitializeComponent();
            pGame = new cGame();
            pictureBox1.Image = new Bitmap(700, 700);
            pictureBox2.Image = new Bitmap(136, 42);
            pictureBox6.Image = imageList1.Images[0];
            pictureBox6.Visible = false;
            pictureBox8.Image = imageList1.Images[1];
            pictureBox8.Visible = false;


            m_pRefreshTimer = new Timer();
            m_pRefreshTimer.Interval = 100;
            m_pRefreshTimer.Tick += m_pTimer_Tick;
            Sound.PlayLooping();
            
        }

        void m_pTimer_Tick(object sender, EventArgs e)
        {
            Game_Refresh();
            Snake_Refresh();
            Apple_Refresh();
            Life_Refreh();
            label5.Text = pGame.Points.ToString();
            
           if (pGame.check)
            {
                m_pRefreshTimer.Stop();
                pictureBox1.Visible = false;
                Life_Refreh();
                pictureBox4.Visible = true;
                pictureBox6.Visible = false;
                pictureBox8.Visible = false;

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if ((e.KeyData == Keys.A)||(e.KeyData==Keys.Left)) //Влево
                {
                    cGame.EDirection enDirection = cGame.EDirection.Left;
                    pGame.Turn(enDirection);
                    n = 4;
                }
                if (e.KeyData == Keys.W) //Вверх
                {
                    cGame.EDirection enDirection = cGame.EDirection.Up;
                    pGame.Turn(enDirection);
                    n = 8;
               }
                if (e.KeyData == Keys.S) // Вниз
                {
                    cGame.EDirection enDirection = cGame.EDirection.Down;
                    pGame.Turn(enDirection);
                    n = 2;
                }
                
                if (e.KeyData == Keys.D) //Вправо
                {
                    cGame.EDirection enDirection = cGame.EDirection.Right;
                    pGame.Turn(enDirection);
                    n = 6;
                }


          }
        }


        #region Refresh
      /// <summary>
      /// Загрузка поля
      /// </summary>
        private void Game_Refresh()
        {
          
            var pImg = pictureBox1.Image;
            

            var pBitmap = new Bitmap(pImg);
            using (Graphics pG = Graphics.FromImage(pBitmap))
            {               
                int i = 0;
                int j = 0;

                for (int C = 0; pGame.Field.FieldCellList.Count > C; C++)
                {
                    if (pGame.Field.FieldCellList[C].Type == true)
                    { 
                        i = pGame.Field.FieldCellList[C].CoordX * 30;
                        j = pGame.Field.FieldCellList[C].CoordY * 30;
                        pG.DrawImage(FieldImage.Images[1], new Point(i, j));
                        pictureBox1.Image = pBitmap;
                    }

                    else
                    {
                        i = pGame.Field.FieldCellList[C].CoordX * 30;
                        j = pGame.Field.FieldCellList[C].CoordY * 30;
                        pG.DrawImage(FieldImage.Images[0], new Point(i, j));
                       pictureBox1.Image = pBitmap;
                       
                    }

                }
            }
            pictureBox1.Invalidate();
          
            
        }
        /// <summary>
        /// Змея
        /// </summary>
        public void Snake_Refresh()
        {
            #region Голова
           var pImg = pictureBox1.Image;
            var pBitmap = new Bitmap(pImg);
            using (Graphics pG = Graphics.FromImage(pBitmap))
            {
                int i = 0;
                int j = 0;
                
                i = pGame.Field.Snake.SnakeCellList[0].CoordX * 30;
                j = pGame.Field.Snake.SnakeCellList[0].CoordY * 30;

                switch (n)
                {
                   case 8: //Вверх
                    {
                       pG.DrawImage(SnakeImage.Images[2], new Point(i, j));
                       pictureBox1.Image = pBitmap;
                       break;
                     }

                    case 2: //Вниз
                     {
                        pG.DrawImage(SnakeImage.Images[4], new Point(i, j));
                        pictureBox1.Image = pBitmap;
                        break;
                      }
                    case 4: // Влево
                      {
                         pG.DrawImage(SnakeImage.Images[5], new Point(i, j));
                         pictureBox1.Image = pBitmap;
                         break;
                      }
                    case 6: //Вправо
                      {
                         pG.DrawImage(SnakeImage.Images[3], new Point(i, j));
                         pictureBox1.Image = pBitmap;
                         break;
                      }
                  }
                #endregion

            #region Тело

                for (int C = 1; pGame.Field.Snake.SnakeCellList.Count  > C; C++) 
             {
                i = pGame.Field.Snake.SnakeCellList[C].CoordX * 30;
                j = pGame.Field.Snake.SnakeCellList[C].CoordY * 30;
                pG.DrawImage(SnakeImage.Images[1], new Point(i, j));
                pictureBox1.Image = pBitmap;
             }
                #endregion

            pictureBox1.Invalidate();

            }
        }   
        
        /// <summary>
        /// Яблоко
        /// </summary>
        private void Apple_Refresh()
        {
            var pImg = pictureBox1.Image;
            var pBitmap = new Bitmap(pImg);

            int n=2;
            switch (pGame.Field.Apple.Type)
            {
                case type.Point:
                    n = 2;
                 break;
                case type.Life:
                    n = 3;
                 break;
                case type.Surprise:
                    n = 4;
                break;
                case type.BigPoint:
                    n = 5;
                    break;

            }



                using (Graphics pG = Graphics.FromImage(pBitmap))
            {
                int i = 0;
                int j = 0;
                i = pGame.Field.Apple.CoordX * 30;
                j = pGame.Field.Apple.CoordY * 30;
                
                pG.DrawImage(FieldImage.Images[n], new Point(i, j));
                pictureBox1.Image = pBitmap;
            }
            pictureBox1.Invalidate();
        }
        
        /// <summary>
        /// Жизнь
        /// </summary>
        private void Life_Refreh()
        {
            var pImg1 = pictureBox2.Image;  
            var pBitmap1 = new Bitmap(pImg1);
            using (Graphics pG = Graphics.FromImage(pBitmap1))
            {
               int i = 0;
               pG.Clear(Color.Empty);

               for (var n = 0; n < pGame.Life; n++)
               {
                 pG.DrawImage(MenuImage.Images[0], new Point(i, 0));
                 pictureBox2.Image = pBitmap1;
                 i = i + 30;
               }
            }
            pictureBox2.Invalidate();
        }

    #endregion

        /// <summary>
        /// Старт игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox6.Visible = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox5.Visible = true;
            label5.Visible = true;
            pictureBox7.Visible = false;
            
            string[] arFiles = Directory.GetFiles(@"Data\", "*.csv");
            foreach (var sFilename in arFiles)
             {
                pGame.CreateField(sFilename);
             }

            pGame.NewGame();
            cGame.EDirection enDirection = cGame.EDirection.Left;
            pGame.Turn(enDirection);
            n = 4;


            Game_Refresh();
            Snake_Refresh();
            Apple_Refresh();
            Life_Refreh();

            System.Threading.Thread.Sleep(300);
            m_pRefreshTimer.Start();

            pGame.Start();
            

            pictureBox4.Visible = false;
        }
               
        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Image = Resources.NG1;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Resources.NG;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox6.Visible = false;
            pGame.Pause();
            pictureBox8.Visible = true;
        }

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox8.Visible = false;
            pGame.Play();
            pictureBox6.Visible = true;
        }
    }
}

       


