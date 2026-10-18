# 1. Sử dụng .NET SDK 8.0 để build code
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copy toàn bộ code trong thư mục hiện tại vào container
COPY . .

# 3. Phục hồi thư viện và Build dự án
RUN dotnet restore "KyNiem6Nam.csproj"
RUN dotnet publish "KyNiem6Nam.csproj" -c Release -o /app/publish

# 4. Sử dụng ASP.NET Runtime siêu nhẹ để chạy web (tiết kiệm RAM)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# 5. Mở cổng 8080 và chạy file DLL
EXPOSE 8080
ENTRYPOINT ["dotnet", "KyNiem6Nam.dll"]