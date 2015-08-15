using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ArcEasyScriptTest
{
    [TestClass]
    public class ScriptTest
    {
        [TestMethod]
        public void mustIdentifyAScriptPattern()
        {

            string[] scripts = {"connect to neosde@GSEProj.sde",
                                "add column NU_PROJECT(LongInteger) to NEOSDE.TB_PROJECT",
                                "AdD column DE_NAME(Text,50) to NEOSDE.TB_PROJECT",
                                "create GeometricNetwork",
                                "rav NEOSDE.TB_PROJECT"};

            string[] strPatterns = { @"^connect", 
                                     @"^add",
                                     @"^create",
                                     @"^rav"};

            List<Regex> _ScriptPatterns = new List<Regex>();
            foreach (var strPattern in strPatterns)
            {
                _ScriptPatterns.Add(new Regex(strPattern, RegexOptions.IgnoreCase));
            }

            List<string> _ErrorPattern = new List<string>();
            List<string> _SuccessPattern = new List<string>();

            _ScriptPatterns.ForEach(_regEx =>
            {
                foreach (var _script in scripts)
                {
                    if (_regEx.IsMatch(_script))
                    {
                        _SuccessPattern.Add(_script);
                    }
                }
            });

            _SuccessPattern.ForEach(x => { System.Console.WriteLine(x); });

            for (uint cont = 0; cont < scripts.Length; cont++)
            {
                Assert.IsTrue(_SuccessPattern.Contains(scripts[cont]));
            }
        }
    }
}
