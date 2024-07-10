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
                canvas.BlendMode = BlendMode.Multiply;
                canvas.StrokeSize = 2;
                SolidPaint black = new SolidPaint(Colors.Black);
                canvas.SetFillPaint(black, dirtyRect);
                canvas.FillRectangle(dirtyRect);
                foreach (ScreenPoint point in Screen_Points)
                {
                    float xs = (float)((point.X / ScreenAndObserver.Screen_Width) + 0.5) * dirtyRect.Width;
                    float ys = (float)((point.Y / ScreenAndObserver.Screen_Height) + 0.5) * dirtyRect.Height;
                    float hue = (float)(((point.F / (10 ^ 12)) - 400) / 300);
                    canvas.StrokeColor = Color.FromHsva(hue, 1f, 1f, 0.5f);
                    canvas.DrawLine(xs, ys, xs + 2f, ys + 2f);
                }
            }
        }
    }
}
