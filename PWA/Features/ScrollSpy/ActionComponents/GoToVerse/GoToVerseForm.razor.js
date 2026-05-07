export function focusElement(element) {
	if (element) {
		// Small delay to ensure modal animation completes
		setTimeout(() => {
			element.focus();
		}, 150);
	}
}