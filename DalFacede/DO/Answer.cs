
namespace DO;

public record Answer
(
    int Id,
    int QuestionId,
    string? AnswerContent
)

{
    public Answer() : this(0, 0, null)
    {
        
    }

}



