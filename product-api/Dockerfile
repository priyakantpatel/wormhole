FROM microsoft/aspnetcore:2.0
ARG source
WORKDIR /app
EXPOSE 80
COPY dist/product-api /app
ENTRYPOINT ["dotnet", "product-api.dll"]
