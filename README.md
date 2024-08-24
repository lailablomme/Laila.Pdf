# Laila.Pdf
A .NET6 Pdfium-based viewer featuring smooth scrolling, text selecting and copying, search and basic PDF forms support and a .NET6 PDF printer.

Written using an extended version of [PDFiumSharp](https://github.com/ArgusMagnus/PDFiumSharp) to which I added PDF forms support.

## Installation
1. Get [the package](https://www.nuget.org/packages/Laila.Pdf/) from NuGet.

2. Get the package [PDFium.Windows](https://www.nuget.org/packages/PDFium.Windows) (Windows 7 compatible) or [PDFium.WindowsV2](https://www.nuget.org/packages/PDFium.WindowsV2) (PDF forms support) from NuGet.

3. Place the control on your WPF form.

4. Set or bind the Document property to the bytes of the document (File.ReadAllBytes) and bind the Tool, SearchTerm and CurrentMatchIndex properties.

5. See the sample application for a simple example of all functions.