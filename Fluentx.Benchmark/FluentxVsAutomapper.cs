using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Fluentx.Benchmark
{
    public class FluentxVsAutomapper
    {
        private const int N = 10;

        private IList<One> source = new List<One>();
        private One source1 = new One();

        private Fluentx.Mapper<One, VMOne> fluentxMapper = new Mapper<One, VMOne>();
        public FluentxVsAutomapper()
        {
            source1 = new One()
            {
                X1 = "Samer"
            };
            //for (int i = 0; i < N; i++)
            //{
            //    var item = new One()
            //    {
            //        X1 = "X" + i,
            //        X2 = i,
            //    };
            //    source.Add(item);
            //}

            AutoMapper.Mapper.Initialize(x =>
            {
                x.CreateMap<One, VMOne>();
            });

        }
        [Benchmark]
        public VMOne FluentxMapper1()
        {
            return fluentxMapper.Map(source1);
        }
        [Benchmark]
        public VMOne Automapper1()
        {
            return AutoMapper.Mapper.Map<One, VMOne>(source1);
        }
        //[Benchmark]
        //public IList<VMOne> FluentxMapper()
        //{
        //    return fluentxMapper.Map(source);
        //}
        //[Benchmark]
        //public IList<VMOne> Automapper()
        //{
        //    return AutoMapper.Mapper.Map<IList<One>, IList<VMOne>>(source);
        //}
    }

    public class One
    {
        public string X1 { get; set; }
        public int X2 { get; set; }
        public Two X3 { get; set; }
        public IList<Three> X4 { get; set; }
        public Three[] X5 { get; set; }
        public ICollection<Three> X6 { get; set; }
        public IList<Three> X7 { get; set; }
        public Three[] X8 { get; set; }
        public ICollection<Three> X9 { get; set; }
        public Two X10 { get; set; }
    }

    public class VMOne
    {
        public string X1 { get; set; }
        public int X2 { get; set; }
        public Two X3 { get; set; }
        public Three[] X4 { get; set; }
        public ICollection<Three> X5 { get; set; }
        public IList<Three> X6 { get; set; }
        public VMThree[] X7 { get; set; }
        public ICollection<VMThree> X8 { get; set; }
        public IList<VMThree> X9 { get; set; }
        public VMTwo X10 { get; set; }
    }
    public class Two
    {
        public Three X11 { get; set; }
        public IList<Three> X12 { get; set; }
    }

    public class VMTwo
    {
        public VMThree X11 { get; set; }
        public IList<VMThree> X12 { get; set; }
    }
    public class Three
    {
        public int X21 { get; set; }
    }

    public class VMThree
    {
        public int X21 { get; set; }
    }
}
