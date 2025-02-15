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
    const goNextBtn = document.querySelector("button[type='submit']");
    const inputText = document.getElementById("input-text");
    const quizForm = document.getElementById("quiz-form");
    const inputSection = document.getElementById("input-section");


    function validateInput() {
        // Enable button only if there's text input or a selected file
        if (inputText.value.trim().length > 0 || fileInput.files.length > 0) {
            goNextBtn.removeAttribute("disabled");
            fileNameDisplay.textContent = `Selected File: ${fileInput.files[0].name}`;
            fileNameDisplay.classList.remove("hidden");
        } else {
            fileNameDisplay.textContent = "";
            fileNameDisplay.classList.add("hidden");
            goNextBtn.setAttribute("disabled", "true");
        }
    }

    // Listen for changes in text input and file selection
    inputText.addEventListener("input", validateInput);
    fileInput.addEventListener("change", validateInput);

    goNextBtn.addEventListener("click", (event) => {
        if (inputText.value.trim().length === 0 && fileInput.files.length === 0) {
            event.preventDefault(); // Stop form submission if both are empty
            alert("Please enter text or attach a file before proceeding.");
        }
    });

    // Initially disable button until input is provided
    goNextBtn.setAttribute("disabled", "true");

    //goNextBtn.addEventListener("click", () => {
    //    quizForm.classList.remove("hidden");
    //    inputSection.classList.add("hidden");
    //});

    


    //const quizForm = document.getElementById("quiz-form");
   // const inputSection = document.getElementById("input-section");
    
    //resetQuizBtn.addEventListener("click", () => {
    //    location.reload();
    //});
});
