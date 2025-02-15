document.addEventListener("DOMContentLoaded", () => {
    let quizData = [];

    // Retrieve questions from the page
    document.querySelectorAll(".question-block").forEach((block, index) => {
        let questionText = block.querySelector("p").textContent;
        let options = [];
        block.querySelectorAll(".quiz-option").forEach(button => {
            options.push({
                optionId: button.getAttribute("data-option"),
                text: button.textContent
            });
        });

        let answerId = block.querySelector(".quiz-option").getAttribute("data-answer");

        quizData.push({
            question: questionText,
            options: options,
            answerId: answerId
        });
    });

    let currentQuestionIndex = 0;

    function loadQuestion() {
        document.querySelectorAll(".question-block").forEach((block, index) => {
            block.style.display = index === currentQuestionIndex ? "block" : "none";
        });

        document.getElementById("prev-btn").disabled = currentQuestionIndex === 0;
        document.getElementById("next-btn").disabled = currentQuestionIndex === quizData.length - 1;
    }

    document.getElementById("prev-btn").addEventListener("click", (event) => {
        event.preventDefault();
        if (currentQuestionIndex > 0) {
            currentQuestionIndex--;
            loadQuestion();
        }
    });

    document.getElementById("next-btn").addEventListener("click", (event) => {
        event.preventDefault();
        if (currentQuestionIndex < quizData.length - 1) {
            currentQuestionIndex++;
            loadQuestion();
        }
    });

    document.getElementById("submit-quiz-btn").addEventListener("click", () => {
        alert("Quiz Submitted!");
    });

    loadQuestion();
});
