using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InteractiveMusicScales
{
    //==============================================================
    // Reasons why this class breaks MVVM's 'no code-behind' rule:
    // 1) To give user 'Esc' and 'Enter' hotkeys - ability to close this window or apply their choice by hitting one keyboard button, as we used to in other web and desktop apps
    // 2) InterfaceData class is already sized enough, small code allocation to other classes, especially code that specific for this exact functionality seems like a necessity to me
    // 3) Ease of management over this dialog result - all management is now may be concentrated in a single method body of InterfaceData class
    //==============================================================

    /// <summary>
    /// Interaction logic for ScaleSaveDialogWindow.xaml
    /// </summary>
    partial class ScaleSaveDialogWindow : Window
    {
        //==============================================================
        //Properties
        public Note[] Notes { get; }

        public Note KeynoteOfChoice { get; set; }
        public string ScaleName { get; set; }

        //==============================================================
        //Constructor
        public ScaleSaveDialogWindow(Note[] notes)
        {
            this.Notes = notes;

            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            this.ScaleNameInputText.Focus();
        }

        //==============================================================
        //Handlers
        private void OKButton_Click(object sender, RoutedEventArgs e) => TryToApplyChoice();

        private void CancelButton_Click(object sender, RoutedEventArgs e) => CancelDialog();

        //==============================================================
        //Methods
        private void TryToApplyChoice()
        {
            //Trigger databinding so that inserted scale name would be delivered
            this.ScaleNameInputText.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            bool allIsGood = true;

            if (string.IsNullOrEmpty(ScaleName))
            {
                this.ScaleNameInputText.Background = new SolidColorBrush(Colors.LightCoral);
                allIsGood = false;
            }

            if (KeynoteOfChoice == null)
            {
                this.ComboBoxSelectionBack.Background = new SolidColorBrush(Colors.LightCoral);
                allIsGood = false;
            }

            if (allIsGood)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void CancelDialog()
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}