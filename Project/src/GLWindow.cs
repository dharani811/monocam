using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
namespace monoCAM
{
    public partial class GLWindow : Form
    {
        public Camera cam;

        public GLWindow()
        {
            InitializeComponent();
            cam = new Camera();
            GLPanel.InitializeContexts();
            Gl.glShadeModel(Gl.GL_SMOOTH);
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            Gl.glClearDepth(1.0f);
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glDepthFunc(Gl.GL_LEQUAL);
            Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST);
            GLWindow_Resize(this, new EventArgs());
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
        }

        private void GLWindow_Resize(object sender, EventArgs e)
        {
            float aspect = (float)GLPanel.Width / (float)GLPanel.Height;
            System.Console.WriteLine("resize: {0} x {1}, aspect= {2},cam.x={3}", GLPanel.Width, GLPanel.Height, aspect, cam.eye.X);
            
            // make sure height or width don't go to zero
            if (GLPanel.Width == 0)
                GLPanel.Width = 1;
            if (GLPanel.Height == 0)
                GLPanel.Height = 1;

            // Gl.glViewport(0, 0, GLPanel.Width, GLPanel.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glViewport(0, 0, GLPanel.Width, GLPanel.Height);
            /* gluPerspective( GLdouble	fovy,   field of view in degrees (y-direction)
			       GLdouble	aspect,             aspect ratio: (width/height)
			       GLdouble	zNear,              distance from viewer to near clipping plane
			       GLdouble	zFar )              distance from viewer to far clipping plane
            */
            Glu.gluPerspective(45d, aspect, 0.1d, 100.0d);
            Glu.gluLookAt(cam.eye.X, cam.eye.Y, cam.eye.Z, cam.cen.X, cam.cen.Y, cam.cen.Z, cam.up.x, cam.up.y, cam.up.z);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
        }

        private void GLWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            GLPanel.DestroyContexts();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GLWindow_Load(object sender, EventArgs e)
        {

        }

        private void GLPanel_Paint(object sender, PaintEventArgs e)
        {

            
            // gluLookAt
            // gluLookAt   	(  	 eyeX  , eyeY  , eyeZ  , centerX  , centerY  , centerZ  , upX  , upY  , upZ  )

            //Gl.glMatrixMode(Gl.GL_MODELVIEW);
            //Gl.glLoadIdentity();
            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            //Gl.glLoadIdentity();

            System.Console.WriteLine("Paint! {0}",cam.eye.X);
            // here's where test stuff go.
            Gl.glColor3f(1, 0, 0);
            Gl.glBegin(Gl.GL_TRIANGLES);						// Drawing Using Triangles
            Gl.glVertex3f(0.0f, 1.0f, 0.0f);				// Top
            Gl.glVertex3f(-1.0f, -1.0f, 0.0f);				// Bottom Left
            Gl.glVertex3f(1.0f, -1.0f, 0.0f);				// Bottom Right
            Gl.glEnd();							// Finished Drawing The Triangle


            // draw some coordinate axes
            Gl.glColor3f(1, 0, 0);
            Gl.glBegin(Gl.GL_LINES);						
            Gl.glVertex3f(0.0f, 0.0f, 0.0f);				
            Gl.glVertex3f(1.0f, 0.0f, 0.0f);				
            Gl.glEnd();

            Gl.glColor3f(0, 1, 0);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(0.0f, 0.0f, 0.0f);
            Gl.glVertex3f(0.0f, 1.0f, 0.0f);
            Gl.glEnd();

            Gl.glColor3f(0, 0, 1);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3f(0.0f, 0.0f, 0.0f);
            Gl.glVertex3f(0.0f, 0.0f, 1.0f);
            Gl.glEnd();

            Gl.glFinish();
            // Gl.glFlush();
        }



        private void GLPanel_KeyDown(object sender, KeyEventArgs e)
        {
            System.Console.WriteLine("you pressed: " + e.KeyCode);
            if (e.KeyCode == Keys.Oemplus)
                cam.zoom(-0.2);
            else if (e.KeyCode == Keys.OemMinus)
                cam.zoom(+0.2);

            // don't know if these are the correct ones to call here
            // doesn't seem to work correctly on my machine...
            GLWindow_Resize(this, null);
            GLPanel_Paint(this, null);
        }

        public class Camera
        {
            public Geo.Point eye;
            public Geo.Point cen;
            public Vector up;

            // camera position in spherical coordinates:
            private double _r;
            private double _theta;
            private double _fi;
            private const double r_minimum = 0.1;
            public Camera()
            {
                eye = new Geo.Point(0, 0, 0);
                cen = new Geo.Point(0, 0, 0);
                up = new Vector(0, 0, 1);

                _theta = 0.1;
                _fi = 1;
                _r = 3;
                recalc();

            }

            private void recalc()
            {
                eye.X = _r * Math.Cos(_theta) * Math.Sin(_fi);
                eye.Y = _r * Math.Sin(_theta) * Math.Sin(_fi);
                eye.Z = _r * Math.Cos(_fi);
            }

            public void zoom(double amount)
            {
                _r += amount;
                if (_r <= 0)
                    _r = r_minimum;
                recalc();
                System.Console.WriteLine("zoomed to r={0}", _r);

            }


        }



    } // end GLWindow class
}