# Start application:

- Go to source folder (`cd .\src\`)
- Navigate to Web API (` cd .\Demo.ApplicationProcess.Api\`)
- Install NuGet packages (`dotnet restore`)
- Build source (`dotnet build`)
- Run source (`dotnet run`)
- Open this `http://localhost:5000` link

# Build application:

- Go to source folder (`cd .\src\`)
- Navigate to Web API (` cd .\Demo.ApplicationProcess.Api\`)
- Publish API source (`dotnet publish`)
- Run application (`dotnet Demo.ApplicationProcess.Api.dll --urls "http://localhost:5000"`)
- Open in this portal `http://localhost:5000`

# Test application:

- Go to source folder (`cd .\src\`)
- Navigate to Integration Test project (` cd .\Demo.ApplicationProcess.IntegrationTest\`)
- run Test API layer (`dotnet test`)
- Navigate to Unit Test projet (` cd .\Demo.ApplicationProcess.UnitTest\`)
- run Test Data acess layer (`dotnet test`)

# Containerize application:

- Build image (`docker-compose build` or `docker -t demo-api build .`)
- Run image (`docker-compose up` or `docker run demo-api`)
- Open in this portal `http://localhost:5000`
