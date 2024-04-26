export type Project = {
    mainTheme: MainTheme,
    id: number,
    title: string,
    description: string,
    sharedplatformId: number
}

export type MainTheme = {
    subject: string,
    id: number
}