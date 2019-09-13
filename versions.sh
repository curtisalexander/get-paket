#!/usr/bin/env bash

# macOS
echo "macOS version:"
defaults read loginwindow SystemVersionStampAsString
echo

# .NET Core
echo "dotnet core version:"
dotnet --version
echo

# Packages
echo "Package versions:"
dotnet list get-paket.fsproj package
echo
