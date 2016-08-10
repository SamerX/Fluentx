using Fluentx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tester
{
    public class Fluentester
    {
        [Fact]
        public void Test_Conditional_1_If_Excuted()
        {
            bool result = false;

            Fx.If(() => { return true; }).Then(() => { result = true; }).Else(() => { result = false; });

            Assert.True(result);
        }
        [Fact]
        public void Test_Conditional_2_Else_Excuted()
        {
            bool result = false;
            Fx.If(() => { return false; }).Then(() => { result = true; }).Else(() => { result = false; });
            Assert.False(result);
        }

        [Fact]
        public void Test_Conditional_3_ElseIf_Excuted()
        {
            int result = 0;
            int expected = 3;
            Fx
                .If(() => { return expected == 997; }).Then(() => { result = 1; })
                .ElseIf(() => { return expected == 998; }).Then(() => { result = 2; })
                .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Conditional_4_And_Evaluated()
        {
            int result = 0;
            int expected = 1;
            Fx
                .If(() => { return true; }).And(() => { return true; }).Then(() => { result = 1; })
                .ElseIf(() => { return expected == 998; }).Then(() => { result = 2; })
                .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Conditional_5_OrNot_Evaluated()
        {
            int result = 0;
            int expected = 2;
            Fx
                .If(() => { return true; }).And(() => { return false; }).Then(() => { result = 1; })
                .ElseIf(() => { return expected == 998; }).OrNot(() => { return false; }).Then(() => { result = 2; })
                .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Conditional_6_Long_Nesting_Evaluated()
        {
            int result = 0;
            int expected = 1;
            Fx
                .If(() => { return true; }).And(() => { return true; }).Or(() => { return false; }).Xor(() => { return false; }).AndNot(() => { return false; }).Then(() => { result = 1; })
                .ElseIf(() => { return expected == 998; }).OrNot(() => { return false; }).Then(() => { result = 2; })
                .Else(() => result = 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Loop_1_WhileTrue()
        {
            int result = 0;

            Fx.WhileTrue(() =>
            {
                ++result;
                return result != 6;
            });
            Assert.Equal(result, 6);
        }

        [Fact]
        public void Test_Loop_2_WhileFalse()
        {
            int result = 0;

            Fx.WhileFalse(() =>
            {
                ++result;
                return result == 6;
            });
            Assert.Equal(result, 6);
        }

        [Fact]
        public void Test_Loop_3_While_Do()
        {
            int result = 0;

            Fx.While(() => { return result < 6; }).Do(() => { ++result; });

            Assert.Equal(result, 6);
        }

        [Fact]
        public void Test_Loop_4_While_Do_Early_Break()
        {
            int result = 0;
            int conditionEvaluationCount = 0;
            Fx.While(() => { ++conditionEvaluationCount; return result < 6; }).EarlyBreakOn(() => { return result == 4; }).Do(() =>
            {
                ++result;
            });

            Assert.Equal(conditionEvaluationCount, 5);
        }

        [Fact]
        public void Test_Loop_5_While_Do_Late_Break()
        {
            int result = 0;
            int conditionEvaluationCount = 0;

            Fx.While(() => { ++conditionEvaluationCount; return result < 6; }).LateBreakOn(() => { return result == 4; }).Do(() =>
            {
                ++result;
            });

            Assert.Equal(conditionEvaluationCount, 4);
        }

        [Fact]
        public void Test_Loop_6_Do_While()
        {
            int result = 0;

            Fx.Do(() =>
            {
                ++result;
            }).While(() => { return result < 5; });

            Assert.Equal(result, 5);
        }

        [Fact]
        public void Test_Try_1_Catch_NoException()
        {
            bool exceptionOccured = false;

            Fx.Try(() => { }).Catch<Exception>(ex => { exceptionOccured = true; });

            Assert.False(exceptionOccured);
        }

        [Fact]
        public void Test_Try_2_Catch_CatchExcuted()
        {
            bool exceptionOccured = false;

            Fx.Try(() => { throw new NotImplementedException(); }).Catch<NotImplementedException>(ex => { exceptionOccured = true; });

            Assert.True(exceptionOccured);
        }

        [Fact]
        public void Test_Try_4_Catch_CatchExcuted_ByCorrectOrder()
        {
            bool exceptionOccured = false;

            Fx.Try(() => { throw new NotImplementedException(); }).Catch<NotImplementedException, Exception>(ex1 => { exceptionOccured = true; }, ex2 => { });

            Assert.True(exceptionOccured);
        }

        [Fact]
        public void Test_Try_5_SwallowIf()
        {
            int result = 0;
            Fx.Try(() =>
            {
                throw new NotImplementedException();
                //++result;
            }).SwallowIf<NotImplementedException>();

            Assert.Equal(result, 0);
        }

        [Fact]
        public void Test_ForEach()
        {
            int result = 0;
            Fx.ForEach(new List<int>() { 1, 2, 3, 4 }, current => { result += current; });

            Assert.Equal(result, 10);
        }

        [Fact]
        public void Test_Is_WithSafeNull()
        {
            PrivateDisposableTestEntity entity = null;
            bool isIDMatched = false;
            if (Fx.Is(() => entity.Id == 900))
            {

                isIDMatched = true;
            }

            Assert.False(isIDMatched);
        }

        [Fact]
        public void Test_Switch_1_Types_NormalCase()
        {
            int result = -1;

            Fx.Switch<string>()
               .Case<int>().Execute(() => { result = 1; })
               .Case<string>().Execute(() => { result = 2; })
               .Default(() => { result = 0; });

            Assert.Equal(result, 2);
        }

        [Fact]
        public void Test_Switch_2_Types_Default()
        {
            int result = -1;
            Fx
               .Switch<short>()
               .Case<int>().Execute(() => { result = 1; })
               .Case<string>().Execute(() => { result = 2; })
               .Default(() => { result = 0; });

            Assert.Equal(result, 0);
        }

        [Fact]
        public void Test_Switch_3_Instances_NormalCase()
        {
            string condition = "two";
            int result = -1;

            Fx.Switch(condition)
               .Case("one").Execute(() => { result = 1; })
               .Case("two").Execute(() => { result = 2; })
               .Default(() => { result = 0; });

            Assert.Equal(result, 2);
        }

        [Fact]
        public void Test_Switch_4_Instances_Default()
        {
            string condition = "three";
            int result = -1;
            Fx
               .Switch(condition)
               .Case("one").Execute(() => { result = 1; })
               .Case("two").Execute(() => { result = 2; })
               .Default(() => { result = 0; });

            Assert.Equal(result, 0);
        }

        [Fact]
        public void Test_RetryOnFail()
        {
            int result = 0;

            Fx.RetryOnFail(() =>
            {
                ++result;
                return result > 5;
            }, attemptSleepInMilliSeconds: 10);

            Assert.Equal(result, 3);
        }

        static bool isDisposed = false;
        [Fact]
        public void Test_Using()
        {
            Fx.Using(new PrivateDisposableTestEntity(), (instance) => { });
            Assert.True(isDisposed);
        }
        [Fact]
        public void Test_IsNull()
        {
            int x = 5;
            Assert.True(!x.IsNull());
        }

        [Fact]
        public void Test_IsNotNull()
        {
            int x = 5;
            Assert.True(x.IsNotNull());
        }

        [Fact]
        public void Test_ToInt32()
        {
            var result = "123".ToInt32();
            Assert.Equal(result, 123);
        }

        [Fact]
        public void Test_In()
        {
            string parameter = null;
            var result = parameter.In(new string[] { "one", null, "three" });

            Assert.True(result);
        }
        [Fact]
        public void Test_NotIn()
        {
            string parameter = "five";
            var result = parameter.NotIn(new string[] { "one", null, "three" });

            Assert.True(result);
        }

        [Fact]
        public void Test_In_ValueType()
        {
            int parameter = 5;
            var result = parameter.In(5, 3, 4);

            Assert.True(result);
        }

        [Fact]
        public void Test_IgnorCaseEqual()
        {
            Assert.True("Fluentx".IgnoreCaseEqual("fLuEnTx"));
        }

        [Fact]
        public void Test_CollectionIsNullOrEmpty()
        {
            string[] list = new string[] { "a", "b", "c" };
            var y = PredicateBuilder.False<string>().Or(x => x == "a").Or(x => x == "A");
            Assert.False(list.IsNullOrEmpty());
        }

        [Fact]
        public void Test_Lock()
        {
            string value = "Fluentx";
            value.Lock(x => { });
        }
        [Fact]
        public void Test_ReplaceFirst()
        {
            string value = "IdentityIdx";
            Assert.Equal("entityIdx", value.ReplaceFirst("Id", ""));
        }
        [Fact]
        public void Test_ReplaceLast()
        {
            string value = "IdentityId";
            Assert.Equal("Identity", value.ReplaceLast("Id", ""));
        }

        [Fact]
        public void Test_CountOccurences()
        {
            string value = "samer";
            Assert.Equal(1, value.CountOccurences("s"));
        }

        [Fact]
        public void Test_Guard_Against()
        {
            try
            {
                Guard.Against<NotImplementedException>(false);
                Assert.Equal(true, true);
            }
            catch (NotImplementedException)
            {
                Assert.Equal(false, true);
            }
        }

        [Fact]
        public void Test_Foreach_WithIndex()
        {
            List<string> data = new List<string>()
            {
                "A", "B", "C", "D"
            };

            var counter = 0;
            data.ForEach((item, index) =>
            {
                counter += 1;
                var y = index;
            });
        }

        [Fact]
        public void Test_And_Specification()
        {
            ISpecification<int> rule1 = new ExpressionSpecification<int>(x => x == 1, "rule1 failed");
            ISpecification<int> rule2 = new ExpressionSpecification<int>(x => x == 2, "rule2 failed");
            ISpecification<int> rule3 = new ExpressionSpecification<int>(x => x == 3, "rule3 failed");
            ISpecification<int> rule4 = rule1.Or(rule2).Or(rule3);

            var result = rule4.ValidateWithMessages(4);

            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Test_Safe()
        {
            PrivateDisposableTestEntity entity = null;
            var result = entity.Safe(x => x.Id);
            Assert.True(result == 0);
        }

        [Fact]
        public void Test_Or_Specification()
        {

        }

        [Fact]
        public void Test_Not_Specification()
        {

        }

        [Fact]
        public void Test_Expression_Specification()
        {
            var x = Fx.RandomString(3);
        }
        [Fact]
        public void Test_Shuffle()
        {
            var data = new string[] { "one", "two", "three", "four", "five", "sex", "seven" };
            var shuffledData = data.Shuffle();
            var x = shuffledData;
        }
        [Fact]
        public void Test_ToCSV()
        {
            string[] strings = new string[] { "one", "two" };
            DateTime[] dates = new DateTime[] { DateTime.Now, DateTime.Now.AddYears(3) };
            string value = strings.ToCSV();
            dates.ToCSV();
        }

        [Fact]
        public void Test_MinBy()
        {
            DateTime[] dates = new DateTime[] { DateTime.Now, DateTime.Now.AddYears(3) };
            var min = dates.MinBy(x => x.Year);
            Assert.Equal(2014, min.Year);
        }

        [Fact]
        public void Test_MaxBy()
        {
            DateTime[] dates = new DateTime[] { DateTime.Now, DateTime.Now.AddYears(3) };
            var max = dates.MaxBy(x => x.Year);
            Assert.Equal(2017, max.Year);
        }
        [Fact]
        public void Test_EndOfDay()
        {
            var eod = DateTime.Now.EndOfDay();
            Assert.Equal(true, true);
        }

        [Fact]
        public void Test_StartOfDay()
        {
            var date = DateTime.Now.StartOfDay();
            Assert.Equal(true, true);
        }
        [Fact]
        public void Test_NextDay()
        {
            var date = DateTime.Now.NextDay().EndOfDay();
            Assert.Equal(true, true);
        }
        [Fact]
        public void Test_Mapper()
        {
            DateTime.Now.At(5, 30);
            var one = new One()
            {
                X1 = "test value",
                X2 = 77,
                X3 = new Two()
                {
                    X11 = new Three()
                    {
                        X21 = 99
                    },
                    X12 = new List<Three>()
                    {
                        new Three(){ X21 = 105 }
                    }
                },
                X4 = new List<Three>()
                {
                    new Three(){X21=203}
                },
                X5 = new Three[]
                {
                    new Three(){X21=204}, null
                },
                X6 = new Collection<Three>()
                {
                    new Three(){X21=205}
                },
                X7 = new List<Three>()
                {
                    new Three(){X21=206}
                },
                X8 = new Three[]
                {
                    new Three(){X21=207}
                },
                X9 = new Collection<Three>()
                {
                    new Three(){X21=208}
                },
                X10 = new Two()
                {
                    X11 = new Three() { X21 = 301 },
                    X12 = new Collection<Three>() { new Three() { X21 = 401 } }
                }
            };

            var mapper = new Mapper<One, VMOne>().UseMapper<Two, VMTwo>().UseMapper<Three, VMThree>();
            var vmOne = mapper.Map(one);
        }

        [Fact]
        public void Test_IoC()
        {
            IoC.AutoRegisterByInterfaces(new Type[] { typeof(IOne) });
            IoC.AutoRegisterByInterfaces(new Type[] { typeof(IOne) });
            //IoC.AutoRegisterByClasses(new Type[] { typeof(One) });
        }
        public interface IOne { }
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
        public class PrivateDisposableTestEntity : IDisposable
        {
            public int Id { get; set; }
            public PrivateDisposableTestEntity()
            {

            }
            public void Dispose()
            {
                isDisposed = true;
            }

        }

        public interface IOneBusinessRules
        {
            ISpecification<One> FirstRule { get; }
            ISpecification<One> SecondRule { get; }

        }
        public class OneBusinessRules : IOneBusinessRules
        {
            public ISpecification<One> FirstRule
            {
                get
                {
                    return new ExpressionSpecification<One>(x =>
                    {
                        return true;
                    });
                }
            }

            public ISpecification<One> SecondRule
            {
                get
                {
                    return new ExpressionSpecification<One>(x =>
                    {
                        return true;
                    });
                }
            }
        }


    }


}
