﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.Controls.Primitives;
using Perspex.LogicalTree;
using Xunit;

namespace Perspex.Controls.UnitTests.Primitives
{
    public class TrackTests
    {
        [Fact]
        public void Measure_Should_Return_Thumb_DesiredWidth_In_Vertical_Orientation()
        {
            var thumb = new Thumb
            {
                Width = 12,
            };

            var target = new Track
            {
                Thumb = thumb,
                Orientation = Orientation.Vertical,
            };

            target.Measure(new Size(100, 100));

            Assert.Equal(new Size(12, 0), target.DesiredSize);
        }

        [Fact]
        public void Measure_Should_Return_Thumb_DesiredHeight_In_Horizontal_Orientation()
        {
            var thumb = new Thumb
            {
                Height = 12,
            };

            var target = new Track
            {
                Thumb = thumb,
                Orientation = Orientation.Horizontal,
            };

            target.Measure(new Size(100, 100));

            Assert.Equal(new Size(0, 12), target.DesiredSize);
        }

        [Fact]
        public void Should_Arrange_Thumb_In_Horizontal_Orientation()
        {
            var thumb = new Thumb
            {
                Height = 12,
            };

            var target = new Track
            {
                Thumb = thumb,
                Orientation = Orientation.Horizontal,
                Minimum = 100,
                Maximum = 200,
                Height = 12,
                Value = 150,
                ViewportSize = 50,
            };

            target.Measure(new Size(100, 100));
            target.Arrange(new Rect(0, 0, 100, 100));

            Assert.Equal(new Rect(25, 0, 50, 12), thumb.Bounds);
        }

        [Fact]
        public void Should_Arrange_Thumb_In_Vertical_Orientation()
        {
            var thumb = new Thumb
            {
                Width = 12,
            };

            var target = new Track
            {
                Thumb = thumb,
                Orientation = Orientation.Vertical,
                Minimum = 100,
                Maximum = 300,
                Value = 150,
                ViewportSize = 50,
                Width = 12,
            };

            target.Measure(new Size(100, 100));
            target.Arrange(new Rect(0, 0, 100, 100));

            Assert.Equal(new Rect(0, 18, 12, 25), thumb.Bounds);
        }

        [Fact]
        public void Thumb_Should_Fill_Track_When_Minimum_Equals_Maximum()
        {
            var thumb = new Thumb
            {
                Height = 12,
            };

            var target = new Track
            {
                Height = 12,
                Thumb = thumb,
                Orientation = Orientation.Horizontal,
                Minimum = 100,
                Maximum = 100,
            };

            target.Measure(new Size(100, 100));
            target.Arrange(new Rect(0, 0, 100, 100));

            Assert.Equal(new Rect(0, 0, 100, 12), thumb.Bounds);
        }

        [Fact]
        public void Thumb_Should_Be_Logical_Child()
        {
            var thumb = new Thumb
            {
                Height = 12,
            };

            var target = new Track
            {
                Height = 12,
                Thumb = thumb,
                Orientation = Orientation.Horizontal,
                Minimum = 100,
                Maximum = 100,
            };

            Assert.Same(thumb.Parent, target);
            Assert.Equal(new[] { thumb }, ((ILogical)target).LogicalChildren);
        }
    }
}
