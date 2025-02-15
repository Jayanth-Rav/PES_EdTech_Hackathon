document.addEventListener("DOMContentLoaded", function () {
    // Retrieve score from session storage
    let totalQuestions = 3;  // Change this based on actual quiz questions
    let correctAnswers = 2 || 0;

    // Calculate percentage
    let scorePercentage = (correctAnswers / totalQuestions) * 100;

    // Update progress bar
    let progressBar = document.getElementById("quiz-progress-bar");
    let scoreText = document.getElementById("quiz-score");

    progressBar.style.width = scorePercentage + "%";
    progressBar.setAttribute("aria-valuenow", scorePercentage);
    progressBar.textContent = Math.round(scorePercentage) + "%";

    // Update score text
    scoreText.textContent = Math.round(scorePercentage);
});