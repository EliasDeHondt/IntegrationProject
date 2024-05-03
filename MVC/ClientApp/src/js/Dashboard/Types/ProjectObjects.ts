export type Project = {
    mainTheme: MainTheme,
    id: number,
    name: string,
    description: string,
    sharedplatformId: number
}

export type MainTheme = {
    subject: string,
    id: number
}