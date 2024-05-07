import {Project} from "./ProjectObjects";

export interface User {
    userName: string,
    email: string
    permissions: UserRoles[]
}

export interface Facilitator extends User {
    projects: Project[]
}

export enum UserRoles {
    SystemAdmin = "SystemAdmin",
    PlatformAdmin = "PlatformAdmin",
    Facilitator = "Facilitator",
    UserPermission = "UserPermission",
    ProjectPermission = "ProjectPermission",
    StatisticPermission = "StatisticPermission",
}

export interface FacilitatorUpdate extends User {
    removedProjects: number[]
    addedProjects: number[]
}