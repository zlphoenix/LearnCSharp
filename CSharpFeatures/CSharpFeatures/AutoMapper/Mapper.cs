using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace CSharpFeatures.AutoMapper
{
    public class Src
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int Index { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }

    public class Dest
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int Index { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
    public class Mapping
    {
        public static void DoMapping()
        {
            //AutoMapperConfiguration.Configure();

            Mapper.AssertConfigurationIsValid();

        }
    }

}
