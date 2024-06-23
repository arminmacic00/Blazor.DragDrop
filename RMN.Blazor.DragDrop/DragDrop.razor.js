export function init(id, handle, filter, sort, component) {
    Sortable.create(document.getElementById(id), {
        animation: 200,
        handle: handle != '' ? `.${handle}` : '',
        filter: filter != '' ? `.${filter}` : '',
        sort: sort,
        forceFallback: true,
        chosenClass: 'dragging-item',
        ghostClass: 'dragging-ghost',
        onUpdate: event => {
            event.item.remove();
            event.to.insertBefore(event.item, event.to.childNodes[event.oldIndex]);

            component.invokeMethodAsync('OnUpdateJS', event.oldDraggableIndex, event.newDraggableIndex);
        }
    });
}