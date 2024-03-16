using Microsoft.AspNetCore.Components;
using Rent2Own_Client.Service.IService;

namespace Rent2Own_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService _authSerivce { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await _authSerivce.Logout();
            _navigationManager.NavigateTo("/");
        }
    }
}
