# Base URLs:
**Nearby Places** https://maps.googleapis.com/maps/api/place/nearbysearch/json?key=AIzaSyDMMpSpBLjVMenIJaCP8zJcuZis08IHSYY
**Lat/Lng From Address** https://maps.googleapis.com/maps/api/geocode/json?key=AIzaSyDMMpSpBLjVMenIJaCP8zJcuZis08IHSYY

# profile info:
## Credentials:
https://console.cloud.google.com/apis/credentials?folder=&organizationId=&project=middlemeet-283002&supportedpurview=project

# API Docs
## Places API
https://developers.google.com/places/web-service/search

> **NOTE** Responses are paginated. See the **next_page_token** field in the response (root level field outside of results).


# Samples:

**Fetch Lat/Lng for our Apartment:**
https://maps.googleapis.com/maps/api/geocode/json?key=AIzaSyDMMpSpBLjVMenIJaCP8zJcuZis08IHSYY&address=2773%20woodlake%20rd%20sw%20apt%206%20wyoming%20mi%2049519

**Fetch nearby places around our Apartments Lat/Lng within 100 meters**
https://maps.googleapis.com/maps/api/place/nearbysearch/json?key=AIzaSyDMMpSpBLjVMenIJaCP8zJcuZis08IHSYY&location=42.876283,-85.733132&radius=100

**API Key**
AIzaSyDMMpSpBLjVMenIJaCP8zJcuZis08IHSYY

# Plan
First, get the latitude and longitude coordinates for all locations in question.

To get the coordinate between those places, use the formula in this stack exchange answer (use lng/lat as x/y respectively):
https://math.stackexchange.com/a/1599274/721543
This will give you back a new lng/lat coordinate. That is the mid point between all locations in question.

Then, do a search for places around that new mid point within a reasonable distance.

# Errors:

If you ever get an error that looks like this:

```
Unhandled exception. System.AggregateException: One or more errors occurred. (Sequence contains no elements)
 ---> System.InvalidOperationException: Sequence contains no elements
   at System.Linq.ThrowHelper.ThrowNoElementsException()
   at System.Linq.Enumerable.Single[TSource](IEnumerable`1 source)
   at MiddleMeet.Domain.GetPlacesAroundMidPointOfLocationsQuery.Run() in /home/ikirkpat/Personal/MiddleMeet/MiddleMeet.Domain/GetPlacesAroundMidPointOfLocationsQuery.cs:line 50
   --- End of inner exception stack trace ---
   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)
   at System.Threading.Tasks.Task`1.get_Result()
   at MiddleMeet.DevConsole.Program.Main(String[] args) in /home/ikirkpat/Personal/MiddleMeet/MiddleMeet.DevConsole/Program.cs:line 42
[1]    27704 abort (core dumped)  dotnet output/MiddleMeet.DevConsole.dll < MiddleMeet.DevConsole/testrun.txt
```

You are trying to make a request to the Google API from a computer that is not authorized to use my Google API Key. If you want to use it, ask Ian to allow your computers IP address to use the API Key.