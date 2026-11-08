using DO;
using DalApi;
using System.Reflection;
using Tools;
namespace Dal;

internal class QuestionForStudentImplementation : IQuestionForStudent
{
    public int Create(QuestionForStudent item)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;
        LogManager.WriteLog(project, func, "Start");

        if (DataSource.QuestionForStudents.Exists(s =>
                s?.StudentId == item.StudentId &&
                s?.TestId == item.TestId &&
                s?.QuestionId == item.QuestionId))
        {
            LogManager.WriteLog(project, func, "Item already exists");
            throw new DalAlreadyExistsException(
                $"QuestionForStudent with StudentId {item.StudentId}, TestId {item.TestId}, QuestionId {item.QuestionId} already exists.");
        }

        DataSource.QuestionForStudents.Add(item);

        LogManager.WriteLog(project, func, "End");
        return item.StudentId;
    }

    public QuestionForStudent? Read(int id)
    {
    
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, "Start");


        QuestionForStudent? result = DataSource.QuestionForStudents.FirstOrDefault(s => s?.StudentId == id);

        if (result == null)
            LogManager.WriteLog(project, func, $"Item with id {id} was not found");

        LogManager.WriteLog(project, func, "End");
        return result;

    }    

    public List<QuestionForStudent> ReadAll()
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;
        LogManager.WriteLog(project, func, "Start");
        List<QuestionForStudent> result = new List<QuestionForStudent>(DataSource.QuestionForStudents);
        LogManager.WriteLog(project, func, "End");

        return result;

    }

    public void Update(QuestionForStudent item)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;
        LogManager.WriteLog(project, func, "Start");

        QuestionForStudent? existing = DataSource.QuestionForStudents.FirstOrDefault(s =>
            s?.StudentId == item.StudentId &&
            s?.TestId == item.TestId &&
            s?.QuestionId == item.QuestionId);

        if (existing == null)
        {
            LogManager.WriteLog(project, func, "Item does not exist");

            throw new DalDoesNotExistException(
                $"QuestionForStudent with StudentId {item.StudentId}, TestId {item.TestId}, QuestionId {item.QuestionId} does not exist.");
        }

        DataSource.QuestionForStudents.Remove(existing);
        DataSource.QuestionForStudents.Add(item);
        LogManager.WriteLog(project, func, "End");

    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible("QuestionForStudent cannot be deleted");
    }
}