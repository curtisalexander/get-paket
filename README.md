# get-paket

Experiment with downloading files via [F#](https://fsharp.org/).

**Under construction.**
:construction:

## Initial Approach

Utilizes [FSharp.Data](https://fsharp.github.io/FSharp.Data/library/Http.html).

## Future Areas of Research

- Research using [HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netcore-3.0) in lieu of [HttpWebRequest](https://docs.microsoft.com/en-us/dotnet/api/system.net.httpwebrequest?&view=netcore-3.0).

- Will be a good opportunity to continue practicing using the [BCL](https://docs.microsoft.com/en-us/dotnet/api/index) within `F#`. In addition, will be an opportunity to practice with [Async](https://docs.microsoft.com/en-us/dotnet/fsharp/tutorials/asynchronous-and-concurrent-programming/async) programming.

- Try to download the file via [streaming](https://github.com/curtisalexander/get-paket/blob/master/Program.fs#L87).

- Try to download the file with [async](https://github.com/curtisalexander/get-paket/blob/master/Program.fs#L88).

## A Better Way

A better way to manage may be as a global tool.

https://gist.github.com/baronfel/80f49ecb93ebacb84ca840fca7a12fc2
