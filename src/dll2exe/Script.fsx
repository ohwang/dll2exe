// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#r @"./bin/Debug/dll2exe.dll"

open dll2exe

let path = "./bin/Debug/dll2exe.dll"

printfn "Convert %s into dll2exe.exe"

Main.main [|path; "dll2exe.Main.main"; "dll2exe.exe"|]
