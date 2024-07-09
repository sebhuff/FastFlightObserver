using System.Timers;

namespace FastFlightObserver;

public partial class MainPage : ContentPage
{
    ScreenAndObserver ScreenAndObserver = new ScreenAndObserver()
    {
        Observer_Distance_From_Screen = 0.7,
        Screen_Width = 0.2,
        Screen_Height = 0.1
    };

    int Video_Length = 10;                                       // seconds
    int Frame_Rate = 50;                                        // Frames per second

    double Start_Time = 0;
    double Motion_Step = 0.8;                                     // seconds

    public MainPage()
    {
        InitializeComponent();

        PofT p1 = new PofT();
        p1.GetX = (double time) => 10 * Math.Sin(time / 20);
        p1.GetY = (double time) => 0;
        p1.GetZ = (double time) => 200 + 10 * Math.Cos(time / 20);
        PointCollection.Points.Add(p1);

        PofT p2 = new PofT();
        p2.GetX = (double time) => 30 * Math.Sin(time / 20);
        p2.GetY = (double time) => 0;
        p2.GetZ = (double time) => 200 + 30 * Math.Cos(time / 20);
        PointCollection.Points.Add(p2);

        PofT p3 = new PofT();
        p3.GetX = (double time) => 20 * Math.Sin(time / 20);
        p3.GetY = (double time) => 10;
        p3.GetZ = (double time) => 200 + 20 * Math.Cos(time / 20);
        PointCollection.Points.Add(p3);

        PofT p4 = new PofT();
        p4.GetX = (double time) => 20 * Math.Sin(time / 20);
        p4.GetY = (double time) => -10;
        p4.GetZ = (double time) => 200 + 20 * Math.Cos(time / 20);
        PointCollection.Points.Add(p4);
    }

    PointCollection PointCollection = new PointCollection();

    System.Timers.Timer Timer;
    int Frame = 0;

    private void OnClick(object sender, EventArgs e)
    {
        // generate screen points from points
        var gd = (graphics_view.Drawable as GraphicsDraw);
        gd.ScreenAndObserver = ScreenAndObserver;
        gd.Screen_Points = PointCollection.GenerateScreenPointsInfiniteC(0, ScreenAndObserver.Observer_Distance_From_Screen);

        Frame = 0;

        Timer = new System.Timers.Timer(1000 / Frame_Rate);
        Timer.Elapsed += Timer_Elapsed;
        Timer.Start();
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        Frame++;

        double time = Start_Time + (Frame * Motion_Step);

        var gd = (graphics_view.Drawable as GraphicsDraw);
        gd.Screen_Points = PointCollection.GenerateScreenPointsInfiniteC(time, ScreenAndObserver.Observer_Distance_From_Screen);

        graphics_view.Invalidate();

        if (Frame >= (Video_Length * Frame_Rate))
        {
            Timer.Elapsed -= Timer_Elapsed;
            Timer.Stop();
        }
    }
}
