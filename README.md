## Disclaimer
Im a beginner at programming.

This code / codesnipet is a result of my learning process and most likely
not the correct, neither the most elegant way of doing it.
I'm aware of this and therefor you should too!

Which doesn't mean feedback isn't welcome - quite the contrary,
it would help me out a lot and is highly appreciated.

I strongly believe that the internet thrives of user participation,
hence I'm hoping that somebody might find this useful / helpful. 

## Introduction
Reads a certain area of a .pdf file and returns the content as stringVariable.

The C# class uses the iTextPdf-librarie to use OCR on an area defined by coordiantes,
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
Step 01) pdf-file
Have a .pdf-file locally on your drive of which you desire to use OCR on a certain area.
![imgExamplePdf](/tut/samplepdf_s.jpg)

Step 02) undersand how coordinates in a pdf-file work.
![imgAnnotatedExamplePdf](/tut/samplepdf_annotation1_s.jpg)

As used by the iTextPdf-librarie:
- all values for coordinates are given in pt
- origin of the coordinate system (0/0) ist the lower-left corner

- an area which OCR should be used on ist defined by to points A and B
- point A is the lower left corner of the rectangle, point B is the upper right corner of the rectangle
- a point consists of a value (float) for the postion on the  X axis and one for the position on the Y axis e.g. A(x/y)
- 

Step 03) use the provided methods



## License
https://sourceforge.net/projects/itextsharp/
