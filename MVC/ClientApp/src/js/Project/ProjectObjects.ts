export type Project = {
    mainTheme: MainTheme,
    id: number,
    title: string,
    description: string,
    sharedplatformId: number
}

export interface MainTheme {
    id: number;
}

export interface SharedPlatform {
    id: number;
    OrganisationName: string
    Projects: Project[]
}

export interface ProjectOrganizer {
    id: number;

}