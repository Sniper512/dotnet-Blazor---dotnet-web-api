# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything to the container
COPY . . 

# Restore dependencies and build the project
RUN dotnet restore
RUN dotnet publish -c Release -o /app/output

# Stage 2: Serve with NGINX
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Copy published output to NGINX root
COPY --from=build-env /app/output/wwwroot/ .

#docker image ls
#REPOSITORY   TAG       IMAGE ID       CREATED          SIZE
#wasm-image   latest    adca54e131eb   57 seconds ago   133MB
#PS E:\BlazorWasm.FoodDelivery-master\BlazorWasm.FoodDelivery> docker run -p 8080:80 --name wasm-container wasm-image
