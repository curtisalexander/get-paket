// Learn more about F# at http://fsharp.org

open System

open FSharp.Data
open FSharp.Data.HttpRequestHeaders
open FSharp.Data.JsonExtensions

[<EntryPoint>]
let main argv =
    (*
    Assumptions:
        - The Github API returns a list of releases in sorted order
    *)
    (*
    Notes:
        - It utilize the latest release API, returns the most recent non-prerelease, non-draft release sorted by created_at attribute
        - Testing on my repo (curtisalexander/pyoxidizer-timer) throws a 404 error because I do not have any non-prerelease, non-draft releases
    *)

    // A UserAgent is required by Github API
    let userAgent = "curtisalexander"

    // Paket
    let paketRootUrl = "https://api.github.com/repos/fsprojects/Paket/releases"
    let paketFileName = "paket.bootstrapper.exe"

    // Test using latest release API endpoint
    // let paketLatestRootUrl = "https://api.github.com/repos/fsprojects/Paket/releases/latest"

    // TODO: Error handling
    let apiResp = Http.Request(paketRootUrl, headers = [ UserAgent userAgent ])

    // Get JSON
    // Decompose -> Text [ { } ]
    let value =
        match apiResp.Body with
        | Text t ->
            match JsonValue.TryParse(t) with
            | Some j ->
                match j with
                | JsonValue.Array elements ->
                    // Return the first element in the array as the JSON returned
                    //   from the Github API is a single array element
                    elements.[0]
                | _ ->
                    printfn "First element of JSON returned does not contain an array"
                    JsonValue.Null
            | None ->
                printfn "Unable to parse JSON returned"
                JsonValue.Null
        | _ ->
            printfn "No text returned within the body returned from the HTTP request"
            JsonValue.Null

    // let tagName = value.TryGetProperty("tag_name")

    let assetsArray = value.GetProperty("assets").AsArray()

    let isName (name:string) (value:JsonValue) =
        let propResult = value.TryGetProperty("name")
        match propResult with
        | Some t -> name = t.AsString()
        | None ->
            printfn "There are not any assets that have the file name %s" name
            false

    let getBrowserUrl (j:JsonValue) =
        let url = j.TryGetProperty("browser_download_url")
        match url with
        | Some b -> b.AsString()
        | None ->
            printfn "There is not any property that matches browser_download_url"
            ""

    let paketBootstrapUrl =
        assetsArray
        |> Array.filter (isName paketFileName)
        |> Array.head
        |> getBrowserUrl


    let dlResp = Http.Request(paketBootstrapUrl, headers = [ UserAgent userAgent ])

    // Download
    (* TODO:
        - Streaming version
        - Async version
    *)
    let writeResult =
        match dlResp.Body with
        | Binary bytes ->
            let paketDir = System.IO.Path.Combine(__SOURCE_DIRECTORY__, ".paket")
            System.IO.Directory.CreateDirectory(paketDir) |> ignore
            System.IO.File.WriteAllBytes(System.IO.Path.Combine(paketDir, paketFileName), bytes)
        | Text t ->
            ()

    0
