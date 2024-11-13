using System.Collections.Generic;

namespace src.CustomTypes.DTOs
{
    public sealed record Subject(in string Name, in Teacher Teacher, ICollection<Student> Students);
}
