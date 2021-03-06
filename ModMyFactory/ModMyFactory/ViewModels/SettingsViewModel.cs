﻿using System;
using System.ComponentModel;
using System.IO;
using ModMyFactory.MVVM;
using Ookii.Dialogs.Wpf;
using ModMyFactory.Views;

namespace ModMyFactory.ViewModels
{
    sealed class SettingsViewModel : ViewModelBase<SettingsWindow>
    {
        static SettingsViewModel instance;

        public static SettingsViewModel Instance => instance ?? (instance = new SettingsViewModel());

        bool factorioDirectoryIsAppData;
        bool factorioDirectoryIsAppDirectory;
        bool factorioDirectoryIsCustom;

        bool modDirectoryIsAppData;
        bool modDirectoryIsAppDirectory;
        bool modDirectoryIsCustom;

        string factorioDirectory;
        string modDirectory;
        bool settingsValid;

        public bool FactorioDirectoryIsAppData
        {
            get { return factorioDirectoryIsAppData; }
            set
            {
                if (value != factorioDirectoryIsAppData)
                {
                    factorioDirectoryIsAppData = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(FactorioDirectoryIsAppData)));
                    ValidateSettings();
                }
            }
        }

        public bool FactorioDirectoryIsAppDirectory
        {
            get { return factorioDirectoryIsAppDirectory; }
            set
            {
                if (value != factorioDirectoryIsAppDirectory)
                {
                    factorioDirectoryIsAppDirectory = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(FactorioDirectoryIsAppDirectory)));
                    ValidateSettings();
                }
            }
        }

        public bool FactorioDirectoryIsCustom
        {
            get { return factorioDirectoryIsCustom; }
            set
            {
                if (value != factorioDirectoryIsCustom)
                {
                    factorioDirectoryIsCustom = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(FactorioDirectoryIsCustom)));
                    ValidateSettings();
                }
            }
        }

        public bool ModDirectoryIsAppData
        {
            get { return modDirectoryIsAppData; }
            set
            {
                if (value != modDirectoryIsAppData)
                {
                    modDirectoryIsAppData = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ModDirectoryIsAppData)));
                    ValidateSettings();
                }
            }
        }

        public bool ModDirectoryIsAppDirectory
        {
            get { return modDirectoryIsAppDirectory; }
            set
            {
                if (value != modDirectoryIsAppDirectory)
                {
                    modDirectoryIsAppDirectory = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ModDirectoryIsAppDirectory)));
                    ValidateSettings();
                }
            }
        }

        public bool ModDirectoryIsCustom
        {
            get { return modDirectoryIsCustom; }
            set
            {
                if (value != modDirectoryIsCustom)
                {
                    modDirectoryIsCustom = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ModDirectoryIsCustom)));
                    ValidateSettings();
                }
            }
        }

        public string FactorioDirectory
        {
            get { return factorioDirectory; }
            set
            {
                if (value != factorioDirectory)
                {
                    factorioDirectory = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(FactorioDirectory)));
                    ValidateSettings();
                }
            }
        }

        public string ModDirectory
        {
            get { return modDirectory; }
            set
            {
                if (value != modDirectory)
                {
                    modDirectory = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ModDirectory)));
                    ValidateSettings();
                }
            }
        }

        public bool SettingsValid
        {
            get { return settingsValid; }
            private set
            {
                if (value != settingsValid)
                {
                    settingsValid = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(SettingsValid)));
                }
            }
        }

        public RelayCommand SelectFactorioDirectoryCommand { get; }

        public RelayCommand SelectModDirectoryCommand { get; }

        private SettingsViewModel()
        {
            SelectFactorioDirectoryCommand = new RelayCommand(SelectFactorioDirectory);
            SelectModDirectoryCommand = new RelayCommand(SelectModDirectory);
        }

        public void Reset()
        {
            FactorioDirectoryIsAppData = false;
            FactorioDirectoryIsAppDirectory = false;
            FactorioDirectoryIsCustom = false;
            FactorioDirectory = string.Empty;
            switch (App.Instance.Settings.FactorioDirectoryOption)
            {
                case DirectoryOption.AppData:
                    FactorioDirectoryIsAppData = true;
                    break;
                case DirectoryOption.ApplicationDirectory:
                    FactorioDirectoryIsAppDirectory = true;
                    break;
                case DirectoryOption.Custom:
                    FactorioDirectoryIsCustom = true;
                    FactorioDirectory = App.Instance.Settings.FactorioDirectory;
                    break;
            }

            ModDirectoryIsAppData = false;
            ModDirectoryIsAppDirectory = false;
            ModDirectoryIsCustom = false;
            ModDirectory = string.Empty;
            switch (App.Instance.Settings.ModDirectoryOption)
            {
                case DirectoryOption.AppData:
                    ModDirectoryIsAppData = true;
                    break;
                case DirectoryOption.ApplicationDirectory:
                    ModDirectoryIsAppDirectory = true;
                    break;
                case DirectoryOption.Custom:
                    ModDirectoryIsCustom = true;
                    ModDirectory = App.Instance.Settings.ModDirectory;
                    break;
            }

            ValidateSettings();
        }

        private void SelectFactorioDirectory()
        {
            var dialog = new VistaFolderBrowserDialog();
            bool? result = dialog.ShowDialog(Window);
            if (result != null && result.Value)
                FactorioDirectory = dialog.SelectedPath;
        }

        private void SelectModDirectory()
        {
            var dialog = new VistaFolderBrowserDialog();
            bool? result = dialog.ShowDialog(Window);
            if (result != null && result.Value)
                ModDirectory = dialog.SelectedPath;
        }

        private bool PathValid(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;

            try
            {
                Path.GetFullPath(path);
            }
            catch (Exception)
            {
                return false;
            }

            return Path.IsPathRooted(path);
        }

        private void ValidateSettings()
        {
            SettingsValid =
                (!FactorioDirectoryIsCustom || PathValid(FactorioDirectory))
                && (!ModDirectoryIsCustom || PathValid(ModDirectory));
        }
    }
}
