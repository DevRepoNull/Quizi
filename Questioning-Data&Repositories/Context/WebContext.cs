using Questioning_Data_Repositories.DataTableFluent.Question_AnswerFluent;
using Questioning_Data_Repositories.DataTableFluent.User_RoleFluent;

namespace Questioning_Data_Repositories.Context
{
    //Primary Constructor
    public class WebContext(DbContextOptions<WebContext> options) : DbContext(options)
    {
        #region Question Db Context Dependency Injection

        //public WebContext(DbContextOptions<WebContext> options) : base(options)
        //{

        //}

        #endregion

        #region User & Permission Dbsets

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        #endregion

        #region Question & Answer Dbsets

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Categories> Categories { get; set; }

        public DbSet<UserAnswer> UserAnswers { get; set; }

        #endregion

        #region Forms Dbsets

        public DbSet<Form> Forms { get; set; }

        public DbSet<RFormQuestion> RFormQuestions { get; set; }

        #endregion

        #region Question Table Fluent Configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            #region User, Permission

            modelBuilder.ApplyConfiguration(new UserFluent());

            modelBuilder.ApplyConfiguration(new RoleFluent());

            #endregion

            #region Common

            //modelBuilder.ApplyConfiguration(new BaseFluent());

            #endregion

            #region Question, Answer

            modelBuilder.ApplyConfiguration(new QuestionFluent());

            modelBuilder.ApplyConfiguration(new AnswerFluent());

            modelBuilder.ApplyConfiguration(new UserAnswerFluent());

            modelBuilder.ApplyConfiguration(new CategoriesFluent());

            #endregion

            #region Forms

            modelBuilder.ApplyConfiguration(new FormFluent());

            modelBuilder.ApplyConfiguration(new RFormQuestionFluent());

            #endregion
        }

        #endregion
    }
}
