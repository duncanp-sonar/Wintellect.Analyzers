/*------------------------------------------------------------------------------
Wintellect.Analyzers - .NET Compiler Platform ("Roslyn") Analyzers and CodeFixes
Copyright (c) Wintellect. All rights reserved
Licensed under the Apache License, Version 2.0
See License.txt in the project root for license information
------------------------------------------------------------------------------*/
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;

namespace Wintellect.Analyzers.Test
{
    [TestClass]
    public abstract class BaseTypeNamingUnitTests : DiagnosticVerifier
    {
        [TestMethod]
        public void InvalidNameTest()
        {
            const String test = @"
using System;

namespace SomeTests
{
    public class XBasicClass
    {
    }
}
";
            var descriptor = GetDescriptor();

            var expected = new DiagnosticResult
            {
                Id = descriptor.Id,
                Message = String.Format(descriptor.MessageFormat.ToString(), "XBasicClass"),
                Severity = descriptor.DefaultSeverity,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 6, 18)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);
        }

        protected abstract DiagnosticDescriptor GetDescriptor();
    }

    [TestClass]
    public class TestRuleA : BaseTypeNamingUnitTests
    {
        protected override DiagnosticDescriptor GetDescriptor() => TypeNameRuleA.Rule;

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer() => new TypeNameRuleA();
    }


    [TestClass]
    public class TestRuleB : BaseTypeNamingUnitTests
    {
        protected override DiagnosticDescriptor GetDescriptor() => TypeNameRuleB.Rule;

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer() => new TypeNameRuleB();
    }

    [TestClass]
    public class TestRuleC : BaseTypeNamingUnitTests
    {
        protected override DiagnosticDescriptor GetDescriptor() => TypeNameRuleC.Rule;

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer() => new TypeNameRuleC();
    }
}
