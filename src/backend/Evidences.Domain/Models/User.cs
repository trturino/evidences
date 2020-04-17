// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="ArcTouch LLC">
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
//   Defines the User type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
namespace Evidences.Domain.Models
{
    public class User : Model
    {
        public string UserName { get; set; }

        public DateTimeOffset Added { get; set; }
    }
}
