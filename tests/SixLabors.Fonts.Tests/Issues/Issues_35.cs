using SixLabors.Fonts.Tests.Fakes;
using Xunit;

namespace SixLabors.Fonts.Tests.Issues
{
    public class Issues_35
    {
        [Fact]
        public void RenderingTabAtStartOrLineTooShort()
        {
            Font font = CreateFont("\t x");
            var xWidth = TextMeasurer.MeasureBounds("x", new RendererOptions(font, (72 * font.EmSize)));
            var tabWidth = TextMeasurer.MeasureBounds("\t", new RendererOptions(font, (72 * font.EmSize)));
            var doublTabWidth = TextMeasurer.MeasureBounds("\t\t", new RendererOptions(font, (72 * font.EmSize)));
            var tabWithXWidth = TextMeasurer.MeasureBounds("\tx", new RendererOptions(font, (72 * font.EmSize)));

            Assert.Equal(tabWidth.Width + xWidth.Width, tabWithXWidth.Width, 2);
        }


        [Fact]
        public void Rendering2TabsAtStartOfLineTooShort()
        {
            Font font = CreateFont("\t x");
            var xWidth = TextMeasurer.MeasureBounds("x", new RendererOptions(font, (72 * font.EmSize)));
            var tabWidth = TextMeasurer.MeasureBounds("\t\t", new RendererOptions(font, (72 * font.EmSize)));
            var tabWithXWidth = TextMeasurer.MeasureBounds("\t\tx", new RendererOptions(font, (72 * font.EmSize)));

            Assert.Equal(tabWidth.Width + xWidth.Width, tabWithXWidth.Width, 2);
        }

        [Fact]
        public void TwoTabsAreDoubleWidthOfOneTab()
        {
            Font font = CreateFont("\t x");
            var xWidth = TextMeasurer.MeasureBounds("x", new RendererOptions(font, (72 * font.EmSize)));
            var tabWidth = TextMeasurer.MeasureBounds("\t", new RendererOptions(font, (72 * font.EmSize)));
            var twoTabWidth = TextMeasurer.MeasureBounds("\t\t", new RendererOptions(font, (72 * font.EmSize)));

            Assert.Equal(twoTabWidth.Width, tabWidth.Width * 2, 2);
        }


        [Fact]
        public void TwoTabsAreDoubleWidthOfOneTabMinusXWidth()
        {
            Font font = CreateFont("\t x");
            var xWidth = TextMeasurer.MeasureBounds("x", new RendererOptions(font, (72 * font.EmSize)));
            var tabWidth = TextMeasurer.MeasureBounds("\tx", new RendererOptions(font, (72 * font.EmSize)));
            var twoTabWidth = TextMeasurer.MeasureBounds("\t\tx", new RendererOptions(font, (72 * font.EmSize)));

            Assert.Equal(twoTabWidth.Width - xWidth.Width, (tabWidth.Width - xWidth.Width) * 2, 2);
        }

        public static Font CreateFont(string text)
        {
            FontCollection fc = new FontCollection();
            Font d = fc.Install(new FakeFontInstance(text)).CreateFont(12);
            return new Font(d, 1);
        }
    }
}
