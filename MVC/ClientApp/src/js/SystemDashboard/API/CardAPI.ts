﻿import {SharedPlatform} from "../../Types/PlatformTypes";

export function generatePlatformCards(platforms: SharedPlatform[]): string {

    return platforms.map(generatePlatformCard).join('');
}

function generatePlatformCard(platform: SharedPlatform): string {

    return `<li class="col-6 list-item">
                <a class="card col flex-row d-flex align-items-center me-4 ms-2 mb-3" style="width: 36rem; height: 9rem;" href="/SharedPlatform/Dashboard/${platform.id}">
                    <div class="col-3">
                        <img class="img-fluid" src="data:image/png;base64, ${platform.logo}" alt="Company logo">
                    </div>
                    <div class="card-body col-9">
                        <h5 class="card-title">${platform.organisationName}</h5>
                    </div>
                </a>
            </li>`
}