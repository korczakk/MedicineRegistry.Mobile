using MedicineRegistry.Mobile.Models;
using Xamarin.Forms;

namespace MedicineRegistry.Mobile.ViewModels
{
  public class MainPageViewModel : BaseViewModel
  {
    public MainPageViewModel()
    {
      ShowLoaderWhenSigningIn = true;

      MessagingCenter.Subscribe<Application, bool>(this, MessageType.SignIn.ToString(), (sender, isSignedIn) =>
      {
        ShowLoaderWhenSigningIn = !isSignedIn;
      });
    }

    private bool showLoaderWhenSigningIn;
    public bool ShowLoaderWhenSigningIn
    {
      get { return showLoaderWhenSigningIn; }
      set { SetProperty(ref showLoaderWhenSigningIn, value); }
    }
  }
}