using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace CupidArrow
{
  /// <summary>
  ///   MainWindow.xaml 的交互逻辑
  /// </summary>
  public partial class MainWindow : Window, ICupidArrow
  {
    private Storyboard _loverStoryboard;
    private Storyboard _myStoryboard;

    public MainWindow()
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
      // 设置位置
      var loverLocalPoint = X_Canvas.PointFromScreen(loverPointOfScreen);
      Canvas.SetLeft(X_Lover, loverLocalPoint.X);
      Canvas.SetTop(X_Lover, loverLocalPoint.Y);
    }

    public void SetArrowRotateAngel(double angel)
    {
      X_RotateTransform.Angle = angel;
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
      X_RightArrow.Width = length;
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

    // 当尺寸改变时，动态调整图片和箭头的位置
    private void X_Canvas_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      Canvas.SetLeft(X_Me, (X_Canvas.ActualWidth - X_Me.ActualWidth) / 2);
      Canvas.SetTop(X_Me, (X_Canvas.ActualHeight - X_Me.ActualHeight) / 2);
      Canvas.SetLeft(X_RightArrow, X_Canvas.ActualWidth / 2 - 400);
      Canvas.SetTop(X_RightArrow, X_Canvas.ActualHeight / 2 - 15);
      WindowLocationChanged?.Invoke();
    }

    private void X_GetLoverButton_OnClick(object sender, RoutedEventArgs e)
    {
      X_GetLover.Visibility = Visibility.Collapsed;
      var lover = new Lover();
      WindowLocationListener.SetMe(this);
      WindowLocationListener.SetLover(lover);
      WindowLocationListener.StartGenRomantic();
      X_GetLover.IsEnabled = false;
      X_Canvas.Visibility = Visibility.Visible;
      lover.Show();
    }

    private void MainWindow_OnLocationChanged(object sender, EventArgs e)
    {
      WindowLocationChanged?.Invoke();
    }

    private void MainWindow_OnClosed(object sender, EventArgs e)
    {
      WindowClosed?.Invoke();
    }
  }
}