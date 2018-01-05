#for the case that that access to the .dll is restricted
#
#Get-ExecutionPolicy
#Unblock-File -Path $dlllocation


#file paths
$dlllocation = "$PSScriptRoot\itextsharp.dll"
$inputfilelocation = "$PSScriptRoot\input.pdf" #our menu-of-the-day is located here.

#sourcecode C#
$source = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
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

        public static string GitTutorialConfig(string filepath)
        {
            //creation of the rectangle-object(area)
            Rectangle tempRectangle = createRect(118.1f, 576.7f, 484.1f, 628.5f);

            //creation of the rectangle-array, which the readRect-method receives
            Rectangle[] tempRectArray = new Rectangle[1]; 
            tempRectArray[0] = tempRectangle;             

            //call of the readRect method
            string[] catchOutput = readRect(tempRectArray, filepath);

            //return of the content as string
            return catchOutput[0];
        }
    }
}
"@

#creates a type and instance of the iTextSharp-library / assemblie 
Add-Type -Path $dlllocation

#path to the referenced assemblies used in the C# code
$refs = ($dlllocation) 

#creates a type and instance of the C# code
Add-Type -ReferencedAssemblies $refs -TypeDefinition $source -Language CSharp 

#use of the C# code
$content = [tutorial_namespace.tutorial_class]::GitTutorialConfig($inputfilelocation)
$content
