using Microsoft.AspNetCore.Blazor.Browser.Interop;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Shared
{
    public class PagerModel : BlazorComponent
    {
        [Parameter] protected Paging Model { get; set; }
        [Parameter]
        protected Action<int> PageChanged { get; set; }


        protected bool Last(int i) => i <= Model.CurrentPage + 5 && i <= Model.PageCount;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        protected void PagerButtonClicked(int page)
        {
            PageChanged?.Invoke(page);
        }
    }
}
