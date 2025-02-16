document.addEventListener("DOMContentLoaded", () => {
    let quizData = [];
    let selectedAnswers = {}; // Store selected answers per question

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

        // Initialize selected answers
        selectedAnswers[index] = null;
    });

    let currentQuestionIndex = 0;

    function loadQuestion() {
        document.querySelectorAll(".question-block").forEach((block, index) => {
            block.style.display = index === currentQuestionIndex ? "block" : "none";

            // Restore selected answer
            if (index === currentQuestionIndex && selectedAnswers[index] !== null) {
                let selectedOption = block.querySelector(`[data-option="${selectedAnswers[index]}"]`);
                if (selectedOption) {
                    selectedOption.classList.add("selected");
                }
            }
        });

        document.getElementById("prev-btn").disabled = currentQuestionIndex === 0;
        document.getElementById("next-btn").disabled = currentQuestionIndex === quizData.length - 1;
    }

    // Event listener for selecting options
    document.querySelectorAll(".quiz-option").forEach(button => {
        button.addEventListener("click", (event) => {
            let questionBlock = event.target.closest(".question-block");
            let questionIndex = parseInt(questionBlock.getAttribute("data-index"));
            let selectedOptionId = event.target.getAttribute("data-option");

            // Remove existing selection in the question block
            questionBlock.querySelectorAll(".quiz-option").forEach(opt => opt.classList.remove("selected"));

            // Highlight the selected option
            event.target.classList.add("selected");

            // Store selected answer
            selectedAnswers[questionIndex] = selectedOptionId;
        });
    });

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
    let userAnswers = [];

    document.querySelectorAll(".question-block").forEach((block, index) => {
        let selectedOption = selectedAnswers[index];
        userAnswers.push({
            questionIndex: index,
            selectedOption: selectedOption
        });
    });

    // Calculate the correct answer count by comparing selected answers with quizData.answerId
    const answerCount = quizData.reduce((acc, question, index) => {
        return acc + (selectedAnswers[index] === question.answerId ? 1 : 0);
    }, 0);

    window.location.href = `/Home/QuizResult?count=${answerCount}`;
});


    loadQuestion();
});
