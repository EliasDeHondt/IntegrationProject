document.addEventListener('DOMContentLoaded', function() {
    setupNavigation();
});
export function setupNavigation() {
    const navigationBar = document.getElementById('navbar') as HTMLElement;
    const mainContent = document.getElementById('maincontent') as HTMLElement;
    const collapseButton = document.getElementById('collapseButton') as HTMLElement;
    const languageButton = document.getElementById('language') as HTMLElement;
    const icon = document.getElementById('nav-icon') as HTMLElement;
    const navbarLogo = document.getElementById('navbar-logo-id') as HTMLElement;
    const arrowbutton = document.getElementById('arrow-button') as HTMLElement;
    const hidables = document.getElementsByClassName('toHide') as HTMLCollectionOf<HTMLElement>;
    
    collapseButton.addEventListener('click', function(e) {
        e.preventDefault();
        navigationBar.classList.toggle('collapsed');

        // Toggle icon
        icon.classList.toggle('bi-caret-left-fill');
        icon.classList.toggle('bi-caret-right-fill');
    });

    collapseButton.addEventListener('click', function() {
        if (navigationBar.classList.contains('collapsed')) {

            for (let hidable of hidables) {
                hidable.style.display = 'none';
            }
            
            navbarLogo.style.visibility = 'hidden';

            collapseButton.style.margin = 'auto';
            collapseButton.style.display = 'block';

            navigationBar.style.width = '80px';

            arrowbutton.style.marginLeft = '0';

            arrowbutton.style.marginTop = '2520%';
        } else {

            for (let hidable of hidables) {
                hidable.style.display = 'block';
            }
            
            navbarLogo.style.visibility = 'visible';
            mainContent.style.paddingRight = '0';

            collapseButton.style.margin = 'unset';

            navigationBar.style.width = '200px';

            arrowbutton.style.marginLeft = '330%';
            arrowbutton.style.marginBottom = '10%';
            arrowbutton.style.marginTop = '0';
        }
    });
}
