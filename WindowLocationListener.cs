using System;
using System.Windows;
using System.Windows.Threading;

namespace CupidArrow
{
  public class WindowLocationListener
  {
    private static ICupidArrow _me;
    private static ICupidArrow _lover;

    public static void SetMe(ICupidArrow me)
    {
      if (_me != null) {
        return;
      }
      _me = me;
      _me.WindowLocationChanged += CupidArrowOnWindowLocationChanged;
      _me.WindowClosed += () => {
        _me = null;
        _lover?.CloseWindow();
      };
    }

    public static void SetLover(ICupidArrow lover)
    {
      if (_lover != null) {
        return;
      }
      _lover = lover;
      _lover.WindowLocationChanged += CupidArrowOnWindowLocationChanged;
      _lover.WindowClosed += () => {
        _lover = null;
        _me?.CloseWindow();
      };
    }

    private static void CupidArrowOnWindowLocationChanged()
    {
      if (_lover == null || _me == null) {
        return;
      }
      // 相互更新彼此 Lover的位置
      var loverHeartLocationOfScreen = _lover.GetMyHeartLocationOfScreen();
      var myHeartLocationOfScreen = _me.GetMyHeartLocationOfScreen();

      _me.SetLoverLocation(loverHeartLocationOfScreen);
      _lover.SetLoverLocation(myHeartLocationOfScreen);

      // 更新箭头角度
      var meArrowAngle =
        AppUtils.CalculateAngleFromXAxis(_me.GetCanvasCenterOfScreen(), _lover.GetCanvasCenterOfScreen());
      _me.SetArrowRotateAngel(meArrowAngle);
      _lover.SetArrowRotateAngel(meArrowAngle + 180.0);

      // 设置箭头长度
      var distance = AppUtils.CalculateEuclideanDistance(loverHeartLocationOfScreen, myHeartLocationOfScreen) + 800;
      _me.SetArrowLength(distance);
      _lover.SetArrowLength(distance);

      // 碰撞检测
      if (CollisionDetect(loverHeartLocationOfScreen, myHeartLocationOfScreen)) {
        _me.CollisionEnter();
        _lover.CollisionEnter();
      }
      else {
        _me.CollisionLeave();
        _lover.CollisionLeave();
      }
    }

    /// <summary>
    ///   条件：同时存在两个窗口，这里糟糕的设计
    /// </summary>
    public static void StartGenRomantic()
    {
      if (_me == null || _lover == null) {
        return;
      }

      var romanticLines = new RomanticLines();
      var timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromSeconds(5);
      timer.Tick += (sender, args) => {
        if (_me == null || _lover == null) {
          timer.Stop();
          return;
        }
        var next = romanticLines.GetNext();
        _me.SetRomanticLines(next);
        _lover.SetRomanticLines(next);
      };

      timer.Start();
    }

    private static bool CollisionDetect(Point p1, Point p2)
    {
      return Math.Abs(p1.X - p2.X) < 500 && Math.Abs(p1.Y - p2.Y) < 500;
    }
  }
}