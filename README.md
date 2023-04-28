# awards-analyzer
###### by Daniel Burghardt

## Running the app
### Using Docker
#### 1. Build the image:
`docker build -t movie-analyzer .`
#### 2. Run the container:
`docker run --name MovieAnalyzer -dp 3001:80 -e "ASPNETCORE_ENVIRONMENT=Development" movie-analyzer`

Access it at: http://localhost:3001/swagger
#### 3. Stop and start the container again using:
`docker stop MovieAnalyzer`

`docker start MovieAnalyzer`

#### To run the tests:
`docker build --target tests -t tests:latest .`

### Using Visual Studio
#### 1. Run the MovieAnalyzer project
Access it at: https://localhost:7019/swagger

#### To run the tests:
Right click on the `MovieAnalyzer.Tests` project and then "Run Tests".