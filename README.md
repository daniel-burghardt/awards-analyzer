# awards-analyzer
###### by Daniel Burghardt

The **awards-analyzer** will read in a `.csv` file upon application startup and save it. This file will contain data about nominations for a movies award, including which were the winners for each year.

The endpoint `GET /award-nominees/intervals` provides statistics about the producers who took the longest and shortest times to win two awards.

An alternative version of this endpoint is in branch [`alternative-solution`](https://github.com/daniel-burghardt/awards-analyzer/tree/alternative-solution). There, only **consecutive** wins are taken into account - meaning a producer who was nominated in the years `2001, 2002, 2003` and won `2001` and `2003` would not be taken into account in the statistics, since he had a nomination in `2002` (in between the two wins) in which he lost. However, had him _**not**_ been nominated in `2002`, so participating only in the years `2001, 2003` and won both, then he would have been taken in consideration - his wins were consecutive.


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
