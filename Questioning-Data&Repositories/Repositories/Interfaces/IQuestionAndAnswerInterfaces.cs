using Microsoft.AspNetCore.Mvc.Rendering;

namespace Questioning_Data_Repositories.Repositories.Interfaces
{
    public interface IQuestionAndAnswerInterfaces
    {
        #region Crud Question From Admin Panel Methods

        Task<List<QuestionListFromAdminPanelViewModel>> GettAllQuestionsAsync();

        Task<bool> CreateQuestionAsync(CreateQuestionFromAdminPanelViewModel model);

        Task<EditQuestionFromAdminPanelViewModel> GetQuestionForEditAsync(string questionId);

        Task<bool> EditQuestionAsync(string questionId, EditQuestionFromAdminPanelViewModel model);

        Task<bool> DeleteQuestionAsync(string questionId);

        #endregion

        #region Answer Selection Methods

        string GetEnumDisplayName(Enum enumValue);

        SelectList SelectAnswerTypeList();

        SelectList SelectAnswerTypeListById(string answerId);

        #endregion

        #region Question Finder Methods

        Task<Question> GetQuestionByQuestionId(string questionId);

        #endregion
    }
}
