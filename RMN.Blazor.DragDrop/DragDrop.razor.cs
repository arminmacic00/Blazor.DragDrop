using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMN.Blazor.DragDrop
{
    public partial class DragDrop<T>
    {
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }



        [Parameter]
        public List<T> Items { get; set; } = new List<T>();

        [Parameter]
        public string RootElement { get; set; } = "div";

        [Parameter]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Parameter]
        public string Class { get; set; } = string.Empty;

        [Parameter]
        public string Handle { get; set; } = string.Empty;

        [Parameter]
        public string Filter { get; set; } = string.Empty;

        [Parameter]
        public bool Sort { get; set; } = true;

        [Parameter]
        public EventCallback OnUpdate { get; set; }

        [Parameter]
        public RenderFragment<T>? DragDropItem { get; set; }

        private DotNetObjectReference<DragDrop<T>>? selfReference;



        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                selfReference = DotNetObjectReference.Create(this);

                var module = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", "./_content/RMN.Blazor.DragDrop/DragDrop.razor.js");

                await module.InvokeAsync<string>(
                    "init",
                    Id,
                    Handle,
                    Filter,
                    Sort,
                    selfReference
                );
            }
        }

        private RenderFragment RenderContent() => builder =>
        {
            var sequence = 0;

            builder.OpenElement(sequence++, RootElement);

            builder.AddAttribute(sequence++, "id", Id);

            if (!string.IsNullOrEmpty(Class))
                builder.AddAttribute(sequence++, "class", Class);

            foreach (var item in Items)
                builder.AddContent(sequence++, DragDropItem!(item));

            builder.CloseElement();
        };

        [JSInvokable]
        public void OnUpdateJS(int oldIndex, int newIndex)
        {
            var itemToMove = Items[oldIndex];

            Items.RemoveAt(oldIndex);
            Items.Insert(newIndex, itemToMove);

            StateHasChanged();

            OnUpdate.InvokeAsync();
        }
    }
}
