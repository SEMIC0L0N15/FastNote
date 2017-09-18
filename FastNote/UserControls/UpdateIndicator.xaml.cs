using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FastNote.Core;

namespace FastNote
{
    /// <summary>
    /// Interaction logic for UpdateIndicator.xaml
    /// </summary>
    public partial class UpdateIndicator : UserControl
    {
        private bool mFirstTimeAnimated = true;
        private bool mFirstTimeFadeOut = true;

        public UpdateIndicator()
        {
            InitializeComponent();

            DataContext = ViewModelLocator.ApplicationViewModel;
        }

        private async void FadeIn_OnCompleted(object sender, EventArgs e)
        {
            if (mFirstTimeAnimated)
            {
                mFirstTimeAnimated = false;
                return;
            }

            await Task.Delay(1000);
            //MessageBox.Show("Start fade out");
            mFirstTimeAnimated = true;
            CheckIconTextBlock.SetValue(StartFadeOut.ValueProperty, true);

        }

        private void FadeOut_OnCompleted(object sender, EventArgs e)
        {
            if (mFirstTimeFadeOut)
            {
                mFirstTimeFadeOut = false;
                //return;
            }

            //MessageBox.Show("Stop fade out");
            mFirstTimeFadeOut = true;
            CheckIconTextBlock.SetValue(StartFadeOut.ValueProperty, false);
        }
    }
}
