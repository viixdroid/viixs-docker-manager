(function setThemeOnLoad(): void {
    const savedTheme = localStorage.getItem('theme') || 'nord';
    console.log("Setting theme on load...: " + savedTheme);
    // let mainBody = document.getElementById("mainBody");
    // if (mainBody) {
    //     mainBody.setAttribute('data-theme', savedTheme);
    // }
    document.documentElement.setAttribute('data-theme', savedTheme);
})();