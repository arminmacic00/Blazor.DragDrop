using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMN.Blazor.DragDrop
{
    public partial class DragDropList<TItem> : IAsyncDisposable
    {
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }



        [Parameter]
        public List<TItem> Items { get; set; } = new List<TItem>();

        [Parameter]
        public string OrderPropertyName { get; set; } = string.Empty;

        [Parameter]
        public string RootElement { get; set; } = "div";

        [Parameter]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Parameter]
        public string Class { get; set; } = string.Empty;

        [Parameter]
        public string Style { get; set; } = string.Empty;

        [Parameter]
        public string DragHandleClass { get; set; } = string.Empty;

        [Parameter]
        public string UndraggableItemClass { get; set; } = string.Empty;

        [Parameter]
        public bool AllowDragging { get; set; } = true;

        [Parameter]
        public bool AllowReorder { get; set; } = true;

        [Parameter]
        public EventCallback OnUpdate { get; set; }

        [Parameter]
        public RenderFragment<TItem>? ChildContent { get; set; }

        [Parameter]
        public RenderFragment<TItem>? ItemTemplate { get; set; }

        private DotNetObjectReference<DragDropList<TItem>>? _selfReference;

        private IJSObjectReference? _jsModuleReference;



        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _selfReference = DotNetObjectReference.Create(this);

                _jsModuleReference = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", "./_content/RMN.Blazor.DragDrop/DragDropList.razor.js");

                await _jsModuleReference.InvokeAsync<string>(
                    "init",
                    Id,
                    DragHandleClass,
                    UndraggableItemClass,
                    AllowDragging,
                    AllowReorder,
                    _selfReference
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

            if (!string.IsNullOrEmpty(Style))
                builder.AddAttribute(sequence++, "style", Style);

            foreach (var item in Items)
                builder.AddContent(sequence++, ChildContent is not null ? ChildContent(item) : ItemTemplate!(item));

            builder.CloseElement();
        };

        [JSInvokable]
        public void OnUpdateJS(int oldIndex, int newIndex)
        {
            var itemToMove = Items[oldIndex];

            Items.Remove(itemToMove);
            Items.Insert(newIndex, itemToMove);

            if (!string.IsNullOrEmpty(OrderPropertyName))
            {
                var orderProperty = typeof(TItem).GetProperty(OrderPropertyName);

                if (orderProperty is not null)
                    Items.ForEach(x => orderProperty.SetValue(x, Items.IndexOf(x)));
            }

            StateHasChanged();

            OnUpdate.InvokeAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_jsModuleReference is not null)
                await _jsModuleReference.DisposeAsync();

            _selfReference?.Dispose();
        }
    }
}
