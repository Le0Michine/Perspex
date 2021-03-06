﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Reactive.Linq;
using Perspex.Markup.Data;
using Xunit;

namespace Perspex.Markup.UnitTests.Data
{
    public class ExpressionObserverTests_Negation
    {
        [Fact]
        public async void Should_Negate_Boolean_Value()
        {
            var data = new { Foo = true };
            var target = new ExpressionObserver(data, "!Foo");
            var result = await target.Take(1);

            Assert.Equal(false, result);
        }

        [Fact]
        public async void Should_Negate_0()
        {
            var data = new { Foo = 0 };
            var target = new ExpressionObserver(data, "!Foo");
            var result = await target.Take(1);

            Assert.Equal(true, result);
        }

        [Fact]
        public async void Should_Negate_1()
        {
            var data = new { Foo = 1 };
            var target = new ExpressionObserver(data, "!Foo");
            var result = await target.Take(1);

            Assert.Equal(false, result);
        }

        [Fact]
        public async void Should_Negate_False_String()
        {
            var data = new { Foo = "false" };
            var target = new ExpressionObserver(data, "!Foo");
            var result = await target.Take(1);

            Assert.Equal(true, result);
        }

        [Fact]
        public async void Should_Negate_True_String()
        {
            var data = new { Foo = "True" };
            var target = new ExpressionObserver(data, "!Foo");
            var result = await target.Take(1);

            Assert.Equal(false, result);
        }

        [Fact]
        public async void Should_Return_UnsetValue_For_String_Not_Convertible_To_Boolean()
        {
            var data = new { Foo = "foo" };
            var target = new ExpressionObserver(data, "!Foo");
            var result = await target.Take(1);

            Assert.Equal(PerspexProperty.UnsetValue, result);
        }

        [Fact]
        public async void Should_Return_Empty_For_Value_Not_Convertible_To_Boolean()
        {
            var data = new { Foo = new object() };
            var target = new ExpressionObserver(data, "!Foo");
            var result = await target.Take(1);

            Assert.Equal(PerspexProperty.UnsetValue, result);
        }

        [Fact]
        public void SetValue_Should_Return_False()
        {
            var data = new { Foo = "foo" };
            var target = new ExpressionObserver(data, "!Foo");

            Assert.False(target.SetValue("bar"));
        }
    }
}
