namespace Elmish.React

open System
open Fable.Inferno
open Fable.Core

[<RequireQualifiedAccess>]
module Program =
    open Fable.Import.Browser

    /// Setup rendering of root React component inside html element identified by placeholderId
    let withReact placeholderId (program:Elmish.Program<_,_,_,_>) =
        let mutable lastRequest = None
        let setState model dispatch =
            match lastRequest with
            | Some r -> window.cancelAnimationFrame r
            | _ -> ()

            lastRequest <- Some (window.requestAnimationFrame (fun _ ->
                render(
                    lazyView2With (fun x y -> obj.ReferenceEquals(x,y)) program.view model dispatch,
                    document.getElementById(placeholderId)
                )))

        { program with setState = setState }
