window.pwaUpdate = {
    async applyUpdate() {
        if (!("serviceWorker" in navigator)) {
            return false;
        }

        const registration = await navigator.serviceWorker.getRegistration();
        if (!registration) {
            return false;
        }

        let reloading = false;

        navigator.serviceWorker.addEventListener("controllerchange", () => {
            if (reloading) {
                return;
            }

            reloading = true;
            window.location.reload();
        });

        await registration.update();

        if (registration.waiting) {
            registration.waiting.postMessage("SKIP_WAITING");
            return true;
        }

        return false;
    }
};