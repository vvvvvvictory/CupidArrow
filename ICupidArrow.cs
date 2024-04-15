using System;
using System.Windows;

namespace CupidArrow
{
  public interface ICupidArrow
  {
    /// <summary>
    ///   窗口移动
    /// </summary>
    event Action WindowLocationChanged;

    event Action WindowClosed;

    /// <summary>
    ///   获取Canvas中心在屏幕上的位置
    /// </summary>
    /// <returns></returns>
    Point GetCanvasCenterOfScreen();

    /// <summary>
    ///   获取MyHeart在屏幕上的坐标
    /// </summary>
    /// <returns></returns>
    Point GetMyHeartLocationOfScreen();

    /// <summary>
    ///   设置Lover在本地Canvas上的定位
    /// </summary>
    /// <param name="loverPointOfScreen">lover在另一个Canvas的屏幕坐标</param>
    void SetLoverLocation(Point loverPointOfScreen);

    /// <summary>
    ///   设置箭的旋转角度
    /// </summary>
    /// <param name="angel">旋转角度</param>
    void SetArrowRotateAngel(double angel);

    /// <summary>
    ///   关闭窗口
    /// </summary>
    void CloseWindow();

    /// <summary>
    ///   开始心脏跳动
    /// </summary>
    void CollisionEnter();

    /// <summary>
    ///   停止跳动
    /// </summary>
    void CollisionLeave();

    /// <summary>
    ///   设置箭头长度
    /// </summary>
    /// <param name="length"></param>
    void SetArrowLength(double length);

    void SetRomanticLines(string lines);
  }
}