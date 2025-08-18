export function setTheme(theme: string): void {
  const htmlElement = document.documentElement;
  const currentTheme = htmlElement.getAttribute('data-theme');

  if (currentTheme !== theme) {
    htmlElement.setAttribute('data-theme', theme);
  }
}

export function getCurrentTheme(): string {
  const htmlElement = document.documentElement;
  return htmlElement.getAttribute('data-theme') || 'nord';
}

