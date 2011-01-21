using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using UrlBuilder;

namespace Tests
{
    [TestFixture]
    public class When_getting_parameters_from_a_method_call
    {
        [Test]
        public void it_does_nothing_if_its_not_a_method_call()
        {
            Expression<Func<int>> expression = () => 3;
            expression.ForEachParameter((_, __) => Assert.Fail());
        }

        [Test]
        public void it_does_nothing_if_there_are_no_parameters()
        {
            Expression<Func<TestClass, int>> expression = c => c.Foo();
            expression.ForEachParameter((_, __) => Assert.Fail());
        }

        [Test]
        public void it_calls_the_delegate_once_if_there_is_one_argument()
        {
            Expression<Func<TestClass, int>> expression = c => c.Bar(1);
            var callCount = 0;
            expression.ForEachParameter((_, __) => callCount++);
            Assert.That(callCount, Is.EqualTo(1));
        }

        [Test]
        public void it_passes_the_argument_expression_for_the_argument()
        {
            Expression<Func<TestClass, int>> expression = c => c.Baz(1, "abc");
            var argValues = new List<object>();
            expression.ForEachParameter((_, argExpr) => argValues.Add(argExpr));
            Assert.That(argValues[0], Is.EqualTo(1));
            Assert.That(argValues[1], Is.EqualTo("abc"));
        }

        [Test]
        public void it_passes_the_parameter_info_for_the_argument()
        {
            Expression<Func<TestClass, int>> expression = c => c.Baz(1, "abc");
            var paramInfos = new List<ParameterInfo>();
            expression.ForEachParameter((paramInfo, _) => paramInfos.Add(paramInfo));
            Assert.That(paramInfos[0].ToString(), Is.EqualTo("Int32 i"));
            Assert.That(paramInfos[1].ToString(), Is.EqualTo("System.String j"));
        }

        [Test]
        public void it_works_with_closures()
        {
            var v = 34;
            Expression<Func<TestClass, int>> expression = c => c.Bar(v);
            ParameterInfo paramInfo = null;
            object argValue = null;
            expression.ForEachParameter((p, e) =>
                                        {
                                            paramInfo = p;
                                            argValue = e;
                                        });
            Assert.That(paramInfo.ToString(), Is.EqualTo("Int32 i"));
            Assert.That(argValue, Is.EqualTo(34));
        }
    }

    public class TestClass
    {
        public int Foo()
        {
            return 0;
        }

        public int Bar(int i)
        {
            return 0;
        }

        public int Baz(int i, string j)
        {
            return 0;
        }
    }
}
