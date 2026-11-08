using DO;
using DalApi;
using Tools;
using System.Reflection;

namespace Dal;

internal class TestImplementation : ITest
{
    public int Create(Test item)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, "Start");

        int newId = DataSource.Config.NextTestId;
        Test newTest = item with { Id = newId };
        DataSource.Tests.Add(newTest);

        LogManager.WriteLog(project, func, $"End, test created with Id {newId}");

        return newId;
    }

    public void Delete(int id)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {id}");

        Test? existing = Read(id);

        if (existing == null)
        {
            LogManager.WriteLog(project, func, $"Test with id {id} does not exist");
            throw new DalDoesNotExistException($"Test with id {id} does not exist.");
        }

        DataSource.Tests.Remove(existing);

        LogManager.WriteLog(project, func, $"End, test {id} deleted");
    }

    public Test? Read(int id)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {id}");

        Test? result = DataSource.Tests.FirstOrDefault(s => s?.Id == id);

        if (result == null)
            LogManager.WriteLog(project, func, $"Test with id {id} was not found");

        LogManager.WriteLog(project, func, "End");

        return result;
    }

    public List<Test> ReadAll()
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, "Start");

        List<Test> result = new List<Test>(DataSource.Tests);

        LogManager.WriteLog(project, func, $"End, {result.Count} items returned");

        return result;
    }

    public void Update(Test item)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {item.Id}");

        Test? existing = Read(item.Id);

        if (existing == null)
        {
            LogManager.WriteLog(project, func, $"Test with id {item.Id} does not exist");
            throw new DalDoesNotExistException($"Test with id {item.Id} does not exist.");
        }

        DataSource.Tests.Remove(existing);
        DataSource.Tests.Add(item);

        LogManager.WriteLog(project, func, $"End, test {item.Id} updated");
    }
}