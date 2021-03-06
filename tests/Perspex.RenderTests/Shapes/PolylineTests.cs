﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.Controls;
using Perspex.Controls.Shapes;
using Perspex.Media;
using Xunit;

#if PERSPEX_CAIRO
namespace Perspex.Cairo.RenderTests.Shapes
#elif PERSPEX_SKIA
namespace Perspex.Skia.RenderTests
#else
namespace Perspex.Direct2D1.RenderTests.Shapes
#endif
{
    public class PolylineTests : TestBase
    {
        public PolylineTests()
            : base(@"Shapes\Polyline")
        {
        }

#if PERSPEX_CAIRO
        [Fact(Skip = "Caused by cairo bug")]
#elif PERSPEX_SKIA_SKIP_FAIL
        [Fact(Skip = "Waiting for https://github.com/mono/SkiaSharp/pull/63")]
#else
        [Fact]
#endif
        public void Polyline_1px_Stroke()
        {
            var polylinePoints = new Point[] { new Point(0, 0), new Point(5, 0), new Point(6, -2), new Point(7, 3), new Point(8, -3),
                new Point(9, 1), new Point(10, 0), new Point(15, 0) };

            Decorator target = new Decorator
            {
                Padding = new Thickness(8),
                Width = 400,
                Height = 200,
                Child = new Polyline
                {
                    Stroke = Brushes.Brown,
                    Points = polylinePoints,
                    Stretch = Stretch.Uniform,
                    StrokeThickness = 1
                }
            };

            RenderToFile(target);
            CompareImages();
        }

#if PERSPEX_CAIRO
        [Fact(Skip = "Caused by cairo bug")]
#elif PERSPEX_SKIA_SKIP_FAIL
        [Fact(Skip = "Waiting for https://github.com/mono/SkiaSharp/pull/63")]
#else
        [Fact]
#endif
        public void Polyline_10px_Stroke_PenLineJoin()
        {
            var polylinePoints = new Point[] { new Point(0, 0), new Point(5, 0), new Point(6, -2), new Point(7, 3), new Point(8, -3),
                new Point(9, 1), new Point(10, 0), new Point(15, 0) };

            Decorator target = new Decorator
            {
                Padding = new Thickness(8),
                Width = 400,
                Height = 200,
                Child = new Polyline
                {
                    Stroke = Brushes.Brown,
                    Points = polylinePoints,
                    Stretch = Stretch.Uniform,
                    StrokeJoin = PenLineJoin.Round,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round,
                    StrokeThickness = 10
                }
            };

            RenderToFile(target);
            CompareImages();
        }
    }
}
