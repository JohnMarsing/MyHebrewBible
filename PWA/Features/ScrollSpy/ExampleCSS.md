
Example CSS for a Floating Action Button (FAB) with options and backdrop. 
This CSS defines the styles for the FAB container, main button, option buttons, and backdrop, 
along with animations for sliding in the options and fading in the backdrop.

```css
.fab-container {
  position: fixed;
  bottom: 20px;
  left: 20px;
  z-index: 1000;
}

.fab {
  width: 56px;
  height: 56px;
  border-radius: 50%;
  border: none;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: all 0.3s ease;
  font-size: 1.5rem;
}

.fab-main {
  background: var(--bs-primary, #0d6efd);
  color: white;
  position: relative;
  z-index: 1002;
}

  .fab-main:hover {
    transform: scale(1.1);
    box-shadow: 0 6px 12px rgba(0, 0, 0, 0.4);
  }

  .fab-main.fab-open {
    transform: rotate(180deg);
  }

.fab-option {
  position: absolute;
  left: 0;
  width: 48px;
  height: 48px;
  border-radius: 50%;
  background: var(--bs-secondary, #6c757d);
  color: white;
  border: none;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  font-size: 1.2rem;
  animation: fab-slide-in 0.2s ease-out;
  z-index: 1001;
}

  .fab-option:hover {
    background: var(--bs-dark, #212529);
    transform: scale(1.05);
  }

.fab-up {
  bottom: 140px;
}

.fab-down {
  bottom: 80px;
}

.fab-backdrop {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.3);
  z-index: 999;
  animation: fade-in 0.2s ease-out;
}

@keyframes fab-slide-in {
  from {
    opacity: 0;
    transform: translateY(20px);
  }

  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes fade-in {
  from {
    opacity: 0;
  }

  to {
    opacity: 1;
  }
}

```