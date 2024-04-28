#! /bin/bash

set -eu -o pipefail

dotnet restore /src/Backend/PS-Tests/PS-Tests.csproj
dotnet test -c Release --results-directory /testresults --logger "trx;LogFileName=ps_test_results.trx" "/src/Backend/PS-Tests/PS-Tests.csproj"
