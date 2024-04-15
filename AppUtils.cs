using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CupidArrow
{
  public class AppUtils
  {
    /// <summary>
    ///   应用心脏跳动动画
    /// </summary>
    /// <param name="image"></param>
    public static Storyboard CreateScaleAnimationUsingStoryboard(Image image)
    {
      var storyboard = new Storyboard();
      storyboard.RepeatBehavior = RepeatBehavior.Forever;
      storyboard.AutoReverse = true;

      // 创建x方向上的动画
      var scaleXAnimation = new DoubleAnimation {
        From = 1.0,
        To = 1.1, // 因为缩放是从500-550，所以等比就是1.0-1.1
        Duration = TimeSpan.FromSeconds(2)
      };

      Storyboard.SetTarget(scaleXAnimation, image);
      Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));

      // 创建y方向上的动画
      var scaleYAnimation = new DoubleAnimation {
        From = 1.0,
        To = 1.1,
        Duration = TimeSpan.FromSeconds(2)
      };
      Storyboard.SetTarget(scaleYAnimation, image);
      Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));

      storyboard.Children.Add(scaleXAnimation);
      storyboard.Children.Add(scaleYAnimation);

      return storyboard;
    }

    public static double CalculateAngleFromXAxis(Point point1, Point point2)
    {
      // 计算两点间的差值
      double deltaX = point2.X - point1.X;
      double deltaY = point2.Y - point1.Y;

      // 计算弧度
      double angleInRadians = Math.Atan2(deltaY, deltaX);

      // 转换为角度
      double angleInDegrees = angleInRadians * (180 / Math.PI);

      // 返回介于 -180 到 180 的角度
      return angleInDegrees;
    }

    public static double CalculateEuclideanDistance(Point point1, Point point2)
    {
      return Math.Sqrt(Square(point2.X - point1.X) + Square(point2.Y - point1.Y));
    }

    private static double Square(double x)
    {
      return x * x;
    }
  }
}