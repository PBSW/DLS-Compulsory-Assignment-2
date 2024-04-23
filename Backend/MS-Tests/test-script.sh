#! /bin/bash

set -eu -o pipefail

dotnet restore /src/Backend/MS-Tests/MS-Tests.csproj
dotnet test -c Release --results-directory /testresults --logger "trx;LogFileName=ms_test_results.trx" "/src/Backend/MS-Tests/MS-Tests.csproj"
