using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public record Answer
    (int Id,
    string? Content,
    Subjects Subject,
    int Correct)
{
    public Answer() : this(0, null, Subjects.Math, 0)
    {

    }
}



