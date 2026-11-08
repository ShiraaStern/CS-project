

namespace DO;


public record Student
(
    int Id,
    string Name,
    int TestId,
    int Score

)
{
    Student() : this(0, "", 0, 0)
    {
    }
}
