
using DO;
namespace Dal;

internal static class DataSource
    {

   internal static List<Student?> Students = new List<Student?>();
   internal static List<Test?> Tests = new List<Test?>();
   internal static List<Question?> Questions = new List<Question?>();
   internal static List<Answer?> Answers = new List<Answer?>();
   internal static List<QuestionForStudent?> QuestionForStudents = new List<QuestionForStudent?>();
    internal static class Config {
        
        internal const int startTestId = 1;
        private static int s_nextTestId = startTestId;
        internal static int NextTestId => s_nextTestId++;

        internal const int startQuestionId = 1;
        private static int s_nextQuestionId = startQuestionId;
        internal static int NextQuestionId => s_nextQuestionId++;

        internal const int startAnswerId = 1;
        private static int s_nextAnswerId = startAnswerId;
        internal static int NextAnswerId => s_nextAnswerId++;
    }

}


