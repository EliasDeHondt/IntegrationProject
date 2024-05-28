import {SharedPlatform} from "../../Types/PlatformTypes";

export function generatePlatformCards(platforms: SharedPlatform[]): string {

    return `<li class="col-6 list-item">
                    <button id="btnAddPlatform" class="card col flex-row d-flex align-items-center me-4 ms-2 mb-3 sys-card bgSecondary">
                        <i class="bi bi-building-fill-add sys-icon m-auto"></i>
                    </button>
                </li>` + platforms.map(generatePlatformCard).join('');
}

export function generatePlatformCard(platform: SharedPlatform): string {

    return `<li class="col-6 list-item">
                <a class="card col flex-row d-flex align-items-center me-4 ms-2 mb-3 sys-card bgSecondary" href="/SharedPlatform/Dashboard/${platform.id}">
                    <div class="col-3 h-75">
                        <img class="img-fluid h-100" src="data:image/png;base64, ${platform.logo}" alt="Company logo">
                    </div>
                    <div class="card-body col-9">
                        <h5 class="card-title">${platform.organisationName}</h5>
                    </div>
                </a>
            </li>`
}