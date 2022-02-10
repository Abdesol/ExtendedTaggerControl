using System;
using System.Collections;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace ExtendedTaggerControl.Sample.Controllers
{
    public class ExtendedTaggerControl : UserControl
    {
        static ExtendedTaggerControl()
        {
            WatermarkProperty.Changed.Subscribe(WatermarkPropertyChanged);
            TagsProperty.Changed.Subscribe(TagsPropertyChanged);
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
                defaultBindingMode: BindingMode.TwoWay);
        
        public IEnumerable Tags
        {
            get => GetValue(TagsProperty);
            set => SetValue(TagsProperty, value);
        }
        
        private static void TagsPropertyChanged(AvaloniaPropertyChangedEventArgs<IEnumerable> e)
        {
            if (e.Sender is ExtendedTaggerControl ctrl)
            {
                var newTags = e.NewValue.Value as ObservableCollection<string>;
                ((ctrl.DataContext as ExtendedTaggerControlViewModel)!).Tags =
                    newTags;
            }
        }
    }
    
    public static class Extensions
    {
        public static void GetControl<T>(this Control control, string name, out T element) where T : Control
        {
            element = control.FindControl<T>(name);
        }
    }
}