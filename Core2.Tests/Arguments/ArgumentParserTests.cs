using Core2.Arguments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Core2.Tests.Arguments
{
    [TestClass]
    public class ArgumentParserTests
    {
        [TestMethod]
        public void Parse_Empty()
        {
            var parser = new ArgumentParser();
            var arguments = parser.Parse(Array.Empty<string>(), requireDefinition: true);
            Assert.IsNotNull(arguments);
            Assert.IsFalse(arguments.Any());
        }

        [TestMethod]
        public void Parse_Literal_Single()
        {
            var parser = new ArgumentParser();
            var arguments = parser.Parse(new[] { "value1" }, requireDefinition: true);

            Assert.IsNotNull(arguments);
            Assert.AreEqual(1, arguments.Count());
            Assert.AreEqual(ArgumentKind.Literal, arguments.First().Kind);
            Assert.AreEqual(1, arguments.First().Values.Count());
            Assert.AreEqual("value1", arguments.First().Values.First());
        }

        [TestMethod]
        public void Parse_Literal_Multiple()
        {
            var parser = new ArgumentParser();
            var arguments = parser.Parse(new[] { "value1", "value2", "value3" }, requireDefinition: true);

            Assert.IsNotNull(arguments);
            Assert.AreEqual(3, arguments.Count());

            var arg1 = arguments.First();
            Assert.AreEqual(ArgumentKind.Literal, arg1.Kind);
            Assert.AreEqual(1, arg1.Values.Count());
            Assert.AreEqual("value1", arg1.Values.First());

            var arg2 = arguments.Skip(1).First();
            Assert.AreEqual(ArgumentKind.Literal, arg2.Kind);
            Assert.AreEqual(1, arg2.Values.Count());
            Assert.AreEqual("value2", arg2.Values.First());

            var arg3 = arguments.Skip(2).First();
            Assert.AreEqual(ArgumentKind.Literal, arg3.Kind);
            Assert.AreEqual(1, arg3.Values.Count());
            Assert.AreEqual("value3", arg3.Values.First());
        }

        [TestMethod]
        public void Parse_Option_Single_RequiresDefinition_CaseInsensitive()
        {
            var definition = new OptionDefinition("option1", maxArguments: 0);
            var parser = new ArgumentParser("/", StringComparer.OrdinalIgnoreCase);
            parser.OptionsDefinitions.Add(definition);
            var arguments = parser.Parse(new[] { "/OPTION1" }, requireDefinition: true);

            Assert.IsNotNull(arguments);
            Assert.AreEqual(1, arguments.Count());
            Assert.AreEqual(ArgumentKind.Option, arguments.First().Kind);
            Assert.AreEqual(definition, arguments.First().Definition);
            Assert.AreEqual(0, arguments.First().Values.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownOptionException))]
        public void Parse_Option_Single_RequiresDefinition_CaseSensitive()
        {
            var definition = new OptionDefinition("option1", maxArguments: 0);
            var parser = new ArgumentParser("/", StringComparer.Ordinal);
            parser.OptionsDefinitions.Add(definition);
            var arguments = parser.Parse(new[] { "/OPTION1" }, requireDefinition: true);
            Assert.Fail("Expected no matches");
        }

        [TestMethod]
        public void Parse_Option_Single_DoesNotRequireDefinition()
        {
            var parser = new ArgumentParser("/", StringComparer.OrdinalIgnoreCase);
            var arguments = parser.Parse(new[] { "/OPTION1" }, requireDefinition: false);

            Assert.IsNotNull(arguments);
            Assert.AreEqual(1, arguments.Count());
            Assert.AreEqual(ArgumentKind.Option, arguments.First().Kind);
            Assert.IsNotNull(arguments.First().Definition);
            Assert.AreEqual(0, arguments.First().Definition.MaxArguments);
            Assert.AreEqual("OPTION1", arguments.First().Definition.Name);
            Assert.AreEqual(0, arguments.First().Values.Count());
        }

        [TestMethod]
        public void Parse_Option_Single_OneArgument()
        {
            var definition = new OptionDefinition("option1", maxArguments: 1);
            var parser = new ArgumentParser("/", StringComparer.OrdinalIgnoreCase);
            parser.OptionsDefinitions.Add(definition);
            var arguments = parser.Parse(new[] { "/option1", "value1" }, requireDefinition: true);

            Assert.IsNotNull(arguments);
            Assert.AreEqual(1, arguments.Count());

            Assert.AreEqual(ArgumentKind.Option, arguments.First().Kind);
            Assert.AreEqual(definition, arguments.First().Definition);
            Assert.AreEqual(1, arguments.First().Values.Count());
            Assert.AreEqual("value1", arguments.First().Values[0]);
        }

        [TestMethod]
        public void Parse_Option_Single_MultipleArguments_AllProvided()
        {
            var definition = new OptionDefinition("option1", maxArguments: 3);
            var parser = new ArgumentParser("/", StringComparer.OrdinalIgnoreCase);
            parser.OptionsDefinitions.Add(definition);
            var arguments = parser.Parse(new[] { "/option1", "value1", "value2", "value3" }, requireDefinition: true);

            Assert.IsNotNull(arguments);
            Assert.AreEqual(1, arguments.Count());

            Assert.AreEqual(ArgumentKind.Option, arguments.First().Kind);
            Assert.AreEqual(definition, arguments.First().Definition);
            Assert.AreEqual(3, arguments.First().Values.Count());
            Assert.AreEqual("value1", arguments.First().Values[0]);
            Assert.AreEqual("value2", arguments.First().Values[1]);
            Assert.AreEqual("value3", arguments.First().Values[2]);
        }

        [TestMethod]
        public void Parse_Option_Single_MultipleArguments_FewerProvided()
        {
            var definition = new OptionDefinition("option1", maxArguments: 2);
            var parser = new ArgumentParser("/", StringComparer.OrdinalIgnoreCase);
            parser.OptionsDefinitions.Add(definition);
            var arguments = parser.Parse(new[] { "/option1", "value1", "value2" }, requireDefinition: true);

            Assert.IsNotNull(arguments);
            Assert.AreEqual(1, arguments.Count());

            Assert.AreEqual(ArgumentKind.Option, arguments.First().Kind);
            Assert.AreEqual(definition, arguments.First().Definition);
            Assert.AreEqual(2, arguments.First().Values.Count());
            Assert.AreEqual("value1", arguments.First().Values[0]);
            Assert.AreEqual("value2", arguments.First().Values[1]);
        }

        [TestMethod]
        public void Parse_Literal_Option_DoesNotRequireDefinition()
        {
            var parser = new ArgumentParser("/", StringComparer.OrdinalIgnoreCase);
            var arguments = parser.Parse(new[] { "value1", "/option1" }, requireDefinition: false);

            Assert.IsNotNull(arguments);
            Assert.AreEqual(2, arguments.Count());

            var arg1 = arguments.First();
            Assert.AreEqual(ArgumentKind.Literal, arg1.Kind);
            Assert.AreEqual(1, arg1.Values.Count());
            Assert.AreEqual("value1", arg1.Values.First());

            var arg2 = arguments.Skip(1).First();
            Assert.AreEqual(ArgumentKind.Option, arg2.Kind);
            Assert.IsNotNull(arg2.Definition);
            Assert.AreEqual(0, arg2.Definition.MaxArguments);
            Assert.AreEqual("option1", arg2.Definition.Name);
            Assert.AreEqual(0, arg2.Values.Count());
        }

        [TestMethod]
        public void Parse_Option_Literal_DoesNotRequireDefinition()
        {
            var parser = new ArgumentParser("/", StringComparer.OrdinalIgnoreCase);
            var arguments = parser.Parse(new[] { "/option1", "value1" }, requireDefinition: false);

            Assert.IsNotNull(arguments);
            Assert.AreEqual(2, arguments.Count());

            var arg1 = arguments.First();
            Assert.AreEqual(ArgumentKind.Option, arg1.Kind);
            Assert.IsNotNull(arg1.Definition);
            Assert.AreEqual(0, arg1.Definition.MaxArguments);
            Assert.AreEqual("option1", arg1.Definition.Name);
            Assert.AreEqual(0, arg1.Values.Count());

            var arg2 = arguments.Skip(1).First();
            Assert.AreEqual(ArgumentKind.Literal, arg2.Kind);
            Assert.AreEqual(1, arg2.Values.Count());
            Assert.AreEqual("value1", arg2.Values.First());
        }

        [TestMethod]
        public void Parse_Option_Single_MultipleArguments_AllProvided_Then_Literal()
        {
            var definition = new OptionDefinition("option1", maxArguments: 3);
            var parser = new ArgumentParser("/", StringComparer.OrdinalIgnoreCase);
            parser.OptionsDefinitions.Add(definition);
            var arguments = parser.Parse(new[] { "/option1", "value1", "value2", "value3", "value4" }, requireDefinition: true);

            Assert.IsNotNull(arguments);
            Assert.AreEqual(2, arguments.Count());

            var arg1 = arguments.First();
            Assert.AreEqual(ArgumentKind.Option, arg1.Kind);
            Assert.AreEqual(definition, arg1.Definition);
            Assert.AreEqual(3, arg1.Values.Count());
            Assert.AreEqual("value1", arg1.Values[0]);
            Assert.AreEqual("value2", arg1.Values[1]);
            Assert.AreEqual("value3", arg1.Values[2]);

            var arg2 = arguments.Skip(1).First();
            Assert.AreEqual(ArgumentKind.Literal, arg2.Kind);
            Assert.AreEqual(1, arg2.Values.Count());
            Assert.AreEqual("value4", arg2.Values.First());
        }

        [TestMethod]
        public void Parse_Option_Multiple_MultipleArguments()
        {
            var definition1 = new OptionDefinition("option1", maxArguments: 2);
            var definition2 = new OptionDefinition("option2", maxArguments: 1);

            var parser = new ArgumentParser("/", StringComparer.OrdinalIgnoreCase);
            parser.OptionsDefinitions.Add(definition1);
            parser.OptionsDefinitions.Add(definition2);
            var arguments = parser.Parse(new[] { "/option1", "value1", "value2", "/option2", "value3" }, requireDefinition: true);

            Assert.IsNotNull(arguments);
            Assert.AreEqual(2, arguments.Count());

            var arg1 = arguments.First();
            Assert.AreEqual(ArgumentKind.Option, arg1.Kind);
            Assert.AreEqual(definition1, arg1.Definition);
            Assert.AreEqual(2, arg1.Values.Count());
            Assert.AreEqual("value1", arg1.Values[0]);
            Assert.AreEqual("value2", arg1.Values[1]);

            var arg2 = arguments.Skip(1).First();
            Assert.AreEqual(ArgumentKind.Option, arg2.Kind);
            Assert.AreEqual(definition2, arg2.Definition);
            Assert.AreEqual(1, arg2.Values.Count());
            Assert.AreEqual("value3", arg2.Values[0]);
        }
    }
}
