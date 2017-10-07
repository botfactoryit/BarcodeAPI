# Use SDK image to build app
FROM microsoft/dotnet:1.1-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./BarcodeAPI/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./BarcodeAPI/* ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:1.1-runtime
WORKDIR /app

# (that's new) copy output from previous image
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BarcodeAPI.dll"]