namespace Questioning_Data_Repositories.Repositories.Interfaces
{
    public interface IFormInterfaces
    {
        #region Form Methods

        Task<List<FormExamListViewModel>> GetAllFormExamAsync();

        Task<Form> GetFormExamById(string formId);

        Task<string> CreateFormAsync(CreateFormExamViewModel model);

        Task<EditFormExamViewModel> GetFormBeforeEditAsync(string formId);

        Task<bool> EditFormAsync(string formId, EditFormExamViewModel model);

        Task<bool> DeleteFormAsync(string formId);

        #endregion

        #region Question Form Methods

        Task<bool> AddQuestionToFormAsync(string formId, string questionId, int order);

        Task<bool> RemoveQuestionFromFormAsync(string formId, string questionId);

        Task<List<Question>> GetQuestionOfFormAsync(string formId);

        #endregion

        #region Question And Answers Data

        Task<List<Question>> GetAllQuestionAsync();

        Task<List<RFormQuestion>> GetQuestionOfRFormAsync(string formId);

        #endregion

        #region Exam Form Pages

        Task<ShowExamFormViewModel> GetExamFormByIdAsync(string formId);

        #endregion

        #region Enter User On Exam Pages

        Task<List<ExamDataViewModel>> GetExamListAsync();

        Task<ExamUserViewModel> GetExamUserAsync(string examId);

        Task SaveUserAnswerAsync(List<UserAnswerForExam> answer);

        Task<ExamAnswerSheetViewModel> GetExamAnswerSheetAsync(string examId, string userId);

        #endregion
    }
}
