using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.StaticValue;

namespace Questioning_Data_Repositories.Repositories.Services
{
    public class FormServices(WebContext context) : IFormInterfaces
    {
        #region Form Primary Dependency

        private readonly WebContext _context = context;

        #endregion

        #region Form Methods

        public async Task<List<FormExamListViewModel>> GetAllFormExamAsync()
        {
            return await _context.Forms
                .Include(f => f.Categories)
                .Where(f => f.Categories.CategoryId == f.FKCategoryId)
                .Select(f => new FormExamListViewModel()
                {
                    FormId = f.FormId,
                    FormTitle = f.FormTitle,
                    IsActive = f.IsActive,
                    IsPublic = f.IsPublic,
                    StartExamTime = PersianDateConverter.ConvertTimeOnlyToTime(f.StartExamTime),
                    EndExamTime = PersianDateConverter.ConvertTimeOnlyToTime(f.EndExamTime),
                    StartExamDate = PersianDateConverter.ConvertDateOnlyToPersianDate(f.StartExamDate),
                    EndExamDate = PersianDateConverter.ConvertDateOnlyToPersianDate(f.EndExamDate),
                    CategoryTitle = f.Categories.CategoryName
                }).ToListAsync();
        }

        public async Task<Form> GetFormExamById(string formId) =>
            await _context.Forms.Where(f => f.FormId == formId).FirstOrDefaultAsync();

        public async Task<string> CreateFormAsync(CreateFormExamViewModel model)
        {
            var form = new Form()
            {
                FormTitle = model.FormTitle,
                Description = model.FormDescription,
                IsActive = model.IsActive,
                IsPublic = model.IsPublic,
                AccessCode = model.AccessCode,
                FkUserId = model.FkUserId,
                FKCategoryId = model.FkCategoryId,
                CreateDate = DateTime.UtcNow,
                UpdateDate = null
            };

            //For Start Date And Time
            if (!string.IsNullOrWhiteSpace(model.StartExamTime))
                form.StartExamTime = PersianDateConverter.ConvertTimeToTimeOnly(model.StartExamTime);

            if (!string.IsNullOrWhiteSpace(model.StartExamDate))
                form.StartExamDate = PersianDateConverter.ConvertPersianDateToDateOnly(model.StartExamDate);

            //For End Date And Time
            if (!string.IsNullOrWhiteSpace(model.EndExamTime))
                form.EndExamTime = PersianDateConverter.ConvertTimeToTimeOnly(model.EndExamTime);

            if (!string.IsNullOrWhiteSpace(model.EndExamDate))
                form.EndExamDate = PersianDateConverter.ConvertPersianDateToDateOnly(model.EndExamDate);

            await _context.AddAsync(form);
            await _context.SaveChangesAsync();

            if (model.SelectedQuestions != null && model.SelectedQuestions.Any())
            {
                //foreach (var questionId in model.SelectedQuestions)
                //{
                //    bool exists = await _context.RFormQuestions
                //        .AnyAsync(f => f.FkFormId == form.FormId && f.FkQuestionId == questionId);

                //    if (!exists)
                //    {
                //        var formQuestion = new RFormQuestion
                //        {
                //            FkFormId = form.FormId,
                //            FkQuestionId = questionId,
                //            Order = order++
                //        };
                //        await _context.AddAsync(formQuestion);
                //    }
                //}
                //await _context.SaveChangesAsync();

                int order = 1;
                foreach (var questionId in model.SelectedQuestions)
                {
                    await AddQuestionToFormAsync(form.FormId, questionId, order++);
                }

            }

            return form.FormId;
        }

        public async Task<EditFormExamViewModel> GetFormBeforeEditAsync(string formId)
        {
            //Get Form Data Before Edit Check If A Null
            var form = await _context.Forms
                .Include(f => f.RFormQuestions)
                .ThenInclude(rf => rf.Question)
                .FirstOrDefaultAsync(f => f.FormId == formId);

            if (form == null) return null;

            //Get list question
            var allQuestions = await GetAllQuestionAsync();

            //Question Selected By FormId
            var selectedQuestionIds = form.RFormQuestions
                .OrderBy(q => q.Order)
                .Select(q => new SelectedQuestionViewModel()
                {
                    QuestionId = q.FkQuestionId,
                    QuestionTitle = q.Question.QuestionText,
                    Order = q.Order
                })
                .ToList();

            //Ready Form Data For View Model
            var model = new EditFormExamViewModel()
            {
                FormId = form.FormId,
                FormTitle = form.FormTitle,
                FormDescription = form.Description,
                IsActive = form.IsActive,
                IsPublic = form.IsPublic,
                AccessCode = form.AccessCode,

                //Convert Georgian Date On Solar Date
                StartExamDate = PersianDateConverter.ConvertDateOnlyToPersianDate(form.StartExamDate),
                StartExamTime = PersianDateConverter.ConvertTimeOnlyToTime(form.StartExamTime),
                EndExamDate = PersianDateConverter.ConvertDateOnlyToPersianDate(form.EndExamDate),
                EndExamTime = PersianDateConverter.ConvertTimeOnlyToTime(form.EndExamTime),

                FkCategoryId = form.FKCategoryId,

                Questions = allQuestions,
                SelectedQuestions = selectedQuestionIds
            };
            return model;
        }

        public async Task<bool> EditFormAsync(string formId, EditFormExamViewModel model)
        {
            var form = await GetFormExamById(formId);
            if (form == null) return false;

            form.FormTitle = model.FormTitle;
            form.Description = model.FormDescription;
            form.IsActive = model.IsActive;
            form.IsPublic = model.IsPublic;
            form.AccessCode = model.AccessCode;
            form.UpdateDate = DateTime.UtcNow;

            // تبدیل رشته تاریخ و زمان به DateOnly و TimeOnly با همان مبدل قبلی
            if (!string.IsNullOrWhiteSpace(model.StartExamTime))
                form.StartExamTime = PersianDateConverter.ConvertTimeToTimeOnly(model.StartExamTime);

            if (!string.IsNullOrWhiteSpace(model.StartExamDate))
                form.StartExamDate = PersianDateConverter.ConvertPersianDateToDateOnly(model.StartExamDate);

            if (!string.IsNullOrWhiteSpace(model.EndExamTime))
                form.EndExamTime = PersianDateConverter.ConvertTimeToTimeOnly(model.EndExamTime);

            if (!string.IsNullOrWhiteSpace(model.EndExamDate))
                form.EndExamDate = PersianDateConverter.ConvertPersianDateToDateOnly(model.EndExamDate);

            form.FKCategoryId = model.FkCategoryId;

            //Delete Old Question On Update Form
            //var existingQuestion = _context.RFormQuestions.Where(q => q.FkFormId == formId);
            //_context.RemoveRange(existingQuestion);

            // 🔥 مدیریت سوالات فرم
            var existingQuestions = _context.RFormQuestions.Where(q => q.FkFormId == formId).ToList();

            // حذف سوالاتی که دیگر انتخاب نشده اند
            var selectedIds = model.SelectedQuestions.Select(s => s.QuestionId).ToList();
            var toRemove = existingQuestions.Where(q => !selectedIds.Contains(q.FkQuestionId)).ToList();
            _context.RFormQuestions.RemoveRange(toRemove);

            // اضافه کردن سوالات جدید یا به روزرسانی Order سوالات موجود
            int orderCounter = 1;
            foreach (var selected in model.SelectedQuestions)
            {
                var existing = existingQuestions.FirstOrDefault(q => q.FkQuestionId == selected.QuestionId);
                if (existing != null)
                {
                    // سوال قبلاً وجود داشته ➔ فقط ترتیب را آپدیت کن
                    existing.Order = orderCounter++;
                    _context.RFormQuestions.Update(existing);
                }
                else
                {
                    // سوال جدید است ➔ اضافه کن
                    await AddQuestionToFormAsync(form.FormId, selected.QuestionId, orderCounter++);
                }
            }

            _context.Update(form);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteFormAsync(string formId)
        {
            var form = await GetFormExamById(formId);
            if (form == null) return false;

            // Cascade Delete Form If RForm Have Any Data
            var relatedQuestions = _context.RFormQuestions.Where(r => r.FkFormId == formId);
            _context.RFormQuestions.RemoveRange(relatedQuestions);

            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Question Form Methods

        public async Task<bool> AddQuestionToFormAsync(string formId, string questionId, int order)
        {
            if (string.IsNullOrWhiteSpace(formId) || string.IsNullOrWhiteSpace(questionId))
                return false;

            bool exists = await _context.RFormQuestions
                .AnyAsync(f => f.FkFormId == formId && f.FkQuestionId == questionId);

            if (exists)
                return false;

            var entity = new RFormQuestion()
            {
                FkFormId = formId,
                FkQuestionId = questionId,
                Order = order
            };

            await _context.AddAsync(entity);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> RemoveQuestionFromFormAsync(string formId, string questionId)
        {
            var entities = await _context.RFormQuestions
                .Where(rf => rf.FkFormId == formId && questionId.Contains(rf.FkQuestionId))
                .ToListAsync();

            if (entities.Any())
            {
                _context.RemoveRange(entities);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<Question>> GetQuestionOfFormAsync(string formId)
        {
            return await _context.RFormQuestions
                .Where(x => x.FkFormId.Equals(formId))
                .OrderBy(x => x.Order)
                .Select(x => x.Question)
                .ToListAsync();
        }

        #endregion

        #region Question And Answers Data

        public async Task<List<Question>> GetAllQuestionAsync()
        {
            return await _context.Questions
                .Include(f => f.Answers)
                .ToListAsync();
        }

        public async Task<List<RFormQuestion>> GetQuestionOfRFormAsync(string formId)
        {
            return await _context.RFormQuestions
                .Where(x => x.FkFormId.Equals(formId))
                .Include(x => x.Question)
                .ToListAsync();
        }

        #endregion

        #region Exam Form Pages

        public async Task<ShowExamFormViewModel> GetExamFormByIdAsync(string formId)
        {
            var form = await _context.Forms
                .Include(f => f.RFormQuestions)
                .ThenInclude(rf => rf.Question)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(f => f.FormId == formId);

            if (form == null) return null;

            var model = new ShowExamFormViewModel
            {
                FormTitle = form.FormTitle,
                Questions = form.RFormQuestions
                    .OrderBy(q => q.Order)
                    .Select(rf => new QuestionWithAnswersViewModel
                    {
                        QuestionId = rf.Question.QuestionId,
                        QuestionText = rf.Question.QuestionText,
                        QuestionType = rf.Question.Answers.FirstOrDefault()?.AnswerTypes ?? AnswerTypeEnumTypes.None,
                        Answers = rf.Question.Answers.Select(a => new AnswerViewModel
                        {
                            AnswerId = a.AnswerId,
                            AnswerText = a.AnswerText,
                            OptionOne = a.TestOptionOne,
                            OptionTwo = a.TestOptionTwo,
                            OptionThree = a.TestOptionThree,
                            OptionFour = a.TestOptionFour,
                            CorrectOption = a.TestCorrectOption,
                            TrueOrFalseAnswer = a.TruOrFalseAnswer
                        }).ToList()

                    }).ToList()
            };

            return model;
        }

        //public async Task<ShowExamFormViewModel> GetExamFormByIdAsync(string formId)
        //{
        //    var form = await _context.Forms
        //        .Where(f => f.FormId == formId)
        //        .Select(f => new ShowExamFormViewModel
        //        {
        //            FormTitle = f.FormTitle,
        //            Questions = f.RFormQuestions.Select(q => new QuestionWithAnswersViewModel
        //            {
        //                QuestionId = q.Question.QuestionId.ToString(),
        //                QuestionText = q.Question.QuestionText,

        //                // ✅ گرفتن QuestionType از اولین Answer
        //                QuestionType = q.Question.Answers.FirstOrDefault() != null
        //                    ? q.Question.Answers.FirstOrDefault().AnswerTypes
        //                    : AnswerTypeEnumTypes.Anatomical, // مقدار پیشفرض اگر جواب وجود نداشت

        //                Answers = q.Question.Answers.Select(a => new AnswerViewModel
        //                {
        //                    AnswerId = a.AnswerId.ToString(),
        //                    AnswerText = a.AnswerText
        //                }).ToList()
        //            }).ToList()
        //        }).FirstOrDefaultAsync();

        //    return form;
        //}

        #endregion

        #region Enter User On Exam Pages

        public async Task<List<ExamDataViewModel>> GetExamListAsync()
        {
            return await _context.Forms.AsNoTracking()
                .Where(f => f.IsActive)
                .Select(f => new ExamDataViewModel()
                {
                    FormId = f.FormId,
                    FormTitle = f.FormTitle,
                    StartExamDate = PersianDateConverter.ConvertDateOnlyToPersianDate(f.StartExamDate),
                    EndExamDate = PersianDateConverter.ConvertDateOnlyToPersianDate(f.EndExamDate),
                    StartExamTime = PersianDateConverter.ConvertTimeOnlyToTime(f.StartExamTime),
                    EndExamTime = PersianDateConverter.ConvertTimeOnlyToTime(f.EndExamTime)
                }).ToListAsync();
        }

        public async Task<ExamUserViewModel> GetExamUserAsync(string examId)
        {
            var exam = await _context.Forms
                .Include(ex => ex.RFormQuestions)
                .ThenInclude(ex => ex.Question)
                .ThenInclude(ex => ex.Answers)
                .FirstOrDefaultAsync(ex => ex.FormId == examId);

            if (exam == null) return null;

            var model = new ExamUserViewModel()
            {
                FormId = examId,
                FormTitle = exam.FormTitle,
                Questions = exam.RFormQuestions
                    .OrderBy(q => q.Order)
                    .Select(rf => new QuestionAndAnswerForUserExamViewModel
                    {
                        QuestionId = rf.Question.QuestionId,
                        QuestionText = rf.Question.QuestionText,
                        QuestionType = rf.Question.Answers.FirstOrDefault()?.AnswerTypes ?? AnswerTypeEnumTypes.None,
                        Answers = rf.Question.Answers.Select(ex => new AnswerUserExamViewModel
                        {
                            AnswerId = ex.AnswerId,
                            AnswerText = ex.AnswerText,
                            OptionOne = ex.TestOptionOne,
                            OptionTwo = ex.TestOptionTwo,
                            OptionThree = ex.TestOptionThree,
                            OptionFour = ex.TestOptionFour,
                            CorrectOption = ex.TestCorrectOption,
                            TrueOrFalseAnswer = ex.TruOrFalseAnswer
                        }).ToList()
                    }).ToList()
            };
            return model;
        }

        public async Task SaveUserAnswerAsync(List<UserAnswerForExam> answer)
        {
            if (answer == null || !answer.Any()) return;

            var userAnswer = answer.Select(a => new UserAnswer()
            {
                FkUserId = a.FkUserId,
                FkFormId = a.FormId,
                FkQuestionId = a.QuestionId,
                FkAnswerId = a.AnswerId ?? null,
                DescriptiveAnswerText = a.DescriptiveAnswerText,
                SelectedOption = a.SelectedOption,
                SelectedTrueOrFalse = a.SelectedTrueOrFalse,
                AnswerDate = DateTime.UtcNow
            }).ToList();

            await _context.UserAnswers.AddRangeAsync(userAnswer);
            await _context.SaveChangesAsync();
        }

        //public async Task<ExamAnswerSheetViewModel> GetExamAnswerSheetAsync(string examId, string userId)
        //{
        //    var form = await _context.Forms
        //        .Include(f => f.RFormQuestions)
        //        .ThenInclude(rfq => rfq.Question)
        //        .ThenInclude(q => q.Answers)
        //        .FirstOrDefaultAsync(f => f.FormId.Equals(examId));

        //    if (form == null)
        //        return null;

        //    var userAnswers = await _context.UserAnswers
        //        .Where(ua => ua.FkFormId.Equals(examId) && ua.FkUserId.Equals(userId))
        //        .ToListAsync();

        //    var model = new ExamAnswerSheetViewModel()
        //    {
        //        FormId = form.FormId,
        //        FormTitle = form.FormTitle,
        //        Questions = form.RFormQuestions.OrderBy(q => q.Order).Select(q =>
        //        {
        //            var userAnswer = userAnswers.FirstOrDefault(ua => ua.FkQuestionId.Equals(q.Question.QuestionId));
        //            var correctAnswer = q.Question.Answers.FirstOrDefault();

        //            return new AnswerSheetQuestionViewModel()
        //            {
        //                QuestionText = q.Question.QuestionText,
        //                QuestionType = correctAnswer?.AnswerTypes ?? AnswerTypeEnumTypes.None,
        //                UserSelectedAnswer = userAnswer == null ? null : new UserAnswerForExam
        //                {
        //                    AnswerId = userAnswer.FkAnswerId,
        //                    FormId = userAnswer.FkFormId,
        //                    QuestionId = userAnswer.FkQuestionId,
        //                    SelectedOption = userAnswer.SelectedOption,
        //                    SelectedTrueOrFalse = userAnswer.SelectedTrueOrFalse,
        //                    DescriptiveAnswerText = userAnswer.DescriptiveAnswerText,
        //                    FkUserId = userAnswer.FkUserId
        //                },
        //                CorrectAnswer = correctAnswer == null ? null : new CorrectAnswerUserExamViewModel()
        //                {
        //                    AnswerId = correctAnswer.AnswerId,
        //                    QuestionId = correctAnswer.FkQuestionId,
        //                    OptionOne = correctAnswer.TestOptionOne,
        //                    OptionTwo = correctAnswer.TestOptionTwo,
        //                    OptionThree = correctAnswer.TestOptionThree,
        //                    OptionFour = correctAnswer.TestOptionFour,
        //                    TrueOrFalseAnswer = correctAnswer.TruOrFalseAnswer,
        //                    AnswerType = correctAnswer.AnswerTypes
        //                }
        //            };
        //        }).ToList()
        //    };

        //    return model;
        //}

        //public async Task<ExamAnswerSheetViewModel> GetExamAnswerSheetAsync(string examId, string userId)
        //{
        //    var form = await _context.Forms
        //        .Include(f => f.RFormQuestions)
        //            .ThenInclude(rfq => rfq.Question)
        //                .ThenInclude(q => q.Answers)
        //        .FirstOrDefaultAsync(f => f.FormId == examId);

        //    if (form == null)
        //        return null;

        //    var userAnswers = await _context.UserAnswers
        //        .Where(ua => ua.FkFormId == examId && ua.FkUserId == userId)
        //        .ToListAsync();

        //    var userAnswersDict = userAnswers.ToDictionary(ua => ua.FkQuestionId);

        //    var model = new ExamAnswerSheetViewModel
        //    {
        //        FormId = form.FormId,
        //        FormTitle = form.FormTitle,
        //        Questions = form.RFormQuestions
        //            .OrderBy(q => q.Order)
        //            .Select(rfq =>
        //            {
        //                var question = rfq.Question;
        //                userAnswersDict.TryGetValue(question.QuestionId, out var userAnswer);

        //                var correctAnswer = question.Answers.FirstOrDefault();

        //                return new AnswerSheetQuestionViewModel
        //                {
        //                    QuestionText = question.QuestionText,
        //                    QuestionType = correctAnswer?.AnswerTypes ?? AnswerTypeEnumTypes.None,

        //                    UserSelectedAnswer = userAnswer == null ? null : new UserAnswerForExam
        //                    {
        //                        AnswerId = userAnswer.FkAnswerId,
        //                        FormId = userAnswer.FkFormId,
        //                        QuestionId = userAnswer.FkQuestionId,
        //                        SelectedOption = userAnswer.SelectedOption,
        //                        SelectedTrueOrFalse = userAnswer.SelectedTrueOrFalse,
        //                        DescriptiveAnswerText = userAnswer.DescriptiveAnswerText,
        //                        FkUserId = userAnswer.FkUserId
        //                    },

        //                    CorrectAnswer = correctAnswer == null ? null : new CorrectAnswerUserExamViewModel
        //                    {
        //                        AnswerId = correctAnswer.AnswerId,
        //                        QuestionId = correctAnswer.FkQuestionId,
        //                        OptionOne = correctAnswer.TestOptionOne,
        //                        OptionTwo = correctAnswer.TestOptionTwo,
        //                        OptionThree = correctAnswer.TestOptionThree,
        //                        OptionFour = correctAnswer.TestOptionFour,
        //                        CorrectOption = correctAnswer.TestCorrectOption?.Trim(), // Trim برای تطابق دقیق
        //                        TrueOrFalseAnswer = correctAnswer.TruOrFalseAnswer,
        //                        AnswerText = correctAnswer.AnswerText,
        //                        AnswerType = correctAnswer.AnswerTypes
        //                    }
        //                };
        //            })
        //            .ToList()
        //    };

        //    return model;
        //}

        //public async Task<ExamAnswerSheetViewModel> GetExamAnswerSheetAsync(string examId, string userId)
        //{
        //    // گرفتن فرم به همراه سوالات و پاسخ‌ها
        //    var form = await _context.Forms
        //        .Include(f => f.RFormQuestions)
        //            .ThenInclude(rfq => rfq.Question)
        //                .ThenInclude(q => q.Answers)
        //        .FirstOrDefaultAsync(f => f.FormId.Equals(examId));

        //    if (form == null)
        //        return null;

        //    // گرفتن پاسخ‌های کاربر
        //    var userAnswers = await _context.UserAnswers
        //        .Where(ua => ua.FkFormId.Equals(examId) && ua.FkUserId.Equals(userId))
        //        .ToListAsync();

        //    var userAnswersDict = userAnswers.ToDictionary(ua => ua.FkQuestionId);

        //    var model = new ExamAnswerSheetViewModel()
        //    {
        //        FormId = form.FormId,
        //        FormTitle = form.FormTitle,
        //        Questions = form.RFormQuestions
        //            .OrderBy(q => q.Order)
        //            .Select(rfq =>
        //            {
        //                var question = rfq.Question;

        //                // پاسخ کاربر
        //                userAnswersDict.TryGetValue(question.QuestionId, out var userAnswer);

        //                // پاسخ صحیح واقعی
        //                //var correctAnswer = question.Answers.FirstOrDefault(a => a.TestCorrectOption == "True");
        //                var correctAnswer = question.Answers.FirstOrDefault();
        //                if (correctAnswer != null)
        //                {
        //                    switch (correctAnswer.AnswerTypes)
        //                    {
        //                        case AnswerTypeEnumTypes.TrueOrFalse:
        //                            // TruOrFalseAnswer قبلاً پر شده
        //                            break;

        //                        case AnswerTypeEnumTypes.Test:
        //                            // CorrectOption قبلاً پر شده
        //                            break;

        //                        case AnswerTypeEnumTypes.Anatomical:
        //                            // AnswerText پر شده
        //                            break;
        //                    }
        //                }

        //                return new AnswerSheetQuestionViewModel()
        //                {
        //                    QuestionText = question.QuestionText,
        //                    QuestionType = correctAnswer?.AnswerTypes ?? AnswerTypeEnumTypes.None,

        //                    UserSelectedAnswer = userAnswer == null ? null : new UserAnswerForExam
        //                    {
        //                        AnswerId = userAnswer.FkAnswerId,
        //                        FormId = userAnswer.FkFormId,
        //                        QuestionId = userAnswer.FkQuestionId,
        //                        SelectedOption = userAnswer.SelectedOption,
        //                        SelectedTrueOrFalse = userAnswer.SelectedTrueOrFalse,
        //                        DescriptiveAnswerText = userAnswer.DescriptiveAnswerText,
        //                        FkUserId = userAnswer.FkUserId
        //                    },

        //                    CorrectAnswer = question.Answers.FirstOrDefault() == null ? null :
        //                        new CorrectAnswerUserExamViewModel
        //                        {
        //                            AnswerId = question.Answers.First().AnswerId,
        //                            QuestionId = question.Answers.First().FkQuestionId,
        //                            OptionOne = question.Answers.First().TestOptionOne,
        //                            OptionTwo = question.Answers.First().TestOptionTwo,
        //                            OptionThree = question.Answers.First().TestOptionThree,
        //                            OptionFour = question.Answers.First().TestOptionFour,
        //                            CorrectOption = question.Answers.First().TestCorrectOption,
        //                            TrueOrFalseAnswer = question.Answers.First().TruOrFalseAnswer,
        //                            AnswerText = question.Answers.First().AnswerText,
        //                            AnswerType = question.Answers.First().AnswerTypes
        //                        }

        //                };
        //            })
        //            .ToList()
        //    };

        //    return model;
        //}

        // در همان کلاس سرویس
        private static string NormalizeOptionId(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return null;

            var v = raw.Trim();
            // حذف فاصله و _
            v = v.Replace(" ", "").Replace("_", "");
            var lower = v.ToLowerInvariant();

            // نگاشت‌های رایج (انگلیسی/فارسی/حروف)
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["1"] = "1",
                ["one"] = "1",
                ["optionone"] = "1",
                ["a"] = "1",
                ["الف"] = "1",
                ["۱"] = "1",
                ["2"] = "2",
                ["two"] = "2",
                ["optiontwo"] = "2",
                ["b"] = "2",
                ["ب"] = "2",
                ["۲"] = "2",
                ["3"] = "3",
                ["three"] = "3",
                ["optionthree"] = "3",
                ["c"] = "3",
                ["ج"] = "3",
                ["۳"] = "3",
                ["4"] = "4",
                ["four"] = "4",
                ["optionfour"] = "4",
                ["d"] = "4",
                ["د"] = "4",
                ["۴"] = "4"
            };

            if (map.TryGetValue(lower, out var id)) return id;

            // اگر مقدار، متن خود گزینه (مثلاً "20") باشد، تشخیص به View واگذار می‌شود
            return null;
        }


        public async Task<ExamAnswerSheetViewModel> GetExamAnswerSheetAsync(string examId, string userId)
        {
            var form = await _context.Forms
                .Include(f => f.RFormQuestions)
                    .ThenInclude(rfq => rfq.Question)
                        .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(f => f.FormId == examId);

            if (form == null)
                return null;

            var userAnswers = await _context.UserAnswers
                .Where(ua => ua.FkFormId == examId && ua.FkUserId == userId)
                .ToListAsync();

            var userAnswersDict = userAnswers.ToDictionary(ua => ua.FkQuestionId);

            var model = new ExamAnswerSheetViewModel
            {
                FormId = form.FormId,
                FormTitle = form.FormTitle,
                Questions = form.RFormQuestions
        .OrderBy(q => q.Order)
        .Select(rfq =>
        {
            var question = rfq.Question;
            userAnswersDict.TryGetValue(question.QuestionId, out var userAnswer);

            var correctAnswer = question.Answers.FirstOrDefault();

            return new AnswerSheetQuestionViewModel
            {
                QuestionText = question.QuestionText,
                QuestionType = correctAnswer?.AnswerTypes ?? AnswerTypeEnumTypes.None,

                UserSelectedAnswer = userAnswer == null ? null : new UserAnswerForExam
                {
                    AnswerId = userAnswer.FkAnswerId,
                    FormId = userAnswer.FkFormId,
                    QuestionId = userAnswer.FkQuestionId,
                    // 👇 نرمال‌سازی
                    SelectedOption = NormalizeOptionId(userAnswer.SelectedOption) ?? userAnswer.SelectedOption?.Trim(),
                    SelectedTrueOrFalse = userAnswer.SelectedTrueOrFalse,
                    DescriptiveAnswerText = userAnswer.DescriptiveAnswerText,
                    FkUserId = userAnswer.FkUserId
                },

                CorrectAnswer = correctAnswer == null ? null : new CorrectAnswerUserExamViewModel
                {
                    AnswerId = correctAnswer.AnswerId,
                    QuestionId = correctAnswer.FkQuestionId,
                    // بهتره Trim هم روی متن‌ها بخوره
                    OptionOne = correctAnswer.TestOptionOne?.Trim(),
                    OptionTwo = correctAnswer.TestOptionTwo?.Trim(),
                    OptionThree = correctAnswer.TestOptionThree?.Trim(),
                    OptionFour = correctAnswer.TestOptionFour?.Trim(),
                    // 👇 نرمال‌سازی
                    CorrectOption = OptionNormalizer.NormalizeOptionId(correctAnswer.TestCorrectOption)
                                    ?? correctAnswer.TestCorrectOption?.Trim(),
                    TrueOrFalseAnswer = correctAnswer.TruOrFalseAnswer,
                    AnswerText = correctAnswer.AnswerText?.Trim(),
                    AnswerType = correctAnswer.AnswerTypes
                }
            };
        })
        .ToList()
            };

            return model;
        }

        #endregion
    }
}
