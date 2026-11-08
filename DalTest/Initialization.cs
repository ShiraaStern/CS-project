using DalApi;
using DO;
namespace DalTest;

public static class Initialization
{
    private static IDal? s_dal;
    private const int NUM_QUESTIONS = 20;
    private const int NUM_ANSWERS_PER_QUESTION = 4;
    private const int NUM_TESTS = 5;
    private const int STUDENTS_PER_TEST = 5;
    private const int NUM_QUESTIONS_PER_TEST = 4;

    static Subjects[] subjects = Enum.GetValues<Subjects>();
    private static void createQuestions()
    {

        for (int i = 0; i < NUM_QUESTIONS; i++)
        {
            Question question = new Question(0, subjects[i % subjects.Length],$"  question number {i+1}", 0);
            s_dal!.Question.Create(question);

        }

    }
    private static void createAnswers()
    {
        foreach (Question q in s_dal!.Question.ReadAll())
        {
            int correctID=0;

            for (int i = 0; i < NUM_ANSWERS_PER_QUESTION; i++)
            {
                Answer answer = new Answer(0, q.Id, $"Answer {i + 1} for question {q.Id}");
                int newId = s_dal!.Answer.Create(answer);
                if (i == 0)
                    correctID = newId;
            }
            s_dal!.Question.Update(q with { Correct = correctID });
        }
    }
    private static void createTests()
    {    
        for (int i = 0; i < NUM_TESTS; i++)
        {
            DateTime date = DateTime.Now.AddDays(i);
        Test tests = new Test(0, subjects[i % subjects.Length], date, NUM_QUESTIONS_PER_TEST);
        s_dal!.Test.Create(tests);
 }
    }
    private static void createStudents()
    {
        string[] names = { "Dina", "Yael", "Rina", "Avigail", "Noa" };
        List<Test> tests = s_dal!.Test.ReadAll();
        for (int i = 0; i < NUM_TESTS * STUDENTS_PER_TEST; i++) { 
            int id = 200 + i;
            Student student = new Student(id, names[i % names.Length], tests[i/ STUDENTS_PER_TEST].Id, 0);
            s_dal!.Student.Create(student);
        }
    }

    private static void createQuestionForStudents()
    {
        List<Student> students = s_dal!.Student.ReadAll();
        List<Question> questions = s_dal!.Question.ReadAll();
        List<Answer> answers = s_dal!.Answer.ReadAll();


        foreach (Student s in students)
        {
            Test? test = s_dal!.Test.Read(s.TestId);
            if (test == null) continue;

            foreach (Question q in questions)
            {
                if (q.Subject == test.Subject)
                {

                    foreach (Answer a in answers)
                    {
                        if (a.QuestionId == q.Id) { 

                            QuestionForStudent questionForStudents = new QuestionForStudent(s.Id, test.Id, q.Id, a.Id);
                            s_dal!.QuestionForStudent.Create(questionForStudents);
                            break;
                        }
                    }

                }
            }
        }
    }    
    public static void Initialize(IDal dal)
    {
        s_dal = dal;
        createQuestions();
        createAnswers();
        createTests();
        createStudents();
        createQuestionForStudents();

    }
}