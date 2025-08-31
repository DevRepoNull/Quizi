$(document).ready(function () {
    $("#answerTypeSelect").change(function () {
        var selected = $(this).val();

        // مخفی کردن همه بخش‌ها ابتدا
        $("#testOptions").hide();
        $("#trueFalseOption").hide();
        $("#textAnswerOption").hide();

        // بر اساس مقدار انتخاب شده نمایش بده
        if (selected == "1") { // فرض بر اینکه EnumTypes تستی = 0 است
            $("#testOptions").show();
        }
        else if (selected == "3") { // فرض بر اینکه EnumTypes درست و غلط = 1 است
            $("#trueFalseOption").show();
        }
        else if (selected == "2") { // فرض بر اینکه EnumTypes تشریحی = 2 است
            $("#textAnswerOption").show();
        }
    });

    // اگر مقدار اولیه از قبل انتخاب شده بود (مثلا در Edit)
    $("#answerTypeSelect").trigger('change');
});