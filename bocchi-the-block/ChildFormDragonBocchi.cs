using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bocchi_the_block
{
    public partial class ChildFormDragonBocchi :    Form
    {
        //for the actual picutre box dragging animation. dangling animation.
        private System.Windows.Forms.Timer animationTimer;

        // other stuff
        private bool isDragging;
        private Random random = new Random();
        private System.Windows.Forms.Timer timer;
        private int duration;
        private int mouseX;
        private int mouseY;
        private Point resizeStartPoint;
        private Rectangle originalBounds;
        private bool isResizing;
        private Point mouseDownLocation;

        //gif stuff 
        private Image gifImage;
        private System.Windows.Forms.Timer switchTimer;
        private int timesHitRecently;
        private System.Windows.Forms.Timer hitTimer;

        public ChildFormDragonBocchi(int pDuration)
        {
            InitializeComponent();
            InitializeSwitchTimer();
            InitializeHitTimer();
            duration = pDuration;
            InitPictureBox1();
            isDragging = false;
            Console.WriteLine("initalized");
            this.KeyPreview = true;
            this.KeyDown += ChildForm_KeyDown;

            // Initialize timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = duration * 1000;  // milliseconds
            timer.Tick += Timer_tick;


            // Remove top right rectangle and title
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true; 
        }

        private void ChildFormDragonBocchi_Load(object sender, EventArgs e)
        {
            //MoveRandom();
            //StartTimer();
        }
        private void InitPictureBox1()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.MouseDown += PictureBox_MouseDown;
            pictureBox1.MouseMove += PictureBox_MouseMove;
            pictureBox1.MouseUp += PictureBox_MouseUp;
            pictureBox1.Image = Properties.Resources.bocchi_stand_idle;
            this.BackColor = Color.FromArgb(0, 255, 0);

            // set green as transparent 
            this.TransparencyKey = Color.FromArgb(0, 255, 0);

            //init physics animation timer 
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 16; // Adjust the interval as needed (around 60 FPS)

            Controls.Add(pictureBox1);
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
                mouseDownLocation = e.Location;
            }

            Debug.WriteLine("PictureBox Mouse Down");
            Debug.WriteLine(e.Location.ToString());
            Debug.WriteLine(pictureBox1.Width.ToString() + pictureBox1.Height.ToString());
            // Check if the mouse is in the bottom-right corner of the PictureBox
            if (e.Button == MouseButtons.Left && e.Location.X >= pictureBox1.Width - 10
                && e.Location.Y >= pictureBox1.Height - 10)
            {
                Debug.WriteLine("In Corner");
                // Set the resizing flag and store the initial mouse position
                isResizing = true;
                resizeStartPoint = e.Location;
                originalBounds = pictureBox1.Bounds;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {

            if (isDragging)
            {
                Debug.WriteLine("DRAGGING");
                // Calculate the new position of the form
                int newX = this.Left + (e.X - mouseX);
                int newY = this.Top + (e.Y - mouseY);

                // Update the form's location
                this.Location = new Point(newX, newY);
            }
            Debug.WriteLine("PictureBox Mouse Move");
            // If resizing is in progress, update the PictureBox size based on mouse movements
            if (isResizing)
            {
                int deltaX = e.X - resizeStartPoint.X;
                int deltaY = e.Y - resizeStartPoint.Y;
                int newWidth = originalBounds.Width + deltaX;
                int newHeight = originalBounds.Height + deltaY;

                // Ensure that the PictureBox maintains a minimum size
                newWidth = Math.Max(newWidth, 50);
                newHeight = Math.Max(newHeight, 50);

                pictureBox1.SetBounds(originalBounds.X, originalBounds.Y, newWidth, newHeight);
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                if (IsClick(e.Location))
                {
                    timesHitRecently += 1;
                    if (!switchTimer.Enabled){
                        if(timesHitRecently >= 3)
                        {
                            switchTimer.Start();
                            pictureBox1.Image = Properties.Resources.bocchi_stand_2;
                        }
                        else
                        {
                            switchTimer.Start();
                            pictureBox1.Image = Properties.Resources.bocchi_stand;
                        }
                        Debug.WriteLine("BOCCHI CLICKED");

                    }
                }
                Debug.WriteLine("MOUSE UP");
                isDragging = false;
            }
            Debug.WriteLine("Picture Box Mouse Up");
            // Reset the resizing flag when the mouse button is released
            isResizing = false;
            animationTimer.Stop();
        }

        //Wrapper for click
        private bool IsClick(Point mouseUpLocation)
        {
            int deltaX = Math.Abs(mouseUpLocation.X - mouseDownLocation.X);
            int deltaY = Math.Abs(mouseUpLocation.Y - mouseDownLocation.Y);
            return (deltaX <= SystemInformation.DoubleClickSize.Width && deltaY <= SystemInformation.DoubleClickSize.Height);
        }

        // When each timer tick is up, call move random and restart timer
        private void Timer_tick(object sender, EventArgs e)
        {
            timer.Stop();
            MoveRandom();
            StartTimer();
        }


        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private void StartTimer()
        {
            MoveRandom();
            timer.Start();
        }

        //Chooses a random spot within screen bounds to put a new child form.
        private void MoveRandom()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            int width = bounds.Width;
            int height = bounds.Height;

            int maxX = width - this.Width;
            int maxY = height - this.Height;

            int nextX = random.Next(maxX);
            int nextY = random.Next(maxY);

            this.Location = new Point(nextX, nextY);
        }

        private void ChildForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Debug.WriteLine("Child CLOSED");
                this.Close();
            }
        }

        private void InitializeSwitchTimer()
        {
            switchTimer = new System.Windows.Forms.Timer();
            switchTimer.Interval = 1500;
            switchTimer.Tick += SwitchTimer_Tick;
        }

        private void InitializeHitTimer()
        {
            hitTimer = new System.Windows.Forms.Timer();
            hitTimer.Interval = 10000;
            hitTimer.Tick += HitTimer_Tick;
        }
        private void HitTimer_Tick(object sender, EventArgs e)
        {
            timesHitRecently = Math.Min(timesHitRecently - 1, 0);
        }

        private void SwitchTimer_Tick(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.bocchi_stand_idle;

            Debug.WriteLine("Bocchi Idle");
            // Stop the timer
            switchTimer.Stop();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}



