using DomainModel;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;

namespace Client.Pages.Users
{
    public class UsersModel : BlazorComponent
    {
        [Inject] protected HttpClient Http { get; set; }
        protected User[] Users = Array.Empty<User>();
        protected bool Loading;

        protected override async Task OnInitAsync()
        {
            Loading = true;
            Users = await Http.GetJsonAsync<User[]>("/api/users");
            Loading = false;
        }

        protected async Task RefreshTable()
        {
            await OnInitAsync();
            StateHasChanged();
        }

        protected void StartLoadingSpinner()
        {
            Loading = true;
            StateHasChanged();
        }
    }
}
