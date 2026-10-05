using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    public record Question
    (
        int id,
        Subjects subject,
        string? content,
        int correct
    )
    {
        Question() : this(0, Subjects.Math, null, 0)
        {
        }
    }
}