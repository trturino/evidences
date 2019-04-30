// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchAsYouTypeBehavior.cs" company="ArcTouch LLC">
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
//   Defines the SearchAsYouTypeBehavior type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;
using Prism.Behaviors;
using Xamarin.Forms;

namespace Evidences.Behaviors
{
    public class SearchAsYouTypeBehavior : BehaviorBase<Entry>
    {

        private IDisposable _subscription;

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);

            _subscription = Observable.FromEventPattern<TextChangedEventArgs>(
                    handler => AssociatedObject.TextChanged += handler,
                    handler => AssociatedObject.TextChanged -= handler)
                .Throttle(TimeSpan.FromMilliseconds(MinimumSearchIntervalMiliseconds))
                .ObserveOn(SynchronizationContext.Current)
                .Select(eventPattern => AssociatedObject.Text)
                .DistinctUntilChanged()
                .Subscribe(query => Device.BeginInvokeOnMainThread(() =>
                {
                    SearchCommand?.Execute(query);
                }));
        }

        public const int DefaultMinimumSearchIntervalMiliseconds = 800;

        public static readonly BindableProperty SearchCommandProperty =
            BindableProperty.Create(nameof(SearchCommand), typeof(ICommand), 
                typeof(SearchAsYouTypeBehavior));

        public static readonly BindableProperty MinimumSearchIntervalMilisecondsProperty =
            BindableProperty.Create(nameof(MinimumSearchIntervalMiliseconds), typeof(int),
                typeof(SearchAsYouTypeBehavior), DefaultMinimumSearchIntervalMiliseconds);

        public ICommand SearchCommand
        {
            get => (ICommand)GetValue(SearchCommandProperty);
            set => SetValue(SearchCommandProperty, value);
        }

        public int MinimumSearchIntervalMiliseconds
        {
            get => (int)GetValue(MinimumSearchIntervalMilisecondsProperty);
            set => SetValue(MinimumSearchIntervalMilisecondsProperty, value);
        }
    }
}
