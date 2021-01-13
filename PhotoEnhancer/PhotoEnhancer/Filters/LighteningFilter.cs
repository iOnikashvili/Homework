using System;

namespace PhotoEnhancer
{
    public class LighteningFilter : PixelFilter<LighteningParameters>
    {
        public override string ToString()
        {
            return "Осветление/затемнение";
        }

        public override Pixel ProcessPixel(Pixel originalPixel,
            LighteningParameters parameters)
        {
            return originalPixel * parameters.Coefficient;
        }    
    }
}
