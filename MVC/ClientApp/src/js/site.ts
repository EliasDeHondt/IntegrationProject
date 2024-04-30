import '@popperjs/core';
import 'jquery';
import 'bootstrap';
import 'bootstrap-icons/font/bootstrap-icons.css';
import 'bootstrap/dist/css/bootstrap.css';

// Custom CSS imports
import '../css/site.scss';

fetch("/api/Gmail/test").then(r => console.log(r))

console.log('The \'site\' bundle has been loaded!');