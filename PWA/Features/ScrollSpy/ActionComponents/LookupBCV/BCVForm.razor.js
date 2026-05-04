export function initializeTypeahead(inputElement, dotNetHelper) {
    if (!inputElement) return;

    inputElement.addEventListener('keydown', async (e) => {
        const key = e.key;
        if (['ArrowDown', 'ArrowUp', 'Enter', 'Escape'].includes(key)) {
            e.preventDefault();
            await dotNetHelper.invokeMethodAsync('HandleKeyDown', key);
        }
    });

    inputElement.addEventListener('input', async () => {
        await dotNetHelper.invokeMethodAsync('HandleKeyDown', 'Input');
    });

    // Auto-focus the search input
    inputElement.focus();
}