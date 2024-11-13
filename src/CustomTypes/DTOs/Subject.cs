using System.Collections.Generic;
using System.Resources;

namespace src.CustomTypes.DTOs
{
    public interface ISubject
    {
        string Name { get; init; }
        Teacher Teacher { get; init; }
        ICollection<IPerson> Students { get; init; }
    }
    public sealed record Subject : ISubject
    {
        public required string Name { get; init; }
        public required Teacher Teacher { get; init; }
        public required ICollection<IPerson> Students { get; init; }
    }
}
