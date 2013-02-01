using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelChina.AF.Util.TestUtil;

namespace TelChina.Lecture.Perfomance.Test
{
    [TestClass]
    public class DealWithStringTest
    {
        [TestMethod]
        public void StringConcatTest()
        {
            CodeTimer.Initialize();

            for (int i = 2; i <= 1024; i *= 2)
            {
                CodeTimer.Time(
                    String.Format("Normal Concat ({0})", i),
                    10000,
                    () => DealWithString.NormalConcat(i));

                CodeTimer.Time(
                    String.Format("StringBuilder ({0})", i),
                    10000,
                    () => DealWithString.StringBuilder(i));
                CodeTimer.Time(
                    String.Format("String.Concat ({0})", i),
                    10000,
                    () => DealWithString.StringConcat(i));
            }
        }
        [TestMethod]
        public void StringFormatTest()
        {
            CodeTimer.Initialize();
            var i = 1024;
            CodeTimer.Time(
                    String.Format("StringFormat ({0})", i),
                    10000,
                    () => DealWithString.StringFormat(i));

            CodeTimer.Time(
                String.Format("StringBuilderFormat ({0})", i),
                10000,
                () => DealWithString.StringBuilderFormat(i));
        }

      
    }
}
