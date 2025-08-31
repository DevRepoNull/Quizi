document.addEventListener('DOMContentLoaded', function () {
    const questionList = document.getElementById('questionList');
    const examDesign = document.getElementById('examDesign');

    addNumbersToQuestionList(); // ✅ شماره گذاری اولیه

    // DRAG START
    questionList.addEventListener('dragstart', function (e) {
        if (e.target && e.target.nodeName === "LI") {
            e.dataTransfer.setData('text/html', e.target.outerHTML);
        }
    });

    // DRAG OVER
    examDesign.addEventListener('dragover', function (e) {
        e.preventDefault();
    });

    // DROP
    examDesign.addEventListener('drop', function (e) {
        e.preventDefault();

        const data = e.dataTransfer.getData('text/html');
        const tempContainer = document.createElement('div');
        tempContainer.innerHTML = data;
        const draggedLi = tempContainer.firstElementChild;

        // حذف سوال از questionList
        const questionItems = questionList.querySelectorAll('li[data-question="true"]');
        questionItems.forEach(item => {
            if (item.getAttribute('data-question-id') === draggedLi.getAttribute('data-question-id')) {
                item.remove();
            }
        });

        // اضافه کردن input hidden برای فرم
        if (!draggedLi.querySelector('input[name="CreateForm.SelectedQuestions"]')) {
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = 'CreateForm.SelectedQuestions';
            input.value = draggedLi.getAttribute('data-question-id');
            draggedLi.appendChild(input);
        }

        // حذف placeholder اگر وجود دارد
        if (examDesign.children.length === 1 && examDesign.children[0].classList.contains('text-muted')) {
            examDesign.innerHTML = "";
        }

        // اضافه به examDesign (بجای insertAdjacentHTML از append استفاده کن)
        examDesign.appendChild(draggedLi);

        updateExamNumbers(); // ✅ شماره گذاری برگه آزمون
        updateQuestionListNumbers(); // ✅ آپدیت شماره سوالات باقی مانده
    });

    // DELETE QUESTION
    examDesign.addEventListener('click', function (e) {
        if (e.target && e.target.classList.contains('delete-btn')) {
            const li = e.target.closest('li');

            // حذف input hidden قبل از بازگردانی به questionList
            const hiddenInput = li.querySelector('input[name="CreateForm.SelectedQuestions"]');
            if (hiddenInput) {
                hiddenInput.remove();
            }

            // بازگردانی سوال به questionList
            questionList.appendChild(li.cloneNode(true));

            // مرتب سازی questionList بر اساس data-order
            const items = Array.from(questionList.querySelectorAll('li[data-question="true"]'));
            items.sort((a, b) => {
                return parseInt(a.getAttribute('data-order')) - parseInt(b.getAttribute('data-order'));
            });

            // بازسازی DOM با ترتیب صحیح
            questionList.innerHTML = "";
            items.forEach(item => {
                questionList.appendChild(item);
            });

            // حذف از examDesign
            li.remove();

            // افزودن placeholder اگر خالی شد
            if (examDesign.children.length === 0) {
                const placeholder = document.createElement('li');
                placeholder.classList.add('list-group-item', 'text-center', 'text-muted');
                placeholder.innerText = 'سوالی اضافه نشده است';
                examDesign.appendChild(placeholder);
            }

            updateExamNumbers(); // ✅ شماره بندی مجدد برگه آزمون
            updateQuestionListNumbers(); // ✅ شماره بندی مجدد لیست سوالات
        }
    });

    // شماره گذاری سوالات در لیست سوالات
    function addNumbersToQuestionList() {
        const items = questionList.querySelectorAll('li[data-question="true"]');
        let counter = 1;
        items.forEach(item => {
            let badge = item.querySelector('.badge.badge-secondary');
            if (!badge) {
                badge = document.createElement('span');
                badge.classList.add('badge', 'badge-secondary', 'mr-2');
                item.prepend(badge);
            }
            badge.innerText = counter++;
        });
    }

    // UPDATE examDesign NUMBERS
    function updateExamNumbers() {
        const items = examDesign.querySelectorAll('li[data-question="true"]');
        let counter = 1;
        items.forEach(item => {
            let badge = item.querySelector('.badge.badge-secondary');
            if (!badge) {
                badge = document.createElement('span');
                badge.classList.add('badge', 'badge-secondary', 'mr-2');
                item.prepend(badge);
            }
            badge.innerText = counter++;
        });
    }

    // UPDATE questionList NUMBERS
    function updateQuestionListNumbers() {
        const items = questionList.querySelectorAll('li[data-question="true"]');
        let counter = 1;
        items.forEach(item => {
            let badge = item.querySelector('.badge.badge-secondary');
            if (!badge) {
                badge = document.createElement('span');
                badge.classList.add('badge', 'badge-secondary', 'mr-2');
                item.prepend(badge);
            }
            badge.innerText = counter++;
        });
    }
});
