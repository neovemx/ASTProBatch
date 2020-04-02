using AST_ProBatch_Mobile.ViewModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace AST_ProBatch_Mobile.Infrastructure
{
    public class InstanceLocator
    {
        #region Properties
        public MainViewModel Main { get; set; }
        #endregion

        #region Constructors
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
