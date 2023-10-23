using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PM2E1C76.controladores
{
    public class ArrayFotos
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource image = null;

            if(value !=null)
            {
                byte[] bytesImg = (byte[])value;
                var stream = new MemoryStream(bytesImg);
                image = ImageSource.FromStream(() => stream);
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
