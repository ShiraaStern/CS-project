using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    public record Test
    (
        int id,
        Subjects subject,
        DateTime Date,
        int numQuestion
    )


    {
        Test() : this(0, Subjects.Math, DateTime.Now, 0)
        {
        }
    }
    
}
