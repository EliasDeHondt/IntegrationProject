export interface Project {
    id: number;
    MainTheme: MainTheme;
    SharedPlatform: SharedPlatform;
    Organizers: ProjectOrganizer[];
}

export interface MainTheme {
    id: number;
}

export interface SharedPlatform {
    id: number;
}

export interface ProjectOrganizer {
    id: number;

}