// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompletedCommandBehavior.cs" company="ArcTouch LLC">
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
//   Defines the CompletedCommandBehavior type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using Prism.Behaviors;
using Xamarin.Forms;
using System.Windows.Input;

namespace Evidences.Behaviors
{
    public class CompletedCommandBehavior : BehaviorBase<Entry>
    {

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Completed += Bindable_Completed;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Completed -= Bindable_Completed;
        }

        void Bindable_Completed(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            Device.BeginInvokeOnMainThread(() =>
            {
                CompletedCommand?.Execute(entry.Text);
            });
        }

        public static readonly BindableProperty CompletedCommandProperty =
            BindableProperty.Create(nameof(CompletedCommand), typeof(ICommand),
                typeof(SearchAsYouTypeBehavior));

        public ICommand CompletedCommand
        {
            get => (ICommand)GetValue(CompletedCommandProperty);
            set => SetValue(CompletedCommandProperty, value);
        }

    }
}

