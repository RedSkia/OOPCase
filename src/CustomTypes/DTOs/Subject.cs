using System.Collections.Generic;

namespace src.CustomTypes.DTOs
{
    public sealed record Subject(in string name, in Teacher teacher, ICollection<Student> students);
}
