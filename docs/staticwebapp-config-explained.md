# staticwebapp.config.json Overview

## What Is It?

`staticwebapp.config.json` is the **configuration file for Azure Static Web Apps**. It controls how Azure serves your application, handles routing, applies security rules, and sets HTTP headers.

## Where It Goes

- **Location**: `wwwroot` folder (root of your published app)
- **Deployment**: Automatically picked up by Azure Static Web Apps when deployed
- **Scope**: Only applies when hosted on Azure Static Web Apps (not IIS, Nginx, etc.)

---

## Key Features

### 1. **Routes & Custom Headers**

Define HTTP headers for specific files or paths:


```
{ "routes": [ { "route": "/service-worker.js", "headers": { "Cache-Control": "no-cache, no-store, must-revalidate" } } ] }
```

**In your case:**
- Prevents browsers from caching service worker files
- Ensures users always get the latest service worker version after deployment
- Critical for PWA updates

### 2. **Navigation Fallback (SPA Support)**

```
{ "navigationFallback": { "rewrite": "/index.html" } }
```

**What this does:**
- Routes all non-file requests to `index.html`
- Enables client-side routing for Blazor WebAssembly
- Allows direct navigation to routes like `/Gen/1/1/0` without 404 errors

### 3. **Other Capabilities** (not in your current config)

#### Authentication & Authorization

```
{ "routes": [ { "route": "/service-worker.js", "headers": { "Cache-Control": "no-cache, no-store, must-revalidate" } } ] }
```

#### Redirects

```
{ "routes": [ { "route": "/old-path", "redirect": "/new-path", "statusCode": 301 } ] }
```


#### Custom Error Pages

```
{ "responseOverrides": { "404": { "rewrite": "/404.html" } } }
```


#### MIME Types
```
{ "mimeTypes": { ".json": "application/json" } }
```


---

## Your Current Configuration Explained

```
{ "routes": [ // Prevent caching of service worker files so users get updates { "route": "/service-worker.js", "headers": { "Cache-Control": "no-cache, no-store, must-revalidate" } }, { "route": "/service-worker.published.js", "headers": { "Cache-Control": "no-cache, no-store, must-revalidate" } }, { "route": "/service-worker-assets.js", "headers": { "Cache-Control": "no-cache, no-store, must-revalidate" } } ], // Enable SPA routing for Blazor WebAssembly "navigationFallback": { "rewrite": "/index.html" } }
```


**Purpose:**
1. ✅ **Fixes PWA update issues** - Service workers are never cached by the browser
2. ✅ **Enables deep linking** - URLs like `/Gen/1/1/0` work when shared or refreshed
3. ✅ **Maintains offline support** - Service worker controls caching, not Azure

---

## When Does It Apply?

| Environment | Uses staticwebapp.config.json? |
|-------------|-------------------------------|
| Azure Static Web Apps | ✅ Yes |
| Local `dotnet run` | ❌ No |
| Local published build (dotnet-serve) | ❌ No |
| IIS / IIS Express | ❌ No (uses `web.config`) |
| Nginx | ❌ No (uses nginx config) |
| GitHub Pages | ❌ No |

---

## Documentation

📚 [Azure Static Web Apps configuration reference](https://learn.microsoft.com/en-us/azure/static-web-apps/configuration)

---

## Summary

`staticwebapp.config.json` is Azure's way of letting you configure server behavior without touching server code. For your PWA, it's essential for:
- Preventing service worker caching issues
- Enabling Blazor's client-side routing
- Ensuring smooth app updates for users

---

## Related Files in This Project

- `PWA/wwwroot/staticwebapp.config.json` - The actual configuration file
- `PWA/wwwroot/service-worker.published.js` - Service worker that benefits from these settings