// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FunctionAppStartup.cs" company="ArcTouch LLC">
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
//   Defines the FunctionAppStartup type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;

namespace Evidences.API
{
    public class FunctionAppStartup : IFunctionAppConfiguration
    {
        public void Build(IFunctionHostBuilder builder)
        {
            builder
                .Setup((serviceCollection, commandRegistry) =>
                {

                })
                .OpenApiEndpoint(openApi => openApi
                    .Title("Evidences API")
                    .Version("1.0.0")
                    .UserInterface()
                )
                //.Functions(functions => functions
                    
                //)
                ;
        }
    }
}
