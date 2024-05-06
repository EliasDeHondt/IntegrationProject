document.addEventListener('DOMContentLoaded', function() {
    setupNavigation();
});
export function setupNavigation() {
    const navigationBar = document.getElementById('navbar') as HTMLElement;
    const mainContent = document.getElementById('maincontent') as HTMLElement;
    const collapseButton = document.getElementById('collapseButton') as HTMLElement;
    const languageButton = document.getElementById('language') as HTMLElement;
    const accountname = document.getElementById('tohideAN') as HTMLElement;
    const dashboard = document.getElementById('tohideD') as HTMLElement;
    const statistics = document.getElementById('tohideS') as HTMLElement;
    const signout = document.getElementById('tohideLO') as HTMLElement;
    const icon = document.getElementById('icon') as HTMLElement;

    collapseButton.addEventListener('click', function(e) {
        e.preventDefault();
        navigationBar.classList.toggle('collapsed');

        // Toggle icon
        icon.classList.toggle('bi-caret-left-fill');
        icon.classList.toggle('bi-caret-right-fill');
    });

    collapseButton.addEventListener('click', function() {
        if (navigationBar.classList.contains('collapsed')) {
            languageButton.style.display = 'none';
            accountname.style.display = 'none';
            dashboard.style.display = 'none';
            statistics.style.display = 'none';
            signout.style.display = 'none';

            collapseButton.style.margin = 'auto';
            collapseButton.style.display = 'block';
        } else {
            languageButton.style.display = 'block';
            accountname.style.display = 'block';
            dashboard.style.display = 'block';
            statistics.style.display = 'block';
            signout.style.display = 'block';
            mainContent.style.paddingRight = '0'; // terug naar origineel

            collapseButton.style.margin = 'unset';
        }
    });
}
