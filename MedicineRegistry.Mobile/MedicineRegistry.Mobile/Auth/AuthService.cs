using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineRegistry.Mobile.Models;
using Microsoft.Identity.Client;
using Xamarin.Forms;

namespace MedicineRegistry.Mobile.Auth
{
  public static class AuthService
  {
    private static string clientId = "b2fc0781-cf1a-4f48-96e2-e7166bb3dd53";
    private static string redirectUri = "msauth://com.companyname.medicineregistry.mobile/G131MJ+ZCY6W0Ib7+TsO3YCbtFs=";
    private static string authority = "https://login.microsoftonline.com/e6aeb244-3784-46da-96ab-65fe833d15ac";
    private static string[] scopes = { "User.Read", "api://f1a4ac9f-77e9-40ee-9865-7961e0b3608c/access-as-user" };
    private static IPublicClientApplication pcl =
      PublicClientApplicationBuilder.Create(clientId)
      .WithAuthority(authority)
      .WithRedirectUri(redirectUri)
      .Build();

    public static AuthenticationResult AuthResult = null;

    public static async Task SignIn(object parentActivity)
    {
      MessagingCenter.Send<Application, bool>(Application.Current, MessageType.SignIn.ToString(), false);

      IEnumerable<IAccount> accounts = await pcl.GetAccountsAsync();

      try
      {
        IAccount firstAccount = accounts.FirstOrDefault();
        AuthResult = await pcl.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();

      }
      catch (MsalUiRequiredException)
      {
        try
        {
          AuthResult = await pcl.AcquireTokenInteractive(scopes)
            .WithParentActivityOrWindow(parentActivity)
            .ExecuteAsync();
        }
        catch (Exception ex)
        {
        }
      }

      if (AuthResult is { })
      {
        MessagingCenter.Send<Application, bool>(Application.Current, MessageType.SignIn.ToString(), true);
      }
    }

    public static bool IsTokenExpired()
    {
      TimeSpan timeDifference = AuthResult.ExpiresOn.UtcDateTime - DateTime.UtcNow;
      return timeDifference.TotalMinutes <= 5;
    }
  }
}
