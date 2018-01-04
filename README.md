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
<img src"tut/testjpg.jpg" width="42">

Step 02) undersand how coordinates in a pdf-file work.


Step 03) use the provided methods



## License
https://sourceforge.net/projects/itextsharp/
