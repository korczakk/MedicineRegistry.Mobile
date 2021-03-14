using MedicineRegistry.Mobile.ViewModels;
using MedicineRegistry.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MedicineRegistry.Mobile
{
  public partial class AppShell : Xamarin.Forms.Shell
  {
    AppShellViewModel vm;

    public AppShell()
    {
      InitializeComponent();
      Routing.RegisterRoute(nameof(AddMedicinePage), typeof(AddMedicinePage));

      BindingContext = vm = new AppShellViewModel();
    }

  }
}
