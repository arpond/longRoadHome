using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using uk.ac.dundee.arpond.longRoadHome.View;


// Modified From http://stackoverflow.com/questions/741956/pan-zoom-image
namespace PanAndZoom
{
    public class ZoomBorder : Border
    {
        private UIElement child = null;
        private Point origin;
        private Point start;
        public Point charLoc { get; set; }

        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }

        public override UIElement Child
        {
            get { return base.Child; }
            set
            {
                if (value != null && value != this.Child)
                    this.Initialize(value);
                base.Child = value;
            }
        }

        public void Initialize(UIElement element)
        {
            this.child = element;
            if (child != null)
            {
                TransformGroup group = new TransformGroup();
                ScaleTransform st = new ScaleTransform();
                st.ScaleX = 3;
                st.ScaleY = 3;
                group.Children.Add(st);
                TranslateTransform tt = new TranslateTransform();
                group.Children.Add(tt);
                child.RenderTransform = group;
                child.RenderTransformOrigin = new Point(0.0, 0.0);
                this.MouseWheel += child_MouseWheel;
                this.MouseLeftButtonDown += child_MouseLeftButtonDown;
                this.MouseLeftButtonUp += child_MouseLeftButtonUp;
                this.MouseMove += child_MouseMove;
                this.PreviewMouseRightButtonDown += new MouseButtonEventHandler(
                  child_PreviewMouseRightButtonDown);
            }
        }

        public void Reset()
        {
            if (child != null)
            {
                // reset zoom
                var st = GetScaleTransform(child);
                st.ScaleX = 3;
                st.ScaleY = 3;

                if (charLoc != null)
                {
                    var tt = GetTranslateTransform(child);
                    //tt.X = 0.0;
                    //tt.Y = 0.0;

                    //FrameworkElement uielem = (FrameworkElement)child;
                    var parentWindow = Window.GetWindow(child);
                    if (parentWindow != null)
                    {
                        tt.X = -charLoc.X * st.ScaleX + parentWindow.Width / 2; //- (uielem.Width/2);
                        tt.Y = -charLoc.Y * st.ScaleY + parentWindow.Height / 2; //- (uielem.Height/2);
                    }
                }
                else
                {
                    // reset pan
                    var tt = GetTranslateTransform(child);
                    tt.X = 0.0;
                    tt.Y = 0.0;
                }
            }
        }

        #region Child Events

        private void child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (child != null)
            {
                var st = GetScaleTransform(child);
                var tt = GetTranslateTransform(child);

                double zoom = e.Delta > 0 ? .2 : -.2;
                if (!(e.Delta > 0) && (st.ScaleX < .4 || st.ScaleY < .4))
                    return;

                Point relative = e.GetPosition(child);
                double abosuluteX;
                double abosuluteY;

                abosuluteX = relative.X * st.ScaleX + tt.X;
                abosuluteY = relative.Y * st.ScaleY + tt.Y;

                if (st.ScaleX + zoom < 3)
                {
                    st.ScaleX = 3;
                    st.ScaleY = 3;
                }
                else if (st.ScaleX + zoom > 4)
                {
                    st.ScaleX = 4;
                    st.ScaleY = 4;
                }
                else
                {
                    st.ScaleX += zoom;
                    st.ScaleY += zoom;
                }

                tt.X = abosuluteX - relative.X * st.ScaleX;
                tt.Y = abosuluteY - relative.Y * st.ScaleY;
            }
        }

        private void child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                var tt = GetTranslateTransform(child);
                start = e.GetPosition(this);
                origin = new Point(tt.X, tt.Y);
                this.Cursor = Cursors.Hand;
                child.CaptureMouse();
            }
        }

        private void child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                child.ReleaseMouseCapture();
                this.Cursor = Cursors.Arrow;
            }
        }

        void child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Reset();
        }

        private void child_MouseMove(object sender, MouseEventArgs e)
        {
            if (child != null)
            {
                if (child.IsMouseCaptured)
                {
                    var tt = GetTranslateTransform(child);
                    Vector v = start - e.GetPosition(this);
                    
                    double toX = e.GetPosition(this).X - start.X + origin.X;
                    double toY = e.GetPosition(this).Y - start.Y + origin.Y;
                    var st = GetScaleTransform(child);
                    double scaleValue = st.ScaleX;
                    var content = (FrameworkElement)child;

                    var rect = new Rect(child.RenderSize);
                    
                    double minToX = content.ActualWidth - (content.ActualWidth * scaleValue);
                    double minToY = content.ActualHeight - (content.ActualHeight* scaleValue);

                    // correct any invalid amounts:
                    if (toX > 0)
                        toX = 0;
                    else if (toX < minToX)
                        toX = minToX;

                    if (toY > 0)
                        toY = 0;
                    else if (toY < minToY)
                        toY = minToY;
                    
                    tt.X = toX;
                    tt.Y = toY;
                }
            }
        }

        #endregion
    }
}
