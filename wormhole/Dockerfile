FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80
COPY dist/wormhole /app
ENV WORMHOLE_CONFIG=/app/config/wormhole-config.json
ENTRYPOINT ["dotnet", "wormhole.dll"]
