using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HATrackScanUI.ViewModels
{
    class MainWindowViewModel : NotificationObject
    {
        private string windowTitle;

        public string WindowTitle
        {
            get { return windowTitle; }
            set
            {
                windowTitle = value;
                this.RaisePropertyChanged("WindowTitle");
            }
        }

        public MainWindowViewModel()
        {
            this.WindowTitle = "淮安鹏鼎轨道扫码软体";
        }
    }
}
