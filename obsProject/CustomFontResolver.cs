using PdfSharp.Fonts;

namespace obsProject
{
    public class CustomFontResolver : IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            return File.ReadAllBytes(@"C:\Windows\Fonts\calibri.ttf");
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            return new FontResolverInfo(@"C:\Windows\Fonts\calibri.ttf");
        }

    }

}
