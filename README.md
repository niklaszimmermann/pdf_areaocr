## Disclaimer
Im a beginner at programming.

This code / codesnipet is a result of my learning process and most likely
not the correct, neither the most elegant way of doing it.
I'm aware of this and therefor you should too!

Which doesn't mean feedback isn't welcome - quite the contrary,
it would help me out a lot and is highly appreciated.

I strongly believe that the internet thrives of user participation,
hence I'm hoping that somebody might find this useful / helpful. 

## Disclaimer - not included but needed Software and Libraries.
iTextPdf:
- the .dll has to be placed in the main directory (not included by me)
- iTextPdf is licensed under AGPL
- download available for example on sourceforge
- for more information look at their website https://itextpdf.com/

Sumatra PDF Reader:
- used to manually elaborate the coordinates of areas in .pdf files (not included by me)
- download available for example on sourceforge
- for more information look at their website https://www.sumatrapdfreader.org/

## Introduction
Reads a certain area of a .pdf file and returns the content as stringVariable.

The C# class uses the iTextPdf-library to use OCR on an area defined by coordiantes,
of which the content is returned as string variable.

I mainly use the class directly in PowerShell.
The needed code and working example will be provided by me at a future stage.

## Example / Use-Case
As aforementioned I mainly use the class in PowerShell.

For example I set up an event which uses PowerShell to:
- automatically download the menu of the day as .pdf-file
- extract the content of several areas
- generate a notification mail using the extracted content
- send me the notfication mail on windows startup

## How-To
#Step 01) pdf-file
Have a .pdf-file locally on your drive of which you desire to use OCR on a certain area.
![imgExamplePdf](/tut/samplepdf_s.jpg)


#Step 02) undersand how coordinates in a pdf-file work
As used by the iTextPdf-librarie:
![imgAnnotatedExamplePdf](/tut/samplepdf_annotation1_s.jpg)

- all values for coordinates are given in pt
- origin of the coordinate system (0/0) ist the lower-left corner
- an area which OCR should be used on ist defined by to points A and B
- point A is the lower left corner of the rectangle, point B is the upper right corner of the rectangle
- a point consists of a value (float) for the postion on the  X axis and one for the position on the Y axis e.g. A(x/y)

#Step 03) get the values
For this step I use the software "Sumatra PDF Reader" which is available under the GPLv3 license
![imgSumatra](/tut/sumatra_s.jpg)

- pressing the hotkey "m" will open an area, which shows the coordinates of your current cursor position
- pressing the hotkey "m" now allows to circle through the units - make sure the values shown in pt
- hover the lower left corner of the desired area and note down the values PointA(x/y)
- hover the upper right corner of the desired area and note down the values PointB(x/y)

In my example that results in PointA(118,1 / 265,1) and PointB(484,1 x 213,3).

#Step 04) correct the values and convert them into the required format

- Sumatra uses the upper left corner as origin, hence we have to adjust our values for the use with the iTextPdf librarie
- the value for the position on the x-axis stays the same.

In my example that means we know the following values for the adjustes Point: corPointA(118,1 / ?)

- to correct the value for the position on the y-axis we need to know the overall height of the document
- hover the lower boarder of the document in Sumatra and write down the y-value
- subtract the y-value of PointA from the elaborated overall / max height

In my example that means 841,8 (max. height) - 265,1 (y-value PointA) = 576,7
--> corPointA(118,1 / 576,7)

Repeating the steps for PointB we end up with the following valid Points: corPointA(118,1 / 576,7); corPointB(484,1 / 628,5)

Valid Data: 118.1f, 576.7f, 484.1f, 628.5f (float values C#)

#Step 05) feed the data into the provided methods

For our example (one area / rectangle) it could look like this:

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

Used in PowerShell: 
![UsedInPowerShell](/tut/powershell.jpg)

Now nothing stand of our way to use this for automationtasks for example sending
a notification mail on system startup.



