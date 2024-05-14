export interface Step {
    id: number;
    stepNumber: number;
    informationViewModel: Information[];
    questionViewModel: Question;
}

export interface Information {
    id: number;
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
    nextStepId?: number;
}
