let dotNetHelper;

export function getOnlineStatus() {
    return navigator.onLine;
}

export function initializeConnectionListener(dotNetRef) {
    dotNetHelper = dotNetRef;

    window.addEventListener('online', updateStatus);
    window.addEventListener('offline', updateStatus);
}

function updateStatus() {
    if (dotNetHelper) {
        dotNetHelper.invokeMethodAsync('UpdateConnectionStatus', navigator.onLine);
    }
}

export function dispose() {
    window.removeEventListener('online', updateStatus);
    window.removeEventListener('offline', updateStatus);
    dotNetHelper = null;
}