FROM  mcr.microsoft.com/windows/servercore:ltsc2019
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]
WORKDIR /app
COPY . ./
RUN iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1')); \
	choco install dotnetcore-runtime --version 5 -y; \        
	choco install aspnetcore-runtimepackagestore --version 5 -y;
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80/tcp
ENTRYPOINT [ "dotnet", "Dotnet.Url.Jumper.UI.dll" ]