# awards-analyzer
###### by Daniel Burghardt

## Running the app
### Using Docker
#### 1. Make sure you are at the root folder: `/awards-analyzer`
#### 2. Build the image:
`docker build -t movie-analyzer .`
#### 3. Run the container:
`docker run --name MovieAnalyzer -dp 3001:80 -e "ASPNETCORE_ENVIRONMENT=Development" movie-analyzer`

Access it at: http://localhost:3001/swagger
#### 4. Stop and start the container again using:
`docker stop MovieAnalyzer`

`docker start MovieAnalyzer`

#### To run the tests:
`docker build --target tests -t tests:latest .`

### Using Visual Studio
#### 1. Run the MovieAnalyzer project
Access it at: https://localhost:7019/swagger

#### To run the tests:
Right click on the `MovieAnalyzer.Tests` project and then "Run Tests".


## Modifying the dataset
If you wish to modify the dataset that is read on application startup, just replace the `MovieAnalyzer/movielist.csv` file or modify its contents.
