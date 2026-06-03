# Prompt
> On my phone, I notice that when my Progressive Web App sits to long it gives me a 404 (see attached) why does it do that?
> ➡️[Grok](https://grok.com/share/bGVnYWN5LWNvcHk_1428ccb9-b1be-40f8-a8ab-090f42d15a9d)


**The 404 error you're seeing after the PWA sits idle is very common with Blazor WebAssembly PWAs hosted on Azure (especially Azure Static Web Apps).**

### Why This Happens
Blazor WASM PWAs rely heavily on **client-side routing** (handled by the browser + your `Router` component). When the app has been in the background for a while:

- The browser's service worker (which powers the PWA offline/caching behavior) may evict or fail to serve cached assets.
- The app attempts a navigation or resource fetch that goes to the network.
- Azure Static Web Apps (or App Service) doesn't find a static file matching that URL → returns the default Azure 404 page.

This is especially noticeable on **deep links**, refreshes, or after inactivity because the server doesn't know about your client-side routes (e.g. `/some-page`).

### Most Likely Fix: Configure Fallback Routing
Create (or update) a file called **`staticwebapp.config.json`** in your **PWA/wwwroot** folder with this content:

```json
{
  "navigationFallback": {
    "rewrite": "/index.html"
  },
  "responseOverrides": {
    "404": {
      "rewrite": "/index.html"
    }
  }
}
```

This tells Azure Static Web Apps to serve your `index.html` for any unknown route, letting Blazor handle the routing.

### Testing
**After adding the file:**
1. Rebuild and redeploy.
2. Clear your browser cache / PWA data (or uninstall + reinstall the PWA).
3. Test by letting it sit idle and reopening.

### Additional Checks for Your Repo
- **Service Worker**: Blazor PWAs generate one automatically. Make sure `PWA.csproj` has `<ServiceWorkerAssetsManifest>service-worker-assets.js</ServiceWorkerAssetsManifest>` or similar.
- **Manifest**: Ensure `wwwroot/manifest.json` exists and is referenced correctly.
- **Deployment**: Confirm you're deploying the `PWA` project output to Azure Static Web Apps (not a regular App Service unless you want server-side capabilities).

### Other Possible Causes
- **Cache Strategy**: If you're using a custom service worker, it might have aggressive caching that fails after expiration. Blazor's default is usually fine.
- **Custom Domain**: Sometimes routing behaves differently with custom domains vs. the `*.azurestaticapps.net` URL.
- **Azure Load Balancer Idle Timeout**: Less likely for a pure static PWA, but Azure drops idle connections after ~4 minutes by default. Not usually the root cause here.

Would you like me to look at a specific file in your repo (e.g., Program.cs, App.razor, or the wwwroot folder) for more targeted advice? Just share the link or describe what you're seeing in the console/dev tools when the 404 occurs.