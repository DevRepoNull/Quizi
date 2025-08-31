using Microsoft.AspNetCore.Mvc.Rendering;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.StaticValue;
using QuestionListFromAdminPanelViewModel = Questioning_Data_Repositories.ViewModel.AdminPanelVms.QuestionListFromAdminPanelViewModel;

namespace Questioning_Data_Repositories.Repositories.Services
{
    public class QuestionAndAnswerServices(WebContext webContext) : IQuestionAndAnswerInterfaces
    {
        #region Q&A Primary Dependency

        private readonly WebContext _webContext = webContext;

        #endregion

        #region Crud Question From Admin Panel Methods

        public async Task<List<QuestionListFromAdminPanelViewModel>> GettAllQuestionsAsync()
        {
            return await _webContext.Questions
                .Include(q => q.Answers)
                .Select(q => new QuestionListFromAdminPanelViewModel()
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    AnswerType = q.Answers.FirstOrDefault() != null ? q.Answers.FirstOrDefault().AnswerTypes : AnswerTypeEnumTypes.None,
                    FkUserId = q.FkUserId
                }).ToListAsync();
        }

        public async Task<bool> CreateQuestionAsync(CreateQuestionFromAdminPanelViewModel model)
        {
            var strategy = _webContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _webContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (model != null)
                        {
                            var question = new Question()
                            {
                                QuestionText = model.QuestionText,
                                FkUserId = model.FkUserId,
                                CreateDate = DateTime.UtcNow
                            };
                            await _webContext.Questions.AddAsync(question);
                            await _webContext.SaveChangesAsync();

                            var answer = new Answer()
                            {
                                AnswerTypes = model.AnswerType,
                                FkQuestionId = question.QuestionId,
                                CreateDate = DateTime.UtcNow
                            };

                            switch (model.AnswerType)
                            {
                                case AnswerTypeEnumTypes.Anatomical:
                                    answer.AnswerText = model.AnswerText;
                                    break;

                                case AnswerTypeEnumTypes.Test:
                                    answer.TestOptionOne = model.TestOptionOne;
                                    answer.TestOptionTwo = model.TestOptionTwo;
                                    answer.TestOptionThree = model.TestOptionThree;
                                    answer.TestOptionFour = model.TestOptionFour;
                                    answer.TestCorrectOption = model.TestCorrectionOption;
                                    break;

                                case AnswerTypeEnumTypes.TrueOrFalse:
                                    answer.TruOrFalseAnswer = model.TrueOrFalseAnswer;
                                    break;
                            }

                            await _webContext.Answers.AddAsync(answer);
                            await _webContext.SaveChangesAsync();
                        }

                        await transaction.CommitAsync();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            });
        }

        public async Task<EditQuestionFromAdminPanelViewModel> GetQuestionForEditAsync(string questionId)
        {
            //return await _webContext.Questions
            //    .Include(q => q.Answers)
            //    .Where(q => q.QuestionId == questionId)
            //    .Select(q => new EditQuestionFromAdminPanelViewModel()
            //    {
            //        QuestionText = q.QuestionText,
            //        FkUserId = q.FkUserId,

            //        //Answer type have any option?
            //        AnswerType = q.Answers.FirstOrDefault().AnswerTypes,

            //        //Anatomical answer
            //        AnswerText = q.Answers.FirstOrDefault().AnswerText,

            //        //Test option answer
            //        TestOptionOne = q.Answers.FirstOrDefault().TestOptionOne,
            //        TestOptionTwo = q.Answers.FirstOrDefault().TestOptionTwo,
            //        TestOptionThree = q.Answers.FirstOrDefault().TestOptionThree,
            //        TestOptionFour = q.Answers.FirstOrDefault().TestOptionFour,
            //        TestCorrectionOption = q.Answers.FirstOrDefault().TestCorrectOption,

            //        //True or false answer
            //        TrueOrFalseAnswer = q.Answers.FirstOrDefault().TruOrFalseAnswer
            //    }).SingleOrDefaultAsync();

            var data = await _webContext.Questions
                .Where(q => q.QuestionId == questionId)
                .SelectMany(q => q.Answers.DefaultIfEmpty(), (q, a) => new
                {
                    q.QuestionText,
                    q.QuestionId,
                    q.FkUserId,
                    Answer = a
                })
                .FirstOrDefaultAsync();

            if (data == null)
                return null;

            return new EditQuestionFromAdminPanelViewModel()
            {
                QuestionId = data.QuestionId,
                QuestionText = data.QuestionText,
                FkUserId = data.FkUserId,

                AnswerType = data.Answer != null ? data.Answer.AnswerTypes : AnswerTypeEnumTypes.None,
                AnswerText = data.Answer != null ? data.Answer.AnswerText : null,

                TestOptionOne = data.Answer != null ? data.Answer.TestOptionOne : null,
                TestOptionTwo = data.Answer != null ? data.Answer.TestOptionTwo : null,
                TestOptionThree = data.Answer != null ? data.Answer.TestOptionThree : null,
                TestOptionFour = data.Answer != null ? data.Answer.TestOptionFour : null,
                TestCorrectionOption = data.Answer != null ? data.Answer.TestCorrectOption : null,

                TrueOrFalseAnswer = data.Answer != null && data.Answer.TruOrFalseAnswer
            };
        }

        public async Task<bool> EditQuestionAsync(string questionId, EditQuestionFromAdminPanelViewModel model)
        {
            if (_webContext != null)
            {
                var question = await GetQuestionByQuestionId(questionId);

                if (question == null)
                    return false;

                question.QuestionId = questionId;
                question.QuestionText = model.QuestionText;
                question.FkUserId = model.FkUserId;
                question.UpdateDate = DateTime.UtcNow;
                _webContext.Update(question);

                var answer = question.Answers.SingleOrDefault();
                if (answer != null)
                {
                    answer.AnswerTypes = model.AnswerType;
                    answer.UpdateDate = DateTime.UtcNow;

                    switch (model.AnswerType)
                    {
                        case AnswerTypeEnumTypes.Anatomical:
                            answer.AnswerText = model.AnswerText;
                            answer.TestOptionOne = null;
                            answer.TestOptionTwo = null;
                            answer.TestOptionThree = null;
                            answer.TestOptionFour = null;
                            answer.TestCorrectOption = null;
                            answer.TruOrFalseAnswer = false;
                            break;

                        case AnswerTypeEnumTypes.Test:
                            answer.TestOptionOne = model.TestOptionOne;
                            answer.TestOptionTwo = model.TestOptionTwo;
                            answer.TestOptionThree = model.TestOptionThree;
                            answer.TestOptionFour = model.TestOptionFour;
                            answer.TestCorrectOption = model.TestCorrectionOption;
                            answer.AnswerText = null;
                            answer.TruOrFalseAnswer = false;
                            break;

                        case AnswerTypeEnumTypes.TrueOrFalse:
                            answer.TruOrFalseAnswer = model.TrueOrFalseAnswer;
                            answer.AnswerText = null;
                            answer.TestOptionOne = null;
                            answer.TestOptionTwo = null;
                            answer.TestOptionThree = null;
                            answer.TestOptionFour = null;
                            answer.TestCorrectOption = null;
                            break;
                    }

                    _webContext.Update(answer);
                }

                await _webContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteQuestionAsync(string questionId)
        {
            var strategy = _webContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _webContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var question = await _webContext.Questions
                            .Include(q => q.Answers)
                            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

                        if (question == null)
                            return false;

                        if (question.Answers != null && question.Answers.Any())
                        {
                            _webContext.Answers.RemoveRange(question.Answers);
                        }

                        _webContext.Questions.Remove(question);

                        await _webContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return true;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            });
        }

        #endregion

        #region Answer Selection Methods

        public string GetEnumDisplayName(Enum enumValue)
        {
            //var displayAttribute = enumValue.GetType()
            //    .GetField(enumValue.ToString())?
            //    .GetCustomAttributes(typeof(DisplayAttribute), false)
            //    .FirstOrDefault() as DisplayAttribute;

            //return displayAttribute != null ? displayAttribute.Name : enumValue.ToString();

            return enumValue.GetType()
                .GetField(enumValue.ToString())?
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() is DisplayAttribute displayAttribute ? displayAttribute.Name : enumValue.ToString();
        }

        public SelectList SelectAnswerTypeList()
        {
            var list = Enum.GetValues(typeof(AnswerTypeEnumTypes))
                .Cast<AnswerTypeEnumTypes>()
                .Where(a => (a != AnswerTypeEnumTypes.None))
                .Select(a => new SelectListItem()
                {
                    Value = ((byte)a).ToString(),
                    Text = GetEnumDisplayName(a)
                }).ToList();

            return new SelectList(list, nameof(SelectListItem.Value), nameof(SelectListItem.Text));
        }

        public SelectList SelectAnswerTypeListById(string answerId)
        {
            var list = Enum.GetValues(typeof(AnswerTypeEnumTypes))
                .Cast<AnswerTypeEnumTypes>()
                .Where(a => (a != AnswerTypeEnumTypes.None))
                .Select(a => new SelectListItem()
                {
                    Value = ((byte)a).ToString(),
                    Text = GetEnumDisplayName(a)
                }).ToList();

            return new SelectList(list, nameof(SelectListItem.Value), nameof(SelectListItem.Text));
        }

        #endregion

        #region Question Finder Methods

        public async Task<Question> GetQuestionByQuestionId(string questionId) =>
            await _webContext.Questions.FindAsync(questionId);

        #endregion
    }
}
