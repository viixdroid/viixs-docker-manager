; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md


### New Rules

Rule ID | Category | Severity | Notes 
---------|----------|----------|-------
VDM001  | ViixsSourceGenerator  | Warning  | Triggered when two or more generated controllers would have the same name, which would cause a compilation error.
VDM002  | ViixsSourceGenerator  | Error    | Triggered when the attribute does not have a query or command type specified.
VDM003  | ViixsSourceGenerator  | Error    | Triggered when the specified query or command type does not implement the required interface.
VDM004  | ViixsSourceGenerator  | Error    | Triggered when a command or query type is missing from a class.