FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
COPY dist/wormhole /app
ENV WORMHOLE_CONFIG=/app/config/wormhole-config.json
ENV ASPNETCORE_URLS http://*
EXPOSE 80
ENTRYPOINT ["dotnet", "wormhole.dll"]
