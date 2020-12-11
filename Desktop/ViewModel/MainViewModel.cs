using Desktop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private APIService _model;

        public MainViewModel(APIService model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            _model = model;
        }
    }
}
