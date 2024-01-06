// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Organize;

namespace Print
{
    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            Closet closet = new();
            Console.WriteLine(closet.StorageBin.ToString());
        }
    }
}
