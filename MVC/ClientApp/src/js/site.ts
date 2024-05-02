import '@popperjs/core';
import 'jquery';
import 'bootstrap';
import 'bootstrap-icons/font/bootstrap-icons.css';
import 'bootstrap/dist/css/bootstrap.css';

// Custom CSS imports
import '../css/site.scss';

fetch("/api/Gmail/SendEmail", {
    method: 'POST',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    },
    body: JSON.stringify({
        To: "codeforge.noreply@gmail.com",
        Subject: "Test",
        Body: "Hello World!"
    })
}).then(response => console.log(response));

console.log('The \'site\' bundle has been loaded!');