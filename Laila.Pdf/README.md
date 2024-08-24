# Laila.Pdf
A .NET6 Pdfium-based viewer featuring smooth scrolling, text selecting and copying, search and basic forms support and a .NET6 PDF printer.

Written using an extended version of [PDFiumSharp](https://github.com/ArgusMagnus/PDFiumSharp).

## Installation
1. Get [the package](https://www.nuget.org/packages/Laila.Pdf/) from NuGet.

2. Get the package PDFium.Windows (Windows 7 compatible) or PDFium.WindowsV2 (PDF forms support).

3. Place the control on your WPF form.

4. Set or bind the Document property to the bytes of the document (File.ReadAllBytes) and bind the Tool property (see the sample application).