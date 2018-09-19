using PassPortable.Properties;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Media;
using System.Windows;

namespace PassPortable.Helpers
{
    [ValueConversion(typeof(System.Drawing.Image), typeof(System.Windows.Media.ImageSource))]
    public sealed class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;



            BitmapImage bitmap = new BitmapImage();
            Image image = (System.Drawing.Image)Resources.NotSpecified;

            switch ((Site.Tag)value)
            {
                case Site.Tag.NotSpecified:
                    image = (System.Drawing.Image)Resources.NotSpecified;
                    break;
                case Site.Tag.Google:
                    image = (System.Drawing.Image)Resources.google;
                    break;
                case Site.Tag.TV:
                    image = (System.Drawing.Image)Resources.TV;
                    break;
                case Site.Tag.Web:
                    image = (System.Drawing.Image)Resources.web;
                    break;
            }
       
            MemoryStream memoryStream = new MemoryStream();

            image.Save(memoryStream, ImageFormat.Png);
            memoryStream.Position = 0;
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            //memoryStream.Seek(0, SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.DecodePixelHeight = 20;
            bitmap.DecodePixelWidth = 20;
            bitmap.EndInit();

            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
