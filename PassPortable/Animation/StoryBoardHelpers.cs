using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace PassPortable
{
    public static class StoryBoardHelpers
    {
        public static void AddSlideFromRight(this Storyboard storyBoard, float seconds, double offset, float decelerationRatio = 0.9f)
        {
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(offset, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = 0.9f
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyBoard.Children.Add(animation);
        }

        public static void AddSlideToLeft(this Storyboard storyBoard, float seconds, double offset, float decelerationRatio = 0.9f)
        {
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                To = new Thickness(-offset, 0, offset, 0),
                From = new Thickness(0),
                DecelerationRatio = 0.9f
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyBoard.Children.Add(animation);
        }

        public static void AddFadeIn(this Storyboard storyBoard, float seconds)
        {
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 1
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            storyBoard.Children.Add(animation);
        }


        public static void AddFadeOut(this Storyboard storyBoard, float seconds)
        {
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 0
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            storyBoard.Children.Add(animation);
        }
    }
}
