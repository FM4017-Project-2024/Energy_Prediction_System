using SkiaSharp;
using SkiaSharp.Views.Maui;
using Microsoft.Maui.Controls;
using SkiaSharp.Views.Maui.Controls;

namespace Energy_Prediction_System.Classes
{
    public class GaugeView : SKCanvasView
    {
        private float _value;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                InvalidateSurface();
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);

            // Set up the dimensions and paint
            var width = e.Info.Width;
            var height = e.Info.Height;
            var radius = Math.Min(width, height) / 2 - 20;

            // Draw the gauge arc
            var paint = new SKPaint
            {
                Color = SKColors.Gray,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 10
            };

            canvas.DrawCircle(width / 2, height / 2, radius, paint);

            // Draw the filled portion based on the value
            var startAngle = 135; // Start from the left
            var sweepAngle = 270 * (_value / 100); // Assuming value ranges from 0 to 100

            paint.Color = SKColors.Red;
            paint.StrokeWidth = 15;
            paint.Style = SKPaintStyle.Stroke;
            canvas.DrawArc(new SKRect(20, 20, width - 20, height - 20), startAngle, sweepAngle, false, paint);

            // Draw the needle
            var needleAngle = (sweepAngle + startAngle) * (Math.PI / 180);
            var needleLength = radius - 40;
            var needleX = (float)(width / 2 + needleLength * Math.Cos(needleAngle - Math.PI / 2));
            var needleY = (float)(height / 2 + needleLength * Math.Sin(needleAngle - Math.PI / 2));

            paint.Color = SKColors.Black;
            paint.StrokeWidth = 5;
            canvas.DrawLine(width / 2, height / 2, needleX, needleY, paint);
        }
    }
}
