using System;
using System.Drawing;

namespace PhotoEnhancer
{
    public class RotateTransformer : ITransformer<RotationParameters>
    {
        public Size ResultSize { get; private set; }

        Size originalSize;
        double angleInRadians;

        public void Initialize(Size size, RotationParameters parameters)
        {
            originalSize = size;
            angleInRadians = parameters.AngleInDegrees * Math.PI / 180;

            var absCos = Math.Abs(Math.Cos(angleInRadians));
            var absSin = Math.Abs(Math.Sin(angleInRadians));

            ResultSize = new Size(
                (int)(size.Width * absCos + size.Height * absSin),
                (int)(size.Width * absSin + size.Height * absCos));
        }

        public Point? MapOrSmth(Point point)
        {
            point = new Point(point.X - ResultSize.Width / 2, point.Y - ResultSize.Height / 2);

            var cos = Math.Cos(angleInRadians);
            var sin = Math.Sin(angleInRadians);

            var x = (int)(point.X * cos - point.Y * sin + originalSize.Width / 2);
            var y = (int)(point.X * sin + point.Y * cos + originalSize.Height / 2);

            if (x < 0 || x >= originalSize.Width || y < 0 || y >= originalSize.Height)
                return null;

            return new Point(x, y);
        }
    }
}
