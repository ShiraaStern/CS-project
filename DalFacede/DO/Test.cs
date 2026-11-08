using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;


    public record Test
    (
        int Id,
        Subjects Subject,
        DateTime Date,
        int NumQuestion
    )


    {
        Test() : this(0, Subjects.Math, DateTime.Now, 0)
        {
        }
    }
    

