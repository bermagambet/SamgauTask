using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalClocks
{
    public partial class Form1 : Form
    {
        Regex rgx = new Regex(@"^[0-9]{2}:[0-9]{2}:[0-9]{2}$");
        Regex rgx2 = new Regex(@"^[0][0-9]$");
        Timer t = new Timer();
        int WIDTH = 300, HEIGHT = 300, secHAND = 140, minHAND = 110, hrHAND = 80;
        // in center   
        int cy, cx;
        Bitmap bmp;
        Graphics cg;

        int ss = DateTime.Now.Second;
        int mm = DateTime.Now.Minute;
        int hh = DateTime.Now.Hour;

        public Form1()
        {

            InitializeComponent();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            

            label2.Visible = false;
            string givenTime = textBox1.Text;

            if (givenTime.Equals(""))
            {
                label2.Visible = true;
                return;
            }

                if (!rgx.IsMatch(givenTime))
            {
                label2.Visible = true;
                return;
            }
            string[] hrsMnsScnds = givenTime.Split(':');
            for(int i = 0; i < hrsMnsScnds.Length; i++)
            {
                if(rgx2.IsMatch(hrsMnsScnds[i]))
                {
                    hrsMnsScnds[i].Remove(0, 1);
                }
            }

            this.hh = int.Parse(hrsMnsScnds[0]);
            this.mm = int.Parse(hrsMnsScnds[1]);
            this.ss = int.Parse(hrsMnsScnds[2]);
            
            if((hh < 0 && hh > 24) || (mm < 0 && mm > 60) || (ss < 0 && ss > 60))
            {
                label2.Visible = true;
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(WIDTH + 1, HEIGHT + 1);
            cx = WIDTH / 2;
            cy = HEIGHT / 2;
            this.BackColor = Color.White;
            t.Interval = 1000; 
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }
        private void t_Tick(object sender, EventArgs e)
        { 
            cg = Graphics.FromImage(bmp);
            int[] handCoord = new int[2];
            cg.Clear(Color.White);
            cg.DrawEllipse(new Pen(Color.Black, 6.0f), 0, 0, WIDTH, HEIGHT);
            cg.DrawString("12", new Font("Ariel", 12), Brushes.Black, new PointF(140, 3));
            cg.DrawString("1", new Font("Ariel", 12), Brushes.Black, new PointF(218, 22));
            cg.DrawString("2", new Font("Ariel", 12), Brushes.Black, new PointF(263, 70));
            cg.DrawString("3", new Font("Ariel", 12), Brushes.Black, new PointF(285, 140));
            cg.DrawString("4", new Font("Ariel", 12), Brushes.Black, new PointF(263, 212));
            cg.DrawString("5", new Font("Ariel", 12), Brushes.Black, new PointF(218, 259));
            cg.DrawString("6", new Font("Ariel", 12), Brushes.Black, new PointF(142, 279));
            cg.DrawString("7", new Font("Ariel", 12), Brushes.Black, new PointF(70, 259));
            cg.DrawString("8", new Font("Ariel", 12), Brushes.Black, new PointF(22, 212));
            cg.DrawString("9", new Font("Ariel", 12), Brushes.Black, new PointF(1, 140));
            cg.DrawString("10", new Font("Ariel", 12), Brushes.Black, new PointF(22, 70));
            cg.DrawString("11", new Font("Ariel", 12), Brushes.Black, new PointF(70, 22));
            
            handCoord = msCoord(ss, secHAND);
            cg.DrawLine(new Pen(Color.Red, 2.0f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));
            handCoord = msCoord(mm, minHAND);
            cg.DrawLine(new Pen(Color.Black, 3.0f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));
            handCoord = hrCoord(hh % 12, mm, hrHAND);
            cg.DrawLine(new Pen(Color.Black, 3.0f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));  
            pictureBox1.Image = bmp;
            this.Text = "Analog Clock - " + hh + ":" + mm + ":" + ss;
            cg.Dispose();
        }
        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val = val * 6;
            if (val >= 0 && val <= 100)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
        //coord for hour   
        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];
            //each hour makes 60 degree with min making 0.5 degree   
            int val = (int)((hval * 30) + (mval * 0.5));
            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;




        }
    }
}
