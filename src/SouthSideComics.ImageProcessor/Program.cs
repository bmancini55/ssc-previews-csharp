using System;
using Microsoft.AspNet.FileProviders;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace SouthSideComics.ImageProcessor
{
    public class Program
    {
        IFileProvider SourceProvider { get; set; }        
        IFileProvider TargetProvider { get; set; }
        string Source { get; set; }
        string Target { get; set; }        

        public void Main(string[] args)
        {
            if(args.Length != 2)
            {
                Console.WriteLine("Requires input and output folders");
                return;
            }

            Source = args[0];
            Target = args[1];            
            SourceProvider = new PhysicalFileProvider(Source);
            TargetProvider = new PhysicalFileProvider(Target);            

            Process();
            Console.ReadKey();   
        }       

        void Process()
        {            
            var directoryContents = SourceProvider.GetDirectoryContents("/");
            foreach(var file in directoryContents)
            {
                var sourcePath = file.PhysicalPath;
                var destinationFolder = GetNestedFolder(file.Name); 
                var destinationPath = destinationFolder + "\\" + file.Name;                
                using (var image = new Bitmap(sourcePath))
                {
                    CreateFolderCascade(destinationFolder);                    
                    CreateImage(image, GetDestinationPath(destinationPath, "_720"), 720);
                    CreateImage(image, GetDestinationPath(destinationPath, "_263"), 263);
                    CreateImage(image, GetDestinationPath(destinationPath, "_213"), 213);
                    CreateImage(image, GetDestinationPath(destinationPath, "_158"), 158);
                }
            }            
        }

        void CreateFolderCascade(string filePath)
        {
            var parts = filePath.Split('\\');
            var processedParts = new List<string>();
            foreach(var part in parts)
            {
                processedParts.Add(part);
                var newDirectory = string.Join("\\", processedParts);

                if (!Directory.Exists(newDirectory))
                {
                    Directory.CreateDirectory(newDirectory);
                    Console.WriteLine("Creating " + newDirectory);
                }
            }               
        }
        
        string GetNestedFolder(string file)
        {
            var CHUNK_LENGTH = 3;
            var fileName = file.Split('.')[0];
            var remaining = fileName;
            var parts = new List<string>();
            while (remaining.Length >= CHUNK_LENGTH)
            {
                parts.Add(remaining.Substring(0, CHUNK_LENGTH));
                remaining = remaining.Substring(CHUNK_LENGTH);
            }
            return Path.Combine(Target, string.Join("\\", parts));
        }

        string GetDestinationPath(string original, string suffix)
        {
            var parts = original.Split('.');
            return parts[0] + suffix + "." + parts[1];
        }        

        void CreateImage(Image image, string destinationPath, int newWidth)
        {            
            int newHeight = 0;
            double resizeFactor = 0.0;
            if(image.Width > newWidth)
            {
                resizeFactor = (double)image.Width / (double)newWidth;
                newHeight = (int)((double)image.Height / resizeFactor);
            }
            else
            {
                newWidth = image.Width;
                newHeight = image.Height;
                resizeFactor = 1.0;
            }

            var codecs = ImageCodecInfo.GetImageDecoders();
            var jpegCodec = (ImageCodecInfo)null;
            foreach (var codec in codecs)
            {
                if (codec.FormatID == ImageFormat.Jpeg.Guid)
                {
                    jpegCodec = codec;
                }
            }

            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 90L);

            using (var newImage = new Bitmap(image, new Size(newWidth, newHeight)))
            {
                newImage.SetResolution(72, 72);
                newImage.Save(destinationPath, jpegCodec, encoderParameters);
                newImage.Dispose();
            }

            Console.WriteLine("Created: " + destinationPath);
        }

       

        // create output directory based on file-name

        // 

        // process existing files



        // reorganize existing files into appropriate folders        
    }
}
