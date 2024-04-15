using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace CupidArrow
{
  /// <summary>
  ///   Lover.xaml 的交互逻辑
  /// </summary>
  public partial class Lover : Window, ICupidArrow
  {
    private Storyboard _loverStoryboard;
    private Storyboard _myStoryboard;

    public Lover()
    {
      InitializeComponent();
      InitializeStoryboard();
    }

    public event Action WindowLocationChanged;
    public event Action WindowClosed;

    public Point GetCanvasCenterOfScreen()
    {
      return X_Canvas.PointToScreen(new Point(X_Canvas.ActualWidth / 2, X_Canvas.ActualHeight / 2));
    }

    public Point GetMyHeartLocationOfScreen()
    {
      return X_Canvas.PointToScreen(new Point((X_Canvas.ActualWidth - X_Me.ActualWidth) / 2,
        (X_Canvas.ActualHeight - X_Me.ActualHeight) / 2));
    }

    public void SetLoverLocation(Point loverPointOfScreen)
    {
      var loverLocalPoint = X_Canvas.PointFromScreen(loverPointOfScreen);
      Canvas.SetLeft(X_Lover, loverLocalPoint.X);
      Canvas.SetTop(X_Lover, loverLocalPoint.Y);

      // 设置箭头长度
      X_LeftArrow.Width =
        AppUtils.CalculateEuclideanDistance(new Point(X_Canvas.ActualWidth / 2, X_Canvas.ActualHeight / 2),
          loverLocalPoint) + 800;
    }

    public void SetArrowRotateAngel(double angel)
    {
      X_ArrowRotateTransform.Angle = angel;
    }

    public void CloseWindow()
    {
      Canvas.SetLeft(X_CloseText, (X_Canvas.ActualWidth - X_CloseText.ActualWidth) / 2);
      Canvas.SetTop(X_CloseText, (X_Canvas.ActualHeight - X_CloseText.ActualHeight) / 2);
      X_Canvas.Visibility = Visibility.Hidden;
      X_CloseText.Visibility = Visibility.Visible;

      var timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromSeconds(3);
      timer.Tick += (sender, args) => {
        timer.Stop();
        Close();
      };
      timer.Start();
    }

    public void CollisionEnter()
    {
      _myStoryboard.Begin();
      _loverStoryboard.Begin();
    }

    public void CollisionLeave()
    {
      _myStoryboard.Stop();
      _loverStoryboard.Stop();
    }

    public void SetArrowLength(double length)
    {
      X_LeftArrow.Width = length;
    }

    public void SetRomanticLines(string lines)
    {
      X_RomanticLines.Text = lines;
    }

    private void InitializeStoryboard()
    {
      _myStoryboard = AppUtils.CreateScaleAnimationUsingStoryboard(X_Me);
      _loverStoryboard = AppUtils.CreateScaleAnimationUsingStoryboard(X_Lover);
    }

    private void X_Canvas_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      Canvas.SetLeft(X_Me, (X_Canvas.ActualWidth - X_Me.ActualWidth) / 2);
      Canvas.SetTop(X_Me, (X_Canvas.ActualHeight - X_Me.ActualHeight) / 2);
      Canvas.SetLeft(X_LeftArrow, X_Canvas.ActualWidth / 2 - 400);
      Canvas.SetTop(X_LeftArrow, X_Canvas.ActualHeight / 2 - 15);
      WindowLocationChanged?.Invoke();
    }

    private void Lover_OnLocationChanged(object sender, EventArgs e)
    {
      WindowLocationChanged?.Invoke();
    }

    private void Lover_OnClosed(object sender, EventArgs e)
    {
      WindowClosed?.Invoke();
    }
  }
}