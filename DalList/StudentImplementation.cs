using DO;
using DalApi;
using Tools;
using System.Reflection;

namespace Dal;

internal class StudentImplementation : IStudent
{
    public int Create(Student item)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {item.Id}");

        if (DataSource.Students.Exists(s => s?.Id == item.Id))
        {
            LogManager.WriteLog(project, func, $"Student with id {item.Id} already exists");
            throw new DalAlreadyExistsException($"Student with id {item.Id} already exists.");
        }

        DataSource.Students.Add(item);

        LogManager.WriteLog(project, func, $"End, student created with Id {item.Id}");

        return item.Id;
    }

    public void Delete(int id)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {id}");

        Student? existing = Read(id);

        if (existing == null)
        {
            LogManager.WriteLog(project, func, $"Student with id {id} does not exist");
            throw new DalDoesNotExistException($"Student with id {id} does not exist.");
        }

        DataSource.Students.Remove(existing);

        LogManager.WriteLog(project, func, $"End, student {id} deleted");
    }

    public Student? Read(int id)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {id}");

        Student? result = DataSource.Students.FirstOrDefault(s => s?.Id == id);

        if (result == null)
            LogManager.WriteLog(project, func, $"Student with id {id} was not found");

        LogManager.WriteLog(project, func, "End");

        return result;
    }

    public List<Student> ReadAll()
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, "Start");

        List<Student> result = new List<Student>(DataSource.Students);

        LogManager.WriteLog(project, func, $"End, {result.Count} items returned");

        return result;
    }

    public void Update(Student item)
    {
        string project = MethodBase.GetCurrentMethod()!.DeclaringType!.FullName!;
        string func = MethodBase.GetCurrentMethod()!.Name;

        LogManager.WriteLog(project, func, $"Start, id = {item.Id}");

        Student? existing = Read(item.Id);

        if (existing == null)
        {
            LogManager.WriteLog(project, func, $"Student with id {item.Id} does not exist");
            throw new DalDoesNotExistException($"Student with id {item.Id} does not exist.");
        }

        DataSource.Students.Remove(existing);
        DataSource.Students.Add(item);

        LogManager.WriteLog(project, func, $"End, student {item.Id} updated");
    }
}