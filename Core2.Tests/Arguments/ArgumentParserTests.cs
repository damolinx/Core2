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
            var args = Array.Empty<string>();
            var parsedArgs = parser.Parse(args).ToArray();
            Assert.AreEqual(0, parsedArgs.Length, "Expected successful parsing of no arguments");
        }

        [TestMethod]
        public void Parse_SingleSimpleLiteral()
        {
            var parser = new ArgumentParser();
            var args = new[] { "File1" };
            var parsedArgs = parser.Parse(args).ToArray();
            Assert.AreEqual(args.Length, parsedArgs.Length, "Expected successful parsing");
            Assert.IsNull(parsedArgs[0].Definition, "Expected no option");
            Assert.AreEqual(1, parsedArgs[0].Values.Count, "Expected argument to carry one value");
            Assert.AreEqual(args[0], parsedArgs[0].Values[0], "Expected argument and value to match");
        }

        [TestMethod]
        public void Parse_SingleQuotedLiteral()
        {
            var parser = new ArgumentParser();
            var args = new[] { "\"File 1\"" };
            var parsedArgs = parser.Parse(args).ToArray();
            Assert.AreEqual(args.Length, parsedArgs.Length, "Expected successful parsing");
            Assert.IsNull(parsedArgs[0].Definition, "Expected no option");
            Assert.AreEqual(1, parsedArgs[0].Values.Count, "Expected argument to carry one value");
            Assert.AreEqual(args[0], parsedArgs[0].Values[0], "Expected argument and value to match");
        }

        [TestMethod]
        public void Parse_MultipleLiterals()
        {
            var parser = new ArgumentParser();
            var args = new[] { "File1", "\"File 2\"", "File3" };
            var parsedArgs = parser.Parse(args).ToArray();
            Assert.AreEqual(args.Length, parsedArgs.Length, $"Expected successful parsing");
            for (var i = 0; i < args.Length; i++)
            {
                Assert.IsNull(parsedArgs[i].Definition, "Expected no option");
                Assert.AreEqual(1, parsedArgs[i].Values.Count, $"Expected argument to carry one value. Index: {i}");
                Assert.AreEqual(args[i], parsedArgs[i].Values[0], $"Expected argument and value to match. Index: {i}");
            }
        }

        [TestMethod]
        public void Parse_SingleNoArgumentOption()
        {
            const string optionName = "Option1";
            var parser = new ArgumentParser();
            parser.OptionsDefinitions.Add(new OptionDefinition(optionName));
            var args = new[] { ArgumentParser.DefaultOptionSuffix + optionName };
            var parsedArgs = parser.Parse(args).ToArray();
            Assert.AreEqual(args.Length, parsedArgs.Length, "Expected successful parsing");
            Assert.AreEqual(optionName, parsedArgs[0].Definition?.Name, "Expected option");
            Assert.AreEqual(0, parsedArgs[0].Values.Count, "Expected argument to carry no value");
        }

        [TestMethod]
        public void Parse_SingleOneArgumentOption()
        {
            const string optionName = "Option1";
            const string optionValue = "File1";
            var parser = new ArgumentParser();
            parser.OptionsDefinitions.Add(new OptionDefinition(optionName, 1));
            var args = new[] { ArgumentParser.DefaultOptionSuffix + optionName, optionValue };
            var parsedArgs = parser.Parse(args).ToArray();
            Assert.AreEqual(1, parsedArgs.Length, "Expected successful parsing");
            Assert.AreEqual(optionName, parsedArgs[0].Definition?.Name, "Expected option");
            Assert.AreEqual(1, parsedArgs[0].Values.Count, "Expected argument to carry one value");
            Assert.AreEqual(optionValue, parsedArgs[0].Values[0], "Expected argument and value to match");
        }

        [TestMethod]
        public void Parse_SingleMultipleArgumentOption()
        {
            const string optionName = "Sources";
            const string optionValue1 = "File1";
            const string optionValue2 = "File2";
            const string optionValue3 = "File3";
            var parser = new ArgumentParser();
            parser.OptionsDefinitions.Add(new OptionDefinition(optionName, 3));
            var args = new[] { ArgumentParser.DefaultOptionSuffix + optionName, optionValue1, optionValue2, optionValue3 };
            var parsedArgs = parser.Parse(args).ToArray();
            Assert.AreEqual(1, parsedArgs.Length, "Expected successful parsing");
            Assert.AreEqual(optionName, parsedArgs[0].Definition?.Name, "Expected option");
            Assert.AreEqual(3, parsedArgs[0].Values.Count, "Expected argument to carry multiple values");
            Assert.AreEqual(optionValue1, parsedArgs[0].Values[0], $"Expected argument and value to match");
            Assert.AreEqual(optionValue2, parsedArgs[0].Values[1], $"Expected argument and value to match");
            Assert.AreEqual(optionValue3, parsedArgs[0].Values[2], $"Expected argument and value to match");
        }

        [TestMethod]
        public void Parse_MultipleMultipleArgumentOption()
        {
            const string optionName1 = "Sources";
            const string optionValue1 = "File1";
            const string optionName2 = "Targets";
            const string optionValue2 = "File2";
            var parser = new ArgumentParser();
            parser.OptionsDefinitions.Add(new OptionDefinition(optionName1, 3));
            parser.OptionsDefinitions.Add(new OptionDefinition(optionName2, 3));
            var args = new[] { ArgumentParser.DefaultOptionSuffix + optionName1, optionValue1, ArgumentParser.DefaultOptionSuffix + optionName2, optionValue2 };
            var parsedArgs = parser.Parse(args).ToArray();
            Assert.AreEqual(2, parsedArgs.Length, "Expected successful parsing");
            //Option1
            Assert.AreEqual(optionName1, parsedArgs[0].Definition?.Name, "Expected option 1");
            Assert.AreEqual(1, parsedArgs[0].Values.Count, "Expected option 1 to carry one value");
            Assert.AreEqual(optionValue1, parsedArgs[0].Values[0], $"Expected value 1 to match");
            //Option2
            Assert.AreEqual(optionName2, parsedArgs[1].Definition?.Name, "Expected option 2");
            Assert.AreEqual(1, parsedArgs[1].Values.Count, "Expected option 2 to carry one value");
            Assert.AreEqual(optionValue2, parsedArgs[1].Values[0], $"Expected value 2 to match");
        }

        [TestMethod]
        public void Parse_LimitedMultipleArgumentOption()
        {
            const string optionName = "Sources";
            const string optionValue1 = "File1";
            const string optionValue2 = "File2";
            const string optionValue3 = "File3";
            const string literalValue1 = "File4";
            const string literalValue2 = "File5";
            var parser = new ArgumentParser();
            parser.OptionsDefinitions.Add(new OptionDefinition(optionName, 3));
            var args = new[] { literalValue1, ArgumentParser.DefaultOptionSuffix + optionName, optionValue1, optionValue2, optionValue3, literalValue2 };
            var parsedArgs = parser.Parse(args).ToArray();
            Assert.AreEqual(3, parsedArgs.Length, "Expected successful parsing");
            // Literal 1
            Assert.IsNull(parsedArgs[0].Definition, "Expected no option");
            Assert.AreEqual(1, parsedArgs[0].Values.Count, "Expected argument to carry one value");
            Assert.AreEqual(literalValue1, parsedArgs[0].Values[0], "Expected argument and value to match");
            // Option
            Assert.AreEqual(optionName, parsedArgs[1].Definition?.Name, "Expected option");
            Assert.AreEqual(3, parsedArgs[1].Values.Count, "Expected argument to carry multiple values");
            Assert.AreEqual(optionValue1, parsedArgs[1].Values[0], $"Expected argument and value to match");
            Assert.AreEqual(optionValue2, parsedArgs[1].Values[1], $"Expected argument and value to match");
            Assert.AreEqual(optionValue3, parsedArgs[1].Values[2], $"Expected argument and value to match");
            // Literal 2
            Assert.IsNull(parsedArgs[2].Definition, "Expected no option");
            Assert.AreEqual(1, parsedArgs[2].Values.Count, "Expected argument to carry one value");
            Assert.AreEqual(literalValue2, parsedArgs[2].Values[0], "Expected argument and value to match");
        }

        [TestMethod]
        public void Parse_RequiresOptionDefinition_False()
        {
            const string optionName = "Option1";
            var parser = new ArgumentParser
            {
                RequiresOptionDefinition = false,
            };
            var args = new[] { ArgumentParser.DefaultOptionSuffix + optionName };
            var parsedArgs = parser.Parse(args).ToArray();
            Assert.AreEqual(args.Length, parsedArgs.Length, "Expected successful parsing");
            Assert.AreEqual(optionName, parsedArgs[0].Definition?.Name, "Expected option");
            Assert.AreEqual(0, parsedArgs[0].Values.Count, "Expected argument to carry no value");
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownOptionException))]
        public void Parse_RequiresOptionDefinition_True()
        {
            const string optionName = "Option1";
            var parser = new ArgumentParser
            {
                RequiresOptionDefinition = true
            };
            var args = new[] { ArgumentParser.DefaultOptionSuffix + optionName };
            var parsedArgs = parser.Parse(args).ToArray();
            Assert.Fail($"Expected {nameof(ArgumentParser)}.{nameof(ArgumentParser.Parse)} to fail");
        }
    }
}
