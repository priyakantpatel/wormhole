FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80
COPY dist/wormhole /app
ENTRYPOINT ["dotnet", "wormhole.dll"]
