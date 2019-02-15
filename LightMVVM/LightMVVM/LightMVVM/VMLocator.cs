
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
//using ServiceLocation;
using CommonServiceLocator;
//using Microsoft.Practices.ServiceLocation;

namespace LightMVVM
{
    public class VMLocator
    {
        public VMLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<ListViewModel>();
        }

      public ListViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ListViewModel>();

            }
        }

        public static void Cleanup()
        {

        }
    }
}
