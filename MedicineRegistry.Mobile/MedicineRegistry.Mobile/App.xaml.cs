using MedicineRegistry.Mobile.Auth;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicineRegistry.Mobile
{
  public partial class App : Application
  {
    public static object ParentWindow { get; set; }

    public App()
    {
      InitializeComponent();

      // DependencyService.Register<MockDataStore>();

      MainPage = new AppShell();
      
    }

    protected async override void OnStart()
    {
      await AuthService.SignIn(ParentWindow);
    }

    protected override void OnSleep()
    {
    }

    protected async override void OnResume()
    {
      if (AuthService.AuthResult is { } && AuthService.IsTokenExpired())
      {
        await AuthService.SignIn(ParentWindow);
      }
    }
  }
}
