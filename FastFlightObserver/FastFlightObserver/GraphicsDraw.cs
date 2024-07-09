using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FastFlightObserver
{
    public class GraphicsDraw : IDrawable
    {
        public GraphicsDraw()
        {
            
        }

        public ICollection<ScreenPoint> Screen_Points { get; set; }
        public ScreenAndObserver ScreenAndObserver { get; set; }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (Screen_Points != null)
            {
                canvas.BlendMode = BlendMode.Normal;
                canvas.StrokeSize = 1;
                foreach (ScreenPoint point in Screen_Points)
                {
                    float xs = (float)((point.X / ScreenAndObserver.Screen_Width) + 0.5) * dirtyRect.Width;
                    float ys = (float)((point.Y / ScreenAndObserver.Screen_Height) + 0.5) * dirtyRect.Height;
                    canvas.StrokeColor = Color.FromRgb(0, 0, 0);
                    canvas.DrawLine(xs, ys, xs + 1f, ys + 1f);
                }
            }
        }
    }
}
