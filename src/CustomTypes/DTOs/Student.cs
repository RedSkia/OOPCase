
using System;

namespace src.CustomTypes.DTOs
{
    public sealed record Student : IPerson
    {
        public required string Name { get; init; }
        public required DateOnly Birthday { get; init; }
    }
}