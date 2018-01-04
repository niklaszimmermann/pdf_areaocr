using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;

//else PowerShell doesn't find the referenced assemblies.
using Rectangle = iTextSharp.text.Rectangle;
using PdfReader = iTextSharp.text.pdf.PdfReader;
using RegionTextRenderFilter = iTextSharp.text.pdf.parser.RegionTextRenderFilter;
using ITextExtractionStrategy = iTextSharp.text.pdf.parser.ITextExtractionStrategy;
using FilteredTextRenderListener = iTextSharp.text.pdf.parser.FilteredTextRenderListener;
using LocationTextExtractionStrategy = iTextSharp.text.pdf.parser.LocationTextExtractionStrategy;
using PdfTextExtractor = iTextSharp.text.pdf.parser.PdfTextExtractor;


namespace tutorial_namespace
{
    public static class tutorial_class
    {
        //
        //THE PROVIDED METHODS
        //

        //creates an rectangle-object (area which OCR is used on)
        //receives 4 float variables
        //pos1 and pos2 represent the coordinates of the lower left corner of the rectangle
        //pos3 and pos4 represent the coordinates of the upper right corner of the rectangle
        public static Rectangle createRect(float pos1, float pos2, float pos3, float pos4)
        {
            iTextSharp.text.Rectangle output_rectangle = new Rectangle(pos1, pos2, pos3,pos4);
            return output_rectangle;
        }

        //receives an array of rectangle-objects (areas) and the filepath of the pdf 
        //loops through each entry(area) in the provided array and writes an entry(OCR-read content) into a stringArray, which is returned 
        public static string[] readRect(Rectangle[] rectarray, string inputpdf_path)
        {
            int outputarraylength = rectarray.Length; 
            string[] output = new string[outputarraylength];
            int counter = 0;

            PdfReader pdfreader = new PdfReader(inputpdf_path);
            
            foreach (Rectangle rectangle in rectarray)
            {
                RegionTextRenderFilter renderfilter = new RegionTextRenderFilter(rectangle);
                ITextExtractionStrategy tes = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), renderfilter);
                output[counter] = PdfTextExtractor.GetTextFromPage(pdfreader, 1, tes);
                counter++;
            }
        return output;
        }

        //
        //EXAMPLE CONFIGURATION AND METHOS FOR THE GITHUB TUTORIAL
        //


        public static void Main(string[] args)
        {
            Console.WriteLine(Config01(@"C:\Users\[...]\menu-of-the-day.pdf"));
            Console.ReadKey();
        }

        public static string TutorialConfigGitHub(string filepath)
        {
            //"database"
            Rectangle[,] rectStorage = new Rectangle[2, 1];                 // 2days, 1area
            rectStorage[0, 0] = createRect(118.1f, 576.7f, 484.1f, 628.5f); //monday
            rectStorage[1, 0] = createRect(122.6f, 473.2f, 316.0f, 525.0f); //tuesday

            //get the current day of the week
            DateTime current = DateTime.Now;
            string currentDayOfWeek = Convert.ToString(current.DayOfWeek);
            currentDayOfWeek = currentDayOfWeek.ToLower();

            //choose the corresponding (to the day of the week) "dataset" out of the "database"
            Rectangle tempRectangle = null;
            switch (currentDayOfWeek)
            {
                case "monday": tempRectangle = rectStorage[0, 0]; break;
                case "tuesday": tempRectangle = rectStorage[1, 0]; break;
                default:
                    Console.WriteLine("no corresponding dataset found. application will be closed.");
                    Console.ReadKey();
                    Environment.Exit(42);
                    break;
            }

            //creation of the rectangle-array, which the readRect-method receives
            Rectangle[] tempRectArray = new Rectangle[1]; 

           //the corresponding Rectangle chosen out of the "database" gets written into the rectangle-array
            tempRectArray[0] = tempRectangle;             //one area = one entry in the rectangle-array

            //readRectangle-method gets called 
            string[] catchOutput = readRect(tempRectArray, filepath);   //receives rectangle-object(area) and filepath to the pdf 

            string temp = catchOutput[0];                   //one rectange-object(area) in input-arry equals one string-variable(content of area)  in the output-array
            return temp;                                    //returns the extracted string
        }
    }
}