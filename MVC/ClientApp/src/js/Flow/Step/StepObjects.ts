export interface Step {
    id:number;
    stepName: string;
    stepNumber: number;
    informationViewModel: Information;
    questionViewModel: Question;
}

export interface Information {
    id: number;
    stepName: string;
    information: string;
    informationType: string;
}

export interface Question {
    id: number;
    question: string;
    questionType: string;
    choices: Choice[];
}

export interface Choice {
    id: number;
    text: string;
}
