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
