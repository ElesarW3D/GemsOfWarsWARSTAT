using System.IO;
using Emgu;
using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using ImageMagick;

class Programm
{
    public static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("not imput and output file");
            return;
        }

        var fileName = args[0];
        if (!File.Exists(fileName))
        {
            Console.WriteLine("not file exist");
            return ;
        }
        var outfileName = Path.GetFileName(fileName) +"_neg"+Path.GetExtension(fileName);
        var logPath = args[args.Length-2];
        var percentStr = args[args.Length-1];
        int percent;
        if (!int.TryParse(percentStr, out percent))
        {
            percent = 75;
        }
        File.WriteAllText( percent.ToString()+".txt","");
        //var str = "1.jpg";
        //UseContrast(fileName, outfileName);
        RemoveNonWhitePixels(fileName, outfileName);
        

        using (var teseract = new Tesseract(".\\Resource", "rus", OcrEngineMode.TesseractLstmCombined))
        {
            var inputImage = new Image<Bgr, byte>(outfileName);
            teseract.SetImage(inputImage);
            teseract.Recognize();
            var text = teseract.GetUTF8Text();
            try
            {
                File.WriteAllText(logPath, text);
                Console.WriteLine($"File {logPath} add succes");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        };
        //if (File.Exists(outfileName))
        //{
        //    File.Delete(outfileName);
        //    Console.WriteLine($"File {outfileName} has been deleted");
        //}
    }

    private static void UseContrast(string fileName, string str)
    {
        Image<Bgr, byte> image = new Image<Bgr, byte>(fileName);

        // Конвертация изображения в оттенки серого
        Image<Gray, byte> grayImage = image.Convert<Gray, byte>();

        // Применение гистограммного выравнивания
        grayImage._EqualizeHist();

        // Если хотите конвертировать обратно в изображение Bgr
        Image<Bgr, byte> contrastImage = grayImage.Convert<Bgr, byte>();
        System.Drawing.Bitmap bmp = contrastImage.ToBitmap();
        bmp.Save(str);

        // Освобождение ресурсов
        bmp.Dispose();
    }

    public static void RemoveNonWhitePixels(string inputImagePath, string outputImagePath, int percentage = 78)
    {
        using (MagickImage image = new MagickImage(inputImagePath))
        {
            
            // Преобразование изображения в черно-белый формат (градации серого)
            image.ColorType = ColorType.Grayscale;
           
            //// Изменение порога цвета, чтобы оставить только белый цвет (255)
            image.Threshold(new Percentage(78));
            image.ContrastStretch(new Percentage(percentage));
            image.Negate();
            // Сохранение результата в новый файл
            image.Write(outputImagePath);
        }
    }
}

