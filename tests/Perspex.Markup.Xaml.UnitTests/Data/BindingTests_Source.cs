﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Moq;
using Perspex.Controls;
using Perspex.Data;
using Perspex.Markup.Data;
using Perspex.Markup.Xaml.Data;
using ReactiveUI;
using Xunit;

namespace Perspex.Markup.Xaml.UnitTests.Data
{
    public class BindingTests_Source
    {
        [Fact]
        public void Source_Should_Be_Used()
        {
            var source = new Source { Foo = "foo" };
            var binding = new Binding { Source = source, Path = "Foo" };
            var target = new TextBlock();

            target.Bind(TextBlock.TextProperty, binding);

            Assert.Equal(target.Text, "foo");
        }

        public class Source : ReactiveObject
        {
            private string _foo;

            public string Foo
            {
                get { return _foo; }
                set { this.RaiseAndSetIfChanged(ref _foo, value); }
            }
        }
    }
}
