using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedTaggerControl.Sample.Controls
{
    public class ExtendedTaggerControl : TemplatedControl
    {
        private TextBox? _addNewTagTextBox;

        /// <summary>
        /// Defines the <see cref="Tags"/> property.
        /// </summary>
        public static readonly DirectProperty<ExtendedTaggerControl, IList> TagsProperty =
            AvaloniaProperty.RegisterDirect<ExtendedTaggerControl, IList>(
                nameof(Tags),
                o => o.Tags,
                (o, v) => o.Tags = v);

        private IList _tags = new AvaloniaList<object>();

        /// <summary>
        /// Gets or sets the Tags-Property
        /// </summary>
        public IList Tags
        {
            get { return _tags; }
            set { SetAndRaise(TagsProperty, ref _tags, value); }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            // If we already have a textbox let's unsubscripe from the event list
            if (_addNewTagTextBox is not null)
            {
                _addNewTagTextBox.KeyDown -= _addNewTagTextBox_KeyDown;
            }

            // Find the text box in the control template
            _addNewTagTextBox = e.NameScope.Find<TextBox>("PART_AddNewTagTextBox");

            // Listen to Key-Events
            if (_addNewTagTextBox is not null)
            {
                _addNewTagTextBox.KeyDown += _addNewTagTextBox_KeyDown;
            }
        }

        private void _addNewTagTextBox_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            if (sender is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        Tags?.Add(textBox.Text);
                        textBox.Clear();
                        break;

                    case Key.Escape:
                        textBox.Clear();
                        break;
                }
            }

        }
    }
}
