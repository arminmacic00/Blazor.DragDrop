# Blazor.DragDrop
[![NuGet Version](https://img.shields.io/nuget/v/RMN.Blazor.DragDrop?logo=nuget&style=plastic)](https://www.nuget.org/packages/RMN.Blazor.DragDrop)

This component provides user-friendly drag and drop functionality for Blazor applications.

## Features
- Drag and drop items within a list, both horizontally and vertically.
- Drag and drop items between multiple lists. (Coming soon)
- Choose any HTML element to serve as the parent.
- Animated movement of items.
- Support for both desktop and mobile devices.
- And more...

## How to set up
1. Install the `RMN.Blazor.DragDrop` NuGet package in your project.

2. Add the component namespace to your `_Imports.razor` file:
```razor
@using RMN.Blazor.DragDrop
```

3. Add `SortableJS`:
```html
<script src="https://cdn.jsdelivr.net/npm/sortablejs@latest/Sortable.min.js"></script>
```

## Parameters and Events
| Name                 | Type              | Default | Description |
| -------------------- | ----------------- | ------- | ----------- |
| Items                | List&lt;TItem&gt; | [ ]     | List of items. |
| OrderPropertyName    | String            | ""      | Item's order property to update after reordering the list. |
| RootElement          | String            | "div"   | Element that will serve as the parent. |
| Id                   | String            | Guid    | Id for the parent element. |
| Class                | String            | ""      | CSS classes for the parent element. |
| Style                | String            | ""      | Inline styles for the parent element. |
| DragHandleClass      | String            | ""      | CSS class for the drag handle. |
| UndraggableItemClass | String            | ""      | CSS class for undraggable items. |
| AllowReorder         | Boolean           | true    | Allow reordering the list. |
| AllowDragging        | Boolean           | true    | Allow dragging of items. |
| Context              | String            | context | Parameter name for the list items. |
| OnUpdate             | EventCallback     |         | The method to be called after reordering the list. |

## Examples

### Basic example
```html
<DragDropList Items="Items" Context="item">
    <p>@item.Name</p>
</DragDropList>
```

### Advanced example
```html
<DragDropList Items="Items"
              OrderPropertyName="Order"
              RootElement="ul"
              DragHandleClass="drag-handle"
              UndraggableItemClass="undraggable-item"
              Context="item"
              OnUpdate="OnListUpdateAsync">

    <li class="@(item.Disabled ? "undraggable-item" : null)>
        <i class="drag-handle icon-drag-handle"></i>
        <span>@item.Name</span>
    </li>

</DragDropList>
```

For updating the order property of your items after reordering the list, you can either specify the `OrderPropertyName` as shown in the example above, or update manually:
```cs
public async Task OnListUpdateAsync() 
{
    Items.ForEach(x => x.Order = Items.IndexOf(x));

    // Saving to database or something else
}
```

## Styling
For styling the item that is being dragged, use the class selector `.dragging-item`.

## License
Project is licensed under the [MIT](https://github.com/aarm1n/Blazor.DragDrop/blob/main/LICENSE) license.
