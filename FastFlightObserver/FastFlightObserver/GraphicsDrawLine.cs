using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFlightObserver
{
    public class GraphicsDraw : IDrawable
    {
        public int time { get; set; } = 0;

        public 

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 6;
            canvas.DrawLine(100 + time, 100, 200 + time, 100);
            
        }
    }
}
