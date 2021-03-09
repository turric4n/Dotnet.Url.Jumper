FROM mcr.microsoft.com/dotnet/aspnet:5.0
EXPOSE 80/tcp
WORKDIR /app
COPY . ./
# install libdl for QuickLogger
RUN apt-get update && \
    apt-get install -y --allow-unauthenticated libc6-dev libcurl3-gnutls
ENTRYPOINT [ "./Dotnet.Url.Jumper.UI" ]