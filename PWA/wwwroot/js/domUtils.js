export function setFocusToElement(elementId) {
  const element = document.getElementById(elementId);
  if (element) {
    element.focus();
  }
}

export function scrollToElement(id) {
  const element = document.getElementById(id);
  if (element) {
    element.scrollIntoView({ behavior: 'smooth' });
  }
}