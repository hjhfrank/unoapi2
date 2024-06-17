# unoapi2
sample網址
https://learn.microsoft.com/zh-tw/aspnet/core/tutorials/min-web-api?view=aspnetcore-8.0&tabs=visual-studio-code
 vs code 
dotnet new web -o TodoApi
cd TodoApi
code -r ../TodoApi

dotnet dev-certs https --trust

dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore

額外安裝
dotnet add package Newtonsoft.Json
