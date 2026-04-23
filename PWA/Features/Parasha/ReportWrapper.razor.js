export function scrollToElementById(elementId) {
  const element = document.getElementById(elementId);
  if (element) {
    element.scrollIntoView({ behavior: 'smooth', block: 'center' });
  }
}

export function setFocusToElement(elementId) {
  const element = document.getElementById(elementId);
  if (element) {
    element.focus();
    // Make it keyboard-accessible if not naturally focusable
    if (!element.hasAttribute('tabindex')) {
      element.setAttribute('tabindex', '-1');
    }
  }
}