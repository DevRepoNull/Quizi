document.addEventListener('DOMContentLoaded', function () {
    const questionList = document.getElementById('questionList');
    const examDesign = document.getElementById('examDesign');

    updateQuestionListNumbers();
    updateExamNumbers();
    updateHiddenInputs();

    // DRAG START
    questionList.addEventListener('dragstart', function (e) {
        if (e.target && e.target.nodeName === "LI") {
            e.dataTransfer.setData('text/plain', e.target.getAttribute('data-question-id'));
        }
    });

    // DRAG OVER
    examDesign.addEventListener('dragover', function (e) {
        e.preventDefault();
    });

    // DROP
    examDesign.addEventListener('drop', function (e) {
        e.preventDefault();

        const questionId = e.dataTransfer.getData('text/plain');
        const draggedLi = questionList.querySelector(`li[data-question-id="${questionId}"]`);

        if (draggedLi) {
            // حذف سوال از questionList
            draggedLi.remove();

            // حذف placeholder اگر وجود دارد
            if (examDesign.children.length === 1 && examDesign.children[0].classList.contains('text-muted')) {
                examDesign.innerHTML = "";
            }

            // کلون تمیز
            const newLi = draggedLi.cloneNode(true);
            newLi.querySelectorAll('input').forEach(input => input.remove()); // حذف hidden input قبلی اگر هست

            examDesign.appendChild(newLi);

            updateExamNumbers();
            updateQuestionListNumbers();
            updateHiddenInputs();
        }
    });

    // DELETE QUESTION
    examDesign.addEventListener('click', function (e) {
        if (e.target && e.target.classList.contains('delete-btn')) {
            const li = e.target.closest('li');
            const questionId = li.getAttribute('data-question-id');

            // حذف از examDesign
            li.remove();

            // بازگردانی سوال به questionList با data-question-id صحیح
            const newLi = li.cloneNode(true);
            newLi.querySelectorAll('input').forEach(input => input.remove()); // حذف hidden input قبلی اگر هست
            questionList.appendChild(newLi);

            updateQuestionListNumbers();
            updateExamNumbers();
            updateHiddenInputs();

            // افزودن placeholder اگر خالی شد
            if (examDesign.children.length === 0) {
                const placeholder = document.createElement('li');
                placeholder.classList.add('list-group-item', 'text-center', 'text-muted');
                placeholder.innerText = 'سوالی اضافه نشده است';
                examDesign.appendChild(placeholder);
            }
        }
    });

    function updateQuestionListNumbers() {
        const items = questionList.querySelectorAll('li[data-question="true"]');
        items.forEach((item, index) => {
            updateBadge(item, index + 1);
        });
    }

    function updateExamNumbers() {
        const items = examDesign.querySelectorAll('li[data-question="true"]');
        items.forEach((item, index) => {
            updateBadge(item, index + 1);
        });
    }

    function updateBadge(item, number) {
        let badge = item.querySelector('.badge.badge-secondary');
        if (!badge) {
            badge = document.createElement('span');
            badge.classList.add('badge', 'badge-secondary', 'mr-2');
            item.prepend(badge);
        }
        badge.innerText = number;
    }

    function updateHiddenInputs() {
        const items = examDesign.querySelectorAll('li[data-question="true"]');

        // حذف hidden inputs قبلی
        items.forEach(item => {
            item.querySelectorAll('input[type="hidden"]').forEach(input => input.remove());
        });

        // افزودن مجدد hidden inputs با اندیس صحیح
        items.forEach((item, index) => {
            const questionId = item.getAttribute('data-question-id');

            const inputQuestionId = document.createElement('input');
            inputQuestionId.type = 'hidden';
            inputQuestionId.name = `EditForm.SelectedQuestions[${index}].QuestionId`;
            inputQuestionId.value = questionId;
            item.appendChild(inputQuestionId);

            const inputOrder = document.createElement('input');
            inputOrder.type = 'hidden';
            inputOrder.name = `EditForm.SelectedQuestions[${index}].Order`;
            inputOrder.value = index + 1;
            item.appendChild(inputOrder);
        });
    }
});
