using Android.Views.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFlightObserver
{
    public class GraphicsDraw : IDrawable
    {
        public GraphicsDraw()
        {
            spaceship.Points.Add(new PofT() { X = -14.285, Y = 0, Z = 50 });
        }

        public int time { get; set; } = 0;

        public Object2D spaceship = new Object2D();

        public void TranslatePointToScreen(Object2D ob)
        {
            double odfs = 0.7;              // observer distance from screen / meters
            double screen_width = 0.2;      // distance of portal/screen we are veiwing the spaceship through / meters
            double screen_height = 0.1;
            double simulation_time = 1;     // time the spaceship is in flight / seconds

            double view_width = 40;         // total observable width (distance spaceship can travel) / meters
            double view_height = 20;

            int canvas_width = 800;         // width in pixels of our canvas
            int canvas_height = 400;

            int number_of_frames = 1000;

            ICollection<ScreenPoint> screen_points = new List<ScreenPoint>();
            ICollection<ScreenPoint> relative_screen_points = new List<ScreenPoint>();

            foreach (PofT p in ob.Points)
            {


                double xsd = (p.X * odfs) / p.Z;
                double ysd = (p.Y * odfs) / p.Z;

                double r = Math.Sqrt((p.X * p.X) + (p.Y * p.Y) + (p.Z * p.Z));

                int xs = (int)((xsd * canvas_width)/ screen_width);
                int ys = (int)((ysd * canvas_height) / screen_height);

                screen_points.Add(new ScreenPoint() { X = xs, Y = ys, R = r });





                relative_screen_points.Add(new ScreenPoint() { X = xs, Y = ys, R = r });
            }
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 6;
            canvas.DrawLine(100 + time, 100, 200 + time, 100);
        }
    }
}
