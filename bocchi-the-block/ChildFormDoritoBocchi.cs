using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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
    public partial class ChildFormDoritoBocchi : Form
    {
        //for the actual picutre box dragging animation. dangling animation.
        private System.Windows.Forms.Timer animationTimer;

        private bool isSwinging;
        private float rotationAngle;
        private float rotationSpeed;
        private float rotationAcceleration;
        private const float MaxRotationSpeed = 10f;
        private const float RotationAccelerationStep = 0.5f;


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

        Random rand = new Random();

        private bool isDoritoDragging = false;
        private Point dragStartPosition;


        public ChildFormDoritoBocchi(int pDuration)
        {
            InitializeComponent();

            //maximize the stuff 
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            duration = pDuration;
            InitPictureBox1();
            isDragging = false;
            Console.WriteLine("initalized");
            this.KeyPreview = true;
            this.MouseDown += ChildForm_MouseDown;
            this.MouseUp += ChildForm_MouseUp;
            this.MouseMove += ChildForm_MouseMove;
            this.KeyDown += ChildForm_KeyDown;


            // Remove top right rectangle and title
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;

            this.Controls.Add(pictureBox1);
        }

        private void ChildFormDoritoBocchi_Load(object sender, EventArgs e)
        {
            //MoveRandom();
            //StartTimer();
            InitTimer();
            InitDorito();
        }

        private void InitTimer()
        {

            // Initialize timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = rand.Next(5000, 10000);
            timer.Tick += DoritoSpawnTimer_Tick;
            timer.Start();
        }

        private void InitDorito()
        {
            Debug.WriteLine("Dorito initialized");
            doritopictureBox.Image = Properties.Resources.dorito; // Add a banana image to your project resources
            doritopictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            doritopictureBox.MouseDown += dorito_MouseDown;
            doritopictureBox.MouseMove += dorito_MouseMove;
            doritopictureBox.MouseUp += dorito_MouseUp;
            doritopictureBox.Visible = false;
            this.Controls.Add(doritopictureBox);
        }

        private void DoritoSpawnTimer_Tick(object sender, EventArgs e)
        {
            timer.Interval = rand.Next(5000, 10000);
            RandomlyPositionDorito();
        }

        private void RandomlyPositionDorito()
        {
            Random random = new Random();
            Debug.WriteLine("SPAWNED DORITO");
            doritopictureBox.Visible = true;
            int x = random.Next(this.ClientSize.Width - doritopictureBox.Width);
            int y = random.Next(this.ClientSize.Height - doritopictureBox.Height);
            doritopictureBox.Location = new Point(x, y);
            doritopictureBox.BringToFront();
        }

        private void dorito_MouseDown(object sender, MouseEventArgs e)
        {
            // Check if the left mouse button is pressed
            if (e.Button == MouseButtons.Left)
            {
                isDoritoDragging = true;
                dragStartPosition = e.Location;
            }
        }

        private void dorito_MouseMove(object sender, MouseEventArgs e)
        {
            // Check if the control is being dragged
            if (isDoritoDragging)
            {
                // Calculate the new position of the control
                int deltaX = e.X - dragStartPosition.X;
                int deltaY = e.Y - dragStartPosition.Y;
                doritopictureBox.Location = new Point(doritopictureBox.Location.X + deltaX, doritopictureBox.Location.Y + deltaY);
            }
        }

        private void dorito_MouseUp(object sender, MouseEventArgs e)
        {
            if (doritopictureBox.Bounds.IntersectsWith(pictureBox1.Bounds))
            {
                // Banana dropped onto the target
                PlayAnimation();
                doritopictureBox.Visible = false;
            }
            timer.Start();
            isDoritoDragging = false;
        }

        private void PlayAnimation()
        {
            // TO DO
            Debug.WriteLine("DORITO!");
        }

        private void InitPictureBox1()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.MouseDown += PictureBox_MouseDown;
            pictureBox1.MouseMove += PictureBox_MouseMove;
            pictureBox1.MouseUp += PictureBox_MouseUp;
            pictureBox1.Image = Properties.Resources.bocchi_dorito;
            this.BackColor = Color.FromArgb(0, 255, 0);

            // set green as transparent 
            this.TransparencyKey = Color.FromArgb(0, 255, 0);

            //init physics animation timer 
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 16; // Adjust the interval as needed (around 60 FPS)
            animationTimer.Tick += AnimationTimer_Tick;

            Controls.Add(pictureBox1);
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
            }

            // Check if the mouse is in the bottom-right corner of the PictureBox
            if (e.Button == MouseButtons.Left && e.Location.X >= pictureBox1.Width - 10
                && e.Location.Y >= pictureBox1.Height - 10)
            {
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
                // Calculate the new position of the form
                int newX = pictureBox1.Left + (e.X - mouseX);
                int newY = pictureBox1.Top + (e.Y - mouseY);

                // Update the form's location
                pictureBox1.Location = new Point(newX, newY);
            }
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
                isDragging = false;
            }
            // Reset the resizing flag when the mouse button is released
            isResizing = false;
            animationTimer.Stop();
        }

        // When each timer tick is up, call move random and restart timer
        private void Timer_tick(object sender, EventArgs e)
        {
            timer.Stop();
            MoveRandom();
            StartTimer();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (isSwinging)
            {
                // Update the rotation angle based on current speed
                rotationAngle += rotationSpeed;

                // Apply acceleration to increase speed
                rotationSpeed += rotationAcceleration;

                // Apply maximum speed limit
                rotationSpeed = Math.Min(rotationSpeed, MaxRotationSpeed);

                // Apply damping to gradually reduce acceleration
                rotationAcceleration *= 0.95f;

                // Update the rotation matrix
                pictureBox1.Image = RotateImage(Properties.Resources.bocchi_1, rotationAngle);

                // Stop the animation when the rag doll reaches a certain rotation
                if (rotationAngle >= 360f)
                {
                    animationTimer.Stop();
                    isSwinging = false;
                }
            }
        }

        private Bitmap RotateImage(Image image, float angle)
        {
            Bitmap rotatedImage = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(image.Width / 2, image.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-image.Width / 2, -image.Height / 2);
                g.DrawImage(image, Point.Empty);
            }
            return rotatedImage;
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

        private void ChildForm_MouseDown(object sender, MouseEventArgs e)
        {
            // Check if the left mouse button is pressed
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void ChildForm_MouseMove(object sender, MouseEventArgs e)
        {
            // Check if the form is being dragged
            if (isDragging)
            {
                // Calculate the new position of the form
                int newX = this.Left + (e.X - mouseX);
                int newY = this.Top + (e.Y - mouseY);

                // Update the form's location
                this.Location = new Point(newX, newY);
            }
        }

        private void ChildForm_MouseUp(object sender, MouseEventArgs e)
        {
            // Check if the left mouse button is released
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void ChildForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}