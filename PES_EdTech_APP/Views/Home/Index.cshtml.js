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
    const generateQuizBtn = document.getElementById("generate-quiz-btn");
    const resetQuizBtn = document.getElementById("reset-quiz-btn");
    const quizContent = document.getElementById("quiz-content");
    const quizResult = document.getElementById("quiz-result");

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

    generateQuizBtn.addEventListener("click", () => {
        for (let key in selectedOptions) {
            if (!selectedOptions[key]) {
                alert("Please select an option for all categories.");
                return;
            }
        }

        let quizContentHtml = `
            <ul>
                <li>1. What is the key concept in this text?</li>
                <li>2. Identify the main points discussed.</li>
                <li>3. Summarize the text.</li>
            </ul>`;

        quizContent.innerHTML = quizContentHtml;
        quizResult.classList.remove("hidden");
    });

    //resetQuizBtn.addEventListener("click", () => {
    //    location.reload();
    //});
});
