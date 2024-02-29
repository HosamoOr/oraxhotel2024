using HotelSys.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ZXing;
using ZXing.QrCode;

namespace HotelSys.BusnessLayer
{
    public class QRService
    {
      public Byte[] GeneralByte(string txt)
        {
            Byte[] byteArray;
            var width = 250; // width of the Qr Code   
            var height = 250; // height of the Qr Code   
            var margin = 0;
            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = margin
                }
            };
            var pixelData = qrCodeWriter.Write(txt);

            //https://github.com/micjahn/ZXing.Net
            //https://jeremylindsayni.wordpress.com/2016/04/02/how-to-read-and-create-barcode-images-using-c-and-zxing-net/

            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference   
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB   
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }
                    // save to stream as PNG   
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byteArray = ms.ToArray();
                }
            }

            return byteArray;

        }

        //مع حفظ الصورة في مجلد
        public QRViewModel GenerateFile(string qrText)
        {

            Byte[] byteArray;
            QRViewModel qRViewModel = new QRViewModel ();

        var width = 150; // width of the Qr Code   
            var height = 150; // height of the Qr Code   
            var margin = 0;
            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = margin
                }
            };
            var pixelData = qrCodeWriter.Write(qrText);

            //https://github.com/micjahn/ZXing.Net
            //https://jeremylindsayni.wordpress.com/2016/04/02/how-to-read-and-create-barcode-images-using-c-and-zxing-net/

            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference   
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB   
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    // save to folder
                    string fileGuid = Guid.NewGuid().ToString().Substring(0, 4);
                    bitmap.Save("wwwroot/qrr/file-" + fileGuid + ".png", System.Drawing.Imaging.ImageFormat.Png);

                    qRViewModel.src = "wwwroot/qrr/file-" + fileGuid + ".png";


                    // save to stream as PNG   
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byteArray = ms.ToArray();

                    qRViewModel.bytes= byteArray;
                }
            }
            return qRViewModel;
        }
        //تحويل صور الباركود الى نصوص 
        //قراءة البراركود
        public List<KeyValuePair<string, string>> ViewFile()
        {
            List<KeyValuePair<string, string>> fileData = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> data;
            string[] files = Directory.GetFiles("wwwroot/qrr");
            foreach (string file in files)
            {
                // create a barcode reader instance
                BarcodeReader reader = new BarcodeReader();
                // load a bitmap
                var barcodeBitmap = (Bitmap)Image.FromFile("wwwroot/qrr/" + Path.GetFileName(file));
                // detect and decode the barcode inside the bitmap

                var result = reader.Decode(barcodeBitmap);
                // do something with the result
                data = new KeyValuePair<string, string>(result.ToString(), "/qrr/" + Path.GetFileName(file));
                fileData.Add(data);

            }
            return fileData;
        }

    }
}
