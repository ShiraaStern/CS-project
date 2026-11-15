
using DalApi;
using DO;
using System.Reflection;
using Tools;

namespace Dal;

internal class AnswerImplementation : IAnswer
{
    public int Create(Answer item)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, "Start");

        int newId = DataSource.Config.NextAnswerId;
        Answer newAnswer = item with { Id = newId };
        DataSource.Answers.Add(newAnswer);

        LogManager.WriteLog(project, func, $"End, answer created with Id {newId}");

        return newId;
    }

    public void Delete(int id)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {id}");

        Answer? existing = Read(id);

        if (existing == null)
        {
            LogManager.WriteLog(project, func, $"Answer with id {id} does not exist");
            throw new DalDoesNotExistException($"Answer with id {id} does not exist.");
        }

        DataSource.Answers.Remove(existing);

        LogManager.WriteLog(project, func, $"End, answer {id} deleted");
    }

    public Answer? Read(int id)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {id}");

        Answer? result = DataSource.Answers.FirstOrDefault(s => s?.Id == id);

        if (result == null)
            LogManager.WriteLog(project, func, $"Answer with id {id} was not found");

        LogManager.WriteLog(project, func, "End");

        return result;
    }

    public List<Answer?> ReadAll(Func<Answer, bool>? filter = null)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, "Start");

        List<Answer?> result = filter == null
            ? DataSource.Answers.Select(item => item).ToList()
            : DataSource.Answers.Where(item => item != null && filter(item)).ToList();

        LogManager.WriteLog(project, func, $"End, {result.Count} items returned");

        return result;
    }

    public Answer? Read(Func<Answer, bool> filter)
    {
        return DataSource.Answers.FirstOrDefault(filter);
    }

    public void Update(Answer item)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {item.Id}");

        Answer? existing = Read(item.Id);

        if (existing == null)
        {
            LogManager.WriteLog(project, func, $"Answer with id {item.Id} does not exist");
            throw new DalDoesNotExistException($"Answer with id {item.Id} does not exist.");
        }

        DataSource.Answers.Remove(existing);
        DataSource.Answers.Add(item);

        LogManager.WriteLog(project, func, $"End, answer {item.Id} updated");
    }
}
