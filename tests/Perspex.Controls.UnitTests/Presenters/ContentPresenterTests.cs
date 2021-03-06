﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System.Linq;
using Moq;
using Perspex.Controls.Presenters;
using Perspex.Controls.Primitives;
using Perspex.Controls.Templates;
using Perspex.UnitTests;
using Perspex.VisualTree;
using Xunit;

namespace Perspex.Controls.UnitTests.Presenters
{
    public class ContentPresenterTests
    {
        [Fact]
        public void Should_Register_With_Host_When_TemplatedParent_Set()
        {
            var host = new Mock<IContentPresenterHost>();
            var target = new ContentPresenter();

            target.SetValue(Control.TemplatedParentProperty, host.Object);

            host.Verify(x => x.RegisterContentPresenter(target));
        }

        [Fact]
        public void Setting_Content_To_Control_Should_Set_Child()
        {
            var target = new ContentPresenter();
            var child = new Border();

            target.Content = child;

            Assert.Null(target.Child);
            target.UpdateChild();
            Assert.Equal(child, target.Child);
        }

        [Fact]
        public void Setting_Content_To_String_Should_Create_TextBlock()
        {
            var target = new ContentPresenter();

            target.Content = "Foo";

            Assert.Null(target.Child);
            target.UpdateChild();
            Assert.IsType<TextBlock>(target.Child);
            Assert.Equal("Foo", ((TextBlock)target.Child).Text);
        }

        [Fact]
        public void Control_Content_Should_Not_Be_NameScope()
        {
            var target = new ContentPresenter();

            target.Content = new TextBlock();

            Assert.Null(target.Child);
            target.UpdateChild();
            Assert.IsType<TextBlock>(target.Child);
            Assert.Null(NameScope.GetNameScope((Control)target.Child));
        }

        [Fact]
        public void DataTemplate_Created_Control_Should_Be_NameScope()
        {
            var target = new ContentPresenter();

            target.Content = "Foo";

            Assert.Null(target.Child);
            target.UpdateChild();
            Assert.IsType<TextBlock>(target.Child);
            Assert.NotNull(NameScope.GetNameScope((Control)target.Child));
        }

        [Fact]
        public void Should_Set_Childs_Parent_To_TemplatedParent()
        {
            var content = new Border();
            var target = new TestContentControl
            {
                Template = new FuncControlTemplate<TestContentControl>(parent =>
                    new ContentPresenter { Content = parent.Child }),
                Child = content,
            };

            target.ApplyTemplate();
            var presenter = ((ContentPresenter)target.GetVisualChildren().Single());
            presenter.UpdateChild();

            Assert.Same(target, content.Parent);
        }

        [Fact]
        public void Should_Set_Childs_Parent_To_Itself_Outside_Template()
        {
            var content = new Border();
            var target = new ContentPresenter { Content = content };

            target.UpdateChild();

            Assert.Same(target, content.Parent);
        }

        [Fact]
        public void Adding_To_Logical_Tree_Should_Reevaluate_DataTemplates()
        {
            var target = new ContentPresenter
            {
                Content = "Foo",
            };

            target.UpdateChild();
            Assert.IsType<TextBlock>(target.Child);

            var root = new TestRoot
            {
                DataTemplates = new DataTemplates
                {
                    new FuncDataTemplate<string>(x => new Decorator()),
                },
            };

            root.Child = target;
            target.ApplyTemplate();
            Assert.IsType<Decorator>(target.Child);
        }

        [Fact]
        public void Assigning_Control_To_Content_Should_Not_Set_DataContext()
        {
            var target = new ContentPresenter
            {
                Content = new Border(),
            };

            Assert.False(target.IsSet(Control.DataContextProperty));
        }

        [Fact]
        public void Assigning_NonControl_To_Content_Should_Set_DataContext()
        {
            var target = new ContentPresenter
            {
                Content = "foo",
            };

            Assert.Equal("foo", target.DataContext);
        }

        private class TestContentControl : TemplatedControl
        {
            public IControl Child { get; set; }
        }
    }
}
