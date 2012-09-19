﻿using Caliburn.Micro;
using Espera.Core.Management;
using Rareform.Patterns.MVVM;
using Rareform.Validation;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;

namespace Espera.View.ViewModels
{
    public sealed class AdministratorViewModel : PropertyChangedBase
    {
        private readonly Library library;
        private bool isWrongPassword;
        private bool show;

        public AdministratorViewModel(Library library)
        {
            if (library == null)
                Throw.ArgumentNullException(() => library);

            this.library = library;
        }

        public ICommand ChangeToPartyCommand
        {
            get
            {
                return new RelayCommand
                (
                    param =>
                    {
                        this.library.ChangeToParty();
                        this.NotifyOfPropertyChange(() => this.IsParty);
                        this.NotifyOfPropertyChange(() => this.IsAdmin);
                    },
                    param => this.IsAdminCreated
                );
            }
        }

        public ICommand CreateAdminCommand
        {
            get
            {
                return new RelayCommand
                (
                    param =>
                    {
                        this.library.CreateAdmin(this.CreationPassword);

                        this.NotifyOfPropertyChange(() => this.IsAdminCreated);
                        this.NotifyOfPropertyChange(() => this.IsAdmin);
                    },
                    param => !string.IsNullOrWhiteSpace(this.CreationPassword) && !this.IsAdminCreated
                );
            }
        }

        public string CreationPassword { get; set; }

        public bool EnablePlaylistTimeout
        {
            get { return this.library.EnablePlaylistTimeout; }
            set
            {
                if (this.library.EnablePlaylistTimeout != value)
                {
                    this.library.EnablePlaylistTimeout = value;

                    this.NotifyOfPropertyChange(() => this.EnablePlaylistTimeout);
                }
            }
        }

        public string Homepage
        {
            get { return "http://github.com/flagbug/Espera"; }
        }

        public bool IsAdmin
        {
            get { return this.library.AccessMode == AccessMode.Administrator; }
        }

        public bool IsAdminCreated
        {
            get { return this.library.IsAdministratorCreated; }
        }

        public bool IsParty
        {
            get { return this.library.AccessMode == AccessMode.Party; }
        }

        public string IssuesPage
        {
            get { return "http://github.com/flagbug/Espera/issues"; }
        }

        public bool IsWrongPassword
        {
            get { return this.isWrongPassword; }
            set
            {
                if (this.IsWrongPassword != value)
                {
                    this.isWrongPassword = value;
                    this.NotifyOfPropertyChange(() => this.IsWrongPassword);
                }
            }
        }

        public bool LockLibraryRemoval
        {
            get { return this.library.LockLibraryRemoval; }
            set { this.library.LockLibraryRemoval = value; }
        }

        public bool LockPlaylistRemoval
        {
            get { return this.library.LockPlaylistRemoval; }
            set { this.library.LockPlaylistRemoval = value; }
        }

        public bool LockPlaylistSwitching
        {
            get { return this.library.LockPlaylistSwitching; }
            set { this.library.LockPlaylistSwitching = value; }
        }

        public bool LockPlayPause
        {
            get { return this.library.LockPlayPause; }
            set { this.library.LockPlayPause = value; }
        }

        public bool LockTime
        {
            get { return this.library.LockTime; }
            set { this.library.LockTime = value; }
        }

        public bool LockVolume
        {
            get { return this.library.LockVolume; }
            set { this.library.LockVolume = value; }
        }

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand
                (
                    param =>
                    {
                        try
                        {
                            this.library.ChangeToAdmin(this.LoginPassword);
                            this.IsWrongPassword = false;
                        }

                        catch (WrongPasswordException)
                        {
                            this.IsWrongPassword = true;
                        }

                        this.NotifyOfPropertyChange(() => this.IsAdmin);
                        this.NotifyOfPropertyChange(() => this.IsParty);
                    },
                    param => !string.IsNullOrWhiteSpace(this.LoginPassword)
                );
            }
        }

        public string LoginPassword { get; set; }

        public ICommand OpenLinkCommand
        {
            get
            {
                return new RelayCommand
                (
                    param => Process.Start((string)param)
                );
            }
        }

        public int PlaylistTimeout
        {
            get { return (int)this.library.PlaylistTimeout.TotalSeconds; }
            set { this.library.PlaylistTimeout = TimeSpan.FromSeconds(value); }
        }

        public bool Show
        {
            get { return this.show; }
            set
            {
                if (this.Show != value)
                {
                    this.show = value;
                    this.NotifyOfPropertyChange(() => this.Show);
                }
            }
        }

        public bool StreamYoutube
        {
            get { return this.library.StreamYoutube; }
            set { this.library.StreamYoutube = value; }
        }

        public string Version
        {
            get
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;

                return String.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Revision);
            }
        }
    }
}