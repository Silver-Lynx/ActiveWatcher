using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveWatcher
{
    class Utility
    {
        public static Image imageFromString(string s)
        {
            //Parse encoded icon into Image object using stream reading
            byte[] iconData = Convert.FromBase64String(s);

            //Make stream and fill with string
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(iconData);

            //Move pointer to start of stream
            writer.Flush();
            stream.Position = 0;

            //Load stream as image
            Image output = Bitmap.FromStream(stream);

            //Dispose objects
            stream.Dispose();
            writer.Dispose();

            return output;
        }

        public static string stringFromImage(Image i)
        {
            //Make memory stream and save image into it
            MemoryStream stream = new MemoryStream();
            i.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            //Load memory stream as raw bytes
            stream.Position = 0;
            byte[] data = stream.ToArray();

            //Convert raw bytes to string
            string output = Convert.ToBase64String(data);

            //Dispose stream object
            stream.Dispose();

            return output;
        }
    }
}
