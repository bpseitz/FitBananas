var now = new Date();
var endOfYear = new Date(now.getFullYear() + 1, 0, 1);
var timeLeft = (endOfYear - now) + ((now.getTimezoneOffset() - endOfYear.getTimezoneOffset()) * 60 * 1000);
var daysLeft = Math.floor(timeLeft / (1000 * 60 * 60 * 24));
var hoursLeft = Math.floor(((timeLeft / (1000 * 60 * 60 * 24)) - daysLeft) * (24));
var string = "" + daysLeft + " days, " + hoursLeft + " hours remaining";
var timeRemaining = document.getElementById('timeRemaining');
timeRemaining.innerHTML = string;