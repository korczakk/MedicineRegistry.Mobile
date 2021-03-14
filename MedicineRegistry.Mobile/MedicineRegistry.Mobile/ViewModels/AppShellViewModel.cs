using MedicineRegistry.Mobile.Auth;
using MedicineRegistry.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MedicineRegistry.Mobile.ViewModels
{
  public class AppShellViewModel : BaseViewModel
  {
    public AppShellViewModel()
    {
      MessagingCenter.Subscribe<Application, bool>(this, MessageType.SignIn.ToString(), (Application sender, bool isSignedIn) =>
      {
        IsSignedIn = isSignedIn;
      });
    }

    private bool isSignedIn;
    public bool IsSignedIn {
      get { return isSignedIn; }
      set { SetProperty(ref isSignedIn, value); }
    }

  }
}
