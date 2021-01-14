using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public class SaturationFilter : PixelFilter<SaturationParameters>
    {
        public override string ToString()
        {
            return "Изменение насыщенности каналов";
        }

        public override Pixel ProcessPixel(Pixel originalPixel,
            SaturationParameters parameters)
        {
            double red, green, blue;
            red = Pixel.Trim(parameters.RedChanel * originalPixel.R);
            green = Pixel.Trim(parameters.GreenChanel * originalPixel.G);
            blue = Pixel.Trim(parameters.BlueChanel * originalPixel.B);
            return new Pixel(red, green, blue);
        }

    }
}
