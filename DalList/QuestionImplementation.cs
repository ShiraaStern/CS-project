using DalApi;
using DO;

namespace Dal;

internal class QuestionImplementation : IQuestion
{
    public int Create(Question item)
    {
        int newId = DataSource.Config.NextQuestionId;
        Question newQuestion = item with { Id = newId };
        DataSource.Questions.Add(newQuestion);
        return newId;
    }
    
    public void Delete(int id)
    {
        Question? existing = Read(id);
        if (existing == null)
        {
            throw new DalDoesNotExistException($"Question with id {id} does not exist.");
        }
        DataSource.Questions.Remove(existing);
    }

    public Question? Read(int id)
    {
        return DataSource.Questions.FirstOrDefault(s => s?.Id == id);
    }

    public List<Question> ReadAll()
    {
        return new List<Question>(DataSource.Questions);
    }

    public void Update(Question item)
    {
        Question? existing = Read(item.Id);
        if (existing == null)
        {
            throw new DalDoesNotExistException($"Question with id {item.Id} does not exist.");
        }
        DataSource.Questions.Remove(existing);
        DataSource.Questions.Add(item);
    }

}
