using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MedicineRegistry.Mobile.Models;
using Microsoft.Identity.Client;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MedicineRegistry.Mobile.Auth
{
  public static class AuthService
  {
    private static string clientId = "b2fc0781-cf1a-4f48-96e2-e7166bb3dd53";
    private static string redirectUri = "msauth://com.companyname.medicineregistry.mobile/G131MJ+ZCY6W0Ib7+TsO3YCbtFs=";
    private static string authority = "https://login.microsoftonline.com/e6aeb244-3784-46da-96ab-65fe833d15ac";

    private static string[] scopes = { "User.Read" };

    private static IPublicClientApplication pcl =
      PublicClientApplicationBuilder.Create(clientId)
      .WithAuthority(authority)
      .WithRedirectUri(redirectUri)
      .Build();

    public static async Task SignIn(object parentActivity)
    {
      // Check if token is still valid ( <= 5 min to expire) and if yes, then return

      AuthenticationResult authResult = null;
      IEnumerable<IAccount> accounts = await pcl.GetAccountsAsync();

      try
      {
        IAccount firstAccount = accounts.FirstOrDefault();
        authResult = await pcl.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();

      }
      catch (MsalUiRequiredException)
      {
        try
        {
          authResult = await pcl.AcquireTokenInteractive(scopes)
            .WithParentActivityOrWindow(parentActivity)
            .ExecuteAsync();
        }
        catch (Exception ex)
        {
        }
      }

      if (authResult is { })
      {
        // await SecureStorage.SetAsync("AccessToken", authResult.AccessToken);

        MessagingCenter.Send<Application, bool>(Application.Current, MessageType.SignIn.ToString(), true);
      }
    }
  }
}
