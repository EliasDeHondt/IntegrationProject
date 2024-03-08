export interface Step {

    StepNumber: number;
    Information: Information;
    Question: Question;
    StepType: string;
    
}

export interface Information {
    GetInformation: string;
}

export interface Question {
    Question: string;
    Choices: string[];
}
