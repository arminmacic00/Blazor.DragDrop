# Blazor.DragDrop
[![NuGet Version](https://img.shields.io/nuget/v/RMN.Blazor.DragDrop?logo=nuget&style=plastic)](https://www.nuget.org/packages/RMN.Blazor.DragDrop)

This component provides simple and user-friendly drag and drop functionality for Blazor applications.

## Features
- Drag and drop items within a list, both horizontally and vertically.
- Drag and drop items between multiple lists. (Coming soon)
- Fully customizable item template.
- Choose any HTML element to serve as the parent.
- Use a drag handle to allow dragging only by the handle.
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

### Properties
|Name                |Type         |Default|Description|
|--------------------|-------------|-------|-----------|
|Items               |List&lt;T&gt;|[ ]    |List of items.|
|RootElement         |String       |"div"  |Element that will serve as the parent.|
|Id                  |String       |Guid   |Id for the parent element.|
|Class               |String       |""     |CSS classes for the parent element.|
|DragHandleClass     |String       |""     |CSS class for the drag handle.|
|UndraggableItemClass|String       |""     |CSS class for items that can't be dragged.|
|AllowReorder        |Boolean      |true   |Enables or disables reordering the list.|
|Context             |String       |context|Parameter name for the list items.|

### Events
`OnUpdate`: The method to be called after reordering the list.

## Examples

### Basic example
```html
<DragDropList Items="Items" Context="item">

    <ItemTemplate>
        <p>@item.Name</p>
    </ItemTemplate>

</DragDropList>
```

### Advanced example
```html
<DragDropList Items="Items"
              RootElement="ul"
              DragHandleClass="drag-handle"
              UndraggableItemClass="undraggable-item"
              Context="item"
              OnUpdate="OnListUpdate">

    <ItemTemplate>
        <li>
            <i class="fa-solid fa-grip-vertical drag-handle @(item.Disabled ? "undraggable-item" : "")"></i>
            <span>@item.Name</span>
        </li>
    </ItemTemplate>

</DragDropList>
```

## Styling
For styling the item that is being dragged, use the class selector `.dragging-item`.

## License
Project is licensed under the [MIT](https://github.com/aarm1n/Blazor.DragDrop/blob/main/LICENSE) license.
