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
            const flowDiv = document.createElement('div');
            flowDiv.classList.add("flow-note-container")
            const flowTitle = document.createElement('h2');
            flowTitle.classList.add("flow-note-title")
            flowTitle.innerText = `Flow ${flow.id}`

            flowDiv.appendChild(flowTitle)

            const hasNotes = flow.steps.some(s => s.notes.length > 0);

            if (!hasNotes) {
                const noteCard = document.createElement('div');
                noteCard.classList.add("card", "note-card")

                const noteCardBody = document.createElement('div');
                noteCardBody.classList.add("note-card-body")

                const text = document.createElement('p');
                text.classList.add("card-text", "note-card-text");
                text.innerHTML = `There are no notes for this flow.`;

                noteCardBody.appendChild(text);
                noteCard.appendChild(noteCardBody);
                flowDiv.appendChild(noteCard);
            } else {
                flow.steps.forEach(step => {
                        step.notes.forEach(note => {
                            const noteCard = document.createElement('div');
                            noteCard.classList.add("card", "note-card")

                            const noteCardBody = document.createElement('div');
                            noteCardBody.classList.add("note-card-body")

                            const title = document.createElement('h3');
                            title.classList.add("card-title", "note-card-title");
                            title.innerText = `Step ${step.stepNumber}`;

                            const text = document.createElement('p');
                            text.classList.add("card-text", "note-card-text");
                            text.innerHTML = `${note.textfield}`;

                            noteCardBody.appendChild(title);
                            noteCardBody.appendChild(text);
                            noteCard.appendChild(noteCardBody);
                            flowDiv.appendChild(noteCard);
                        })
                    }
                )
            }

            notesContainer.appendChild(flowDiv);
        });
    }

    return notesContainer;
}
