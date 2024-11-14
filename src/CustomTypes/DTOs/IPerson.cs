using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.CustomTypes.DTOs
{
    public interface IPerson
    {
        string Name { get; }
        IBirthday Birthday { get; }
    }
}
