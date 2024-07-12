# Blazor.DragDrop
[![NuGet Version](https://img.shields.io/nuget/v/RMN.Blazor.DragDrop?logo=nuget&style=plastic)](https://www.nuget.org/packages/RMN.Blazor.DragDrop)

This component provides simple and user-friendly drag and drop functionality for Blazor applications.

## Features
- Drag and drop items within a list, both horizontally and vertically.
- Drag and drop items between multiple lists. (Coming soon)
- Choose any HTML element to serve as the parent.
- Use a drag handle to drag items only by the handle.
- Fully customizable item template.
- Animated movement of items.
- Support for both desktop and mobile devices.
- And more...

## How to set up
1. Install the **RMN.Blazor.DragDrop** NuGet package in your project.

2. Add the component namespace to your `_Imports.razor` file:
```razor
@using RMN.Blazor.DragDrop
```

3. Add **SortableJS**:
```html
<script src="https://cdn.jsdelivr.net/npm/sortablejs@latest/Sortable.min.js"></script>
```

## API

### Parameters
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
| AllowDragging        | Boolean           | true    | Enables or disables dragging of all items. |
| AllowReorder         | Boolean           | true    | Enables or disables reordering the list. |
| Context              | String            | context | Parameter name for the list items. |

### Events
`OnUpdate`: The method to be called after reordering the list.

## Examples

### Basic example
```html
<DragDropList Items="Items" Context="item">

    <p>@item.Name</p>

</DragDropList>
```

### Advanced example 1
```html
<DragDropList Items="Items"
              RootElement="ul"
              DragHandleClass="drag-handle"
              Context="item"
              OnUpdate="OnListUpdate">

    <li>
        <i class="fa-solid fa-grip-vertical drag-handle"></i>
        <span>@item.Name</span>
    </li>

</DragDropList>
```

### Advanced example 2
```html
<DragDropList Items="Items.OrderBy(x => x.Order).ToList()"
              OrderPropertyName="Order"
              RootElement="tbody"
              UndraggableItemClass="undraggable-item"
              Context="item"
              OnUpdate="OnListUpdate">

    <tr class="@(item.Disabled ? "undraggable-item" : null)">
        <td>@item.Name</td>
    </tr>

</DragDropList>
```

For updating the order property of your items after reordering the list, you have 2 options: 

1. Specify the order property name as shown in the example above.

2. Update manually:
```csharp
public async Task OnListUpdate() 
{
    Items.ForEach(x => x.Order = Items.IndexOf(x));

    // Saving to database or something else
}
```

## Styling
For styling the item that is being dragged, use the class selector `.dragging-item`.

## License
Project is licensed under the [MIT](https://github.com/aarm1n/Blazor.DragDrop/blob/main/LICENSE) license.
