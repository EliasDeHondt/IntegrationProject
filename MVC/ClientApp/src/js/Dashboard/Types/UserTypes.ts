import {Project} from "./ProjectObjects";

export interface User {
    userName: string,
    email: string
}

export interface Facilitator extends User {
    projects: Project[]
}