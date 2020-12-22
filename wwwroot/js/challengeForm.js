var buttons = document.getElementsByClassName('btn-check');
for (var i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener('click', function() {
        allOptions = document.getElementsByTagName("option")
        document.getElementById('elevationGain').disabled = false;
        for(var k = 0; k < allOptions.length; k++){
            allOptions[k].style.display = "none";
        }
        if(document.getElementById('run').checked && document.getElementById('distance').checked) {
            //var runOptions = document.getElementById('Run');
            //runOptions.style.display="none";
            options = document.getElementsByClassName("Run-Distance")
            for(var j = 0; j < options.length; j++){
                options[j].style.display = "block";
            }
        }
        if(document.getElementById('run').checked && document.getElementById('elevationGain').checked) {
            //var runOptions = document.getElementById('Run');
            //runOptions.style.display="none";
            options = document.getElementsByClassName("Run-Elevation")
            for(var j = 0; j < options.length; j++){
                options[j].style.display = "block";
            }
        }
        if(document.getElementById('bike').checked && document.getElementById('distance').checked) {
            //var runOptions = document.getElementById('Run');
            //runOptions.style.display="none";
            options = document.getElementsByClassName("Bike-Distance")
            for(var j = 0; j < options.length; j++){
                options[j].style.display = "block";
            }
        }
        if(document.getElementById('bike').checked && document.getElementById('elevationGain').checked) {
            //var runOptions = document.getElementById('Run');
            //runOptions.style.display="none";
            options = document.getElementsByClassName("Bike-Elevation")
            for(var j = 0; j < options.length; j++){
                options[j].style.display = "block";
            }
        }
        if(document.getElementById('swim').checked) {
            //var runOptions = document.getElementById('Run');
            //runOptions.style.display="none";
            options = document.getElementsByClassName("Swim-Distance")
            for(var j = 0; j < options.length; j++){
                options[j].style.display = "block";
            }
            document.getElementById('elevationGain').disabled = true;
        }
    });
}