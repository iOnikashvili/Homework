using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhotoEnhancer
{
    class ReversePerspectiveTransformer : ITransformer<ReversePerspectiveParameters>
    {
        public Size ResultSize { get; set; }

        Size originalSize;
        double CuttedPart;
        public void Initialize(Size size, ReversePerspectiveParameters parameters)
        {
            originalSize = size;
            CuttedPart = parameters.CuttedPart;
            ResultSize = originalSize;
        }

        public Point? MapOrSmth(Point point)
        {
            {
                float x = point.X;
                float y = point.Y;
                float progress = 1.0f - y / ResultSize.Height;

                float lineZoom = (float)(100.0f / (100.0f - CuttedPart + CuttedPart * progress));
                x = x * lineZoom + ResultSize.Width * (1.0f - lineZoom) / 2.0f;

                if (x < 0 || x > originalSize.Width - 1)
                    return null;

                return new Point((int)x, (int)y);
            }

        }

    }   
}


