using DalApi;
using DO;
using System.Reflection;
using Tools;

namespace Dal;

internal class QuestionImplementation : IQuestion
{
    public int Create(Question item)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, "Start");

        int newId = DataSource.Config.NextQuestionId;
        Question newQuestion = item with { Id = newId };
        DataSource.Questions.Add(newQuestion);

        LogManager.WriteLog(project, func, $"End, question created with Id {newId}");

        return newId;
    }

    public void Delete(int id)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {id}");

        Question? existing = Read(id);

        if (existing == null)
        {
            LogManager.WriteLog(project, func, $"Question with id {id} does not exist");
            throw new DalDoesNotExistException($"Question with id {id} does not exist.");
        }

        DataSource.Questions.Remove(existing);

        LogManager.WriteLog(project, func, $"End, question {id} deleted");
    }

    public Question? Read(int id)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {id}");

        Question? result = DataSource.Questions.FirstOrDefault(s => s?.Id == id);

        if (result == null)
            LogManager.WriteLog(project, func, $"Question with id {id} was not found");

        LogManager.WriteLog(project, func, "End");

        return result;
    }

    public List<Question> ReadAll()
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, "Start");

        List<Question> result = new List<Question>(DataSource.Questions);

        LogManager.WriteLog(project, func, $"End, {result.Count} items returned");

        return result;
    }

    public void Update(Question item)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {item.Id}");

        Question? existing = Read(item.Id);

        if (existing == null)
        {
            LogManager.WriteLog(project, func, $"Question with id {item.Id} does not exist");
            throw new DalDoesNotExistException($"Question with id {item.Id} does not exist.");
        }

        DataSource.Questions.Remove(existing);
        DataSource.Questions.Add(item);

        LogManager.WriteLog(project, func, $"End, question {item.Id} updated");
    }
}