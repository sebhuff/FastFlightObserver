using System.Linq;
using System.Timers;

namespace FastFlightObserver;

public partial class MainPage : ContentPage
{
    ScreenAndObserver ScreenAndObserver = new ScreenAndObserver()
    {
        Observer_Distance_From_Screen = 0.2,
        Screen_Width = 0.2,
        Screen_Height = 0.1
    };

    int Frame_Rate = 60;                                        // Frames per second

    double Run_Time = 0.01;
    double Run_Step = 0.000001;                                     // seconds

    public MainPage()
    {
        InitializeComponent();

        double rad = 5000;
        double distance = 100000;
        double orbit_rad = 40000;

        double orbit_speed = 1000;
        double spin_speed = 10000;

        PofT p1 = new PofT();
        p1.GetX = (double time) => orbit_rad * Math.Sin(time * orbit_speed) + rad * Math.Sin(time * spin_speed);
        p1.GetY = (double time) => 0;
        p1.GetZ = (double time) => distance + orbit_rad * Math.Cos(time * orbit_speed) + rad * Math.Cos(time * spin_speed);
        PointCollection.Points.Add(p1);

        PofT p2 = new PofT();
        p2.GetX = (double time) => orbit_rad * Math.Sin(time * orbit_speed) - rad * Math.Sin(time * spin_speed);
        p2.GetY = (double time) => 0;
        p2.GetZ = (double time) => distance + orbit_rad * Math.Cos(time * orbit_speed) - rad * Math.Cos(time * spin_speed);
        PointCollection.Points.Add(p2);

        PofT p3 = new PofT();
        p3.GetX = (double time) => orbit_rad * Math.Sin(time * orbit_speed);
        p3.GetY = (double time) => rad;
        p3.GetZ = (double time) => distance + orbit_rad * Math.Cos(time * orbit_speed);
        PointCollection.Points.Add(p3);

        PofT p4 = new PofT();
        p4.GetX = (double time) => orbit_rad * Math.Sin(time * orbit_speed);
        p4.GetY = (double time) => -rad;
        p4.GetZ = (double time) => distance + orbit_rad * Math.Cos(time * orbit_speed);
        PointCollection.Points.Add(p4);

        PofT p5 = new PofT();
        p5.GetX = (double time) => orbit_rad * Math.Sin(time * orbit_speed) + (rad * Math.Sin(time * spin_speed) * Math.Sqrt(0.5));
        p5.GetY = (double time) => rad * Math.Sqrt(0.5);
        p5.GetZ = (double time) => distance + orbit_rad * Math.Cos(time * orbit_speed) + (rad * Math.Cos(time * spin_speed) * Math.Sqrt(0.5));
        PointCollection.Points.Add(p5);

        PofT p6 = new PofT();
        p6.GetX = (double time) => orbit_rad * Math.Sin(time * orbit_speed) - (rad * Math.Sin(time * spin_speed) * Math.Sqrt(0.5));
        p6.GetY = (double time) => rad * Math.Sqrt(0.5);
        p6.GetZ = (double time) => distance + orbit_rad * Math.Cos(time * orbit_speed) - (rad * Math.Cos(time * spin_speed) * Math.Sqrt(0.5));
        PointCollection.Points.Add(p6);

        PofT p7 = new PofT();
        p7.GetX = (double time) => orbit_rad * Math.Sin(time * orbit_speed) + (rad * Math.Sin(time * spin_speed) * Math.Sqrt(0.5));
        p7.GetY = (double time) => -rad * Math.Sqrt(0.5);
        p7.GetZ = (double time) => distance + orbit_rad * Math.Cos(time * orbit_speed) + (rad * Math.Cos(time * spin_speed) * Math.Sqrt(0.5));
        PointCollection.Points.Add(p7);

        PofT p8 = new PofT();
        p8.GetX = (double time) => orbit_rad * Math.Sin(time * orbit_speed) - (rad * Math.Sin(time * spin_speed) * Math.Sqrt(0.5));
        p8.GetY = (double time) => -rad * Math.Sqrt(0.5);
        p8.GetZ = (double time) => distance + orbit_rad * Math.Cos(time * orbit_speed) - (rad * Math.Cos(time * spin_speed) * Math.Sqrt(0.5));
        PointCollection.Points.Add(p8);
    }

    PointCollection PointCollection = new PointCollection();

    System.Timers.Timer Timer;
    int Frame = 0;

    List<ScreenPoint> ScreenPoints = new List<ScreenPoint>();

    private void OnClick(object sender, EventArgs e)
    {
        // generate screen points for non-relativistic
        var gd = (graphics_view.Drawable as GraphicsDraw);
        gd.ScreenAndObserver = ScreenAndObserver;
        gd.Screen_Points = PointCollection.GenerateScreenPointsInfiniteC(0, ScreenAndObserver.Observer_Distance_From_Screen);

        // generate points for relativistic

        var gdr = (graphics_view_rel.Drawable as GraphicsDraw);
        gdr.ScreenAndObserver = ScreenAndObserver;

        ScreenPoints = PointCollection.GenerateScreenPoints(Run_Step, Run_Time, ScreenAndObserver.Observer_Distance_From_Screen).OrderBy(sp => sp.T).ToList();

        gdr.Screen_Points = new List<ScreenPoint>();

        while ((ScreenPoints.Count > 0) && (ScreenPoints.First().T < (Run_Step / 2)))
        {
            gdr.Screen_Points.Add(ScreenPoints.First());
            ScreenPoints.RemoveAt(0);
        }
        
        Frame = 0;

        Timer = new System.Timers.Timer(1000 / Frame_Rate);
        Timer.Elapsed += Timer_Elapsed;
        Timer.Start();
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        Frame++;

        double time = Frame * Run_Step;

        var gd = (graphics_view.Drawable as GraphicsDraw);
        gd.Screen_Points = PointCollection.GenerateScreenPointsInfiniteC(time, ScreenAndObserver.Observer_Distance_From_Screen);

        // refresh the infinity canvas
        graphics_view.Invalidate();

        var gdr = (graphics_view_rel.Drawable as GraphicsDraw);
        gdr.Screen_Points = new List<ScreenPoint>(ScreenPoints.TakeWhile(sp => sp.T < (time + Run_Step / 2)));
        ScreenPoints.RemoveAll(sp => sp.T < (time + Run_Step / 2));

        // refresh relativistic canvas
        graphics_view_rel.Invalidate();

        if (time >= Run_Time)
        {
            // time to stop
            Timer.Elapsed -= Timer_Elapsed;
            Timer.Stop();
        }
    }
}
