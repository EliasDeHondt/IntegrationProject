import {Step} from "./Step/StepObjects";

export interface Flow {
    id: number;
    flowType: string;
    steps: Step[];
    participations: Participations[];
    themeId: number;
}

export interface Participations {
    
}