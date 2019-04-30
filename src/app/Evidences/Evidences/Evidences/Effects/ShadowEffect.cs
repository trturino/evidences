// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShadowEffect.cs" company="ArcTouch LLC">
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
//   Defines the ShadowEffect type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using Xamarin.Forms;

namespace Evidences.Effects
{
    public class ShadowEffect : RoutingEffect
    {
        public float ShadowOpacity { get; set; }
        public float BlurRadius { get; set; }
        public Color ShadowColor { get; set; }
        public float HorizontalLength { get; set; }
        public float VerticalLength { get; set; }

        public ShadowEffect() : base("Evidences.ShadowEffect")
        {
        }
    }
}
