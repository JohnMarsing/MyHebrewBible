
export function setFocusToElement(elementId) {
  var element = document.getElementById(elementId);
  if (element) {
    //console.log(`Element with ID '${elementId}' WAS found.`);
    element.focus();
  }
  //else {
  //  console.log(`Element with ID '${elementId}' was NOT found.`);
  //}
}

export function scrollToElement(id) {
  const element = document.getElementById(id);
  if (element) {
    element.scrollIntoView({ behavior: 'smooth' });
  }
}