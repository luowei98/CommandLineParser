#region File Descrption

// /////////////////////////////////////////////////////////////////////////////
// 
// Project: VsSolution.Rename.VsSolution.Rename
// File:    ParamaterParser.cs
// 
// Create by Robert.L at 2012/10/19 19:17
// 
// /////////////////////////////////////////////////////////////////////////////

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RobertLw.Tools.CommandLineParser
{
    public class ParamaterParser
    {
        public IEnumerable<string> ParamaterTexts { get; private set; }

        public ParamaterParser(IEnumerable<string> args)
        {
            var paraTexts = new List<string>();
            foreach (var a in args)
            {
                if (a.StartsWith("-"))
                {
                    paraTexts.Add(a);
                }
                else
                {
                    paraTexts[paraTexts.Count - 1] += " " + a;
                }
            }
            ParamaterTexts = paraTexts;
        }

        public Arguments Get()
        {
            var infos = typeof (Arguments).GetProperties();

            ParamaterValidation(infos);

            return CreateParamaters();
        }

        private void ParamaterValidation(IEnumerable<PropertyInfo> infos)
        {
            var reqInfosEnum = infos.Where(p => Attribute.IsDefined(p, typeof(RequiredAttribute)));
            var reqInfos = reqInfosEnum as PropertyInfo[] ?? reqInfosEnum.ToArray();

            // no required paramater
            if (!reqInfos.Any()) return;

            // missing required paramater
            if (!ParamaterTexts.Any())
            {
                var desc = GetParamatersDescription();
                if (!string.IsNullOrEmpty(desc))
                {
                    throw new ArgumentException(desc);
                }
                throw new Exception("Paramater required!");
            }

            // check required paramater
            foreach (var p in reqInfos)
            {
                var nameAttr = GetAttribute<ArgumentNameAttribute>(p);
                if (nameAttr == null ||
                    ParamaterTexts.Count(o => o.StartsWith("-" + nameAttr.Name) || o.StartsWith("--" + nameAttr.FullName)) == 1)
                    continue;
                var descAttr = GetAttribute<DescriptionAttribute>(p);
                if (descAttr != null)
                {
                    throw new ArgumentException(descAttr.Text); 
                }
                throw new Exception("Error paramater!");
            }
        }

        private Arguments CreateParamaters()
        {
            var paramaters = new Arguments();
            var infos = paramaters.GetType().GetProperties();
            foreach (var pi in infos)
            {
                if (!pi.CanWrite) continue;

                var nameAttr = GetAttribute<ArgumentNameAttribute>(pi);
                if (nameAttr == null) continue;

                var paraText = ParamaterTexts
                    .SingleOrDefault(pt => (nameAttr.Name != '\0' &&
                                            pt.StartsWith("-" + nameAttr.Name)) ||
                                           (nameAttr.FullName != null &&
                                            pt.StartsWith("--" + nameAttr.FullName)));

                if (pi.PropertyType == typeof(string))
                {
                    if (paraText != null)
                    {
                        pi.SetValue(paramaters, paraText.Substring(paraText.IndexOf(' ')), null);
                    }
                }
                else if (pi.PropertyType == typeof(bool))
                {
                    
                }
            }

            return paramaters;
        }

        private static T GetAttribute<T>(MemberInfo info) where T : Attribute
        {
            return Attribute.GetCustomAttribute(info, typeof (T)) as T;
        }

        private static string GetParamatersDescription()
        {
            var type = typeof (Arguments);
            if (Attribute.IsDefined(type, typeof(DescriptionAttribute)))
            {
                var descAttrs = type.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descAttrs.Any() && descAttrs[0] != null)
                {
                    return ((DescriptionAttribute)descAttrs[0]).Text;
                }
            }
            return "";
        }
    }
}