using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Fluentx.Tester
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
        public void Test_Celius()
        {
            var result = 90.ToCelcius();
        }

        [Fact]
        public void Test_KGtoLBS()
        {
            var result = 6.ToKG();

            var backResult = result.ToLBS();
        }

        [Fact]
        public void Test_MeterToFeet()
        {
            var result = 6.ToMeter();

            var backResult = result.ToFeet();
        }

        [Fact]
        public void Test_COMBGuid()
        {
            var g1 = Fx.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);
            var g2 = Fx.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);
            var g3 = Fx.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);

            var g21 = BitConverter.ToString(Fx.NewSequentialGuid(SequentialGuidType.SequentialAsBinary).ToByteArray());
            var g31 = BitConverter.ToString(Fx.NewSequentialGuid(SequentialGuidType.SequentialAsBinary).ToByteArray());
            var g11 = BitConverter.ToString(Fx.NewSequentialGuid(SequentialGuidType.SequentialAsBinary).ToByteArray());

            var g111 = Fx.NewSequentialGuid(SequentialGuidType.SequentialAsString);
            var g211 = Fx.NewSequentialGuid(SequentialGuidType.SequentialAsString);
            var g311 = Fx.NewSequentialGuid(SequentialGuidType.SequentialAsString);

            var str = $"\n{g1} \n{g2} \n{g3} \n\n{g11} \n{g21} \n{g31} \n\n{g111} \n{g211} \n{g311}";


        }

        [Fact]
        public void Test_Age()
        {
            var age = 27.May(1983).Age();
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
        public void Test_RandomRange()
        {
            var list = new List<One>()
            {
                new One(){X2 = 10},
                new One(){X2 = 20},
                new One(){X2 = 30},
                new One(){X2 = 40}
            };

            var randomList = list.RandomRange();
        }

        [Fact]
        public void Test_InsertionSort()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.InsertionSort();
            Assert.True(list.IsSorted());

        }
        [Fact]
        public void Test_InsertionSortDescending()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.InsertionSortDescending();
            Assert.True(list.IsSortedDescending());
        }

        [Fact]
        public void Test_ShellSort()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.ShellSort();
            Assert.True(list.IsSorted());
        }

        [Fact]
        public void Test_ShellSortDescending()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.ShellSortDescending();
            Assert.True(list.IsSortedDescending());
        }

        [Fact]
        public void Test_SelectionSort()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.SelectionSort();
            Assert.True(list.IsSorted());
        }

        [Fact]
        public void Test_SelectionSortDescending()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.SelectionSortDescending();
            Assert.True(list.IsSortedDescending());
        }

        [Fact]
        public void Test_GnomeSort()
        {
            var list = new List<string>() { "c", "d", "a", "g", "x", "c" };
            list.GnomeSort();
            Assert.True(list.IsSorted());
        }

        [Fact]
        public void Test_GnomeSortDescending()
        {
            var list = new List<string>() { "c", "d", "a", "g", "x", "c" };
            list.GnomeSortDescending();
            Assert.True(list.IsSortedDescending());
        }

        [Fact]
        public void Test_QuickSort()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.QuickSort();
            Assert.True(list.IsSorted());
        }

        [Fact]
        public void Test_QuickSortDescending()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.QuickSortDescending();
            Assert.True(list.IsSortedDescending());
        }
        [Fact]
        public void Test_CocktailSort()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.CocktailSort();
            Assert.True(list.IsSorted());

        }
        [Fact]
        public void Test_CocktailSortDescending()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.CocktailSortDescending();
            Assert.True(list.IsSortedDescending());
        }
        [Fact]
        public void Test_BubbleSort()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.BubbleSort();
            Assert.True(list.IsSorted());
        }
        [Fact]
        public void Test_BubbleSortDescending()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.BubbleSortDescending();
            Assert.True(list.IsSortedDescending());
        }

        [Fact]
        public void Test_HeapSort()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.HeapSort();
            Assert.True(list.IsSorted());
        }

        [Fact]
        public void Test_BogoSort()
        {
            var data = new string[] { "four", "two", "three", "one", "five", "sex", "seven" };
            int count = data.BogoSort();
            Assert.True(data.IsSorted());
        }

        [Fact]
        public void Test_IntroSort()
        {
            var data = new string[] { "four", "two", "three", "one", "five", "sex", "seven" };
            data.IntroSort();
            Assert.True(data.IsSorted());
        }

        [Fact]
        public void Test_IntroSortDescending()
        {
            var data = new string[] { "four", "two", "three", "one", "five", "sex", "seven" };
            data.IntroSortDescending();
            Assert.True(data.IsSortedDescending());
        }
        //[Fact]
        //public void Test_RadixSort()
        //{
        //    var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
        //    list.RadixSort();
        //    Assert.True(list.IsSorted());
        //}

        [Fact]
        public void Test_BogoSortDescending()
        {
            var data = new string[] { "four", "two", "three", "one", "five", "sex", "seven" };
            int count = data.BogoSortDescending();
            Assert.True(data.IsSortedDescending());
        }
        //[Fact]
        //public void Test_HeapSortDescending()
        //{
        //    var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
        //    list.HeapSortDescending();
        //    Assert.True(list.IsSortedDescending());
        //}
        [Fact]
        public void Test_MergeSort()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.MergeSort();
            Assert.True(list.IsSorted());
        }
        [Fact]
        public void Test_MergeSortDescending()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.MergeSortDescending();
            Assert.True(list.IsSortedDescending());
        }

        [Fact]
        public void Test_OddEvenSort()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.OddEvenSort();
            Assert.True(list.IsSorted());

        }
        [Fact]
        public void Test_OddEvenSortDescending()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.OddEvenSortDescending();
            Assert.True(list.IsSortedDescending());

        }
        [Fact]
        public void Test_CombSort()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.CombSort();
            Assert.True(list.IsSorted());
        }
        [Fact]
        public void Test_CombSortDescending()
        {
            var list = new List<int>() { 4, 5, 1, 9, 2, 1, 7, 9, 64, 3 };
            list.CombSortDescending();
            Assert.True(list.IsSortedDescending());
        }
        [Fact]
        public void Test_BinarySearch()
        {
            var list = new List<string>() { "samer", "ahmad", "ali", "gada", "xerox", "chair" };
            //list.QuickSort();
            list.BinarySearchFx("xerox");
        }

        [Fact]
        public void Test_Resverse()
        {
            IList<string> list = new List<string>() { "samer", "ahmad", "xerox", "ali", "MID", "gada", "xerox", "chair" };
            //list.QuickSort();
            list.Reverse();
            Assert.True(list[0] == "chair");
        }

        [Fact]
        public void Test_Between()
        {
            Assert.False(1.Between(2, 7));
            Assert.False(2.Between(2, 7));
            Assert.False(7.Between(2, 7));
            Assert.True(5.Between(2, 7));

            Assert.False(1.BetweenIncludeEdges(2, 7));
            Assert.True(2.BetweenIncludeEdges(2, 7));
            Assert.True(7.BetweenIncludeEdges(2, 7));
            Assert.True(5.BetweenIncludeEdges(2, 7));
            Assert.False(1.BetweenRegardless(7, 2));
            Assert.False(2.BetweenRegardless(7, 2));
            Assert.False(7.BetweenRegardless(7, 2));
            Assert.True(5.BetweenRegardless(7, 2));
            Assert.False(1.BetweenRegardlessIncludeEdges(7, 2));
            Assert.True(2.BetweenRegardlessIncludeEdges(7, 2));
            Assert.True(7.BetweenRegardlessIncludeEdges(7, 2));
            Assert.True(5.BetweenRegardlessIncludeEdges(7, 2));
        }

        [Fact]
        public void Test_BetweenRegardless()
        {
            var list = new List<One>()
            {
                new One(){X2 = 10},
                new One(){X2 = 20},
                new One(){X2 = 30},
                new One(){X2 = 40}
            };

            {
                var r1 = 1.BetweenRegardless(7, 2);
                var r2 = 2.BetweenRegardless(7, 2);
                var r3 = 7.BetweenRegardless(7, 2);
                var r4 = 5.BetweenRegardless(7, 2);
            }

            {
                var r1 = 1.BetweenIncludeEdges(2, 7);
                var r2 = 2.BetweenIncludeEdges(2, 7);
                var r3 = 7.BetweenIncludeEdges(2, 7);
                var r4 = 5.BetweenIncludeEdges(2, 7);
            }
        }

        //[Fact]
        //public void Test_And_Specification()
        //{
        //    ISpecification<int> rule1 = new ExpressionSpecification<int>(x => x == 1, "rule1 failed");
        //    ISpecification<int> rule2 = new ExpressionSpecification<int>(x => x == 2, "rule2 failed");
        //    ISpecification<int> rule3 = new ExpressionSpecification<int>(x => x == 3, "rule3 failed");
        //    ISpecification<int> rule4 = rule1.Or(rule2).Or(rule3);

        //    var result = rule4.ValidateWithMessages(4);

        //    Assert.True(result.Count() > 0);
        //}

        [Fact]
        public void Test_Safe()
        {
            PrivateDisposableTestEntity entity = null;
            var result = entity.Safe(x => x.Id);
            Assert.True(result == 0);


        }

        [Fact]
        public void Test_PredicateBuilder()
        {
            var test = PredicateBuilder.True<int>();
            test = test.AndNot(x => x > 1);
            var array = new List<int> { 1, 2, 3 };
            var result = array.Where(test.Compile()).ToList();
        }
        [Fact]
        public void Test_And_Specification()
        {
            var spec1 = new ExpressionSpecification<int>((x) =>
            {
                return x > 10;
            }
            , new string[] { "less than 10" });
            var spec2 = new ExpressionSpecification<int>((x) =>
            {
                return x > 15;
            }
            , new string[] { "less than 15" });
            var result = spec1.And(spec2).ValidateWithMessagesAndContinue(22);
        }

        [Fact]
        public void Test_Or_Specification()
        {
            var spec1 = new ExpressionSpecification<int>((x) =>
            {
                return x > 10;
            }
            , new string[] { "less than 10" });
            var spec2 = new ExpressionSpecification<int>((x) =>
            {
                return x > 15;
            }
            , new string[] { "less than 15" });
            var result = spec1.Or(spec2).ValidateWithMessages(12);
        }

        [Fact]
        public void Test_Xor_Specification()
        {
            var spec1 = new ExpressionSpecification<int>((x) =>
            {
                return x > 10;
            }
            , new string[] { "less than 10" });
            var spec2 = new ExpressionSpecification<int>((x) =>
            {
                return x > 15;
            }
            , new string[] { "less than 15" });
            var result = spec1.Xor(spec2).ValidateWithMessagesAndContinue(12);
        }

        [Fact]
        public void Test_Specification_PlayGround()
        {
            var spec1 = new ExpressionSpecification<int>((x) =>
            {
                return x > 10;
            }
            , new string[] { "less than 10" });
            var spec2 = new ExpressionSpecification<int>((x) =>
            {
                return x > 25;
            }
            , new string[] { "less than 25" });

            var spec3 = new ExpressionSpecification<int>((x) =>
            {
                return x > 15;
            }
            , new string[] { "less than 15" });
            var res = spec1 & spec2;
            var result = spec1.And(spec2).And(spec3).ValidateWithMessages(22);
        }

        [Fact]
        public void Test_Enumclass()
        {
            var result = EOne.List();
            //var x = EOne.Parse("First");

            var combined = EOne.one | EOne.two;
            var backward = combined & EOne.one;

        }
        //[Fact]
        //public void Test_Not_Specification()
        //{
        //    var spec1 = new ExpressionSpecification<int>((x) =>
        //    {
        //        return x > 10;
        //    }
        //    , new string[] { "less than 10" });
        //    var result = spec1.Not().ValidateWithMessagesAndContinue(12);
        //}

        [Fact]
        public void Test_Expression_Specification()
        {
            var x = Fx.RandomString(3);
        }
        [Fact]
        public void Test_Shuffle()
        {
            var data = new string[] { "one", "two", "three", "four", "five", "sex", "seven" };
            data.Shuffle();
        }
        [Fact]
        public void Test_ToCSV()
        {
            string[] strings = new string[] { "one", "two" };
            DateTime[] dates = new DateTime[] { DateTime.Now, DateTime.Now.AddYears(3) };
            string value = strings.ToCSV();
            dates.ToCSV();
            Assert.Equal("one,two", value);
        }

        [Fact]
        public void Test_MinBy()
        {
            DateTime[] dates = new DateTime[] { 27.May(2017), 27.May(2017).AddYears(3) };
            var min = dates.MinBy(x => x.Year);
            Assert.Equal(2017, min.Year);

            27.May(1983);
            new DateTime(1983, 5, 27);
        }

        [Fact]
        public void Test_MaxBy()
        {
            DateTime[] dates = new DateTime[] { 27.May(2017), 27.May(2017).AddYears(3) };
            var max = dates.MaxBy(x => x.Year);
            Assert.Equal(2020, max.Year);
        }
        [Fact]
        public void Test_EndOfDay()
        {
            var eod = DateTime.Now.EndOfDay();
            Assert.Equal(59, eod.Second);
        }

        [Fact]
        public void Test_StartOfDay()
        {
            var date = DateTime.Now.StartOfDay();
            Assert.Equal(0, date.Second);
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

            var mapper = new Mapper<One, VMOne>()
                .UseMappers(new Mapper<Two, VMTwo>().UseMapper<Three, VMThree>())
                .UseMapper<Three, VMThree>();
            var vmOne = mapper.Map(one);
        }

        [Fact]
        public void Test_Mapper_Collection()
        {
            var source = new List<One>()
            {
                new One(){X1 = "X1"},
                new One(){X1 = "X1_1"}
            };

            var mapper = new Mapper<One, VMOne>();
            var dest = mapper.Map(source);
        }

        [Fact]
        public void Test_Lowest_Sequence()
        {
            IList<int> list = null; // new List<int> { 10, 1, 8, 2, 7 };
            //list.QuickSort();

            int min = 1;
            int max = 10;

            //var value = list.FindAllMissing(-5, 10);

        }

        [Fact]
        public void Test_IoC()
        {
            IoC.AutoRegisterByInterfaces(new Type[] { typeof(IOne) });
            IoC.AutoRegisterByInterfaces(new Type[] { typeof(IOne) });
            //IoC.AutoRegisterByClasses(new Type[] { typeof(One) });
            
        }

        [Fact]
        public void Test_SingletonGeneration()
        {
            var text = Fx.GenerateSingletonClass("Test", SingletonType.ThreadSafeFullLazy);
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

        public class EOne : Enumclass<EOne>
        {
            public EOne(int value) : base(value)
            {

            }

            public static EOne one = new EOne(1);
            public static EOne two = new EOne(2);
            public static EOne four = new EOne(4);
            public static EOne eight = new EOne(8);
        }

    }
}
