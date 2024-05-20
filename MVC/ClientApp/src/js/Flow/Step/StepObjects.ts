export interface Step {
    id: number;
    stepNumber: number;
    notes: Note[];
    visible: boolean;
}

export interface InformationStep extends Step {
    informationViewModel: Information[];
}

export interface QuestionStep extends Step {
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

export interface Note {
    id: number;
    textfield: string;
}

export function isInformationStep(step: Step): step is InformationStep {
    return step && 'informationViewModel' in step;
}

export function isQuestionStep(step: Step): step is QuestionStep {
    return step && 'questionViewModel' in step;
}