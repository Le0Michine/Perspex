﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Perspex.Media;
using Perspex.RenderHelpers;
using SharpDX.Direct2D1;

namespace Perspex.Direct2D1.Media
{
    public sealed class TileBrushImpl : BrushImpl
    {
        public TileBrushImpl(
            TileBrush brush,
            SharpDX.Direct2D1.RenderTarget target,
            Size targetSize)
        {
            var helper = new TileBrushImplHelper(brush, targetSize);
            if (!helper.IsValid)
                return;

            using (var intermediate = new BitmapRenderTarget(target, CompatibleRenderTargetOptions.None, helper.IntermediateSize.ToSharpDX()))
            {
                using (var ctx = new RenderTarget(intermediate).CreateDrawingContext())
                {
                    intermediate.Clear(null);
                    helper.DrawIntermediate(ctx);
                }

                PlatformBrush = new BitmapBrush(
                    target,
                    intermediate.Bitmap,
                    GetBitmapBrushProperties(brush),
                    GetBrushProperties(brush, helper.DestinationRect));
            }
        }

        private static BrushProperties GetBrushProperties(TileBrush brush, Rect destinationRect)
        {
            var tileTransform = 
                brush.TileMode != TileMode.None ? 
                Matrix.CreateTranslation(destinationRect.X, destinationRect.Y) :
                Matrix.Identity;

            return new BrushProperties
            {
                Opacity = (float)brush.Opacity,
                Transform = tileTransform.ToDirect2D(),
            };
        }

        private static BitmapBrushProperties GetBitmapBrushProperties(TileBrush brush)
        {
            var tileMode = brush.TileMode;

            return new BitmapBrushProperties
            {
                ExtendModeX = GetExtendModeX(tileMode),
                ExtendModeY = GetExtendModeY(tileMode),
            };
        }

        private static ExtendMode GetExtendModeX(TileMode tileMode)
        {
            return (tileMode & TileMode.FlipX) != 0 ? ExtendMode.Mirror : ExtendMode.Wrap;
        }

        private static ExtendMode GetExtendModeY(TileMode tileMode)
        {
            return (tileMode & TileMode.FlipY) != 0 ? ExtendMode.Mirror : ExtendMode.Wrap;
        }

        public override void Dispose()
        {
            ((BitmapBrush)PlatformBrush)?.Bitmap.Dispose();
            base.Dispose();
        }
    }
}
