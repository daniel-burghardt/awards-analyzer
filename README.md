# awards-analyzer
###### by Daniel Burghardt

## Running the app
### Using Docker
#### To run the container:
`docker build -t movie-analyzer .`
`docker run -dp 3000:3000 movie-analyzer`

Access it at: http://localhost:3000/swagger/index.html

#### To run the unit tests:
`docker build --target tests -t tests:latest .`

### Using Visual Studio
#### 1. Run the MovieAnalyzer project
Access it at: https://localhost:7019/swagger/index.html