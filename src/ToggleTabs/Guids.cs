// Guids.cs
// MUST match guids.h
using System;

namespace Headspring.ToggleTabs
{
    static class GuidList
    {
        public const string guidToggleTabsPkgString = "5200adcf-be8d-4fbb-aa3a-e742a8482a48";
        public const string guidToggleTabsCmdSetString = "03d662ef-3961-40c4-a7ea-f9e1511d3bff";

        public static readonly Guid guidToggleTabsCmdSet = new Guid(guidToggleTabsCmdSetString);
    };
}