using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BrandNewShip
{
    static class Program
    {
        static void Main()
        {
            Form form = new Form();            
            form.Width = 700;
            form.Height = 700;
            Game.LoadAsteroid();
            Game.LoadStar();
            Game.LoadStarImagePoints();
            Game.Init(form);            
            Application.Run(form);                   
        }
    }
}
