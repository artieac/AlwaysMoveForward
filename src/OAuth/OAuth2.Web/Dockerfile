
FROM microsoft/aspnetcore-build:2.0.0 as build

COPY . /src
WORKDIR /src

RUN dotnet restore ../../../AlwaysMoveForward.OAuth2.sln && \
    dotnet publish ../../../AlwaysMoveForward.OAuth2.sln -c Release -o ./obj/Docker/publish


FROM microsoft/aspnetcore:2.0.0

# Install wget
#RUN \
#  apt-get update -qq && \
#  apt-get install -qq wget software-properties-common openssl && \
#  wget https://secure.globalsign.com/cacert/gsorganizationvalsha2g2r1.crt && \

WORKDIR /app
EXPOSE 80

COPY --from=build /src/AlwaysMoveForward.OAuths/obj/Docker/publish .

ENTRYPOINT ["dotnet", "AlwaysMoveForward.OAuth2.Web.dll"]
