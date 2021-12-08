using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.Storage.Streams;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using Windows.Graphics.Imaging;
using Microsoft.UI.Xaml.Media;

namespace WinKeg.UI.Converters
{
    public class ByteToImageConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is null)
                return null;

            byte[] imageData = (byte[])value;

            BitmapImage bi = new BitmapImage();
            MemoryStream ms = new MemoryStream();
            ms.Write(imageData, 0, imageData.Length);
            ms.Seek(0, SeekOrigin.Begin);
            bi.SetSource(ms.AsRandomAccessStream());
            return bi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }

        /*
        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            var img = new BitmapImage();
            var ms = new MemoryStream();
            ms.Write(imageByteArray, 0, imageByteArray.Length);
            img.SetSource(ms.AsRandomAccessStream());
            return img;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BitmapImage img = new BitmapImage();
            if (value != null)
            {
                img = this.ConvertByteArrayToBitMapImage(value as byte[]);
            }
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
        */
    }
}
