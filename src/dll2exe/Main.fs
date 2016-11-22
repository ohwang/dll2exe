namespace dll2exe

open Microsoft.FSharp.Compiler.SimpleSourceCodeServices
open System.IO
open System

/// Documentation for the Main module
///
/// ## Example
///
///     let h = Library.hello 1
///     printfn "%d" h
///
module Main = 

  let scs = SimpleSourceCodeServices()

  let programTemplate = """
#r "{0}"
{1}(System.Environment.GetCommandLineArgs())
"""

  let populateFsFile content =
    let tempPath = 
        Path.GetTempFileName()
        |> (fun x -> Path.ChangeExtension(x, "fsx"))

    File.WriteAllText(tempPath, content)

    tempPath
  
  // [<EntryPoint>]
  // Drop EntryPoint label as we are compiling into library
  let main (args : string array) : int =
    if args.Length <> 3 then failwith "Usage ... [dll-path] [entry-point] [output-path]"

    let dllPath = args.[0]
    let entryPoint = args.[1]

    let program = System.String.Format(programTemplate, dllPath, entryPoint)

    printfn "Program\n%A" program

    let tempFsFilePath = populateFsFile program

    let outputPath = args.[2]

    let errors, exitCode = scs.Compile([|"fsc.exe"; tempFsFilePath; "--target:exe"; "-o"; outputPath; "--standalone"|])

    errors |> Array.iter (printfn "%A")

    File.Delete(tempFsFilePath)

    exitCode

