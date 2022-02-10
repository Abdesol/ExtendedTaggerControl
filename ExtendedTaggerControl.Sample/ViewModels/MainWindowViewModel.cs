using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using ReactiveUI;

namespace ExtendedTaggerControl.Sample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Tags = new()
            {
                "This", "is", "Sample"
            };
        }

        private ObservableCollection<string> _tags;
        public ObservableCollection<string> Tags
        {
            get => _tags;
            set => this.RaiseAndSetIfChanged(ref _tags, value);
        }

        public async void GetCommand()
        {
            Debug.WriteLine($"Tags length is {Tags.Count}");
        }
    }
}
