// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NowPlayingPage.xaml.cs" company="ArcTouch LLC">
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
//   Defines the NowPlayingPage.xaml type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using Xamarin.Forms;

namespace Evidences.Views
{
    public partial class NowPlayingPage : ContentPage
    {
        public NowPlayingPage()
        {
            InitializeComponent();
        }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            var label = sender as Label;
            label.ScaleTo(1.5, 200);
            await label.FadeTo(0.5, 200);
            label.Scale = 1;
            label.Opacity = 1;
        }
    }
}
