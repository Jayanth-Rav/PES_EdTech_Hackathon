document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("quizForm");
    const quizTopicHidden = document.getElementById("quizTopicHidden");
    const inputTextHidden = document.getElementById("inputTextHidden");

    // Retrieve Quiz Topic from localStorage
    const storedQuizTopic = localStorage.getItem("quizTopic");
    if (storedQuizTopic) {
        quizTopicHidden.value = storedQuizTopic;
    }

    form.addEventListener("submit", (event) => {
        let isValid = true;
        let errorMessage = "";

        // Get selected values for each category
        const quizTypeSelected = getSelectedValue("quiz-type");
        const usageTypeSelected = getSelectedValue("usage-type");
        const numberOfQuestionsSelected = getSelectedValue("num-questions");
        const quizModeSelected = getSelectedValue("quiz-mode");

        // Ensure Quiz Topic is not empty
        if (!inputTextHidden.value.trim()) {
            isValid = false;
            errorMessage += "Quiz Topic cannot be empty.\n";
        }

        // Validation
        if (!quizTypeSelected) {
            isValid = false;
            errorMessage += "Please select a Quiz Type.\n";
        }
        if (!usageTypeSelected) {
            isValid = false;
            errorMessage += "Please select a Usage Type.\n";
        }
        if (!numberOfQuestionsSelected || isNaN(parseInt(numberOfQuestionsSelected))) {
            isValid = false;
            errorMessage += "Please select a valid number of questions.\n";
        }
        if (!quizModeSelected) {
            isValid = false;
            errorMessage += "Please select a Quiz Mode.\n";
        }

        if (!isValid) {
            event.preventDefault();
            alert(errorMessage);
            return;
        }

        // Remove existing hidden inputs if they exist
        removeHiddenInputs();

        // Append selections as hidden input fields
        appendHiddenInput(form, "QuizTopic", inputTextHidden);
        appendHiddenInput(form, "QuizType", quizTypeSelected);
        appendHiddenInput(form, "UsageType", usageTypeSelected);
        appendHiddenInput(form, "NumberOfQuestions", numberOfQuestionsSelected);
        appendHiddenInput(form, "QuizMode", quizModeSelected);
    });

    // Function to get selected button value based on data-group
    function getSelectedValue(groupName) {
        const selectedButton = document.querySelector(`button[data-group="${groupName}"].active`);
        return selectedButton ? selectedButton.getAttribute("data-value") : null;
    }

    // Function to append hidden input fields
    function appendHiddenInput(form, name, value) {
        let input = document.createElement("input");
        input.type = "hidden";
        input.name = name;
        input.value = value;
        form.appendChild(input);
    }

    // Function to remove existing hidden inputs before re-adding them
    function removeHiddenInputs() {
        document.querySelectorAll("#quizForm input[type='hidden']").forEach(input => {
            if (input.id !== "quizTopicHidden") { // Preserve QuizTopic
                input.remove();
            }
        });
    }

    // Function to handle selection for each button group
    function setupButtonGroup(groupName) {
        const buttons = document.querySelectorAll(`button[data-group="${groupName}"]`);
        buttons.forEach(btn => btn.addEventListener("click", function (event) {
            event.preventDefault();
            buttons.forEach(b => b.classList.remove("active"));
            this.classList.add("active");
        }));
    }

    // Set up button groups
    setupButtonGroup("quiz-type");
    setupButtonGroup("usage-type");
    setupButtonGroup("num-questions");
    setupButtonGroup("quiz-mode");
});
