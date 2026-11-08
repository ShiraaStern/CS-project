using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

    public record QuestionForStudent
    (
        int StudentId,
        int TestId,
        int QuestionId,
        int? AnswerId
   )
    {
        QuestionForStudent() : this(0, 0, 0, null)
        {
        }
    }


