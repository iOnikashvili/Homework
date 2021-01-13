using System;

namespace PhotoEnhancer
{
    public class GrayscaleFilter : PixelFilter<EmptyParameters>
    {       
        public override string ToString()
        {
            return "Оттенки серого";
        }

        public override Pixel ProcessPixel(Pixel originalPixel,
            EmptyParameters parameters)
        {
            var chanel = 0.3 * originalPixel.R +
                        0.6 * originalPixel.G +
                        0.1 * originalPixel.B;

            return new Pixel(chanel, chanel, chanel);
        }
    }
}
