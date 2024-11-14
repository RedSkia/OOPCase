using System;

namespace src.CustomTypes.DTOs
{
    public sealed record Teacher : IPerson
    {
        public required string Name { get; init; }
        public required IBirthday Birthday { get; init; }
    }
}
