// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShadowPlatformEffect.cs" company="ArcTouch LLC">
//   Copyright 2019 ArcTouch LLC.
//   All rights reserved.
//
//   This file, its contents, concepts, methods, behavior, and operation
//   (collectively the "Software") are protected by trade secret, patent,
//   and copyright laws. The use of the Software is governed by a license
//   agreement. Disclosure of the Software to third parties, in any form,
//   in whole or in part, is expressly prohibited except as authorized by
//   the license agreement.
// </copyright>
// <summary>
//   Defines the ShadowPlatformEffect type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Linq;
using CoreGraphics;
using Evidences.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Evidences.ShadowEffect")]
[assembly: ExportEffect(typeof(ShadowEffect), "ShadowEffect")]
namespace Evidences.iOS.Effects
{
    public class ShadowPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);

                if (effect != null)
                {
                    Container.Layer.CornerRadius = effect.BlurRadius;
                    Container.Layer.ShadowColor = effect.ShadowColor.ToCGColor();
                    Container.Layer.ShadowOffset = new CGSize(effect.HorizontalLength, effect.VerticalLength);
                    Container.Layer.ShadowOpacity = effect.ShadowOpacity;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ShadowPlatformEffect 💩 ", ex.Message);
            }
        }

        protected override void OnDetached()
        {

        }
    }
}
