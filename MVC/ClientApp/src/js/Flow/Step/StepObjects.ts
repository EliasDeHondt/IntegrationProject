export interface Step {
    id: number;
    stepNumber: number;
    informationViewModel: Information[];
    questionViewModel: Question;
    notes: Note[];
    visible: boolean;
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
    text: string;
    nextStepId?: number;
}

export interface Note {
    id: number;
    textfield: string;
}
