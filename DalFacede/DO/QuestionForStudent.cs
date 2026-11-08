namespace DO;

public record QuestionForStudent
(
    int StudentId,
    int TestId,
    int QuestionId,
    int? AnswerId
)
{
    public QuestionForStudent() : this(0, 0, 0, null)
    {
    }
}


