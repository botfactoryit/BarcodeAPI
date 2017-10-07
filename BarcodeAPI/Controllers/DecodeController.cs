using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ZXing;
using System.Drawing;
using ZXing.CoreCompat.System.Drawing;

namespace BarcodeAPI.Controllers
{
    [Route("/[controller]")]
    public class DecodeController : Controller
    {
        [HttpPost]
        public string Post(IFormFile file)
        {
            if (file == null)
            {
                return "";
            }

            // create a barcode reader instance
            IBarcodeReader reader = new ZXing.CoreCompat.System.Drawing.BarcodeReader()
            {
                AutoRotate = true,
                Options = new ZXing.Common.DecodingOptions()
                {
                    TryHarder = true,
                    PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.All_1D }
                }
            };

            // load a bitmap
            Bitmap bitmap = (Bitmap)Image.FromStream(file.OpenReadStream());
            BitmapLuminanceSource image = new BitmapLuminanceSource(bitmap);

            // detect and decode the barcode inside the bitmap
            Result result = reader.Decode(image);

            return result?.Text;
        }
    }
}
