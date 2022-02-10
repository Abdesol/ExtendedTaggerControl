using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Threading;
using DesktopTrayServer.Utils;

namespace DesktopTrayServer.Controllers
{
    public class ExtendedTaggerControl : UserControl
    {
        static ExtendedTaggerControl()
        {
            TagsProperty.Changed.Subscribe(TagsPropertyChanged);
            WatermarkProperty.Changed.Subscribe(WatermarkPropertyChanged);
        }
        
        private TextBox _textBox;
        private ItemsControl _tagsList;
        public ExtendedTaggerControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.GetControl("TagAdder", out _textBox);
            this.GetControl("TagsList", out _tagsList);
            
            _textBox.AddHandler(KeyDownEvent, (sender, args) =>
            {
                if (args.Key == Key.Enter)
                {
                    if(string.IsNullOrEmpty(_textBox.Text)) return;
                    ((DataContext as ExtendedTaggerControlViewModel)!).AddTag(_textBox.Text);
                    _textBox.Clear();
                }
            });

            ((DataContext as ExtendedTaggerControlViewModel)!).Tags.CollectionChanged += (sender, args) =>
            {
                Tags = ((DataContext as ExtendedTaggerControlViewModel)!).Tags;
            };
        }
        
        public new static readonly StyledProperty<string> WatermarkProperty =
            AvaloniaProperty.Register<ExtendedTaggerControl, string>(nameof(Watermark));

        public new string Watermark
        {
            get => GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        private static void WatermarkPropertyChanged(AvaloniaPropertyChangedEventArgs<string> e)
        {
            if (e.Sender is ExtendedTaggerControl ctrl)
            {
                ctrl._textBox.Watermark = e.NewValue.Value;
            }
        }
        
        
        public new static readonly StyledProperty<IEnumerable> TagsProperty =
            AvaloniaProperty.Register<ExtendedTaggerControl, IEnumerable>(
                nameof(Tags),
                defaultValue: new ObservableCollection<string>(),
                defaultBindingMode: BindingMode.TwoWay);
        public IEnumerable Tags
        {
            get => GetValue(TagsProperty);
            set => SetValue(TagsProperty, value);
        }
        
        private static void TagsPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Sender is ExtendedTaggerControl ctrl)
            {
                ((ctrl.DataContext as ExtendedTaggerControlViewModel)!).Tags =
                    (e.NewValue as ObservableCollection<string>)!;

            }
        }
    }
}