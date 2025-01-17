# Cria um ambiente com a imagem abaixo de SDK dotnet para conseguir publicar a api
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
# Se posiciona dentro da pasta /app no meu container
WORKDIR /app

# Vai copiar o que está dentro da mesma pasta do dockerfile e jogar para a pasta /app do container
COPY . ./

# Roda um comando dotnet usado para recuperar os pacotes usados pela api
RUN dotnet restore
RUN dotnet publish -c Release -o out

# A parte a cima serve para configurar o ambiente para que seja possivel publicar a api na pasta /out do container por isso é existe o from nos comandos para ter o sdk de desenvolvimento e conseguir rodar os 2 comandos dotnet, já a parte abaixo será as configurações para rodar a api no container, onde é usado os runtimes do dotnet

# Cria um ambiente final com a imagem abaixo de runtime para que seja possivel rodar a api no container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# Cria e se localiza dentro da pasta /runtime-app
WORKDIR /runtime-app
# Pega as dlls publicadas por o ambiente de build-env para rodar a api
COPY --from=build-env /app/out .

#
EXPOSE 8080
ENTRYPOINT ["dotnet", "RinhaDeBackEnd2023.dll"]