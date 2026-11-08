using Dal;
using DalApi;
using DO;
using Tools;
using System.Reflection;

namespace DalTest
{
    class Program
    {
        private static readonly IDal s_dal = new DalList();

        static void Main(string[] args)
        {
            try
            {
                Initialization.Initialize(s_dal);

                int input = -1;
                while (input != 0)
                {
                    printMenu();
                    if (!int.TryParse(Console.ReadLine(), out input))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                        input = -1; // TryParse sets 0 on failure, and 0 means exit
                    }
                    else
                    {
                        switch (input)
                        {
                            case 0:
                                Console.WriteLine("Bye! Exiting the program...");
                                break;
                            case 1:
                                crudMenu("Student", s_dal.Student, inputStudent);
                                break;
                            case 2:
                                crudMenu("Test", s_dal.Test, inputTest);
                                break;
                            case 3:
                                crudMenu("Question", s_dal.Question, inputQuestion);
                                break;
                            case 4:
                                crudMenu("Answer", s_dal.Answer, inputAnswer);
                                break;
                            case 5:
                                crudMenu("QuestionForStudent", s_dal.QuestionForStudent, inputQuestionForStudent);
                                break;
                            case 6:
                                LogManager.DeleteOldLogs();
                                Console.WriteLine("Old log folders were deleted.");
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please choose 0-6");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
                string func = MethodBase.GetCurrentMethod()!.Name;

                LogManager.WriteLog(project, func, $"Error: {ex.Message}");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void printMenu()
        {
            Console.WriteLine("Welcome to the Test Program!");
            Console.WriteLine("===== Main Menu =====");
            Console.WriteLine("To work with Students, press 1");
            Console.WriteLine("To work with Tests, press 2");
            Console.WriteLine("To work with Questions, press 3");
            Console.WriteLine("To work with Answers, press 4");
            Console.WriteLine("To work with QuestionForStudent, press 5");
            Console.WriteLine("To clean old log folders, press 6");
            Console.WriteLine("To exit the program, press 0");
            Console.Write("Your choice: ");
        }

        private static void printCrudMenu(string entityName)
        {
            Console.WriteLine($"===== {entityName} Menu =====");
            Console.WriteLine($"To create a new {entityName}, press 1");
            Console.WriteLine($"To view a single {entityName} by ID, press 2");
            Console.WriteLine($"To view all {entityName}s, press 3");
            Console.WriteLine($"To update an existing {entityName}, press 4");
            Console.WriteLine($"To delete a {entityName}, press 5");
            Console.WriteLine("To go back to the main menu, press 0");
            Console.Write("Your choice: ");
        }

        private static void crudMenu<T>(string entityName, ICrud<T> crud, Func<bool, T> inputItem)
        {
            int choice = -1;
            while (choice != 0)
            {
                printCrudMenu(entityName);
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                    choice = -1;
                }
                else
                {
                    switch (choice)
                    {
                        case 0:
                            break;
                        case 1:
                            create(crud, () => inputItem(false));
                            break;
                        case 2:
                            read(crud);
                            break;
                        case 3:
                            readAll(crud);
                            break;
                        case 4:
                            update(crud, () => inputItem(true));
                            break;
                        case 5:
                            delete(crud);
                            break;
                        case 6:
                            LogManager.DeleteOldLogs();
                            Console.WriteLine("Old log folders were deleted.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please choose 0-5.");
                            break;
                    }
                }
            }
        }

        private static void create<T>(ICrud<T> crud, Func<T> createItem)
        {
            try
            {
                T item = createItem();
                int result = crud.Create(item);
                Console.WriteLine($"Item created successfully with ID: {result}");
            }
            catch (Exception ex)
            {
                string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
                string func = MethodBase.GetCurrentMethod()!.Name;

                LogManager.WriteLog(project, func, $"Error: {ex.Message}");
                Console.WriteLine($"Error creating item: {ex.Message}");
            }
        }

        private static void read<T>(ICrud<T> crud)
        {
            Console.Write("Enter the ID to read: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                T? item = crud.Read(id);
                if (item != null)
                {
                    Console.WriteLine(item);
                }
                else
                {
                    Console.WriteLine($"Item with ID {id} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
        }

        private static void readAll<T>(ICrud<T> crud)
        {
            crud.ReadAll().ForEach(item => Console.WriteLine(item));
        }

        private static void update<T>(ICrud<T> crud, Func<T> inputItem)
        {
            try
            {
                T item = inputItem();
                crud.Update(item);
                Console.WriteLine("Item updated successfully.");
            }
            catch (Exception ex)
            {
                string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
                string func = MethodBase.GetCurrentMethod()!.Name;

                LogManager.WriteLog(project, func, $"Error: {ex.Message}");
                Console.WriteLine($"Error updating item: {ex.Message}");
            }
        }

        private static void delete<T>(ICrud<T> crud)
        {
            Console.Write("Enter the ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    crud.Delete(id);
                    Console.WriteLine($"Item with ID {id} deleted successfully.");
                }
                catch (Exception ex)
                {
                    string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
                    string func = MethodBase.GetCurrentMethod()!.Name;

                    LogManager.WriteLog(project, func, $"Error: {ex.Message}");

                    Console.WriteLine($"Error deleting item: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
        }

        private static int readInt(string message)
        {
            Console.Write(message);
            if (!int.TryParse(Console.ReadLine(), out int value))
            {
                throw new FormatException("Invalid input! Please enter a valid integer.");
            }
            return value;
        }

        private static int? readNullableInt(string message)
        {
            Console.Write(message);
            string? text = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }
            if (!int.TryParse(text, out int value))
            {
                throw new FormatException("Invalid input! Please enter a valid integer, or leave empty.");
            }
            return value;
        }

        private static DateTime readDate(string message)
        {
            Console.Write(message);
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime value))
            {
                throw new FormatException("Invalid input! Please enter a valid date.");
            }
            return value;
        }

        private static Subjects readSubject()
        {
            Console.WriteLine("Choose a subject: Math, English, Tora, History, Geography");
            Console.Write("Your choice: ");
            if (!Enum.TryParse<Subjects>(Console.ReadLine(), true, out Subjects subject))
            {
                throw new FormatException("Invalid subject! Please choose one of the listed subjects.");
            }
            return subject;
        }

        private static Student inputStudent(bool isUpdate)
        {
            int id = readInt("Enter Student ID: ");
            Console.Write("Enter Student Name: ");
            string? name = Console.ReadLine();
            int testId = readInt("Enter Test ID: ");
            int score = readInt("Enter Student Score: ");
            return new Student(id, name ?? "", testId, score);
        }

        private static Test inputTest(bool isUpdate)
        {
            int id = isUpdate ? readInt("Enter ID of the Test to update: ") : 0;
            Subjects subject = readSubject();
            DateTime date = readDate("Enter Test Date (yyyy-MM-dd): ");
            int numQuestions = readInt("Enter Number of Questions: ");
            return new Test(id, subject, date, numQuestions);
        }

        private static Question inputQuestion(bool isUpdate)
        {
            int id = isUpdate ? readInt("Enter ID of the Question to update: ") : 0;
            Subjects subject = readSubject();
            Console.Write("Enter Question Text: ");
            string? content = Console.ReadLine();
            int correct = readInt("Enter ID of the correct Answer (0 if there is none yet): ");
            return new Question(id, subject, content ?? "", correct);
        }

        private static Answer inputAnswer(bool isUpdate)
        {
            int id = isUpdate ? readInt("Enter ID of the Answer to update: ") : 0;
            int questionId = readInt("Enter Question ID: ");
            Console.Write("Enter Answer Text: ");
            string? content = Console.ReadLine();
            return new Answer(id, questionId, content ?? "");
        }

        private static QuestionForStudent inputQuestionForStudent(bool isUpdate)
        {
            int studentId = readInt("Enter Student ID: ");
            int testId = readInt("Enter Test ID: ");
            int questionId = readInt("Enter Question ID: ");
            int? answerId = readNullableInt("Enter chosen Answer ID (leave empty if not answered yet): ");
            return new QuestionForStudent(studentId, testId, questionId, answerId);
        }
    }
}