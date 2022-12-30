#!/bin/bash

dotnet publish Webion.Licensing.Tool \
  -r osx.12-arm64 \
  -c Release \
  /p:PublishSingleFile=true \
  --no-self-contained \
  -o dist/wl

mv dist/wl/Webion.Licensing.Tool dist/wl/wl