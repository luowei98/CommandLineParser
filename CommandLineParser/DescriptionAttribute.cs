#region File Descrption

// /////////////////////////////////////////////////////////////////////////////
// 
// Project: VsSolution.Rename.VsSolution.Rename
// File:    DescriptionAttribute.cs
// 
// Create by Robert.L at 2012/10/19 18:02
// 
// /////////////////////////////////////////////////////////////////////////////

#endregion

using System;

namespace RobertLw.Tools.CommandLineParser
{
    public class DescriptionAttribute : Attribute
    {
        public string Text { get; set; }

        public DescriptionAttribute(string text)
        {
            Text = text;
        }
    }
}