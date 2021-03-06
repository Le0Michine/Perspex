﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using Perspex.Platform;

namespace Perspex.Media
{
    /// <summary>
    /// Represents a piece of text with formatting.
    /// </summary>
    public class FormattedText : PerspexDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedText"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fontFamilyName">The font family.</param>
        /// <param name="fontSize">The font size.</param>
        /// <param name="fontStyle">The font style.</param>
        /// <param name="textAlignment">The text alignment.</param>
        /// <param name="fontWeight">The font weight.</param>
        public FormattedText(
            string text,
            string fontFamilyName,
            double fontSize,
            FontStyle fontStyle,
            TextAlignment textAlignment,
            FontWeight fontWeight)
        {
            Contract.Requires<ArgumentNullException>(text != null);
            Contract.Requires<ArgumentNullException>(fontFamilyName != null);
            Contract.Requires<ArgumentException>(fontSize > 0);

            Text = text;
            FontFamilyName = fontFamilyName;
            FontSize = fontSize;
            FontStyle = fontStyle;
            FontWeight = fontWeight;
            TextAlignment = textAlignment;

            var platform = PerspexLocator.Current.GetService<IPlatformRenderInterface>();

            if (platform == null)
            {
                throw new Exception("Could not create FormattedText: IPlatformRenderInterface not registered.");
            }

            PlatformImpl = platform.CreateFormattedText(
                text,
                fontFamilyName,
                fontSize,
                fontStyle,
                textAlignment,
                fontWeight);
        }

        /// <summary>
        /// Gets or sets the constraint of the text.
        /// </summary>
        public Size Constraint
        {
            get
            {
                CheckDisposed();
                return PlatformImpl.Constraint;
            }
            set
            {
                CheckDisposed();
                PlatformImpl.Constraint = value;
            }
        }

        /// <summary>
        /// Gets the font family.
        /// </summary>
        public string FontFamilyName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the font size.
        /// </summary>
        public double FontSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the font style.
        /// </summary>
        public FontStyle FontStyle
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the font weight.
        /// </summary>
        public FontWeight FontWeight
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets platform-specific platform implementation.
        /// </summary>
        public IFormattedTextImpl PlatformImpl
        {
            get; }

        /// <summary>
        /// Gets the text alignment.
        /// </summary>
        public TextAlignment TextAlignment
        {
            get;
            private set;
        }

        /// <summary>
        /// Disposes of unmanaged resources associated with the formatted text.
        /// </summary>
        protected override void DoDispose()
        {
            PlatformImpl.Dispose();
        }

        /// <summary>
        /// Gets the lines in the text.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="FormattedTextLine"/> objects.
        /// </returns>
        public IEnumerable<FormattedTextLine> GetLines()
        {
            CheckDisposed();
            return PlatformImpl.GetLines();
        }

        /// <summary>
        /// Hit tests a point in the text.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// A <see cref="TextHitTestResult"/> describing the result of the hit test.
        /// </returns>
        public TextHitTestResult HitTestPoint(Point point)
        {
            CheckDisposed();
            return PlatformImpl.HitTestPoint(point);
        }

        /// <summary>
        /// Gets the bounds rectangle that the specified character occupies.
        /// </summary>
        /// <param name="index">The index of the character.</param>
        /// <returns>The character bounds.</returns>
        public Rect HitTestTextPosition(int index)
        {
            CheckDisposed();
            return PlatformImpl.HitTestTextPosition(index);
        }

        /// <summary>
        /// Gets the bounds rectangles that the specified text range occupies.
        /// </summary>
        /// <param name="index">The index of the first character.</param>
        /// <param name="length">The number of characters in the text range.</param>
        /// <returns>The character bounds.</returns>
        public IEnumerable<Rect> HitTestTextRange(int index, int length)
        {
            CheckDisposed();
            return PlatformImpl.HitTestTextRange(index, length);
        }

        /// <summary>
        /// Gets the size of the text, taking <see cref="Constraint"/> into account.
        /// </summary>
        /// <returns>The bounds box of the text.</returns>
        public Size Measure()
        {
            CheckDisposed();
            return PlatformImpl.Measure();
        }

        /// <summary>
        /// Sets the foreground brush for the specified text range.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="startIndex">The start of the text range.</param>
        /// <param name="length">The length of the text range.</param>
        public void SetForegroundBrush(IBrush brush, int startIndex, int length)
        {
            CheckDisposed();
            PlatformImpl.SetForegroundBrush(brush, startIndex, length);
        }
    }
}
