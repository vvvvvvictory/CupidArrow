using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CupidArrow
{
  /// <summary>
  ///   按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
  ///   步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
  ///   将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
  ///   元素中:
  ///   xmlns:MyNamespace="clr-namespace:CupidArrow"
  ///   步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
  ///   将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
  ///   元素中:
  ///   xmlns:MyNamespace="clr-namespace:CupidArrow;assembly=CupidArrow"
  ///   您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
  ///   并重新生成以避免编译错误:
  ///   在解决方案资源管理器中右击目标项目，然后依次单击
  ///   “添加引用”->“项目”->[浏览查找并选择此项目]
  ///   步骤 2)
  ///   继续操作并在 XAML 文件中使用控件。
  ///   <MyNamespace:LeftArrow />
  /// </summary>
  public class LeftArrow : Control
  {
    public static readonly DependencyProperty LeftArrowFillProperty = DependencyProperty.Register(nameof(LeftArrowFill),
      typeof(Brush),
      typeof(LeftArrow), new PropertyMetadata(Brushes.Black));

    // Using a DependencyProperty as the backing store for ArrowWidth.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LeftArrowWidthProperty =
      DependencyProperty.Register(nameof(LeftArrowWidth), typeof(double), typeof(LeftArrow),
        new PropertyMetadata(5.0, OnArrowWidthChanged, CoerceArrowWidth));

    // Using a DependencyProperty as the backing store for ShaftWidth.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LeftShaftWidthProperty =
      DependencyProperty.Register(nameof(LeftShaftWidth), typeof(double), typeof(LeftArrow),
        new PropertyMetadata(10.0, OnShaftWidthChanged, CoerceShaftWidth));

    static LeftArrow()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(LeftArrow), new FrameworkPropertyMetadata(typeof(LeftArrow)));
    }

    public Brush LeftArrowFill {
      get => (Brush)GetValue(LeftArrowFillProperty);
      set => SetValue(LeftArrowFillProperty, value);
    }

    public double LeftArrowWidth {
      get => (double)GetValue(LeftArrowWidthProperty);
      set => SetValue(LeftArrowWidthProperty, value);
    }

    public double LeftShaftWidth {
      get => (double)GetValue(LeftShaftWidthProperty);
      set => SetValue(LeftShaftWidthProperty, value);
    }

    private static object CoerceArrowWidth(DependencyObject d, object basevalue)
    {
      return Math.Min((double)basevalue, (double)d.GetValue(WidthProperty));
    }

    private static void OnArrowWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((LeftArrow)d).UpdateControlPoint();
    }

    private static object CoerceShaftWidth(DependencyObject d, object basevalue)
    {
      return Math.Min((double)basevalue, (double)d.GetValue(WidthProperty));
    }

    private static void OnShaftWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((LeftArrow)d).UpdateControlPoint();
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      if (!(Template.FindName("PART_LeftArrow", this) is Path leftArrow)) {
        return;
      }
      var geometry = new PathGeometry();
      leftArrow.Data = geometry;

      _startPoint.IsClosed = true;
      UpdateControlPoint();

      geometry.Figures.Add(_startPoint);
      _startPoint.Segments.Add(_top0);
      _startPoint.Segments.Add(_top1);
      _startPoint.Segments.Add(_top2);
      _startPoint.Segments.Add(_bottom0);
      _startPoint.Segments.Add(_bottom1);
      _startPoint.Segments.Add(_bottom2);
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == WidthProperty || e.Property == HeightProperty) {
        UpdateControlPoint();
      }
    }

    private void UpdateControlPoint()
    {
      _startPoint.StartPoint = new Point(0, Height / 2);
      _top0.Point = new Point(LeftArrowWidth, 0);
      _top1.Point = new Point(LeftArrowWidth, Height / 2 - LeftShaftWidth / 2);
      _top2.Point = new Point(Width, Height / 2 - LeftShaftWidth / 2);
      _bottom0.Point = new Point(Width, Height / 2 + LeftShaftWidth / 2);
      _bottom1.Point = new Point(LeftArrowWidth, Height / 2 + LeftShaftWidth / 2);
      _bottom2.Point = new Point(LeftArrowWidth, Height);
    }

    #region Control Point
    private PathFigure _startPoint = new PathFigure();
    private LineSegment _top0 = new LineSegment();
    private LineSegment _top1 = new LineSegment();
    private LineSegment _top2 = new LineSegment();
    private LineSegment _bottom0 = new LineSegment();
    private LineSegment _bottom1 = new LineSegment();
    private LineSegment _bottom2 = new LineSegment();
    #endregion
  }
}