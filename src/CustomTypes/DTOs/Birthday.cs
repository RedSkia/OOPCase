using System;

namespace src.CustomTypes.DTOs
{
    public interface IBirthday
    {
        DateOnly Date { get; init; }
        byte YearAge { get; }
    }
    public readonly struct Birthday : IBirthday
    {
        public required DateOnly Date { get; init; }
        public byte YearAge => (byte)((DateOnly.FromDateTime(DateTime.Today).Year - Date.Year) -
                                        (DateOnly.FromDateTime(DateTime.Today) < Date.AddYears(DateOnly.FromDateTime(DateTime.Today).Year - Date.Year) ? 1 : 0));
    }
}