export function init(params) {
    Sortable.create(document.getElementById(params.id), {
        handle: params.dragHandleClass ? `.${params.dragHandleClass}` : '',
        filter: params.undraggableItemClass ? `.${params.undraggableItemClass}` : '',
        sort: params.allowReorder,
        disabled: !params.allowDragging,
        animation: 200,
        chosenClass: 'dragging-item',
        ghostClass: 'dragging-ghost',
        forceFallback: true,
        onUpdate: event => {
            event.item.remove();
            event.to.insertBefore(event.item, event.to.childNodes[event.oldIndex]);

            params.component.invokeMethodAsync('OnUpdateJS', event.oldIndex, event.newIndex);
        }
    });
}
