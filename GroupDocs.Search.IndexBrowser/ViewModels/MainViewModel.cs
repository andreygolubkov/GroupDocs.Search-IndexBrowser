﻿using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GroupDocs.Search.IndexBrowser.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private const int MaxLogItemCount = 1000;

        private bool windowEnabled = true;
        private readonly ObservableCollection<LogItemViewModel> log = new ObservableCollection<LogItemViewModel>();
        private LogItemViewModel selectedLogItem;

        private readonly Settings settings;
        private readonly StartViewModel startViewModel;
        private ViewModelBase currentViewModel;

        public MainViewModel(Settings settings)
        {
            this.settings = settings;
            startViewModel = new StartViewModel(this);
            CurrentViewModel = startViewModel;
        }

        public bool WindowEnabled
        {
            get { return windowEnabled; }
            set { UpdateProperty(ref windowEnabled, value); }
        }

        public Settings Settings => settings;

        public ObservableCollection<LogItemViewModel> Log => log;

        public LogItemViewModel SelectedLogItem
        {
            get { return selectedLogItem; }
            set { UpdateProperty(ref selectedLogItem, value); }
        }

        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set => UpdateProperty(ref currentViewModel, value);
        }

        public void AddLogEntry(DateTime time, string message)
        {
            var item = new LogItemViewModel(time, message);
            AddLogEntryPrivate(item);

            SelectedLogItem = item;
        }

        public void AddLogEntry(string message)
        {
            AddLogEntry(DateTime.Now, message);
        }

        public void CloseCurrentView()
        {
            CurrentViewModel = startViewModel;
        }

        private void AddLogEntryPrivate(LogItemViewModel item)
        {
            while (Log.Count >= MaxLogItemCount)
            {
                Log.RemoveAt(0);
            }

            Log.Add(item);
        }
    }
}
