#region File Descrption

// /////////////////////////////////////////////////////////////////////////////
// 
// Project: VsSolution.Rename.VsSolution.Rename
// File:    OptionNameAttribute.cs
// 
// Create by Robert.L at 2012/10/20 9:46
// 
// /////////////////////////////////////////////////////////////////////////////

#endregion

using System;

namespace RobertLw.Tools.CommandLineParser
{
    public class ArgumentNameAttribute : Attribute
    {
        public char Name { get; set; }
        public string FullName { get; set; }

        public ArgumentNameAttribute()
        {
        }
    }
}