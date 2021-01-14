using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEnhancer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();

            mainForm.AddFilter(new PixelFilter<LighteningParameters>(
                "Осветление/затемнение",
                (pixel, parameters) => pixel * parameters.Coefficient
                ));

            mainForm.AddFilter(new PixelFilter<EmptyParameters>(
                "Оттенки серого",
                (pixel, parameters) =>
                {
                    var chanel = 0.3 * pixel.R +
                                0.6 * pixel.G +
                                0.1 * pixel.B;

                    return new Pixel(chanel, chanel, chanel);
                }
                ));
            mainForm.AddFilter(new PixelFilter<SaturationParameters>(
                "Изменение насыщенности каналов",
                (pixel, parameters) =>
                {
                    double red, green, blue;
                    red = Pixel.Trim(parameters.RedChanel * pixel.R);
                    green = Pixel.Trim(parameters.GreenChanel * pixel.G);
                    blue = Pixel.Trim(parameters.BlueChanel * pixel.B);
                    return new Pixel(red, green, blue);
                }));

            mainForm.AddFilter(new TransformFilter(
                "Поворот на 90° по часовой стрелке",
                size => new Size(size.Height, size.Width),
                (point, size) => new Point(point.Y, size.Height - point.X -1)
                ));
            mainForm.AddFilter(new TransformFilter<ReversePerspectiveParameters>(
                "Сужение нижней части", new ReversePerspectiveTransformer()));
            Application.Run(mainForm);
        }
    }
}
    

