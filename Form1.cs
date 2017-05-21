using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Animation_Test
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// Frames while standing
        /// </summary>
        /// 
        Bitmap Stand_1 = new Bitmap(Properties.Resources.Standing_1);
        Bitmap Stand_2 = new Bitmap(Properties.Resources.Standing_2);
        Bitmap Stand_3 = new Bitmap(Properties.Resources.Standing_3);
        Bitmap Stand_4 = new Bitmap(Properties.Resources.Standing_4);
        Bitmap Stand_5 = new Bitmap(Properties.Resources.Standing_5);
        /// <summary>
        /// Frames while walking
        /// </summary>
        /// 
        Bitmap Walk_1 = new Bitmap(Properties.Resources.Walk_1);
        Bitmap Walk_2 = new Bitmap(Properties.Resources.Walk_2);
        Bitmap Walk_3 = new Bitmap(Properties.Resources.Walk_3);
        Bitmap Walk_4 = new Bitmap(Properties.Resources.Walk_4);
        Bitmap Walk_5 = new Bitmap(Properties.Resources.Walk_5);
        Bitmap Walk_6 = new Bitmap(Properties.Resources.Walk_6);
        /// <summary>
        /// Control Keys
        /// </summary>
        /// 
        Keys moveRight = Keys.D, moveLeft = Keys.A, Jump = Keys.A, Crouch = Keys.C;
        /// <summary>
        /// Timer for walk animation
        /// </summary>
        /// 
        Timer walkTimer = new Timer();
        /// <summary>
        /// Timer for stand animation
        /// </summary>
        /// 
        Timer standTimer = new Timer();
        /// <summary>
        /// Timer for walking backwards
        /// </summary>
        /// 
        Timer Reverse_walkTimer = new Timer();
        /// <summary>
        /// The current standing frame to be displayed
        /// </summary>
        /// 
        Bitmap standingFrame;
        /// <summary>
        /// The current walkking frame to be displayed
        /// </summary>
        /// 
        Bitmap walkingFrame;
        /// <summary>
        /// The current backwards walking frame to be displayed
        /// </summary>
        Bitmap reverse_walkingFrame;
        /// <summary>
        /// An array containing all the standing frames
        /// </summary>
        /// 
        Bitmap[] Stand_Frames;
        /// <summary>
        /// An array containing all the walking frames
        /// </summary>
        /// 
        Bitmap[] Walk_Frames;
        /// <summary>
        ///  a number use to loop through the arrays of the frames to return a picture
        /// </summary>
        /// 
        int num = 0;
        /// <summary>
        /// a float number used to represent the X-Coordinate Position
        /// </summary>
        /// 
        float PositionX = 20;
        /// <summary>
        /// a float number used to represent the Y-Coordinate Position
        /// </summary>
        /// 
        float PositionY = 260;
        /// <summary>
        /// gets a pixel at a specific location of a bitmap ... will be used to make bitmaps transparent
        /// </summary>
        /// 
        Color tracer;


        public Form1()
        {
            InitializeComponent();
        }

        private void Standing_Animator()
        {
            standTimer.Interval = 100;
            standTimer.Tick += Timer1_Tick;
            standTimer.Start();
        }

        public Bitmap Stand_Frame2Draw()
        {
            Stand_Frames = new Bitmap[] { Stand_1, Stand_2, Stand_3, Stand_4, Stand_5};

            if (num < Stand_Frames.Length)
            {
                standingFrame = Stand_Frames[num];
                num++;
            }
            else
            {
                num = 0;
                standingFrame = Stand_Frames[num];
            }

            tracer = standingFrame.GetPixel(1, 1);
            standingFrame.MakeTransparent(tracer);

            return standingFrame;
        }
        /// <summary>
        /// Timer for walking animation
        /// </summary>
        private void Walking_Animator()
        {
            walkTimer.Interval = 105;
            walkTimer.Tick += Timer1_Tick;
            walkTimer.Start();
        }

        public Bitmap Walk_Frame2Draw()
        {

            Walk_Frames = new Bitmap[] { Walk_1, Walk_2, Walk_3, Walk_4, Walk_5, Walk_6 };

            if (num < Walk_Frames.Length)
            {
                walkingFrame = Walk_Frames[num];
                num++;
            }
            else
            {
                walkingFrame = Walk_1;
                num = 0;
            }

            tracer = walkingFrame.GetPixel(1, 1);
            walkingFrame.MakeTransparent(tracer);

            return walkingFrame;
        }
        /// <summary>
        /// Timer for reverse walking
        /// </summary>
        private void Reverse_Walking_Animator()
        {
            Reverse_walkTimer.Interval = 105;
            Reverse_walkTimer.Tick += Timer1_Tick;
            Reverse_walkTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Standing_Animator();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == moveRight)
            {
                standTimer.Stop();
                Reverse_walkTimer.Stop();
                Walking_Animator();
                PositionX += 2;

            } else if ( e.KeyCode == moveLeft)
            {
                standTimer.Stop();
                walkTimer.Stop();
                Reverse_Walking_Animator();
                PositionX -= 2;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == moveRight)
            {
                walkTimer.Stop();
                standTimer.Start();

            } else if (e.KeyCode == moveLeft)
            {
                Reverse_walkTimer.Stop();
                standTimer.Start();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           
            if (walkTimer.Enabled == true)
            {
                e.Graphics.DrawImage(Walk_Frame2Draw(), PositionX, PositionY);

            } else if (standTimer.Enabled == true)
            {
                e.Graphics.DrawImage(Stand_Frame2Draw(), PositionX, PositionY);

            } else if (Reverse_walkTimer.Enabled == true)
            {
                Bitmap reverseWalk = new Bitmap (Walk_Frame2Draw());
                reverseWalk.RotateFlip(RotateFlipType.Rotate180FlipY);
                e.Graphics.DrawImage(reverseWalk, PositionX, PositionY);
            }
        }
    }
}
