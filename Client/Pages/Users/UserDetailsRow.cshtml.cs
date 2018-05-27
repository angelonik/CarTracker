using DomainModel;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Blazor;
using System.Threading.Tasks;

namespace Client.Pages.Users
{
    public class UserDetailsRowModel : BlazorComponent
    {
        [Inject] protected HttpClient Http { get; set; }
        [Parameter] protected User User { get; set; }
        [Parameter] protected Func<Task> TableChanged { get; set; }
        [Parameter] protected Action StartLoadingSpinner { get; set; }
        protected bool editable = false;
        protected string name;
        protected string email;

        protected override void OnParametersSet()
        {
            name = User.Name;
            email = User.Email;
        }

        protected void EditClicked()
        {
            editable = true;
        }

        protected async Task EditSave()
        {
            User.Name = name;
            User.Email = email;
            await Http.PutJsonAsync($"/api/users/{User.Id}", User);
            editable = false;
        }

        protected void Cancel()
        {
            editable = false;
            OnParametersSet();
        }

        protected async Task Delete()
        {
            StartLoadingSpinner();
            await Http.DeleteAsync($"/api/users/{User.Id}");
            await TableChanged();
        }
    }
}
