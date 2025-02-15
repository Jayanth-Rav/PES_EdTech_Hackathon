function resetQuiz() {
    location.reload(); // Quick way to reset everything
}
document.addEventListener("DOMContentLoaded", () => {
    let selectedOptions = {
        "quiz-type": null,
        "usage-type": null,
        "num-questions": null,
        "quiz-mode": null
    };

    const fileInput = document.getElementById("fileInput");
    const fileNameDisplay = document.getElementById("file-name");
    const goNextBtn = document.getElementById("go-next-btn");
    const inputText = document.getElementById("input-text");
    const quizForm = document.getElementById("quiz-form");
    const inputSection = document.getElementById("input-section");


    fileInput.addEventListener("change", () => {
        if (fileInput.files.length > 0) {
            fileNameDisplay.textContent = `Selected File: ${fileInput.files[0].name}`;
            fileNameDisplay.classList.remove("hidden");
            goNextBtn.classList.remove("hidden");
        } else {
            fileNameDisplay.textContent = "";
            fileNameDisplay.classList.add("hidden");
            goNextBtn.classList.add("hidden");
        }
    });

    inputText.addEventListener("input", () => {
        goNextBtn.classList.toggle("hidden", inputText.value.trim().length === 0);
    });

    goNextBtn.addEventListener("click", () => {
        quizForm.classList.remove("hidden");
        inputSection.classList.add("hidden");
    });

    document.querySelectorAll(".btn-group button").forEach(button => {
        button.addEventListener("click", (event) => {
            let category = event.target.getAttribute("data-category");
            let value = event.target.getAttribute("data-value");

            selectedOptions[category] = value;

            let buttons = event.target.parentElement.children;
            Array.from(buttons).forEach(btn => {
                btn.classList.remove("btn-primary");
                btn.classList.add("btn-outline-primary");
            });

            event.target.classList.remove("btn-outline-primary");
            event.target.classList.add("btn-primary");
        });
    });


    //const quizForm = document.getElementById("quiz-form");
   // const inputSection = document.getElementById("input-section");
    const generateQuizBtn = document.getElementById("generate-quiz-btn");
    const quizSection = document.getElementById("quiz-section");
    const quizQuestion = document.getElementById("quiz-question");
    const quizOptions = document.getElementById("quiz-options");
    const prevBtn = document.getElementById("prev-btn");
    const nextBtn = document.getElementById("next-btn");

    let quizData = [
        {
            question: "What is the key concept in this text?",
            options: ["Option A", "Option B", "Option C", "Option D"],
            correctAnswers: ["Option A", "Option B"]
        },
        {
            question: "Identify the main points discussed.",
            options: ["Option 1", "Option 2", "Option 3", "Option 4"],
            correctAnswers: ["Option 1", "Option 2"]
        },
        {
            question: "Summarize the text.",
            options: ["Summary A", "Summary B", "Summary C", "Summary D"],
            correctAnswers: ["Summary A", "Summary B"]
        }
    ];

    let currentQuestionIndex = 0;

    function loadQuestion() {
        let questionObj = quizData[currentQuestionIndex];
        quizQuestion.textContent = questionObj.question;

        quizOptions.innerHTML = "";
        questionObj.options.forEach((option) => {
            let button = document.createElement("button");
            button.classList.add("list-group-item", "list-group-item-action");
            button.textContent = option;
            button.onclick = () => selectAnswer(button, option, questionObj.correctAnswers);
            quizOptions.appendChild(button);
        });

        prevBtn.disabled = currentQuestionIndex === 0;
        nextBtn.disabled = currentQuestionIndex === quizData.length - 1;
    }

    function selectAnswer(button, selected, correctAnswers) {
        let isCorrect = correctAnswers.includes(selected);

        // Reset all button borders before setting new selection
        document.querySelectorAll("#quiz-options button").forEach(btn => {
            btn.style.border = ""; // Reset to default
        });

        if (isCorrect) {
            button.style.border = "2px solid green"; // Green border for correct
        } else {
            button.style.border = "2px solid red";   // Red border for incorrect
        }

        sessionStorage.setItem(`answer-${currentQuestionIndex}`, selected);
    }



    window.nextQuestion = function () {
        if (currentQuestionIndex < quizData.length - 1) {
            currentQuestionIndex++;
            loadQuestion();
        }
    };

    window.prevQuestion = function () {
        if (currentQuestionIndex > 0) {
            currentQuestionIndex--;
            loadQuestion();
        }
    };

    window.resetQuiz = function () {
        location.reload();
    };

    generateQuizBtn.addEventListener("click", () => {
        quizForm.classList.add("hidden");
        inputSection.classList.add("hidden");
        // quizSection.classList.remove("hidden");
        $("#quiz-section").show();
        loadQuestion();
    });
    //resetQuizBtn.addEventListener("click", () => {
    //    location.reload();
    //});
});
