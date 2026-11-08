using DO;
using DalApi;

namespace Dal;

internal class StudentImplementation : IStudent
{
        public int Create(Student item)
        {
            if (DataSource.Students.Exists(s => s?.Id == item.Id))
            {
                throw new DalAlreadyExistsException($"Student with id {item.Id} already exists.");

            }
            else
                {
                DataSource.Students.Add(item);
                return item.Id;
            }
        }

    public void Delete(int id)
    {
        Student? existing = Read(id);
        if (existing == null)
        {
            throw new DalDoesNotExistException($"Student with id {id} does not exist.");
        }
        DataSource.Students.Remove(existing);
    }

    public Student? Read(int id)
    {
        return DataSource.Students.FirstOrDefault(s => s?.Id == id);
    }

    public List<Student> ReadAll()
    {
        return new List<Student>(DataSource.Students);
    }

    public void Update(Student item)
    {
        Student? existing = Read(item.Id);
        if (existing == null) {
            throw new DalDoesNotExistException($"Student with id {item.Id} does not exist.");
        }
        DataSource.Students.Remove(existing);
        DataSource.Students.Add(item);


    }

}
