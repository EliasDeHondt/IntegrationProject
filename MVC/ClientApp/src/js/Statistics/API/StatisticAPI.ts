import {
    drawBarChart, drawDoughnutChart,
    drawLineChart,
    drawPieChart,
    drawRadarChart,
    initChoicesNames, initDataStatistics, initQuestionNames,
    generateAnswerSummary
} from "../Statistics";
import {Question, Answer} from "../../Types/ProjectObjects";

//get data - flows
export async function GetCountStepsPerFlow(labels: string[]){
    await fetch("/api/Statistics/GetCountStepsPerFlow", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => drawBarChart("Aantal Steps per Flow",labels,'Aantal Steps', data))
        .catch(error => console.error("Error:", error))
}

export async function GetCountParticipationsPerFlow(labels: string[]){
    await fetch("/api/Statistics/GetCountParticipationsPerFlow", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => drawLineChart("Aantal Participations per Flow",labels,'Aantal Participations', data))
        .catch(error => console.error("Error:", error))
}

export async function GetNamesPerFlow(){
    await fetch("/api/Statistics/GetNamesPerFlow", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(labels => {
            initDataStatistics(labels)
        } )
        .catch(error => console.error("Error:", error))
}
// export async function GetQuestionNames(flowname: string){
//     await fetch("/api/Statistics/GetQuestionNames/" + flowname, {
//         method: "GET",
//         headers: {
//             "Accept": "application/json",
//             "Content-Type": "application/json"
//         }
//     })
//         .then(response => response.json())
//         .then(labels => {
//             initQuestionNames(labels)
//         } )
//         .catch(error => console.error("Error:", error))
// }
export async function GetQuestionNames(flowname: string): Promise<void> {
    try {
        const response = await fetch("/api/Statistics/GetQuestionNames/" + flowname, {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            const labels: Question[] = [{ id: 0, question: "No questions available." }];
            initQuestionNames(labels);
        }
        
        const labels: Question[] = await response.json();
        console.log("labels Question",labels)
        initQuestionNames(labels);
    } catch (error) {
        console.error("Error:", error);
    }
}
export async function GetChoicesNames(question: string){
    //question = "If you were to prepare the budget for your city or municipality, where would you mainly focus on in the coming years? Choose one.";
    console.log("question: GetChoicesNames",question)
    await fetch("/api/Statistics/GetChoicesNames/" + question, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(labels => {
            initChoicesNames(labels)
        } )
        .catch(error => console.error("Error:", error))
}
export async function GetParticipatoinNames(flowname: string){
    await fetch("/api/Statistics/GetParticipatoinNames/" + flowname, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(labels => {
            GetRespondentsFromFlow(labels,flowname);
        } )
        .catch(error => console.error("Error:", error))
}

//get data - flow
export async function GetQuestionsFromFlow(flowname: string){
    const labels: string[] = ["MultipleChoiceQuestion", "SingleChoiceQuestion","OpenQuestion","RangeQuestion"];
    await fetch("/api/Statistics/GetQuestionsFromFlow/" + flowname, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => drawRadarChart("Aantal Questions voor de Flow: "+flowname,labels,'Aantal Questions', data))
        .catch(error => console.error("Error:", error))
}
export async function GetRespondentsFromFlow(labels: string[],flowname: string){
    await fetch("/api/Statistics/GetRespondentsFromFlow/" + flowname, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => drawPieChart("Aantal Respondents voor de Flow \""+flowname+"\" verdeeld over de participations",labels,'Aantal Respondents', data))
        .catch(error => console.error("Error:", error))
}
export async function GetAnswerCountsForQuestions(labels: string[],question: string,questionText: string, questionNumber: string){
    console.log("question: GetAnswerCountsForQuestions",question)
    //voor elk answer te tellen
    await fetch("/api/Statistics/GetAnswerCountsForQuestions/" + question, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            console.log("data QA",data)
            drawDoughnutChart("Aantal antwoorden voor de vraag: "+questionText, labels,'Aantal Answers', data)
            generateAnswerSummary(parseInt(questionNumber));
        })
        .catch(error => console.error("Error:", error))
}

// Summaries

export async function GetAnswersFromQuestion(questionId: number): Promise<string[]> {
    return await fetch(`/api/Statistics/GetAnswersFromQuestion/${questionId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
}

export async function GetQuestionText(questionId: number): Promise<string> {
    return await fetch(`/api/Statistics/GetQuestionText/${questionId}`, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
}

export async function GenerateSummary(question: string, answers: string[]): Promise<string> {
    return await fetch('/api/Ai/GenerateSummary', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            question: question,
            answers: answers
        })
    })
        .then(response => response.json())
        .then(data => {
            return data
        })
}