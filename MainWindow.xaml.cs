using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using note2;

namespace WpfTutorialSamples.Rich_text_controls
{
	public partial class RichTextEditorSample : Window
	{
		public RichTextEditorSample()
		{
			InitializeComponent();
			cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
		}

		private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
		{


			//CharCount.Text = CharCount.Text + "\nCharacter count is " + rtbEditor.Document.Content.Length + "/2000000000" ;

		}

		private void New_Executed(object sender, RoutedEventArgs e)
		{
			rtbEditor.Document.Blocks.Clear();
			Filename.Text = " ";
		}

		private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog()
			{
				Title = "Open file",
				Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*",
                FileName = ""
            };

            if (open.ShowDialog() == true)
			{
				FileStream fileStream = new FileStream(open.FileName, FileMode.Open);
				TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
				range.Load(fileStream, DataFormats.Rtf);
			}
            Filename.Text = "Filename: " + System.IO.Path.GetFileNameWithoutExtension(open.FileName);
		}

		private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
		{


			SaveFileDialog save = new SaveFileDialog()
            {
                Title = "Save your file",
                Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*",
                FileName = " "
            };
            if (save.ShowDialog() == true)
			{

                StreamWriter streamwriter = new StreamWriter(File.Create(save.FileName));
                streamwriter.Write(save.FileName);
                streamwriter.Dispose();


            }

            Filename.Text = "Filename: " + System.IO.Path.GetFileNameWithoutExtension(save.FileName);
        }

		private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(cmbFontFamily.SelectedItem != null)
				rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
		}

		private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
		{
			rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
		}

        private void about_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }

        private void Error_Click(object sender, RoutedEventArgs e)
        {
            Error err = new Error();
            err.Show();
        }
    }

}

