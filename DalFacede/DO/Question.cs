using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;


    public record Question
    (
        int Id,
        Subjects Subject,
        string? Content,
        int Correct
    )
    
    {
        Question() : this(0, Subjects.Math, null, 0)
        {
        }
    }
