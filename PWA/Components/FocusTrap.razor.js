const _focusTraps = new Map();

export function trapFocus(modalElement) {
	const focusable = () => Array.from(
		modalElement.querySelectorAll(
			'a[href],button:not([disabled]),input:not([disabled]),select:not([disabled]),textarea:not([disabled]),[tabindex]:not([tabindex="-1"])'
		)
	).filter(el => !el.closest('[hidden]'));

	function handler(e) {
		if (e.key !== 'Tab') return;
		const els = focusable();
		if (!els.length) { e.preventDefault(); return; }
		const first = els[0];
		const last = els[els.length - 1];
		if (e.shiftKey) {
			if (document.activeElement === first) { e.preventDefault(); last.focus(); }
		} else {
			if (document.activeElement === last) { e.preventDefault(); first.focus(); }
		}
	}

	document.addEventListener('keydown', handler, true);
	_focusTraps.set(modalElement, handler);
}

export function releaseFocusTrap(modalElement) {
	const handler = _focusTraps.get(modalElement);
	if (handler) {
		document.removeEventListener('keydown', handler, true);
		_focusTraps.delete(modalElement);
	}
}
