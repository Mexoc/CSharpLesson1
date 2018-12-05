using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BrandNewShip
{
    static class Game
    {
        public static BufferedGraphics buffer;
        public static BufferedGraphicsContext context;
        public static Graphics g;        
        private static int width;
        private static int height;
        private static Random rnd = new Random();
        private static int n = 20;        
        public static Point[] asteroidpoint = new Point[n];
        public static Point[] starpoint = new Point[60];
        public static Point[] starpointimage = new Point[60];
        public static BaseObject[] asteroid = new BaseObject[n];
        public static Image planet = Image.FromFile("planet.png");
        public static Image starImage = Image.FromFile("star.jpg");
        public static Star[] stars = new Star[60];
         
        

        public static int Width
        {
            get { return width; }
            set { width = value; }
        }

        public static int Height
        {
            get { return height; }
            set { height = value; }
        }

        public static void Init(Form form)
        {
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Timer timer1 = new Timer { Interval = 100 };            
            timer1.Start();
            timer1.Tick += Timer_Tick;
        }                

        //рисуем форму и закрашиваем её черным
        public static void DrawForm()
        {
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            buffer.Graphics.Clear(Color.Black);
            buffer.Render();            
        }

        //загружаем координаты астероидов и сами астероиды 
        public static void LoadAsteroid()
        {
            for (int i = 0; i < asteroid.Length; i++)
            {
                asteroidpoint[i] = new Point(rnd.Next(700), rnd.Next(700));
                asteroid[i] = new BaseObject(asteroidpoint[i], new Point(1, 0), new Size(10,10));
            }
        }

        //рисуем астероиды на форме
        public static void DrawAsteroid()
        {
            foreach (BaseObject obj in asteroid)
                obj.Draw();
            buffer.Render();
        }

        //рисуем планету
        public static void DrawPlanet()
        {
            buffer.Graphics.DrawImage(planet, Width / 2, Height / 2);
            buffer.Render();
        }

        public static void LoadStar()
        {
            for (int i = 0; i < stars.Length; i++)
            {
                starpoint[i] = new Point(rnd.Next(700), rnd.Next(700));
                stars[i] = new Star(starpoint[i], new Point(i, 0), new Size(2, 2));
            }
        }

        public static void DrawStar()
        {
            foreach (Star obj in stars) obj.Draw();
            buffer.Render();
        }

        public static void LoadStarImagePoints()
        {
            for (int i = 0; i < n; i++)
            {
                starpointimage[i] = new Point(rnd.Next(700), rnd.Next(700));
            }
        }

        public static void DrawStarImage()
        {
            for (int i = 0; i < n; i++)
            {
                buffer.Graphics.DrawImage(starImage, starpointimage[i].X, starpointimage[i].Y);
            }
            buffer.Render();
        }

        public static void StarUpdate()
        {
            foreach (Star obj in stars) obj.Update();
        }        
        
        public static void StarImageUpdate()
        {
            for (int i = 0; i < n; i++)
            {
                starpointimage[i].X = starpointimage[i].X - 2;
                if (starpointimage[i].X == 0) starpointimage[i].X = width;
            }            
        }

        public static void Update()
        {
            foreach (BaseObject obj in asteroid)
            {
                obj.Update();
            }
        }

        //таймер изменения состояний
        public static void Timer_Tick(object sender, EventArgs e)
        {
            
            DrawForm();
            DrawPlanet();
            DrawAsteroid();
            DrawStar();
            DrawStarImage();
            Update();
            StarImageUpdate();
            StarUpdate();           
            buffer.Dispose();
        }        
    }
}
