using DomainModel;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Blazor.Services;

namespace Client.Pages.Users
{
    public class UsersModel : BlazorComponent
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected IUriHelper UriHelper { get; set; }

        [Parameter] protected string Page { get; set; } = "1";
        protected PagedResult<User> pagedResult = new PagedResult<User>();
        protected IEnumerable<User> users = Enumerable.Empty<User>();
        protected bool loading;

        protected override async Task OnInitAsync()
        {
            loading = true;
            pagedResult = await Http.GetJsonAsync<PagedResult<User>>($"/api/users?page={Page}");
            users = pagedResult.Results;
            loading = false;
        }

        protected async Task RefreshTable()
        {
            await OnInitAsync();
            StateHasChanged();
        }

        protected async Task Delete(int id)
        {
            loading = true;
            await Http.DeleteAsync($"/api/users/{id}");
            await RefreshTable();
        }

        protected void PagerPageChanged(int page)
        {
            Console.WriteLine("Current page: " + page);
            UriHelper.NavigateTo("/users/" + page);
            RefreshTable();
        }

    }
}
