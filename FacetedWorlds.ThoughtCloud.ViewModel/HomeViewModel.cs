﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FacetedWorlds.ThoughtCloud.Model;
using UpdateControls.XAML;
using System;
using FacetedWorlds.ThoughtCloud.ViewModel.Models;

namespace FacetedWorlds.ThoughtCloud.ViewModel
{
    public class HomeViewModel
    {
        private readonly Identity _identity;
        private readonly NavigationModel _navigation;

        public HomeViewModel(Identity identity, NavigationModel navigation)
        {
            _identity = identity;
            _navigation = navigation;
        }

        public IEnumerable<CloudSummaryViewModel> Clouds
        {
            get
            {
                return
                    from c in _identity.SharedClouds
                    select new CloudSummaryViewModel(c);
            }
        }

        public CloudSummaryViewModel SelectedCloud
        {
            get
            {
                return _navigation.SelectedCloud == null
                    ? null
                    : new CloudSummaryViewModel(_navigation.SelectedCloud);
            }
            set
            {
            	_navigation.SelectedCloud = value == null
                    ? null
                    : value.Cloud;
            }
        }

        public ICommand AddCloud
        {
            get
            {
                return MakeCommand
                    .Do(() =>
                    {
                        Cloud cloud = _identity.NewCloud();
                        _navigation.OpenCloud(cloud);
                    });
            }
        }

        public ICommand OpenCloud
        {
            get
            {
                return MakeCommand
                    .When(() => _navigation.SelectedCloud != null)
                    .Do(() => _navigation.OpenCloud(_navigation.SelectedCloud));
            }
        }
    }
}
