using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using RegexAnalyzer.Extensions;

namespace RegexAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    //[DebuggerDisplay("Rule={DiagnosticIds.AvoidCallingMethodsWithParamArgsInLoopsAnalyzer}")]
    public sealed class AvoidCallingLcpMethodsInLoopsAnalyzer : DiagnosticAnalyzer
    {
        internal static DiagnosticDescriptor Rule =
            new DiagnosticDescriptor("GSP_BEF_001",
            "不在循环中调用LCP方法", "当前lcp方法 {0} 不应在循环中调用",
            "性能", DiagnosticSeverity.Warning, true);
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeIdentifier, SyntaxKind.IdentifierName);
        }

        private void AnalyzeIdentifier(SyntaxNodeAnalysisContext context)
        {
            if (context.IsGeneratedOrNonUserCode())
            {
                return;
            }
            var typeName = "";
            // Look at the method
            // If it has parameters, check if see if the last parameter has the IsParams set.
            var indentifierName = context.Node as IdentifierNameSyntax;
            var methodSymbol = MatchMethodOfType(context, "Inspur.GSP.Bef.Api.Lcp.IStandardLcp");
            if (methodSymbol == null)
                return;


            if (context.Node.IsNodeInALoop())
            {
                // We got us a problem here, boss.
                var diagnostic = Diagnostic.Create(Rule,
                    indentifierName.GetLocation(),
                    indentifierName.Parent.ToString());
                context.ReportDiagnostic(diagnostic);
            }
            //var count = methodSymbol.OriginalDefinition.Parameters.Length;
            //if (count == 0) return;
            //if (methodSymbol.OriginalDefinition.Parameters[count - 1].IsParams)
            //{
            //    // Only report the error if this call is inside a loop.

            //}
        }

        private IMethodSymbol MatchMethodOfType(SyntaxNodeAnalysisContext context, string typeName)
        {
            // Look at the method
            // If it has parameters, check if see if the last parameter has the IsParams set.
            var indentifierName = context.Node as IdentifierNameSyntax;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(indentifierName).Symbol as IMethodSymbol;
            if (methodSymbol == null)
                return null;
            var type = methodSymbol.ContainingType;
            if (type.ToString() == typeName)
            {

                return methodSymbol;
            }

            if (type.AllInterfaces.Any(item => item.ToString().Equals(typeName)))
            {
                return methodSymbol;
            }

            return null;
        }

        private static string GetTypeFullName(INamedTypeSymbol type)
        {
            return $"{type.ContainingNamespace.Name}.{type.Name}";
        }
    }
}