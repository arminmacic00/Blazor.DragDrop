using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMN.Blazor.DragDrop
{
    public partial class DragDropList<TItem>
    {
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }



        [Parameter]
        public List<TItem> Items { get; set; } = new List<TItem>();

        [Parameter]
        public string RootElement { get; set; } = "div";

        [Parameter]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Parameter]
        public string Class { get; set; } = string.Empty;

        [Parameter]
        public string DragHandleClass { get; set; } = string.Empty;

        [Parameter]
        public string UndraggableItemClass { get; set; } = string.Empty;

        [Parameter]
        public bool AllowReorder { get; set; } = true;

        [Parameter]
        public EventCallback OnUpdate { get; set; }

        [Parameter]
        public RenderFragment<TItem>? ItemTemplate { get; set; }

        private DotNetObjectReference<DragDropList<TItem>>? selfReference;



        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                selfReference = DotNetObjectReference.Create(this);

                var module = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", "./_content/RMN.Blazor.DragDrop/DragDropList.razor.js");

                await module.InvokeAsync<string>(
                    "init",
                    Id,
                    DragHandleClass,
                    UndraggableItemClass,
                    AllowReorder,
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
                builder.AddContent(sequence++, ItemTemplate!(item));

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
