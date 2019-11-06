using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InteractiveMusicScales
{
    //==============================================================
    // Reasons why this class breaks MVVM's 'no code-behind' rule:
    // 1) Main reason is to give user 'Esc' and 'Enter' hotkeys - ability to close this window or apply their choice by hitting one keyboard button, as we used to in other web and desktop apps
    // I, in my heart, strongly believe that user experience, usability and flexibility must be prioritized over patterns.
    // I could be wrong or missing something to make it possible even without code-behind and this decision wasn't easy, but I made it that way
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
        public string ScaleName { get; set; }
        public Note[] Notes { get; }
        public Note KeynoteOfChoice { get; set; }

        public ScaleSaveDialogWindow(Note[] notes)
        {
            this.Notes = notes;

            InitializeComponent();
        }
    }
}
