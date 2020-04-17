// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BoolStarConverter.cs" company="ArcTouch LLC">
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
//   Defines the BoolStarConverter type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Evidences.ValueConverters
{
    public class BoolStarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "filled.png";
            } 
            else
            {
                return "notfilled.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

