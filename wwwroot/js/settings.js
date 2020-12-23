var unitButtons = document.getElementsByName("units");
for(var i = 0; i < unitButtons.length; i++){
    unitButtons[i].addEventListener('click', function(){
        document.getElementById("unit-form").submit();
    })
}