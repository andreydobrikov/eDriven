﻿namespace eDriven.Audio
{
    public sealed class Info
    {
        public const string AssemblyName = "eDriven.Audio";
        public const string AssemblyVersion = "1.8.0";
        public const string Author = "Danko Kozar";
        public const string Copyright = "Copyright (c) Danko Kozar 2010-2013";
        public const string Web = "edriven.dankokozar.com";

        public override string ToString()
        {
            return string.Format(@"[{0} {1}]
[by {2}, (C) {3}, {4}]", AssemblyName, AssemblyVersion, Author, Copyright, Web);
        }
    }
}