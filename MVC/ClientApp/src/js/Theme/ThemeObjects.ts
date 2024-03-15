/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

export interface Theme {
    id: number;
    subject: string;
    flows: number[];
}

export interface MainTheme {
    theme: Theme;
    subthemes: number[];
}

export interface SubTheme {
    theme: Theme;
    maintheme: number;
}
