# SearchApp

This Web API project is created with .Net 6 LTM in VisualStudio 2022.

## Debugging the application

Load the solution in VisualStudio and debug (F5) or start without debugging (ctrl + F5) with debug profile named "SearchApp" which will launch the application using the Kestrel web server built in. The SSL port configured is 7206. The same port is linked on the SearchAppClient to communicate with the WebApi.

## Tests

There is a separate project called SearchAppTests which uses NUnit testing framework to unit test the functions in SearchService, PrepareDataSourceService and the SearchController.


