using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{

    class Class1 : Form
    {
        private Ball b1;
        private Cart c1;
        private Image im;
        private int t;
        private bool isOver;
        private bool isIn;
        private int dx, dy;
        private Label lb;
        private string s;

        public static void Main()
        {
            Application.Run(new Class1());

        }
        public Class1()
        {
            this.Text = "サンプル";
            this.ClientSize = new Size(600, 300);
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
                t = 0;
             
             lb = new Label();
            lb.Dock = DockStyle.Top;
           
            lb.Parent = this;

            im = Image.FromFile("C:\\mark\\1280px-Sky.bmp");
            isOver = false;
            isIn = false;

            b1 = new Ball();
            b1.image = Image.FromFile("C:\\mark\\apple3.png");
            b1.point = new Point(0, 0);

            c1 = new Cart();
            c1.image = Image.FromFile("C:\\mark\\3210-300x300.jpg");
            c1.point = new Point(this.ClientSize.Width / 2, this.ClientSize.Height - 80);

            dx = 4;
            dy = 4;
            Timer tm = new Timer();
            tm.Interval = 15;
            tm.Start();

            this.Paint += new PaintEventHandler(pe);
            tm.Tick += new EventHandler(te);
            this.KeyDown += new KeyEventHandler(ke);
        }
        public void pe(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(im, 0, 0, im.Width, im.Height);
            Point bp = b1.point;
            Image bim = b1.image;
            g.DrawImage(bim, bp.X, bp.Y, bim.Width, bim.Height);
            Point cp = c1.point;
            Image cim = c1.image;
            g.DrawImage(cim, cp.X, cp.Y, cim.Width, cim.Height);

            if (isOver == true)
            {
                Font f = new Font("SansSerif", 30);
                SizeF sf = g.MeasureString("Game Over", f);
                float fx = (this.ClientSize.Width - sf.Width) / 2;
                float fy = (this.ClientSize.Height - sf.Height) / 2;
                g.DrawString("Game Over", f, new SolidBrush(Color.Blue), fx, fy);
            }

        }
        public void te(object sender, EventArgs e)
        {
            Point bp = b1.point;
            Point cp = c1.point;
            Image cim = c1.image;
            Image bim = b1.image;

            if (bp.X < 0 || bp.X > this.ClientSize.Width - bim.Width)
            {
                dx = -dx;
            }
            if (bp.Y < 0)
            {
                dy = -dy;
            }
            if ((isIn == false) && (bp.X>=cp.X-bim.Width&&bp.X<=cp.X+cim.Width)&&(bp.Y>cp.Y-bim.Height&&bp.Y<cp.Y-bim.Height/2))
            {
                isIn = true;
                dy = -dy;
                t++;
                s = t.ToString();
                
                lb.Text = s;

            }
            if (bp.Y < cp.Y - bim.Height)
            {
                isIn = false;
            }
            if (bp.Y > this.ClientSize.Height)
            {
                isOver = true;
                Timer t = (Timer)sender;
                t.Start();
            }
            bp.X = bp.X + dx;
            bp.Y = bp.Y + dy;

            b1.point = bp;
            this.Invalidate();

        }
        public void ke(object sender, KeyEventArgs e)
        {
            Point cp = c1.point;
            Image cim = c1.image;

            if (e.KeyCode == Keys.Right)
            {
                cp.X = cp.X + 15;
                if (cp.X > this.ClientSize.Width - cim.Width)
                {
                    cp.X = this.ClientSize.Width - cim.Width;
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                cp.X = cp.X - 15;
                if (cp.X < 0)
                {
                    cp.X = 0;
                }
            }

            c1.point = cp;
            this.Invalidate();


        }

        public class Ball
        {
            public Image image;
            public Point point;

        }
        public class Cart
        {
            public Image image;
            public Point point;
        }
    }
}


