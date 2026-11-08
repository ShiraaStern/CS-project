using DO;
using DalApi;

namespace Dal;

internal class TestImplementation : ITest
{
    public int Create(Test item)
    {
        int newId = DataSource.Config.NextTestId;
        Test newTest = item with { Id = newId };
        DataSource.Tests.Add(newTest);
        return newId;
    }
    public void Delete(int id)
    {
        Test? existing = Read(id);
        if (existing == null)
        {
            throw new DalDoesNotExistException($"Test with id {id} does not exist.");
        }
        DataSource.Tests.Remove(existing);
    }
    public Test? Read(int id)
    {
        return DataSource.Tests.FirstOrDefault(s => s?.Id == id);
    }
    public List<Test> ReadAll()
    {
        return new List<Test>(DataSource.Tests);
    }
    public void Update(Test item)
    {
        Test? existing = Read(item.Id);
        if (existing == null)
        {
            throw new DalDoesNotExistException($"Test with id {item.Id} does not exist.");
        }
        DataSource.Tests.Remove(existing);
        DataSource.Tests.Add(item);
    }
}
