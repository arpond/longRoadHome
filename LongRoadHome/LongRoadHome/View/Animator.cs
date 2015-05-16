using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;

namespace uk.ac.dundee.arpond.longRoadHome.View
{
    public class Animator
    {
        private List<Image> frames;
        private Image currentFrame;
        private int frameNumber;
        private Timer animationTimer;
        private NextFrame nf;

        public Animator(List<Image> frames, NextFrame nf, int interval)
        {
            this.frames = frames;
            this.nf = nf;
            animationTimer = new Timer(interval);
            animationTimer.Elapsed += new ElapsedEventHandler(animationTimer_Tick);
            animationTimer.Elapsed += new ElapsedEventHandler(nf);
            currentFrame = frames[0];
            
        }
        public void StartAnimation()
        {
            frameNumber = 0;
            animationTimer.Start();
        }
        public void StopAnimation()
        {
            animationTimer.Stop();
        }

        private void animationTimer_Tick(object source, ElapsedEventArgs e)
        {
            if (frameNumber >= frames.Count)
            {
                frameNumber = 0;
            }
            currentFrame = frames[frameNumber];
            frameNumber++;
        }

        public Image GetCurrentFrame()
        {
            return currentFrame;
        }
    }

}
