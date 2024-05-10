export interface Step {
    stepNumber: number;
    informationViewModel: Information[];
    questionViewModel: Question;
}

export interface Information {
    information: string;
    informationType: string;
}

export interface Question {
    question: string;
    questionType: string;
    choices: Choice[];
}

export interface Choice {
    text: string;
}
