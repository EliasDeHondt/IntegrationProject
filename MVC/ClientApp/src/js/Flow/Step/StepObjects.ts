export interface Step {
    id:number;
    stepNumber: number;
    informationViewModel: Information;
}

export interface Information {
    id: number;
    information: string;
    informationType: string;
}

export interface Question {
    Question: string;
    Choices: string[];
}
