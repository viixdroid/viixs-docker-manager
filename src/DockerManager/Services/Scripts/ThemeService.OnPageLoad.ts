(function setThemeOnLoad(): void {
    console.log("Setting theme on load...");
    const savedTheme = localStorage.getItem('theme') || 'nord';
    document.documentElement.setAttribute('data-theme', savedTheme);
})();