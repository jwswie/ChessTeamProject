using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ChessTeamProject
{
    public class Animation
    {
        public static void AnimatePiece(Image piece)
        {
            var fadeInAnimation = new DoubleAnimation
            {
                From = 0, // Starting transparency value (0 - transparent)
                To = 1, // Final transparency value (0 - visible)
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = new RepeatBehavior(2) // Repeat 3 times
            };

            piece.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation); // Changing the 'Opacity' property using the fadeInAnimation
        }

        public static void GetSideAnimation(string side, Grid MainGrid)
        {
            UIElementCollection figures = MainGrid.Children; // All Grid elements

            foreach (var figure in figures)
            {
                if (figure is Image)
                {
                    var image = (Image)figure;
                    if ((string)image.Tag == side) // Check if figure belongs to the specified side
                    {
                        AnimatePiece(image);
                    }
                }
            }
        }

        public static void SetFiguresClickability(string side, bool clickable, Grid MainGrid)
        {
            UIElementCollection figures = MainGrid.Children; // All Grid elements

            foreach (var figure in figures)
            {
                if (figure is Image)
                {
                    var image = (Image)figure;

                    if ((string)image.Tag == side) // Check if figure belongs to the specified side
                    {
                        image.IsEnabled = clickable;
                    }
                }
            }
        }
    }
}
