using DO;
using DalApi;

namespace Dal;

internal class QuestionForStudentImplementation : IQuestionForStudent
{
    public int Create(QuestionForStudent item)
    {
        if (DataSource.QuestionForStudents.Exists(s =>
                s?.StudentId == item.StudentId &&
                s?.TestId == item.TestId &&
                s?.QuestionId == item.QuestionId))
        {
            throw new DalAlreadyExistsException(
                $"QuestionForStudent with StudentId {item.StudentId}, TestId {item.TestId}, QuestionId {item.QuestionId} already exists.");
        }

        DataSource.QuestionForStudents.Add(item);

        return item.StudentId;
    }

    public QuestionForStudent? Read(int id)
    {
    
        return DataSource.QuestionForStudents.FirstOrDefault(s => s?.StudentId == id);
    }    

    public List<QuestionForStudent> ReadAll()
    {
        return new List<QuestionForStudent>(DataSource.QuestionForStudents);
    }

    public void Update(QuestionForStudent item)
    {
        QuestionForStudent? existing = DataSource.QuestionForStudents.FirstOrDefault(s =>
            s?.StudentId == item.StudentId &&
            s?.TestId == item.TestId &&
            s?.QuestionId == item.QuestionId);

        if (existing == null)
        {
            throw new DalDoesNotExistException(
                $"QuestionForStudent with StudentId {item.StudentId}, TestId {item.TestId}, QuestionId {item.QuestionId} does not exist.");
        }

        DataSource.QuestionForStudents.Remove(existing);
        DataSource.QuestionForStudents.Add(item);
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible("QuestionForStudent cannot be deleted");
    }
}