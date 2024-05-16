import {getIdProject, loadNotesProject} from "./API/ProjectAPI";
import {Note} from "../Flow/Step/StepObjects";
import {Flow} from "../Flow/FlowObjects";

const notesContainer = document.getElementById("notesContainer") as HTMLDivElement;

if (notesContainer) {
    loadNotesProject(getIdProject()).then(flows => {
        showNotes(flows, notesContainer);
    })
}

export function showNotes(flows: Flow[], notesContainer: HTMLDivElement) {
    console.log(flows);
    notesContainer.innerHTML = "";
    if (flows.length > 0) {
        flows.forEach(flow => {
            flow.steps.forEach(step => {
                step.notes.forEach(note => {
                    console.log(note);
                    const noteDiv = document.createElement('div');
                    noteDiv.classList.add("col");

                    const noteCard = document.createElement('div');
                    noteCard.classList.add("card")

                    const noteCardBody = document.createElement('div');

                    const title = document.createElement('h3');
                    title.classList.add("card-title");
                    title.innerText = `Flow ${flow.id} - Step ${step.stepNumber}`;

                    const text = document.createElement('p');
                    text.classList.add("card-text");
                    text.innerHTML = `${note.textfield}`;

                    noteCardBody.appendChild(title);
                    noteCardBody.appendChild(text);
                    noteCard.appendChild(noteCardBody);
                    noteDiv.appendChild(noteCard);

                    notesContainer.appendChild(noteDiv);
                })
            })
        });
    }

    return notesContainer;
}
