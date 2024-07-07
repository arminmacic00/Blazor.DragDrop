export function init(id, dragHandleClass, undraggableItemClass, allowDragging, allowReorder, component) {
    Sortable.create(document.getElementById(id), {
        animation: 200,
        handle: dragHandleClass != '' ? `.${dragHandleClass}` : '',
        filter: undraggableItemClass != '' ? `.${undraggableItemClass}` : '',
        disabled: !allowDragging,
        sort: allowReorder,
        forceFallback: true,
        chosenClass: 'dragging-item',
        ghostClass: 'dragging-ghost',
        onUpdate: event => {
            event.item.remove();
            event.to.insertBefore(event.item, event.to.childNodes[event.oldIndex]);

            component.invokeMethodAsync('OnUpdateJS', event.oldIndex, event.newIndex);
        }
    });
}
