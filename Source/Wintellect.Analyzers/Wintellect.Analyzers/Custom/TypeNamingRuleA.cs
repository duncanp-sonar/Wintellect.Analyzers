/*------------------------------------------------------------------------------
Wintellect.Analyzers - .NET Compiler Platform ("Roslyn") Analyzers and CodeFixes
Copyright (c) Wintellect. All rights reserved
Licensed under the Apache License, Version 2.0
See License.txt in the project root for license information
------------------------------------------------------------------------------*/
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace Wintellect.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class TypeNameRuleA : DiagnosticAnalyzer
    {
        private const string CharToCheck = "A";

        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor("SonarRoslyn_Type" + CharToCheck,
            "Type names must start with " + CharToCheck,
            "Invalid type: {0}. Type names must start with " + CharToCheck,
            "Naming", DiagnosticSeverity.Warning, true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSymbolAction(CheckTypeName, SymbolKind.NamedType);
        }

        private void CheckTypeName(SymbolAnalysisContext context)
        {
            if(!context.Symbol.Name.StartsWith(CharToCheck))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Symbol.Locations.FirstOrDefault(), context.Symbol.Name));
            }
        }
    }
}