using DO;
using DalApi;

namespace Dal;

internal class AnswerImplementation : IAnswer
{
    public int Create(Answer item)
    {
        int newId = DataSource.Config.NextAnswerId;
        Answer newAnswer = item with { Id = newId };
        DataSource.Answers.Add(newAnswer);
        return newId;
    }

    public void Delete(int id)
    {
        Answer? existing = Read(id);
        if (existing == null)
        {
            throw new DalDoesNotExistException($"Answer with id {id} does not exist.");
        }
        DataSource.Answers.Remove(existing);
    }

    public Answer? Read(int id)
    {
        return DataSource.Answers.FirstOrDefault(s => s?.Id == id);
    }

    public List<Answer> ReadAll()
    {
        return new List<Answer>(DataSource.Answers);
    }

    public void Update(Answer item)
    {
        Answer? existing = Read(item.Id);
        if (existing == null)
        {
            throw new DalDoesNotExistException($"Answer with id {item.Id} does not exist.");
        }
        DataSource.Answers.Remove(existing);
        DataSource.Answers.Add(item);
    }
}
