#region File Descrption
// /////////////////////////////////////////////////////////////////////////////
// 
// Project: VsSolution.Rename.VsSolution.Rename
// File:    OptionException.cs
// 
// Create by Robert.L at 2012/10/20 10:44
// 
// /////////////////////////////////////////////////////////////////////////////
#endregion

using System;

namespace RobertLw.Tools.CommandLineParser
{
    public class ArgumentException : ApplicationException
    {
        public ArgumentException(string message) : base(message)
        {
        }
    }
}